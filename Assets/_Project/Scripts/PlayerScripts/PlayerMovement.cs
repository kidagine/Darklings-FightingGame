using Demonics.Sounds;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPushboxResponder
{
	[SerializeField] private LayerMask _wallLayerMask = default;
	private Rigidbody2D _rigidbody;
	private PlayerStats _playerStats;
	private Player _player;
	private Audio _audio;
	private bool _onTopOfPlayer;
	public bool HasJumped { get; set; }
	public bool HasDoubleJumped { get; set; }
	public bool HasAirDashed { get; set; }
	public float MovementSpeed { get; set; }
	public Vector2 MovementInput { get; set; }
	public bool IsGrounded { get; set; } = true;
	public bool CanDoubleJump { get; set; } = true;
	public bool IsInCorner { get; private set; }

	void Awake()
	{
		_player = GetComponent<Player>();
		_playerStats = GetComponent<PlayerStats>();
		_rigidbody = GetComponent<Rigidbody2D>();
		_audio = GetComponent<Audio>();
	}

	void Start()
	{
		MovementSpeed = _playerStats.PlayerStatsSO.walkSpeed;
	}

	void Update()
	{
		CheckIsInCorner();
	}

	void FixedUpdate()
	{
		JumpControl();
	}

	public void ResetMovement()
	{
		_rigidbody.velocity = Vector2.zero;
	}

	private void JumpControl()
	{
		if (_rigidbody.velocity.y < 0)
		{
			_rigidbody.velocity += (4 - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
		}
		else if (_rigidbody.velocity.y > 0)
		{
			_rigidbody.velocity += (3 - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
		}
	}

	public void TravelDistance(Vector2 travelDistance)
	{
		_rigidbody.velocity = Vector2.zero;
		_rigidbody.AddForce(new Vector2(travelDistance.x * 3.0f, travelDistance.y * 3.0f), ForceMode2D.Impulse);
	}

	public void GroundedPoint(Transform other, float point)
	{
		if (_rigidbody.velocity.y < 0.0f)
		{
			float pushForceX = 8.0f;
			float pushForceY = -4.0f;
			if (_player.OtherPlayerMovement.IsInCorner && transform.localScale.x > 0.0f)
			{
				_onTopOfPlayer = true;
				_rigidbody.velocity = Vector2.zero;
				_rigidbody.AddForce(new Vector2(-pushForceX, pushForceY), ForceMode2D.Impulse);
			}
			else if (_player.OtherPlayerMovement.IsInCorner && transform.localScale.x < 0.0f)
			{
				_onTopOfPlayer = true;
				_rigidbody.velocity = Vector2.zero;
				_rigidbody.AddForce(new Vector2(pushForceX, pushForceY), ForceMode2D.Impulse);
			}
		}
	}

	public void AddForce(int moveHorizontally)
	{
		float jumpForce = _playerStats.PlayerStatsSO.jumpForce - 3.5f;
		int direction = 0;
		if (moveHorizontally == 1)
		{
			direction = (int)transform.localScale.x * -1;
		}
		_rigidbody.AddForce(new Vector2(Mathf.Round(direction) * (jumpForce / 2.5f), jumpForce + 1.0f), ForceMode2D.Impulse);
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
		IsGrounded = true;
	}

	public void OnAir()
	{
		IsGrounded = false;
	}

	public void Knockback(Vector2 knockbackDirection, Vector2 knockbackForce, float knockbackDuration)
	{
		_rigidbody.MovePosition(new Vector2(transform.position.x + knockbackForce.x, transform.position.y + knockbackForce.y));
		StartCoroutine(KnockbackCoroutine(knockbackForce * knockbackDirection, knockbackDuration));
	}

	IEnumerator KnockbackCoroutine(Vector2 knockback, float knockbackDuration)
	{
		Vector2 startingPosition = transform.position;
		Vector2 finalPosition = new(transform.position.x + knockback.x, transform.position.y + knockback.y);
		float elapsedTime = 0;
		while (elapsedTime < knockbackDuration)
		{
			_rigidbody.MovePosition(Vector3.Lerp(startingPosition, finalPosition, elapsedTime / knockbackDuration));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		_rigidbody.MovePosition(finalPosition);
	}

	private void CheckIsInCorner()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-transform.localScale.x, 0.0f), 1.0f, _wallLayerMask);
		if (hit.collider != null)
		{
			IsInCorner = true;
		}
		else
		{
			IsInCorner = false;
		}
	}

	public Vector2 OnWall()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-transform.localScale.x, 0.0f), 2.0f, _wallLayerMask);
		if (hit.collider != null)
		{
			return new Vector2(hit.point.x - (0.2f * -transform.localScale.x), transform.localPosition.y);
		}
		else
		{
			return Vector2.zero;
		}
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
		if (MovementSpeed == _playerStats.PlayerStatsSO.runSpeed)
		{
			_audio.Sound("Run").Stop();
			MovementSpeed = _playerStats.PlayerStatsSO.walkSpeed;
		}
	}

	public void SetRigidbodyKinematic(bool state)
	{
		if (state)
		{
			_rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
		}
		else
		{
			_rigidbody.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
		}
		_rigidbody.isKinematic = state;
	}
}
