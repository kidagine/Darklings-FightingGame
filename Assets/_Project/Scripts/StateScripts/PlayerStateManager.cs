using Demonics.Sounds;
using UnityEngine;

public class PlayerStateManager : StateMachine
{
    [SerializeField] private State _initialState = default;
    [Header("Components")]
    [SerializeField] private Player _player = default;
    [SerializeField] private PlayerMovement _playerMovement = default;
    [SerializeField] private PlayerAnimator _playerAnimator = default;
    [SerializeField] private PlayerStats _playerStats = default;
    [SerializeField] private PlayerController _playerController = default;
    [SerializeField] private BrainController _brainController = default;
    [SerializeField] private PlayerComboSystem _playerComboSystem = default;
    [SerializeField] private Audio _audio = default;
    [SerializeField] private Rigidbody2D _rigidbody = default;
    private TrainingMenu _trainingMenu;
    private PlayerUI _playerUI;

    public AirborneHurtState AirborneHurtState { get; private set; }

    public void Initialize(PlayerUI playerUI, TrainingMenu trainingMenu)
    {
        _playerUI = playerUI;
        _trainingMenu = trainingMenu;
        foreach (State state in GetComponents<State>())
        {
            state.Initialize(
            this, _rigidbody, _playerAnimator, _player, _playerMovement, _playerUI, _playerStats, _playerComboSystem, _audio
            );
            state.SetController(_brainController.ActiveController);
        }
        AirborneHurtState = GetComponent<AirborneHurtState>();
    }

    public void ResetToInitialState()
    {
        ChangeState(_initialState);
    }

    public bool TryToAttackState(InputEnum inputEnum)
    {
        return CurrentState.ToAttackState(inputEnum);
    }

    public bool TryToArcanaState()
    {
        return CurrentState.ToArcanaState();
    }

    public bool TryToGrabState()
    {
        return CurrentState.ToGrabState();
    }

    public bool TryToThrowState()
    {
        return CurrentState.ToThrowState();
    }

    public bool TryToKnockdownState()
    {
        return CurrentState.ToKnockdownState();
    }

    public bool TryToKnockbackState()
    {
        return CurrentState.ToKnockbackState();
    }

    public bool TryToAssistCall()
    {
        return CurrentState.AssistCall();
    }

    public bool TryToHurtState(AttackSO attack)
    {
        if (attack.causesKnockdown)
        {
            AirborneHurtState.Initialize(attack);
            ChangeState(AirborneHurtState);
        }
        else
        {
            CurrentState.ToHurtState(attack);
        }
        return true;
    }

    public bool TryToGrabbedState()
    {
        return CurrentState.ToGrabbedState();
    }

    public bool TryToBlockState(AttackSO attack)
    {
        return CurrentState.ToBlockState(attack);
    }

    protected override State GetInitialState()
    {
        return _initialState;
    }

    protected override void OnStateChange()
    {
        _trainingMenu.SetState(_player.IsPlayerOne, CurrentState.stateName);
    }
}
