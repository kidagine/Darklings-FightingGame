using Demonics.Sounds;
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
    private float _movementSpeed;
    private bool _canDoubleJump = true;
    private bool _isMovementLocked;
    private bool _onTopOfPlayer;
    private bool _hasDashedMiddair;

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
    }

    public void SetController()
    {
        _playerController = GetComponent<BaseController>();
    }


    void Start()
    {
        _movementSpeed = _playerStatsSO.walkSpeed;
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
                if (!_player.IsPlayerOne)
                {
                    Debug.Log("a");
                }
                _rigidbody.velocity = new Vector2(MovementInput.x * _movementSpeed, _rigidbody.velocity.y);
                _playerAnimator.SetMovementX(MovementInput.x * transform.localScale.x);
                if (_rigidbody.velocity.x != 0.0f)
                {
                    if (_rigidbody.velocity.x > 0.0f && transform.localScale.x == 1.0f)
                    {
                        _player.ArcaneSlowdown = 4.5f;
                    }
                    else if (_rigidbody.velocity.x < 0.0f && transform.localScale.x == -1.0f)
                    {
                        _player.ArcaneSlowdown = 4.5f;
                    }
                    else
                    {
                        ResetToWalkSpeed();
                    }
                    IsMoving = true;
                    _playerAnimator.SetMove(true);
                }
                else
                {
                    ResetToWalkSpeed();
                    _player.ArcaneSlowdown = 6.0f;
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
        if (!_player.IsAttacking && !_player.IsBlocking && !IsDashing)
        {
            ResetToWalkSpeed();
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
        if (!_player.IsAttacking && !_player.IsBlocking && !IsDashing)
        {
            if (IsGrounded)
            {
                Jump(_playerStatsSO.jumpForce);
            }
            else if (_canDoubleJump)
            {
                _canDoubleJump = false;
                _playerAnimator.Rebind();
                Jump(_playerStatsSO.jumpForce - 1.5f);
            }
        }
    }

    private void Jump(float jumpForce)
    {
        ResetToWalkSpeed();
        _player.SetPushboxTrigger(true);
        _player.SetAirPushBox(true);
        Instantiate(_dustUpPrefab, transform.position, Quaternion.identity);
        _audio.Sound("Jump").Play();
        IsGrounded = false;
        _playerAnimator.IsJumping(true);
        _playerAnimator.SetMovementX(MovementInput.x * transform.localScale.x);
        _isMovementLocked = true;
        _rigidbody.velocity = Vector2.zero;
        if (MovementInput.x == 0.0f)
        {
            _rigidbody.AddForce(new Vector2(0.0f, _playerStatsSO.jumpForce), ForceMode2D.Impulse);
        }
        else
        {
            _rigidbody.AddForce(new Vector2(Mathf.Round(MovementInput.x) * (jumpForce / 2.5f), jumpForce + 1.0f), ForceMode2D.Impulse);
        }
    }

    public void SetLockMovement(bool state)
    {
        MovementInput = Vector2.zero;
        _rigidbody.velocity = Vector2.zero;
        _isMovementLocked = state;
        _playerAnimator.SetMove(false);
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
            ResetGravity();
            _hasDashedMiddair = false;
            _canDoubleJump = true;
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

    public void DashAction(float directionX)
    {
        if (!IsCrouching && !_player.IsAttacking)
        {
            if (IsGrounded)
            {
                Dash(directionX);
            }
            else if (!_hasDashedMiddair)
            {
                _hasDashedMiddair = true;
                _player.SetPushboxTrigger(false);
                Dash(directionX);
            }
        }
    }

    private void Dash(float directionX)
    {
        _audio.Sound("Dash").Play();
        _playerAnimator.IsDashing(true);
        Transform dashEffect = Instantiate(_dashPrefab, transform.position, Quaternion.identity).transform;
        if (MovementInput.x > 0.0f)
        {
            dashEffect.position = new Vector2(dashEffect.position.x - 1.0f, dashEffect.position.y);
        }
        else
        {
            dashEffect.localScale = new Vector2(-1.0f, transform.localScale.y);
            dashEffect.position = new Vector2(dashEffect.position.x + 1.0f, dashEffect.position.y);
        }
        _rigidbody.velocity = new Vector2(directionX, 0.0f) * _playerStatsSO.dashForce;
        IsDashing = true;
        ZeroGravity();
        StartCoroutine(DashCoroutine());
    }

    IEnumerator DashCoroutine()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject playerGhost = Instantiate(_playerGhostPrefab, transform.position, Quaternion.identity);
            playerGhost.GetComponent<PlayerGhost>().SetSprite(_playerAnimator.GetCurrentSprite(), transform.localScale.x, Color.white);
            yield return new WaitForSeconds(0.07f);
        }
        _playerAnimator.IsDashing(false);
        _rigidbody.velocity = Vector2.zero;
        ResetGravity();
        if (!IsGrounded)
        {
            _player.SetPushboxTrigger(true);
        }
        if (MovementInput.x * transform.localScale.x > 0.0f && IsGrounded)
        {
            _audio.Sound("Run").Play();
            _playerAnimator.IsRunning(true);
            _movementSpeed = _playerStatsSO.runSpeed;
            IsDashing = false;
            StartCoroutine(RunCoroutine());
        }
        else
        {
            yield return new WaitForSeconds(0.05f);
            IsDashing = false;
        }
    }

    IEnumerator RunCoroutine()
    {
        while(_movementSpeed == _playerStatsSO.runSpeed)
        {
            GameObject playerGhost = Instantiate(_playerGhostPrefab, transform.position, Quaternion.identity);
            playerGhost.GetComponent<PlayerGhost>().SetSprite(_playerAnimator.GetCurrentSprite(), transform.localScale.x, Color.white);
            yield return new WaitForSeconds(0.1f);
        }
    }


    public void StopDash()
    {
        _playerAnimator.IsDashing(false);
        _rigidbody.velocity = Vector2.zero;
        ResetGravity();
        if (!IsGrounded)
        {
            _player.SetPushboxTrigger(true);
        }
        IsDashing = false;
    }

    public void ResetGravity()
    {
        _rigidbody.gravityScale = 2.0f;
    }

    public void ZeroGravity()
    {
        _rigidbody.gravityScale = 0.0f;
    }

    public void ResetToWalkSpeed()
    {
        _playerAnimator.IsRunning(false);
        if (_movementSpeed == _playerStatsSO.runSpeed)
        {
            _audio.Sound("Run").Stop();
            _movementSpeed = _playerStatsSO.walkSpeed;
        }
    }
}
