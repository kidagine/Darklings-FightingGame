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
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private EventSystem _eventSystem = default;
    [SerializeField] private TextMeshProUGUI _deviceText = default;
    [SerializeField] private PlayerInput _playerInput = default;
    [SerializeField] private Button _firstCharacterButton = default;
    [SerializeField] private CharacterAssistSelector _characterAssistSelector = default;
    [SerializeField] private CharacterColorSelector _characterColorSelector = default;
    [SerializeField] private GameObject _assignButtonImage = default;
    [SerializeField] private GameObject _backgroundDarken = default;
    [SerializeField] private RectTransform _scrollView = default;
    [SerializeField] private Transform _rebindContainer = default;
    [SerializeField] private bool _secondPlayer = default;
    private readonly List<RebindButton> _rebindButtons = new();
    private readonly string _controlRebindKey = "rebinds";
    private InputAction _inputAction;
    private InputDevice _inputDevice;
    private RebindingOperation _rebindingOperation;
    private string _controlMatch;
    private string _controlCancel;

    public PromptsInput PreviousPromptsInput { get; set; }
    //Load the bindings
    void Awake()
    {
        string rebinds = DemonicsSaver.Load(_controlRebindKey);
        _playerInput.actions.LoadBindingOverridesFromJson(rebinds);
    }

    //Update the rebind visual images based on the bindings
    void Start()
    {
        for (int i = 0; i < _rebindContainer.childCount; i++)
        {
            if (_rebindContainer.GetChild(i).TryGetComponent(out RebindButton rebindButton))
            {
                _rebindButtons.Add(rebindButton);
                rebindButton.UpdatePromptImage();
            }
        }
    }

    void OnDisable()
    {
        MouseSetup.Instance.SetCursor(true);
    }

    //Set the binding rules based on the input device
    void OnEnable()
    {
        MouseSetup.Instance.SetCursor(false);
        _backgroundDarken.SetActive(true);
        if (!_secondPlayer)
        {
            _inputDevice = SceneSettings.ControllerOne;
        }
        else
        {
            _inputDevice = SceneSettings.ControllerTwo;
        }
        if (_inputDevice.displayName.Contains("Keyboard"))
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

    //Called from UI, hide the menu and if none of the selectors are active then re-select the first character
    public void HideRebind()
    {
        Hide();
        _backgroundDarken.SetActive(false);
        if (!_characterAssistSelector.gameObject.activeSelf && !_characterColorSelector.gameObject.activeSelf)
            _firstCharacterButton.Select();
        _inputManager.SetPrompts(PreviousPromptsInput);
    }

    //Called from UI, rebind the given button
    public void AssignButton(RebindButton rebindButton)
    {
        _eventSystem.sendNavigationEvents = false;
        _eventSystem.SetSelectedGameObject(null);
        _assignButtonImage.SetActive(true);
        _inputAction = rebindButton.ActionReference.action;
        int index = rebindButton.CompositeIndex;
        _rebindingOperation = _inputAction.PerformInteractiveRebinding(rebindButton.ControlBindingIndex)
            .WithControlsHavingToMatchPath(_controlMatch)
            .WithCancelingThrough(_controlCancel)
            .OnMatchWaitForAnother(0.1f)
            .OnCancel(operation => RebindCancelled(rebindButton))
            .OnComplete(operation => RebindComplete(rebindButton));
        _rebindingOperation.Start();
    }

    //Cancel rebind operation
    private void RebindCancelled(RebindButton rebindButton)
    {
        _rebindingOperation.Dispose();
        _assignButtonImage.SetActive(false);
        StartCoroutine(RebindCompleteCoroutine(rebindButton));
    }

    //Complete rebind operation
    private void RebindComplete(RebindButton rebindButton)
    {
        _rebindingOperation.Dispose();
        _assignButtonImage.SetActive(false);
        string rebinds = _playerInput.actions.SaveBindingOverridesAsJson();
        DemonicsSaver.Save(_controlRebindKey, rebinds);
        StartCoroutine(RebindCompleteCoroutine(rebindButton));
    }

    //Wait a small amount before updating the visuals and reselecting the button for better game-feel
    IEnumerator RebindCompleteCoroutine(RebindButton rebindButton)
    {
        yield return new WaitForSeconds(0.1f);
        _eventSystem.sendNavigationEvents = true;
        rebindButton.UpdatePromptImage();
        rebindButton.GetComponent<Button>().Select();
    }

    //Called from UI, reset all bindings to default
    public void ResetRebindToDefault()
    {
        if (_playerInput.devices[0].displayName.Contains(_deviceText.text))
        {
            for (int i = 0; i < _rebindButtons.Count; i++)
            {
                InputAction inputAction = _rebindButtons[i].ActionReference.action;
                InputActionRebindingExtensions.RemoveAllBindingOverrides(inputAction);
                _rebindButtons[i].UpdatePromptImage();
            }
            string rebinds = _playerInput.actions.SaveBindingOverridesAsJson();
            DemonicsSaver.Save(_controlRebindKey, rebinds);
        }
    }
}
