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
        _audio.Sound("Landed").Play();
        _playerAnimator.WallSplat();
        _player.SetHurtbox(false);
        _playerMovement.ZeroGravity();
        _playerMovement.StopAllCoroutines();
        _player.transform.position = _playerMovement.OnWall();
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
        _rigidbody.velocity = Vector2.zero;
    }

    public override bool ToKnockdownState()
    {
        return false;
    }

    public override void Exit()
    {
        base.Exit();
        _playerAnimator.ResetPosition();
    }
}