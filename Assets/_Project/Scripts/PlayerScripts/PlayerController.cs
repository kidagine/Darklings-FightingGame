using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(InputBuffer))]
public class PlayerController : BaseController
{
    public PromptsInput CurrentPrompts { get; set; }
    private readonly string _controlRebindKey = "rebinds";
    private bool _dashForwardPressed;
    private bool _dashBackPressed;
    private int _dashForwardLastInputTime;
    private int _dashBackLastInputTime;
    private readonly int _dashTime = 12;
    private Vector2Int _previousInput;
    public static UnityEvent<Vector2Int, bool> OnInputDirection = new UnityEvent<Vector2Int, bool>();

    void Start()
    {
        string rebinds = PlayerPrefs.GetString(_controlRebindKey);
        _playerInput.actions.LoadBindingOverridesFromJson(rebinds);
        _playerInput.actions.actionMaps[(int)ActionSchemeTypes.Training].Enable();
    }

    //GAMEPLAY
    //Sequences
    public void Movement(CallbackContext callbackContext)
    {
        Vector2Int input = new Vector2Int(Mathf.RoundToInt(callbackContext.ReadValue<Vector2>().x), Mathf.RoundToInt(callbackContext.ReadValue<Vector2>().y));
        OnInputDirection?.Invoke(input, _player.IsPlayerOne);
        if (callbackContext.performed && IsControllerEnabled && _previousInput != input)
        {
            if (input.y != 0 && input.x != 0)
            {
                if (input.y == 1 && input.x == 1)
                {
                    _previousInput = input;
                    if (_player.IsPlayerOne)
                        NetworkInput.ONE_UP_RIGHT_INPUT = true;
                    else
                        NetworkInput.TWO_UP_RIGHT_INPUT = true;
                }
                if (input.y == 1 && input.x == -1)
                {
                    _previousInput = input;
                    if (_player.IsPlayerOne)
                        NetworkInput.ONE_UP_LEFT_INPUT = true;
                    else
                        NetworkInput.TWO_UP_LEFT_INPUT = true;
                }
                if (input.y == -1 && input.x == 1)
                {
                    _previousInput = input;
                    if (_player.IsPlayerOne)
                        NetworkInput.ONE_DOWN_RIGHT_INPUT = true;
                    else
                        NetworkInput.TWO_DOWN_RIGHT_INPUT = true;
                }
                if (input.y == -1 && input.x == -1)
                {
                    _previousInput = input;
                    if (_player.IsPlayerOne)
                        NetworkInput.ONE_DOWN_LEFT_INPUT = true;
                    else
                        NetworkInput.TWO_DOWN_LEFT_INPUT = true;
                }
            }
            else
            {
                if (input.y == 1)
                {
                    _previousInput = input;
                    if (_player.IsPlayerOne)
                        NetworkInput.ONE_UP_INPUT = true;
                    else
                        NetworkInput.TWO_UP_INPUT = true;
                }
                if (input.y == -1)
                {
                    _previousInput = input;
                    if (_player.IsPlayerOne)
                        NetworkInput.ONE_DOWN_INPUT = true;
                    else
                        NetworkInput.TWO_DOWN_INPUT = true;
                }
                if (input.x == 1)
                {
                    _previousInput = input;
                    if (_player.IsPlayerOne)
                        NetworkInput.ONE_RIGHT_INPUT = true;
                    else
                        NetworkInput.TWO_RIGHT_INPUT = true;
                }
                if (input.x == -1)
                {
                    _previousInput = input;
                    if (_player.IsPlayerOne)
                        NetworkInput.ONE_LEFT_INPUT = true;
                    else
                        NetworkInput.TWO_LEFT_INPUT = true;
                }
            }
        }
        if (input == Vector2Int.zero)
            _previousInput = Vector2Int.zero;
        if (input.x == 0)
        {
            if (_player.IsPlayerOne)
            {
                NetworkInput.ONE_UP_RIGHT_INPUT = false;
                NetworkInput.ONE_UP_LEFT_INPUT = false;
                NetworkInput.ONE_DOWN_RIGHT_INPUT = false;
                NetworkInput.ONE_DOWN_LEFT_INPUT = false;
                NetworkInput.ONE_RIGHT_INPUT = false;
                NetworkInput.ONE_LEFT_INPUT = false;
            }
            else
            {
                NetworkInput.TWO_UP_RIGHT_INPUT = false;
                NetworkInput.TWO_UP_LEFT_INPUT = false;
                NetworkInput.TWO_DOWN_RIGHT_INPUT = false;
                NetworkInput.TWO_DOWN_LEFT_INPUT = false;
                NetworkInput.TWO_RIGHT_INPUT = false;
                NetworkInput.TWO_LEFT_INPUT = false;
            }

        }
        if (input.y == 0)
        {
            if (_player.IsPlayerOne)
            {
                NetworkInput.ONE_UP_RIGHT_INPUT = false;
                NetworkInput.ONE_UP_LEFT_INPUT = false;
                NetworkInput.ONE_DOWN_RIGHT_INPUT = false;
                NetworkInput.ONE_DOWN_LEFT_INPUT = false;
                NetworkInput.ONE_UP_INPUT = false;
                NetworkInput.ONE_DOWN_INPUT = false;
            }
            else
            {
                NetworkInput.TWO_UP_RIGHT_INPUT = false;
                NetworkInput.TWO_UP_LEFT_INPUT = false;
                NetworkInput.TWO_DOWN_RIGHT_INPUT = false;
                NetworkInput.TWO_DOWN_LEFT_INPUT = false;
                NetworkInput.TWO_UP_INPUT = false;
                NetworkInput.TWO_DOWN_INPUT = false;
            }
        }
        if (input == Vector2.zero)
        {
            if (_player.IsPlayerOne)
                NetworkInput.ONE_NEUTRAL_INPUT = true;
            else
                NetworkInput.TWO_NEUTRAL_INPUT = true;
        }
        else
        {
            if (_player.IsPlayerOne)
                NetworkInput.ONE_NEUTRAL_INPUT = false;
            else
                NetworkInput.TWO_NEUTRAL_INPUT = false;
        }
    }

    //Triggers
    public void Light(CallbackContext callbackContext) => SetInput(callbackContext, ref NetworkInput.ONE_LIGHT_INPUT, ref NetworkInput.TWO_LIGHT_INPUT);

    public void Medium(CallbackContext callbackContext) => SetInput(callbackContext, ref NetworkInput.ONE_MEDIUM_INPUT, ref NetworkInput.TWO_MEDIUM_INPUT);

    public void Heavy(CallbackContext callbackContext) => SetInput(callbackContext, ref NetworkInput.ONE_HEAVY_INPUT, ref NetworkInput.TWO_HEAVY_INPUT);

    public void Arcane(CallbackContext callbackContext) => SetInput(callbackContext, ref NetworkInput.ONE_ARCANA_INPUT, ref NetworkInput.TWO_ARCANA_INPUT);

    public void Assist(CallbackContext callbackContext) => SetInput(callbackContext, ref NetworkInput.ONE_SHADOW_INPUT, ref NetworkInput.TWO_SHADOW_INPUT);

    public void Throw(CallbackContext callbackContext) => SetInput(callbackContext, ref NetworkInput.ONE_GRAB_INPUT, ref NetworkInput.TWO_GRAB_INPUT);

    public void Parry(CallbackContext callbackContext) => SetInput(callbackContext, ref NetworkInput.ONE_BLUE_FRENZY_INPUT, ref NetworkInput.TWO_BLUE_FRENZY_INPUT);

    public void RedFrenzy(CallbackContext callbackContext) => SetInput(callbackContext, ref NetworkInput.ONE_RED_FRENZY_INPUT, ref NetworkInput.TWO_RED_FRENZY_INPUT);

    private void SetInput(CallbackContext callbackContext, ref bool inputOne, ref bool InputTwo)
    {
        if (callbackContext.performed && IsControllerEnabled)
            if (_player.IsPlayerOne)
                inputOne = true;
            else
                InputTwo = true;
        else
        {
            if (_player.IsPlayerOne)
                inputOne = false;
            else
                InputTwo = false;
        }
    }

    public void DashForward(CallbackContext callbackContext)
    {
        if (callbackContext.performed && IsControllerEnabled)
            if (!_dashForwardPressed)
            {
                _dashForwardPressed = true;
                _dashForwardLastInputTime = DemonicsWorld.Frame;
            }
            else
            {
                int timeSinceLastPress = DemonicsWorld.Frame - _dashForwardLastInputTime;
                if (timeSinceLastPress <= _dashTime)
                {
                    if (_player.IsPlayerOne)
                        NetworkInput.ONE_DASH_FORWARD_INPUT = true;
                    else
                        NetworkInput.TWO_DASH_FORWARD_INPUT = true;
                    _dashForwardPressed = false;
                }
                _dashForwardLastInputTime = DemonicsWorld.Frame;
            }
        else
        {
            if (_player.IsPlayerOne)
                NetworkInput.ONE_DASH_FORWARD_INPUT = false;
            else
                NetworkInput.TWO_DASH_FORWARD_INPUT = false;
        }
    }

    public void DashBack(CallbackContext callbackContext)
    {
        if (callbackContext.performed && IsControllerEnabled)
            if (!_dashBackPressed)
            {
                _dashBackPressed = true;
                _dashBackLastInputTime = DemonicsWorld.Frame;
            }
            else
            {
                int timeSinceLastPress = DemonicsWorld.Frame - _dashBackLastInputTime;
                if (timeSinceLastPress <= _dashTime)
                {
                    if (_player.IsPlayerOne)
                        NetworkInput.ONE_DASH_BACKWARD_INPUT = true;
                    else
                        NetworkInput.TWO_DASH_BACKWARD_INPUT = true;
                    _dashBackPressed = false;
                }
                _dashBackLastInputTime = DemonicsWorld.Frame;
            }
        else
        {
            if (_player.IsPlayerOne)
                NetworkInput.ONE_DASH_BACKWARD_INPUT = false;
            else
                NetworkInput.TWO_DASH_BACKWARD_INPUT = false;
        }
    }

    public void Pause(CallbackContext callbackContext)
    {
        if (NetworkInput.IS_LOCAL)
        {
            if (callbackContext.performed)
                _player.Pause(_brainController.IsPlayerOne);
            if (callbackContext.canceled)
                _player.UnPause();
        }
    }
    //TRAINING
    public void Reset(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (_player.IsPlayerOne)
                GameplayManager.Instance.ResetRound(GameSimulation._players[0].direction);
            else
                GameplayManager.Instance.ResetRound(GameSimulation._players[1].direction);
        }
    }

    public void Switch(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
            GameplayManager.Instance.SwitchCharacters();
    }
    //UI
    public void Confirm(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
            CurrentPrompts?.OnConfirm?.Invoke();
    }

    public void Back(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
            CurrentPrompts?.OnBack?.Invoke();
    }

    public void Stage(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
            CurrentPrompts?.OnStage?.Invoke();
    }

    public void Coop(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
            CurrentPrompts?.OnCoop?.Invoke();
    }

    public void Controls(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
            CurrentPrompts?.OnControls?.Invoke();
    }

    public void ToggleFramedata(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
            CurrentPrompts?.OnToggleFramedata?.Invoke();
    }

    public void PageLeft(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
            CurrentPrompts?.OnLeftPage?.Invoke();
    }

    public void PageRight(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
            CurrentPrompts?.OnRightPage?.Invoke();
    }

    public void NavigationRight(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
            CurrentPrompts?.OnRightNavigation?.Invoke();
    }

    public void NavigationLeft(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
            CurrentPrompts?.OnLeftNavigation?.Invoke();
    }
}
