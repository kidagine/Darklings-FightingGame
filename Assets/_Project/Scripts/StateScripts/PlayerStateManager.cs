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
    [SerializeField] private BaseController _baseController = default;
    [SerializeField] private PlayerComboSystem _playerComboSystem = default;
    [SerializeField] private Audio _audio = default;
    [SerializeField] private Rigidbody2D _rigidbody = default;
    private TrainingMenu _trainingMenu;
    private PlayerUI _playerUI;
    public AttackState AttackState { get; private set; }
    public ThrowState ThrowState { get; private set; }
    public IdleState IdleState { get; private set; }
    public WalkState WalkState { get; private set; }
    public HurtState HurtState { get; private set; }
    public AirborneHurtState AirborneHurtState { get; private set; }
    public KnockdownState KnockbackState { get; private set; }
    public ArcanaState ArcanaState { get; private set; }
    public FallState FallState { get; private set; }

    public void Initialize(PlayerUI playerUI, TrainingMenu trainingMenu)
    {
        _playerUI = playerUI;
        _trainingMenu = trainingMenu;
        foreach (State state in GetComponents<State>())
        {
            state.Initialize(
            this, _rigidbody, _playerAnimator, _player, _playerMovement, _playerUI, _playerStats, _baseController, _playerComboSystem, _audio
            );
        }
        AttackState = GetComponent<AttackState>();
        ThrowState = GetComponent<ThrowState>();
        IdleState = GetComponent<IdleState>();
        WalkState = GetComponent<WalkState>();
        HurtState = GetComponent<HurtState>();
        AirborneHurtState = GetComponent<AirborneHurtState>();
        KnockbackState = GetComponent<KnockdownState>();
        ArcanaState = GetComponent<ArcanaState>();
        FallState = GetComponent<FallState>();
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

    public bool TryToThrowState()
    {
        return CurrentState.ToThrowState();
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

    protected override State GetInitialState()
    {
        return _initialState;
    }

    protected override void OnStateChange()
    {
        _trainingMenu.SetState(_player.IsPlayerOne, CurrentState.stateName);
    }
}
