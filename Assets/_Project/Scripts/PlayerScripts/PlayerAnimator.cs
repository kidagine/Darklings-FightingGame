using Demonics.Sounds;
using UnityEngine;

public class PlayerAnimator : DemonicsAnimator
{
    [SerializeField] private PlayerCollisionBoxes _playerCollisionBoxes = default;
    [SerializeField] private Player _player = default;
    [SerializeField] private PlayerMovement _playerMovement = default;
    [SerializeField] private InputBuffer _inputBuffer = null;
    [SerializeField] private Audio _audio = default;
    [SerializeField] private DemonicsPhysics _grabPoint = default;
    private Shadow _shadow;

    public PlayerStatsSO PlayerStats { get { return _player.playerStats; } set { } }


    void Awake()
    {
        _shadow = GetComponent<Shadow>();
    }

    void Start()
    {
        _animation = _player.playerStats._animation;
    }

    protected override void CheckEvents()
    {
        base.CheckEvents();
        if (GetEvent().projectile)
        {
            _player.CreateEffect(true);
        }
        if (GetEvent().jump)
        {
            _playerMovement.AddForce((GetEvent().jumpDirection.x));
        }
        if (GetEvent().footstep)
        {
            _audio.SoundGroup("Footsteps").PlayInRandom();
        }
        if (GetEvent().grabPoint != Vector2.zero)
        {
            DemonicsVector2 throwOffset = new DemonicsVector2((DemonicsFloat)GetEvent().grabPoint.x * (DemonicsFloat)(_player.transform.localScale.x), (DemonicsFloat)GetEvent().grabPoint.y);
            _player.OtherPlayerMovement.Physics.SetPositionWithRender(new DemonicsVector2(_playerMovement.Physics.Position.x + throwOffset.x, _playerMovement.Physics.Position.y + throwOffset.y));
        }
        if (GetEvent().throwEnd)
        {
            _audio.Sound("Impact6").Play();
            CameraShake.Instance.Shake(_animation.GetGroup(_group).cameraShake);
            _player.OtherPlayerStateManager.TryToKnockdownState();
        }
        if (GetEvent().throwArcanaEnd)
        {
            _audio.Sound("Impact6").Play();
            CameraShake.Instance.Shake(_animation.GetGroup(_group).cameraShake);
            _player.OtherPlayerStateManager.TryToHurtState(_player.CurrentAttack);
        }
        _player.Parrying = GetEvent().parry;
        _player.Invincible = GetEvent().invisibile;
    }

    protected override void CheckAnimationBoxes()
    {
        base.CheckAnimationBoxes();
        _playerCollisionBoxes.SetHurtboxes(GetHurtboxes());
        _playerCollisionBoxes.SetHitboxes(GetHitboxes());
    }

    public void SetInvinsible(bool state)
    {
        _spriteRenderer.enabled = !state;
        _shadow.SetInvinsible(state);
    }

    protected override void AnimationEnded()
    {
        base.AnimationEnded();
        if (_inputBuffer != null)
        {
            _inputBuffer.CheckInputBuffer();
        }
    }

    public bool InRecovery()
    {
        for (int i = 0; i < _animation.animationCelsGroup[_group].animationCel.Count; i++)
        {
            if (i < _cel)
            {
                if (_animation.animationCelsGroup[_group].animationCel[i].hitboxes.Count > 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool InActive()
    {
        if (GetHitboxes().Length > 0)
        {
            return true;
        }
        return false;
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
        transform.localPosition = new Vector2(0, 1);
        transform.localRotation = Quaternion.Euler(0, 0, -90);
        SetAnimation("Wallsplat");
    }

    public void Throw()
    {
        SetAnimation("Throw");
    }

    public void BlueFrenzy()
    {
        SetAnimation("Parry");
    }

    public void RedFrenzy()
    {
        SetAnimation("RedFrenzy");
    }

    public void Arcana(string arcanaType)
    {
        SetAnimation(arcanaType);
    }

    public void ArcanaThrow()
    {
        SetAnimation("5AEnd");
    }

    public void Hurt()
    {
        SetAnimation("Hurt");
    }

    public void HurtAir()
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

    public void ResetPosition()
    {
        transform.localPosition = Vector2.zero;
        transform.localRotation = Quaternion.identity;
    }

    public Sprite GetCurrentSprite()
    {
        return _spriteRenderer.sprite;
    }

    public void SpriteSuperArmorEffect()
    {
        _spriteRenderer.color = Color.red;
    }

    public void SpriteNormalEffect()
    {
        _spriteRenderer.color = Color.white;
    }

    public int SetSpriteLibraryAsset(int skinNumber)
    {
        _animation = _player.playerStats._animation;
        if (skinNumber > _animation.spriteAtlas.Length - 1)
        {
            _skin = 0;
        }
        else if (skinNumber < 0)
        {
            skinNumber = _animation.spriteAtlas.Length - 1;
        }
        _skin = skinNumber;
        return skinNumber;
    }

    public void SetSpriteOrder(int index)
    {
        _spriteRenderer.sortingOrder = index;
    }
}
