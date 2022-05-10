using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerAnimator : MonoBehaviour
{
	[SerializeField] private PlayerStats _playerStats = default;
	[SerializeField] private InputBuffer _inputBuffer = default;
	[SerializeField] private GameObject _playerShadowPrefab = default;
	private Animator _animator;
	private SpriteLibrary _spriteLibrary;
	private SpriteRenderer _spriteRenderer;

	public PlayerStats PlayerStats { get { return _playerStats; } private set { } }

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_spriteLibrary = GetComponent<SpriteLibrary>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		PlayerShadow playerShadow = Instantiate(_playerShadowPrefab).transform.GetChild(0).GetComponent<PlayerShadow>();
		playerShadow.Initialize(transform.root, _spriteRenderer);
	}

	void Start()
	{
		_animator.runtimeAnimatorController = _playerStats.PlayerStatsSO.runtimeAnimatorController;
	}

	public void SetMove(bool state)
	{
		_animator.SetBool("IsMoving", state);
	}

	public void SetMovementX(float value)
	{
		_animator.SetFloat("MovementInputX", value);
	}

	public void IsCrouching(bool state)
	{
		_animator.SetBool("IsCrouching", state);
	}

	public void IsJumping(bool state)
	{
		_animator.SetBool("IsJumping", state);
	}

	public void CancelAttack()
	{
		_animator.SetTrigger("Cancel");
	}

	public void CancelHurt()
	{
		_animator.SetTrigger("CancelHurt");
	}

	public void ResetAnimation(string name)
	{
		_animator.Play(name, -1, 0f);
	}

	public void ResetTrigger(string name)
	{
		_animator.ResetTrigger(name);
	}

	public void Attack(string attackType)
	{
		_animator.SetTrigger(attackType);
	}
	public void Shadowbreak()
	{
		_animator.SetTrigger("Shadowbreak");
	}
	public void Throw()
	{
		_animator.SetTrigger("Throw");
	}

	public void Intro()
	{
		_animator.SetTrigger("Intro");
	}

	public void ThrowEnd()
	{
		_animator.SetTrigger("ThrowEnd");
	}

	public void Arcana()
	{
		_animator.SetTrigger("Arcana");
	}

	public void ArcanaEnd()
	{
		_animator.SetTrigger("ArcanaEnd");
	}

	public void Hurt()
	{
		_animator.SetTrigger("Hurt");
	}

	public void IsBlocking(bool state)
	{
		_animator.SetBool("IsBlocking", state);
	}

	public void IsBlockingLow(bool state)
	{
		_animator.SetBool("IsBlockingLow", state);
	}
	public void IsBlockingAir(bool state)
	{
		_animator.SetBool("IsBlockingAir", state);
	}

	public void IsDashing(bool state)
	{
		_animator.SetBool("IsDashing", state);
	}

	public void IsRunning(bool state)
	{
		_animator.SetBool("IsRunning", state);
	}

	public void Taunt()
	{
		_animator.SetTrigger("Taunt");
	}

	public void Death()
	{
		_animator.SetTrigger("Death");
	}

	public void IsKnockedDown(bool state)
	{
		_animator.SetBool("IsKnockedDown", state);
	}

	public void Rebind()
	{
		_animator.Rebind();
	}

	public Sprite GetCurrentSprite()
	{
		return _spriteRenderer.sprite;
	}

	public int SetSpriteLibraryAsset(int skinNumber)
	{
		if (skinNumber > PlayerStats.PlayerStatsSO.spriteLibraryAssets.Length - 1)
		{
			skinNumber = 0;
		}
		else if (skinNumber < 0)
		{
			skinNumber = PlayerStats.PlayerStatsSO.spriteLibraryAssets.Length - 1;
		}
		_spriteLibrary.spriteLibraryAsset = PlayerStats.PlayerStatsSO.spriteLibraryAssets[skinNumber];
		return skinNumber;
	}

	public void SetSpriteOrder(int index)
	{
		_spriteRenderer.sortingOrder = index;
	}
}
	