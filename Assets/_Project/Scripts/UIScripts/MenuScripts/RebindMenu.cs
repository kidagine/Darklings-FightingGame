using Demonics.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

public class RebindMenu : BaseMenu
{
	[SerializeField] private EventSystem _eventSystem = default;
	[SerializeField] private PlayerInput _playerInput = default;
	[SerializeField] private Button _firstCharacterButton = default;
	[SerializeField] private CharacterAssistSelector _characterAssistSelector = default;
	[SerializeField] private CharacterColorSelector _characterColorSelector = default;
	[SerializeField] private GameObject _assignButtonImage = default;
	[SerializeField] private RectTransform _scrollView = default;
	[SerializeField] private Transform _rebindContainer = default;
	private readonly List<RebindButton> _rebindButtons = new();
	private InputAction _inputAction;
	private RebindingOperation _rebindingOperation;


	void Awake()
	{
		if (File.Exists(Application.persistentDataPath + "/controlsOverrides.dat"))
		{
			LoadControlOverrides();
		}
	}

	void Start()
	{
		for (int i = 0; i < _rebindContainer.childCount; i++)
		{
			if (_rebindContainer.GetChild(i).TryGetComponent(out RebindButton rebindButton))
			{
				_rebindButtons.Add(rebindButton);
			}
		}
	}

	void OnEnable()
	{
		_scrollView.anchoredPosition = Vector2.zero;
	}

	public void HideRebind()
	{
		Hide();
		if (!_characterAssistSelector.gameObject.activeSelf && !_characterColorSelector.gameObject.activeSelf)
		{
			_firstCharacterButton.Select();
		}
	}

	public void AssignButton(RebindButton rebindButton)
	{
		_inputAction = _playerInput.actions.FindAction(rebindButton.ActionReference.action.id);
		_eventSystem.sendNavigationEvents = false;
		_eventSystem.SetSelectedGameObject(null);
		_assignButtonImage.SetActive(true);
		_rebindingOperation = _inputAction.PerformInteractiveRebinding()
			.WithControlsExcluding("<Gamepad>/leftStick")
			.WithControlsExcluding("<Gamepad>/rightStick")
			.WithCancelingThrough("<Keyboard>/tab")
			.WithCancelingThrough("<Gamepad>/start")
			.OnCancel(operation => RebindComplete(rebindButton))
			.OnComplete(operation => RebindComplete(rebindButton));
		_rebindingOperation.Start();
	}

	private void RebindComplete(RebindButton rebindButton)
	{
		_rebindingOperation.Dispose();
		_assignButtonImage.SetActive(false);
		AddOverrideToDictionary(_inputAction.id, _inputAction.bindings[rebindButton.ControlBindingIndex].effectivePath, rebindButton.ControlBindingIndex);
		StartCoroutine(RebindCompleteCoroutine(rebindButton));
		SaveControlOverrides();
	}

	IEnumerator RebindCompleteCoroutine(RebindButton rebindButton)
	{
		yield return null;
		_eventSystem.sendNavigationEvents = true;
		rebindButton.UpdatePromptImage();
		rebindButton.GetComponent<Button>().Select();
	}

	public void ResetRebindToDefault()
	{
		for (int i = 0; i < _rebindButtons.Count; i++)
		{
			InputAction inputAction = _rebindButtons[i].ActionReference.action;
			InputActionRebindingExtensions.RemoveAllBindingOverrides(inputAction);
			_rebindButtons[i].UpdatePromptImage();
			Debug.Log(_rebindButtons[i].ControlBindingIndex);
			AddOverrideToDictionary(_inputAction.id, _inputAction.bindings[_rebindButtons[i].ControlBindingIndex].effectivePath, _rebindButtons[i].ControlBindingIndex);
		}
		SaveControlOverrides();
	}

	public Dictionary<string, string> OverridesDictionary = new();

	private void AddOverrideToDictionary(Guid actionId, string path, int bindingIndex)
	{
		string key = string.Format("{0} : {1}", actionId.ToString(), bindingIndex);

		if (OverridesDictionary.ContainsKey(key))
		{
			OverridesDictionary[key] = path;
		}
		else
		{
			OverridesDictionary.Add(key, path);
		}
	}

	private void SaveControlOverrides()
	{
		FileStream file = new(Application.persistentDataPath + "/controlsOverrides.dat", FileMode.OpenOrCreate);
		BinaryFormatter bf = new();
		bf.Serialize(file, OverridesDictionary);
		file.Close();
	}

	private void LoadControlOverrides()
	{
		if (!File.Exists(Application.persistentDataPath + "/controlsOverrides.dat"))
		{
			return;
		}

		FileStream file = new(Application.persistentDataPath + "/controlsOverrides.dat", FileMode.OpenOrCreate);
		BinaryFormatter bf = new();
		OverridesDictionary = bf.Deserialize(file) as Dictionary<string, string>;
		file.Close();

		foreach (var item in OverridesDictionary)
		{
			string[] split = item.Key.Split(new string[] { " : " }, StringSplitOptions.None);
			Guid id = Guid.Parse(split[0]);
			int index = int.Parse(split[1]);
			_playerInput.actions.FindAction(id).ApplyBindingOverride(index, item.Value);
		}
	}
}
