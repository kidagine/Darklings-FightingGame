using FixMath.NET;
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
        _player.DecreaseArcana((Fix64)1);
        _playerAnimator.OnCurrentAnimationFinished.AddListener(ToFallState);
        _playerAnimator.Shadowbreak();
        _audio.Sound("Shadowbreak").Play();
        CameraShake.Instance.Shake(_cameraShaker);
        Transform shadowbreak = Instantiate(_shadowbreakPrefab, _playerAnimator.transform).transform;
        shadowbreak.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
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

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void Exit()
    {
        base.Exit();
        _player.OtherPlayer.StopComboTimer();
        _playerAnimator.OnCurrentAnimationFinished.RemoveAllListeners();
    }
}