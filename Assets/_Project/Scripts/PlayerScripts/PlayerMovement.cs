using Demonics.Sounds;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPushboxResponder
{
	[SerializeField] private PlayerAnimator _playerAnimator = default;
	[SerializeField] private GameObject _dustUpPrefab = default;
	[SerializeField] private GameObject _dustDownPrefab = default;
	[SerializeField] private GameObject _dashPrefab = default;
	[SerializeField] private GameObject _playerGhostPrefab = default;
	private Player _player;
	private BrainController _playerController;
	private Rigidbody2D _rigidbody;
	private PlayerStats _playerStats;
	private InputBuffer _inputBuffer;
	private Coroutine _ghostsCoroutine;
	private Audio _audio;
	private float _movementSpeed;
	private bool _isMovementLocked;
	private bool _onTopOfPlayer;
	private bool _hasDashedMiddair;

	public Vector2 MovementInput { get; set; }
	public bool IsGrounded { get; set; } = true;
	public bool IsCrouching { get; private set; }
	public bool IsMoving { get; private set; }
	public bool IsDashing { get; private set; }
	public bool CanDoubleJump { get; set; } = true;
	public bool IsInCorner { get; set; }


	void Awake()
	{
		_player = GetComponent<Player>();
		_playerStats = GetComponent<PlayerStats>();
		_rigidbody = GetComponent<Rigidbody2D>();
		_inputBuffer = GetComponent<InputBuffer>();
		_audio = GetComponent<Audio>();
	}

	public void SetController()
	{
		_playerController = GetComponent<BrainController>();
	}


	void Start()
	{
		_movementSpeed = _playerStats.PlayerStatsSO.walkSpeed;
	}

	void FixedUpdate()
	{
		Movement();
		JumpControl();
	}

	public void ResetPlayerMovement()
	{
		IsGrounded = true;
		SetLockMovement(false);
		CanDoubleJump = true;
		ResetGravity();
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

		if (!IsCrouching && !_player.IsAttacking && !_onTopOfPlayer && !IsDashing && !_isMovementLocked)
		{
			if (!_player.IsBlocking && !_player.IsKnockedDown)
			{
				_rigidbody.velocity = new Vector2(MovementInput.x * _movementSpeed, _rigidbody.velocity.y);
			}
			else
			{
				_rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
			}
			_playerAnimator.SetMovementX(MovementInput.x * transform.localScale.x);
			if (_rigidbody.velocity.x != 0.0f)
			{
				if (_rigidbody.velocity.x > 0.0f && transform.localScale.x == 1.0f)
				{
					_player.ArcaneSlowdown = 6.5f;
				}
				else if (_rigidbody.velocity.x < 0.0f && transform.localScale.x == -1.0f)
				{
					_player.ArcaneSlowdown = 6.5f;
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
				IsMoving = false;
				_player.ArcaneSlowdown = 8.0f;
				_playerAnimator.SetMove(false);
			}
		}
		else
		{
			IsMoving = false;
		}
	}

	public void TravelDistance(Vector2 travelDistance)
	{
		_rigidbody.velocity = Vector2.zero;
		_rigidbody.AddForce(new Vector2(travelDistance.x * 3.0f, travelDistance.y * 3.0f), ForceMode2D.Impulse);
	}

	public bool CrouchAction()
	{
		if (!_player.IsAttacking && !_player.IsBlocking && !IsDashing && !_player.IsKnockedDown)
		{
			ResetToWalkSpeed();
			if (IsGrounded)
			{
				_rigidbody.velocity = Vector2.zero;
			}
			IsCrouching = true;
			_playerAnimator.IsCrouching(true);
			return true;
		}
		return false;
	}

	public bool StandUpAction()
	{
		if (!_player.IsAttacking)
		{
			IsCrouching = false;
			_playerAnimator.IsCrouching(false);
			return true;
		}
		return false;
	}

	public void JumpAction()
	{
		if (!_player.IsAttacking && !_player.IsBlocking && !IsDashing && !_player.IsKnockedDown)
		{
			if (IsGrounded)
			{
				Jump(_playerStats.PlayerStatsSO.jumpForce);
			}
			else if (CanDoubleJump && _playerStats.PlayerStatsSO.canDoubleJump)
			{
				CanDoubleJump = false;
				_playerAnimator.Rebind();
				Jump(_playerStats.PlayerStatsSO.jumpForce - 1.5f);
			}
		}
	}

	private void Jump(float jumpForce)
	{
		_player.CanFlip = false;
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
			_rigidbody.AddForce(new Vector2(0.0f, _playerStats.PlayerStatsSO.jumpForce), ForceMode2D.Impulse);
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
			if (IsInCorner && transform.localScale.x > 1.0f)
			{
				_onTopOfPlayer = true;
				_rigidbody.velocity = Vector2.zero;
				_rigidbody.AddForce(new Vector2(-pushForceX, pushForceY), ForceMode2D.Impulse);
			}
			else if (IsInCorner && transform.localScale.x < 1.0f)
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
		StartCoroutine(OnGroundedCoroutine());
	}

	IEnumerator OnGroundedCoroutine()
	{
		if (!IsGrounded && _rigidbody.velocity.y <= 0.0f)
		{
			_player.CanFlip = true;
			_playerAnimator.IsJumping(false);
			ResetGravity();
			_hasDashedMiddair = false;
			CanDoubleJump = true;
			_onTopOfPlayer = false;
			_player.SetPushboxTrigger(false);
			_player.SetAirPushBox(false);
			Instantiate(_dustDownPrefab, transform.position, Quaternion.identity);
			_audio.Sound("Landed").Play();
			IsGrounded = true;
			if (_player.HitMiddair)
			{
				_player.StopStun(true);
				_player.HitMiddair = false;
				SetLockMovement(false);
				if (!_player.IsKnockedDown)
				{
					_playerController.ActivateInput();
				}
				_playerAnimator.IsHurt(false);
			}
			if (_player.IsKnockedDown && !_player.IsDead)
			{
				_player.Knockdown();
			}
			else
			{
				_isMovementLocked = false;
			}
			yield return null;
			//TODO Replace this code
			if (_player.CurrentAttack)
			{
				if (_player.CurrentAttack.name != "VoidBeam")
				{
					_player.IsAttacking = false;
				}
			}
			//_inputBuffer.CheckForInputBufferItem();
		}
	}

	public void OnAir()
	{
		IsGrounded = false;
		_playerAnimator.IsJumping(true);
	}

	public void Knockback(Vector2 knockbackDirection, float knockbackForce, float knockbackDuration)
	{
		_rigidbody.MovePosition(new Vector2(transform.position.x + knockbackForce, transform.position.y));
		StartCoroutine(KnockbackCoroutine(knockbackForce * knockbackDirection, knockbackDuration));
	}

	IEnumerator KnockbackCoroutine(Vector2 knockback, float knockbackDuration)
	{
		Vector2 startingPosition = transform.position;
		Vector2 finalPosition = new Vector2(transform.position.x + knockback.x, transform.position.y + knockback.y);
		float elapsedTime = 0;
		while (elapsedTime < knockbackDuration)
		{
			_rigidbody.MovePosition(Vector3.Lerp(startingPosition, finalPosition, elapsedTime / knockbackDuration));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		_rigidbody.MovePosition(finalPosition);
	}

	public void DashAction(float directionX)
	{
		if (!IsCrouching && !_player.IsAttacking && !_player.IsBlocking && !_player.IsKnockedDown)
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
		_rigidbody.velocity = new Vector2(directionX, 0.0f) * _playerStats.PlayerStatsSO.dashForce;
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
			_movementSpeed = _playerStats.PlayerStatsSO.runSpeed;
			IsDashing = false;
			_player.CanFlip = true;
			_ghostsCoroutine = StartCoroutine(RunCoroutine());
		}
		else
		{
			if (IsGrounded)
			{
				yield return new WaitForSeconds(0.05f);
			}
			IsDashing = false;
			_player.CanFlip = true;
		}
		yield return null;
		_inputBuffer.CheckForInputBufferItem();
	}

	public void StopGhosts()
	{
		if (_ghostsCoroutine != null)
		{
			StopCoroutine(_ghostsCoroutine);
		}
	}

	IEnumerator RunCoroutine()
	{
		while (_movementSpeed == _playerStats.PlayerStatsSO.runSpeed)
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
			_player.CanFlip = true;
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
		if (_movementSpeed == _playerStats.PlayerStatsSO.runSpeed)
		{
			_audio.Sound("Run").Stop();
			_movementSpeed = _playerStats.PlayerStatsSO.walkSpeed;
		}
	}
}
