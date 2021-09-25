using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPushboxResponder
{
    [SerializeField] private PlayerAnimator _playerAnimator = default;
    [SerializeField] private PlayerStatsSO _playerStatsSO = default;
    [SerializeField] private GameObject _dustUpPrefab = default;
    [SerializeField] private GameObject _dustDownPrefab = default;
    [SerializeField] private GameObject _dashPrefab = default;
    [SerializeField] private GameObject _playerGhostPrefab = default;
    private Player _player;
    private BaseController _playerController;
    private Rigidbody2D _rigidbody;
    private Audio _audio;
    private bool _isMovementLocked;
    private bool _onTopOfPlayer;

    public Vector2 MovementInput { get; set; }
    public bool IsGrounded { get; set; } = true;
    public bool IsCrouching { get; private set; }
    public bool IsMoving { get; private set; }
	public bool IsDashing { get; private set; }


	void Awake()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _audio = GetComponent<Audio>();
        _playerController = GetComponent<BaseController>();
    }

    void FixedUpdate()
    {
        Movement();
        JumpControl();
    }

    private void JumpControl()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (4 - 1) * Time.deltaTime;
        }
        else if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (3 - 1) * Time.deltaTime;
        }
    }

    private void Movement()
    {
        if (!IsCrouching && !_player.IsAttacking && !_player.IsBlocking && !_onTopOfPlayer && !IsDashing)
        {
            if (!_isMovementLocked)
            {
                _rigidbody.velocity = new Vector2(MovementInput.x * _playerStatsSO.walkSpeed, _rigidbody.velocity.y);
                _playerAnimator.SetMovement(MovementInput.x * transform.localScale.x);
                if (_rigidbody.velocity.x != 0.0f)
                {
                    IsMoving = true;
                    _playerAnimator.SetMove(true);
                }
                else
                {
                    IsMoving = false;
                    _playerAnimator.SetMove(false);
                }
            }
        }
    }

    public void TravelDistance(float travelDistance)
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(new Vector2(travelDistance * 3.0f, 0.0f), ForceMode2D.Impulse);
    }

    public void CrouchAction()
    {
        if (!_player.IsAttacking && !_player.IsBlocking)
        {
            if (IsGrounded)
            {
                _rigidbody.velocity = Vector2.zero;
            }
            IsCrouching = true;
            _playerAnimator.IsCrouching(true);
        }
    }

    public void StandUpAction()
    {
        IsCrouching = false;
        _playerAnimator.IsCrouching(false);
    }

    public void JumpAction()
	{
        if (IsGrounded && !_player.IsAttacking && !_player.IsBlocking && !IsDashing)
        {
            _player.SetPushboxTrigger(true);
            _player.SetAirPushBox(true);
            Instantiate(_dustUpPrefab, transform.position, Quaternion.identity);
            _audio.Sound("Jump").Play();
            IsGrounded = false;
            _playerAnimator.IsJumping(true);
            _playerAnimator.SetMovement(MovementInput.x * transform.localScale.x);
            _isMovementLocked = true;
            _rigidbody.velocity = Vector2.zero;
            if (MovementInput.x == 0.0f)
            {
                _rigidbody.AddForce(new Vector2(0.0f, _playerStatsSO.jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                _rigidbody.AddForce(new Vector2(Mathf.Round(MovementInput.x) * (_playerStatsSO.jumpForce / 2.5f), _playerStatsSO.jumpForce + 1.0f), ForceMode2D.Impulse);
            }
        }
    }

    public void JumpStopAction()
    {
    }

    public void SetLockMovement(bool state)
    {
        MovementInput = Vector2.zero;
        _rigidbody.velocity = Vector2.zero;
        _isMovementLocked = state;
	}

    public void GroundedPoint(Transform other, float point)
    {
        if (_rigidbody.velocity.y < 0.0f)
        {
            float pushForceX = 8.0f;
            float pushForceY = -4.0f;
            if (other.position.x > 9.0f)
            {
                _onTopOfPlayer = true;
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.AddForce(new Vector2(-pushForceX, pushForceY), ForceMode2D.Impulse);
            }
            else if (other.position.x < -9.0f)
            {
                _onTopOfPlayer = true;
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.AddForce(new Vector2(pushForceX, pushForceY), ForceMode2D.Impulse);
            }
        }
    }

    public void GroundedPointExit()
    {
        if (_onTopOfPlayer)
        {
            _onTopOfPlayer = false;
        }
    }

    public void OnGrounded()
	{
        if (!IsGrounded && _rigidbody.velocity.y <= 0.0f)
        {
            _onTopOfPlayer = false;
            _player.SetPushboxTrigger(false);
            _player.SetAirPushBox(false);
            Instantiate(_dustDownPrefab, transform.position, Quaternion.identity);
            _audio.Sound("Landed").Play();
            _player.IsAttacking = false;
            _playerAnimator.IsJumping(false);
            IsGrounded = true;
            _isMovementLocked = false;
            if (_player.HitMiddair)
            {
                _player.HitMiddair = false;
                SetLockMovement(false);
                _playerController.ActivateInput();
                _playerAnimator.IsHurt(false);
            }
        }
    }

    public void OnAir()
	{
        IsGrounded = false;
        _playerAnimator.IsJumping(true);
    }

    public void Knockback(Vector2 knockbackDirection, float knockbackForce)
    {
        _rigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    public void Dash(float directionX)
    {
        if (IsGrounded && !IsDashing && !IsCrouching && !_player.IsAttacking)
        {
            _audio.Sound("Dash").Play();
            _playerAnimator.IsDashing(true);
            Instantiate(_dashPrefab, transform.position, Quaternion.identity);
            _rigidbody.velocity = new Vector2(directionX, 0.0f) * _playerStatsSO.dashForce;
            IsDashing = true;
            ZeroGravity();
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject playerGhost = Instantiate(_playerGhostPrefab, transform.position, Quaternion.identity);
            playerGhost.GetComponent<PlayerGhost>().SetSprite(_playerAnimator.GetCurrentSprite(), transform.localScale.x, Color.white);
            yield return new WaitForSeconds(0.08f);
        }
        _playerAnimator.IsDashing(false);
        _rigidbody.velocity = Vector2.zero;
        ResetGravity();
        yield return new WaitForSeconds(0.1f);
        IsDashing = false;
    }

    public void ResetGravity()
    {
        _rigidbody.gravityScale = 2.0f;
    }

    private void ZeroGravity()
    {
        _rigidbody.gravityScale = 0.0f;
    }
}
