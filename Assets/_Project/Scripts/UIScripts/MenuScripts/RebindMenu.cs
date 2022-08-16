using Demonics.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

public class RebindMenu : BaseMenu
{
	[SerializeField] private EventSystem _eventSystem = default;
	[SerializeField] private TextMeshProUGUI _deviceText = default;
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
	private string _controlMatch;
	private string _controlCancel;


	void Awake()
	{
		if (File.Exists(Application.persistentDataPath + "/controlsOverrides.dat"))
		{
			//LoadControlOverrides();
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
		if (_playerInput.devices[0].displayName.Contains("Keyboard"))
		{
			_deviceText.text = "Keyboard";
			_controlMatch = "<Keyboard>";
			_controlCancel = "<Keyboard>/tab";
		}
		else
		{
			_deviceText.text = "Controller";
			_controlMatch = "<Gamepad>";
			_controlCancel = "<Gamepad>/start";
		}
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
		_eventSystem.sendNavigationEvents = false;
		_eventSystem.SetSelectedGameObject(null);
		_assignButtonImage.SetActive(true);
		_inputAction = rebindButton.ActionReference.action;

		if (rebindButton.CompositeIndex > 0)
		{
			_rebindingOperation = _inputAction.PerformInteractiveRebinding(rebindButton.CompositeIndex)
				.WithControlsHavingToMatchPath(_controlMatch)
				.WithCancelingThrough(_controlCancel)
				.OnMatchWaitForAnother(0.1f)
				.OnCancel(operation => RebindCancelled(rebindButton))
				.OnComplete(operation => RebindComplete(rebindButton));
		}
		else
		{
			_rebindingOperation = _inputAction.PerformInteractiveRebinding()
				.WithControlsHavingToMatchPath(_controlMatch)
				.WithCancelingThrough(_controlCancel)
				.OnMatchWaitForAnother(0.1f)
				.OnCancel(operation => RebindCancelled(rebindButton))
				.OnComplete(operation => RebindComplete(rebindButton));
		}
		_rebindingOperation.Start();
	}

	private void RebindCancelled(RebindButton rebindButton)
	{
		Debug.Log("cancel");
		_rebindingOperation.Dispose();
		_assignButtonImage.SetActive(false);
		StartCoroutine(RebindCompleteCoroutine(rebindButton));
	}

	private void RebindComplete(RebindButton rebindButton)
	{
		Debug.Log("complete");
		_rebindingOperation.Dispose();
		_assignButtonImage.SetActive(false);
		StartCoroutine(RebindCompleteCoroutine(rebindButton));
	}

	IEnumerator RebindCompleteCoroutine(RebindButton rebindButton)
	{
		yield return new WaitForSeconds(0.1f);
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
		}
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
