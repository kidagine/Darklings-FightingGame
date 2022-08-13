using Demonics.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

public class RebindMenu : BaseMenu
{
	[SerializeField] private PlayerInput _playerInput = default;
	[SerializeField] private Button _firstCharacterButton = default;
	[SerializeField] private CharacterAssistSelector _characterAssistSelector = default;
	[SerializeField] private CharacterColorSelector _characterColorSelector = default;
	[SerializeField] private GameObject _assignButtonImage = default;
	[SerializeField] private Transform _rebindContainer = default;
	private List<RebindButton> _rebindButtons = new();
	private InputAction _inputAction;
	private RebindingOperation _rebindingOperation;


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
		EventSystem.current.sendNavigationEvents = false;
		EventSystem.current.SetSelectedGameObject(null);
		Debug.Log(_inputAction.GetBindingDisplayString(1, InputBinding.DisplayStringOptions.DontUseShortDisplayNames));
		_assignButtonImage.SetActive(true);
		_rebindingOperation = _inputAction.PerformInteractiveRebinding().OnComplete(operation => RebindComplete(rebindButton));
		_rebindingOperation.Start();
	}

	private void RebindComplete(RebindButton rebindButton)
	{
		EventSystem.current.sendNavigationEvents = true;
		_assignButtonImage.SetActive(false);
		rebindButton.UpdatePromptImage();
		rebindButton.GetComponent<Button>().Select();
		_rebindingOperation.Dispose();
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
}
