using System.Collections;
using UnityEngine;

public class WallSplatState : State
{
    [SerializeField] private GameObject _wallSplatPrefab = default;
    private AirborneHurtState _airborneHurtState;

    void Awake()
    {
        _airborneHurtState = GetComponent<AirborneHurtState>();
    }

    public override void Enter()
    {
        base.Enter();
        _audio.Sound("WallSplat").Play();
        _playerAnimator.WallSplat();
        _player.SetHurtbox(false);
        _playerMovement.StopAllCoroutines();
        _physics.SetFreeze(true);
        _playerUI.DisplayNotification(NotificationTypeEnum.WallSplat);
        GameObject effect = Instantiate(_wallSplatPrefab);
        SpriteRenderer effectSpriteRenderer = effect.GetComponent<SpriteRenderer>();
        effect.transform.localPosition = new Vector2(transform.position.x + (_player.transform.localScale.x * effectSpriteRenderer.sprite.bounds.size.x / 2), transform.position.y);
        effect.transform.localScale = new Vector2(_player.transform.localScale.x * -1, 1);
        _playerAnimator.OnCurrentAnimationFinished.AddListener(ToAirborneHurtState);
    }

    private void ToAirborneHurtState()
    {
        _airborneHurtState.WallSplat = true;
        _stateMachine.ChangeState(_airborneHurtState);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override bool ToKnockdownState()
    {
        return false;
    }

    public override void Exit()
    {
        base.Exit();
        _physics.SetFreeze(false);
        _playerAnimator.ResetPosition();
    }
}