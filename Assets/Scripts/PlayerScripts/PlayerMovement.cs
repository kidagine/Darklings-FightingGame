using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPushboxResponder
{
    [SerializeField] private PlayerAnimator _playerAnimator = default;
    [SerializeField] private PlayerStatsSO _playerStatsSO = default;
    [SerializeField] private GameObject _dustUpPrefab = default;
    [SerializeField] private GameObject _dustDownPrefab = default;
    private Player _player;
    private Rigidbody2D _rigidbody;
    private Audio _audio;
    private bool _isJumping;
    private bool _isMovementLocked;

    public Vector2 MovementInput { get; set; }
    public bool IsGrounded { get; set; } = true;
    public bool IsCrouching { get; private set; }
    public bool IsMoving { get; private set; }


    void Awake()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _audio = GetComponent<Audio>();
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
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (3 - 1) * Time.deltaTime;
        }
        else if (_rigidbody.velocity.y > 0 && !_isJumping)
        {
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (6 - 1) * Time.deltaTime;
        }
    }

    private void Movement()
    {
        if (!IsCrouching && !_player.IsAttacking && !_isMovementLocked && !_player.IsBlocking)
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
        if (IsGrounded && !_player.IsAttacking && !_player.IsBlocking)
        {
            Instantiate(_dustUpPrefab, transform.position, Quaternion.identity);
            _isJumping = true;
            _audio.Sound("Jump").Play();
            IsGrounded = false;
            _playerAnimator.IsJumping(true);
            _isMovementLocked = true;
            _rigidbody.velocity = Vector2.zero;
            if (MovementInput.x == 0.0f)
            {
                _rigidbody.AddForce(new Vector2(0.0f, _playerStatsSO.jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                _rigidbody.AddForce(new Vector2(Mathf.Round(MovementInput.x) * (_playerStatsSO.jumpForce / 2.0f), _playerStatsSO.jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    public void JumpStopAction()
    {
        _isJumping = false;
    }

    public void SetLockMovement(bool state)
    {
        MovementInput = Vector2.zero;
        _rigidbody.velocity = Vector2.zero;
        _isMovementLocked = state;
  //      if (state)
		//{
		//	_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
		//}
		//else
		//{
		//	_rigidbody.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
		//}
	}

    public void GroundedPoint(Transform other, float point)
    {
        if (point == -1.0f && _rigidbody.velocity.y < 0.0f)
        {
            if (transform.position.x > 7.85f)
            {
                transform.position += new Vector3(-1f, -1.5f, 0.0f);
            }
            else if (transform.position.x < -7.85f)
            {
                transform.position += new Vector3(1f, -1.5f, 0.0f);
            }
            else 
            {
                transform.position += new Vector3(-0.5f * transform.localScale.x, -1.5f, 0.0f);
            }
            _rigidbody.velocity = Vector2.zero;
        }
    }

	public void OnGrounded()
	{
        if (!IsGrounded && _rigidbody.velocity.y <= 0.0f)
        {
            Instantiate(_dustDownPrefab, transform.position, Quaternion.identity);
            _audio.Sound("Landed").Play();
            _player.IsAttacking = false;
            _playerAnimator.IsJumping(false);
            IsGrounded = true;
            _isMovementLocked = false;
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
}
