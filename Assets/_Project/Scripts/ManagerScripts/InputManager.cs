using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demonics
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput = default;
        private string _currentControlScheme;

        public event Action ControlsChanged;

        public static InputManager Instance { get; private set; }
        public ControlSchemeEnum ActiveControlScheme { get; private set; }


        void Awake()
        {
            CheckInstance();
        }

        void Start()
        {
            OnControlChanged();
        }

        private void CheckInstance()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void OnControlChanged()
        {
            if (_playerInput.currentControlScheme != _currentControlScheme)
            {
                _currentControlScheme = _playerInput.currentControlScheme;
                if (_currentControlScheme.Equals(ControlSchemeEnum.KeyboardMouse.ToString()))
                {
                    ActiveControlScheme = ControlSchemeEnum.KeyboardMouse;
                    ControlsChanged?.Invoke();
                }
                else if (_currentControlScheme.Equals(ControlSchemeEnum.Xbox.ToString()))
                {
                    ActiveControlScheme = ControlSchemeEnum.Xbox;
                    ControlsChanged?.Invoke();
                }
            }
        }

        public InputAction GetPlayerInputAction(string actionName)
        {
            return _playerInput.actions.FindAction(actionName);
        }

        public PlayerInput GetPlayerInput()
        {
            return _playerInput;
        }
    }
}