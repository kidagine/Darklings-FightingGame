using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D.Animation;

public class PlayerAnimator : MonoBehaviour
{
	[SerializeField] private PlayerStats _playerStats = default;
	[SerializeField] private InputBuffer _inputBuffer = null;
	private Animator _animator;
	private SpriteLibrary _spriteLibrary;
	private SpriteRenderer _spriteRenderer;

	[HideInInspector] public UnityEvent OnCurrentAnimationFinished;
	
	public PlayerStats PlayerStats { get { return _playerStats; } private set { } }

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_spriteLibrary = GetComponent<SpriteLibrary>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		_animator.runtimeAnimatorController = _playerStats.PlayerStatsSO.runtimeAnimatorController;
	}

	void Update()
	{
		if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
		{
			OnCurrentAnimationFinished?.Invoke();
			OnCurrentAnimationFinished.RemoveAllListeners();
			if (_inputBuffer != null)
			{
				_inputBuffer.CheckInputBuffer();
			}
		}
	}

	public void Walk()
	{
		_animator.Play("Walk");
	}

	public void Idle()
	{
		_animator.Play("Idle");
	}

	public void Crouch()
	{
		_animator.Play("Crouch");
	}

	public void Jump(bool reset = false)
	{
		if (reset)
		{
			_animator.Play("Jump", -1, 0f);
		}
		else
		{
			_animator.Play("Jump");
		}
	}

	public void JumpForward(bool reset = false)
	{
		if (reset)
		{
			_animator.Play("JumpForward", -1, 0f);
		}
		else
		{
			_animator.Play("JumpForward");
		}
	}

	public void Attack(string attackType, bool reset = false)
	{
		if (reset)
		{
			_animator.Play(attackType, -1, 0f);
		}
		else
		{
			_animator.Play(attackType);
		}
	}

	public void Shadowbreak()
	{
		_animator.Play("Shadowbreak");
	}

	public void Grab()
	{
		_animator.Play("Grab");
	}

	public void Throw()
	{
		_animator.Play("Throw");
	}

	public void Arcana()
	{
		_animator.Play("Arcana", -1, 0f);
	}

	public void ArcanaCrouch()
	{
		_animator.Play("ArcanaCrouch", -1, 0f);
	}

	public void ArcanaThrow()
	{
		_animator.Play("ArcanaThrow");
	}

	public void Hurt(bool reset = false)
	{
		if (reset)
		{
			_animator.Play("Hurt", -1, 0f);
		}
		else
		{
			_animator.Play("Hurt");
		}
	}

	public void HurtAir(bool reset = false)
	{
		if (reset)
		{
			_animator.Play("HurtAir", -1, 0f);
		}
		else
		{
			_animator.Play("HurtAir");
		}
	}

	public void Block()
	{
		_animator.Play("Block");
	}

	public void BlockLow()
	{
		_animator.Play("BlockLow");
	}
	public void BlockAir()
	{
		_animator.Play("BlockAir");
	}

	public void Dash()
	{
		_animator.Play("Dash");
	}

	public void AirDash()
	{
		_animator.Play("Jump");
	}

	public void Run()
	{
		_animator.Play("Run");
	}

	public void Taunt()
	{
		_animator.Play("Taunt", -1, 0f);
	}

	public void Knockdown()
	{
		_animator.Play("Knockdown");
	}

	public void WakeUp()
	{
		_animator.Play("WakeUp");
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
		if (_spriteLibrary != null)
		{
			_spriteLibrary.spriteLibraryAsset = PlayerStats.PlayerStatsSO.spriteLibraryAssets[skinNumber];
		}
		return skinNumber;
	}

	public void SetSpriteOrder(int index)
	{
		_spriteRenderer.sortingOrder = index;
	}
}
