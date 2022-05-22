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
    [SerializeField] private PlayerComboSystem _playerComboSystem = default;
    [SerializeField] private Audio _audio = default;
    [SerializeField] private Rigidbody2D _rigidbody = default;
    private TrainingMenu _trainingMenu;
    private PlayerUI _playerUI;
    public AttackState AttackState { get; private set; }
    public IdleState IdleState { get; private set; }
    public HurtState HurtState { get; private set; }
    public ArcanaState ArcanaState { get; private set; }
    public FallState FallState { get; private set; }

    public void Initialize(PlayerUI playerUI, TrainingMenu trainingMenu)
    {
        _playerUI = playerUI;
        _trainingMenu = trainingMenu;
        foreach (State state in GetComponents<State>())
        {
            state.Initialize(
            this, _rigidbody, _playerAnimator, _player, _playerMovement, _playerUI, _playerStats, _playerController, _playerComboSystem, _audio
            );
        }
        AttackState = GetComponent<AttackState>();
        IdleState = GetComponent<IdleState>();
        HurtState = GetComponent<HurtState>();
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

    protected override State GetInitialState()
    {
        return _initialState;
    }

    protected override void OnStateChange()
    {
        _trainingMenu.SetState(_player.IsPlayerOne, CurrentState.stateName);
    }
}
