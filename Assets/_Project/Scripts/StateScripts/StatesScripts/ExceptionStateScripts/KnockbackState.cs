using System.Collections;
using UnityEngine;

public class KnockbackState : State
{
    [SerializeField] private GameObject _groundedPrefab = default;
    [SerializeField] private CameraShakerSO _cameraShaker = default;
    private KnockdownState _knockdownState;
    private Coroutine _canCheckGroundCoroutine;
    private bool _canCheckGround;
    private readonly float _knockbackDirectionY = 0.5f;
    private readonly int _knockbackDuration = 10;
    private readonly float _knockbackForce = 2.5f;

    protected void Awake()
    {
        _knockdownState = GetComponent<KnockdownState>();
    }

    public override void Enter()
    {
        _playerAnimator.HurtAir();
        base.Enter();
        _playerMovement.KnockbackNow(new Vector2(
            _player.OtherPlayer.transform.localScale.x, _knockbackDirectionY), new Vector2(_knockbackForce, _knockbackForce), _knockbackDuration);
        CameraShake.Instance.Shake(_cameraShaker);
        _canCheckGroundCoroutine = StartCoroutine(CanCheckGroundCoroutine());
    }

    IEnumerator CanCheckGroundCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        _canCheckGround = true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToKnockdownState();
    }

    private new void ToKnockdownState()
    {
        if (_playerMovement.IsGrounded && _canCheckGround)
        {
            Instantiate(_groundedPrefab, transform.position, Quaternion.identity);
            _stateMachine.ChangeState(_knockdownState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        if (_canCheckGroundCoroutine != null)
        {
            _player.OtherPlayer.StopComboTimer();
            _canCheckGround = false;
            StopCoroutine(_canCheckGroundCoroutine);
        }
    }
}