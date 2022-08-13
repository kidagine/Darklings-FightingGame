using Demonics.UI;
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
	private InputAction _inputAction = default;
	private RebindingOperation _rebindingOperation;

	public void HideRebind()
	{
		Hide();
		if (!_characterAssistSelector.gameObject.activeSelf && !_characterColorSelector.gameObject.activeSelf)
		{
			_firstCharacterButton.Select();
		}
	}

	public void AssignButton(InputActionReference actionReference)
	{
		_inputAction = _playerInput.actions.FindAction(actionReference.action.id);
		EventSystem.current.sendNavigationEvents = false;
		Debug.Log(_inputAction.GetBindingDisplayString(0, InputBinding.DisplayStringOptions.DontUseShortDisplayNames));
		_assignButtonImage.SetActive(true);
		_rebindingOperation = _inputAction.PerformInteractiveRebinding().OnComplete(operation => RebindComplete());
		_rebindingOperation.Start();
	}

	private void RebindComplete()
	{
		EventSystem.current.sendNavigationEvents = true;
		_assignButtonImage.SetActive(false);
		_rebindingOperation.Dispose();
	}

	public void ResetRebindToDefault()
	{
		InputActionRebindingExtensions.RemoveAllBindingOverrides(_playerInput.currentActionMap);
	}
}
