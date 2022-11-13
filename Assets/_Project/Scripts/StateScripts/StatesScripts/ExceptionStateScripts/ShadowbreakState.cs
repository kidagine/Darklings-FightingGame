using UnityEngine;

public class ShadowbreakState : State
{
    [SerializeField] private GameObject _shadowbreakPrefab = default;
    [SerializeField] private CameraShakerSO _cameraShaker = default;
    private FallState _fallState;
    private HurtState _hurtState;
    private AirborneHurtState _airborneHurtState;

    private void Awake()
    {
        _fallState = GetComponent<FallState>();
        _hurtState = GetComponent<HurtState>();
        _airborneHurtState = GetComponent<AirborneHurtState>();
    }

    public override void Enter()
    {
        base.Enter();
        _player.DecreaseArcana((DemonicsFloat)1);
        _playerAnimator.OnCurrentAnimationFinished.AddListener(ToFallState);
        _playerAnimator.Shadowbreak();
        _audio.Sound("Shadowbreak").Play();
        CameraShake.Instance.Shake(_cameraShaker);
        Transform shadowbreak = Instantiate(_shadowbreakPrefab, _playerAnimator.transform).transform;
        shadowbreak.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
        _player.hitstopEvent.RemoveAllListeners();
        _playerMovement.ExitHitstop();
        _playerMovement.StopKnockback();
        _playerMovement.Physics.SetPositionWithRender(new DemonicsVector2(_playerMovement.Physics.Position.x, _playerMovement.Physics.Position.y + 1));
        _playerMovement.Physics.SetFreeze(true);
        _playerMovement.Physics.EnableGravity(false);
    }

    private void ToFallState()
    {
        _stateMachine.ChangeState(_fallState);
    }

    public override bool ToHurtState(AttackSO attack)
    {
        _hurtState.Initialize(attack);
        _stateMachine.ChangeState(_hurtState);
        return true;
    }

    public override bool ToAirborneHurtState(AttackSO attack)
    {
        _airborneHurtState.Initialize(attack);
        _stateMachine.ChangeState(_airborneHurtState);
        return true;
    }

    public override void Exit()
    {
        base.Exit();
        _playerMovement.Physics.Velocity = DemonicsVector2.Zero;
        _playerMovement.Physics.SetFreeze(false);
        _playerMovement.Physics.EnableGravity(true);
        _player.OtherPlayer.StopComboTimer();
        _playerAnimator.OnCurrentAnimationFinished.RemoveAllListeners();
    }
}