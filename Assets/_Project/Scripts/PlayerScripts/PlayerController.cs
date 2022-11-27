using UnityEngine;
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
    Vector2Int _previousInput;

    void Start()
    {
        string rebinds = PlayerPrefs.GetString(_controlRebindKey);
        _playerInput.actions.LoadBindingOverridesFromJson(rebinds);
        _playerInput.actions.actionMaps[(int)ActionSchemeTypes.Training].Enable();
    }

    //GAMEPLAY
    public void Movement(CallbackContext callbackContext)
    {
        Vector2Int input = new Vector2Int(Mathf.RoundToInt(callbackContext.ReadValue<Vector2>().x), Mathf.RoundToInt(callbackContext.ReadValue<Vector2>().y));
        if (callbackContext.performed && IsControllerEnabled && _previousInput != input)
        {
            if (input.x == 1)
            {
                _previousInput = input;
                _inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Right);
            }
            if (input.x == -1)
            {
                _previousInput = input;
                _inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Left);
            }
            if (input.y == 1)
            {
                _previousInput = input;
                _inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Up);
            }
            if (input.y == -1)
            {
                _previousInput = input;
                _inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Down);
            }
        }
        if (input == Vector2Int.zero)
        {
            _previousInput = Vector2Int.zero;
        }

        if (input.x == 0)
        {
            _inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.NoneHorizontal);
        }
        if (input.y == 0)
        {
            _inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.NoneVertical);
        }
    }

    public override bool Jump()
    {
        if (InputDirection.y > 0)
        {
            return true;
        }

        return false;
    }

    public override bool Crouch()
    {
        if (InputDirection.y < 0)
        {
            return true;
        }
        return false;
    }

    public override bool StandUp()
    {
        if (InputDirection.y == 0)
        {
            _inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.NoneVertical);
            return true;
        }
        return false;
    }

    public void Jump(CallbackContext callbackContext)
    {
        //_inputBuffer.AddInputBufferItem(InputEnum.Light);
    }

    public void Crouch(CallbackContext callbackContext)
    {
        //_inputBuffer.AddInputBufferItem(InputEnum.Light);
    }

    public void StandUp(CallbackContext callbackContext)
    {
        //_inputBuffer.AddInputBufferItem(InputEnum.Light);
    }

    public void Light(CallbackContext callbackContext)
    {
        if (callbackContext.performed && IsControllerEnabled)
        {
            _inputBuffer.AddInputBufferItem(InputEnum.Light);
        }
    }

    public void Medium(CallbackContext callbackContext)
    {
        if (callbackContext.performed && IsControllerEnabled)
        {
            _inputBuffer.AddInputBufferItem(InputEnum.Medium);
        }
    }

    public void Heavy(CallbackContext callbackContext)
    {
        if (callbackContext.performed && IsControllerEnabled)
        {
            _inputBuffer.AddInputBufferItem(InputEnum.Heavy);
        }
    }

    public void Arcane(CallbackContext callbackContext)
    {
        if (callbackContext.performed && IsControllerEnabled)
        {
            _inputBuffer.AddInputBufferItem(InputEnum.Special);
        }
    }
    public void Assist(CallbackContext callbackContext)
    {
        if (callbackContext.performed && IsControllerEnabled)
        {
            _inputBuffer.AddInputBufferItem(InputEnum.Assist);
        }
    }

    public void Throw(CallbackContext callbackContext)
    {
        if (callbackContext.performed && IsControllerEnabled)
        {
            _inputBuffer.AddInputBufferItem(InputEnum.Throw);
        }
    }

    public void Parry(CallbackContext callbackContext)
    {
        if (callbackContext.performed && IsControllerEnabled)
        {
            _inputBuffer.AddInputBufferItem(InputEnum.Parry);
        }
    }

    public void RedFrenzy(CallbackContext callbackContext)
    {
        if (callbackContext.performed && IsControllerEnabled)
        {
            _inputBuffer.AddInputBufferItem(InputEnum.RedFrenzy);
        }
    }

    public void DashForward(CallbackContext callbackContext)
    {
        if (callbackContext.performed && IsControllerEnabled)
        {
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
                    _inputBuffer.AddInputBufferItem(InputEnum.ForwardDash);
                    _dashForwardPressed = false;
                }
                _dashForwardLastInputTime = DemonicsWorld.Frame;
            }
        }
    }

    public void DashBack(CallbackContext callbackContext)
    {
        if (callbackContext.performed && IsControllerEnabled)
        {
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
                    _inputBuffer.AddInputBufferItem(InputEnum.BackDash);
                    _dashBackPressed = false;
                }
                _dashBackLastInputTime = DemonicsWorld.Frame;
            }
        }
    }

    public void Pause(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            _player.Pause(_brainController.IsPlayerOne);
        }
        if (callbackContext.canceled)
        {
            _player.UnPause();
        }
    }
    //TRAINING
    public void Reset(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            GameManager.Instance.ResetRound(InputDirection);
        }
    }

    public void Switch(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            GameManager.Instance.SwitchCharacters();
        }
    }
    //UI
    public void Confirm(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            CurrentPrompts?.OnConfirm?.Invoke();
        }
    }

    public void Back(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            CurrentPrompts?.OnBack?.Invoke();
        }
    }

    public void Stage(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            CurrentPrompts?.OnStage?.Invoke();
        }
    }

    public void Coop(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            CurrentPrompts?.OnCoop?.Invoke();
        }
    }

    public void Controls(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            CurrentPrompts?.OnControls?.Invoke();
        }
    }

    public void ToggleFramedata(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            CurrentPrompts?.OnToggleFramedata?.Invoke();
        }
    }

    public void PageLeft(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            CurrentPrompts?.OnLeftPage?.Invoke();
        }
    }

    public void PageRight(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            CurrentPrompts?.OnRightPage?.Invoke();
        }
    }

    public void NavigationRight(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            CurrentPrompts?.OnRightNavigation?.Invoke();
        }
    }

    public void NavigationLeft(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            CurrentPrompts?.OnLeftNavigation?.Invoke();
        }
    }
}
