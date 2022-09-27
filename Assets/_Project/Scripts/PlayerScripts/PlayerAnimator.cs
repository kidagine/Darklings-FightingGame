using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D.Animation;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player _player = default;
    [SerializeField] private InputBuffer _inputBuffer = null;
    [SerializeField] private AnimationSO _animation = default;
    private Animator _animator;
    private SpriteLibrary _spriteLibrary;
    private SpriteRenderer _spriteRenderer;
    private int _frame;
    private int _cel;
    private int _group;
    private int _skin;
    private bool _isPaused;

    [HideInInspector] public UnityEvent OnCurrentAnimationFinished;

    public PlayerStatsSO PlayerStats { get { return _player.playerStats; } set { } }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteLibrary = GetComponent<SpriteLibrary>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _animator.runtimeAnimatorController = _player.playerStats.runtimeAnimatorController;
    }

    void FixedUpdate()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        if (!_isPaused)
        {
            if (_frame == _animation.GetCel(_group, _cel).frames)
            {
                _cel++;
                if (_cel > _animation.GetGroup(_group).animationCel.Length - 1)
                {
                    AnimationEnded();
                    if (!_animation.GetGroup(_group).loop)
                    {
                        return;
                    }
                }
                CheckAnimationBoxes();
                _frame = 0;
            }
            _spriteRenderer.sprite = _animation.GetSprite(_skin, _group, _cel);
            _frame++;
        }
    }

    private void CheckAnimationBoxes()
    {
        if (_animation.GetCel(_group, _cel).active)
        {
            _player.SetHitbox(true);
            //_animation.GetCel(_group, _cel).hurtboxes[0];
        }
        else
        {
            _player.SetHitbox(false);
        }
    }

    private void AnimationEnded()
    {
        if (!_animation.GetGroup(_group).loop)
        {
            _isPaused = true;
        }
        _frame = 0;
        _cel = 0;
        OnCurrentAnimationFinished?.Invoke();
        OnCurrentAnimationFinished.RemoveAllListeners();
        if (_inputBuffer != null)
        {
            _inputBuffer.CheckInputBuffer();
        }
    }

    private void SetAnimation(string name)
    {
        _frame = 0;
        _cel = 0;
        _group = _animation.GetGroupId(name);
        _spriteRenderer.sprite = _animation.GetSprite(_skin, _group, _cel);
        _isPaused = false;
    }

    public void Pause()
    {
        _animator.speed = 0;
    }

    public void Resume()
    {
        _animator.speed = 1;
    }

    public void Walk()
    {
        SetAnimation("Walk");
    }

    public void Idle()
    {
        SetAnimation("Idle");
    }

    public void Crouch()
    {
        SetAnimation("Crouch");
    }

    public void Jump(bool reset = false)
    {
        SetAnimation("Jump");
    }

    public void JumpForward(bool reset = false)
    {
        SetAnimation("JumpForward");
    }

    public void Attack(string attackType, bool reset = false)
    {
        SetAnimation(attackType);
    }

    public void Shadowbreak()
    {
        SetAnimation("Shadowbreak");
    }

    public void Grab()
    {
        SetAnimation("Grab");
    }

    public void WallSplat()
    {
        SetAnimation("WallSplat");
    }

    public void Throw()
    {
        SetAnimation("Throw");
    }

    public void Parry()
    {
        SetAnimation("Parry");
    }

    public void Arcana(string arcanaType)
    {
        SetAnimation(arcanaType);
    }

    public void ArcanaThrow()
    {
        SetAnimation("ArcanaThrow");
    }

    public void Hurt(bool reset = false)
    {
        SetAnimation("Hurt");
    }

    public void HurtAir(bool reset = false)
    {
        SetAnimation("HurtAir");
    }

    public void Block()
    {
        SetAnimation("Block");
    }

    public void BlockLow()
    {
        SetAnimation("BlockLow");
    }
    public void BlockAir()
    {
        SetAnimation("BlockAir");
    }

    public void Dash()
    {
        SetAnimation("Dash");
    }

    public void AirDash()
    {
        SetAnimation("Jump");
    }

    public void Run()
    {
        SetAnimation("Run");
    }

    public void Taunt()
    {
        SetAnimation("Taunt");
    }

    public void Knockdown()
    {
        SetAnimation("Knockdown");
    }

    public void WakeUp()
    {
        SetAnimation("WakeUp");
    }

    public Sprite GetCurrentSprite()
    {
        return _spriteRenderer.sprite;
    }

    public int SetSpriteLibraryAsset(int skinNumber)
    {
        if (skinNumber > PlayerStats.spriteLibraryAssets.Length - 1)
        {
            _skin = 0;
        }
        else if (skinNumber < 0)
        {
            skinNumber = PlayerStats.spriteLibraryAssets.Length - 1;
        }
        if (_spriteLibrary != null)
        {
            _spriteLibrary.spriteLibraryAsset = PlayerStats.spriteLibraryAssets[skinNumber];
        }
        _skin = skinNumber;
        return skinNumber;
    }

    public void SetSpriteOrder(int index)
    {
        _spriteRenderer.sortingOrder = index;
    }
}
