using Demonics;
using UnityEngine;

public class PlayerAnimator : DemonicsAnimator
{
    [SerializeField] private Player _player = default;
    [SerializeField] private Material _normal = default;
    [SerializeField] private Material _invincible = default;
    [SerializeField] private Material _armor = default;
    [SerializeField] private Material _fire = default;
    [SerializeField] private Material _parry = default;
    [SerializeField] private Material _ar = default;
    [SerializeField] private Material _arArmor = default;
    [SerializeField] private Material _arInvincible = default;

    private Shadow _shadow;


    void Awake()
    {
        _shadow = GetComponent<Shadow>();
    }

    void Start()
    {
        _animation = _player.playerStats._animation;
    }

    public void NormalMaterial() => _spriteRenderer.material = _normal;
    public void InvincibleMaterial() => _spriteRenderer.material = _invincible;
    public void ArmorMaterial() => _spriteRenderer.material = _armor;
    public void FireMaterial() => _spriteRenderer.material = _fire;
    public void ParryMaterial() => _spriteRenderer.material = _parry;
    public void ARMaterial() => _spriteRenderer.material = _ar;
    public void ARArmorMaterial() => _spriteRenderer.material = _arArmor;
    public void ARInvincibleMaterial() => _spriteRenderer.material = _arInvincible;

    public bool GetProjectile(string name, int frame)
    {
        return GetEvent(name, frame, out _).projectile;
    }
    public bool GetParrying(string name, int frame)
    {
        return GetEvent(name, frame, out _).parry;
    }
    public bool GetThrowArcanaEnd(string name, int frame)
    {
        return GetEvent(name, frame, out _).throwArcanaEnd;
    }
    public bool GetInvincible(string name, int frame)
    {
        return GetEvent(name, frame, out _).invisibile;
    }
    public bool GetFootstep(string name, int frame, out int cel)
    {
        return GetEvent(name, frame, out cel).footstep;
    }
    public DemonVector2 GetJump(string name, int frame)
    {
        DemonVector2 jumpDirection = DemonVector2.Zero;
        if (GetEvent(name, frame, out _).jump)
        {
            jumpDirection = new DemonVector2((DemonFloat)GetEvent(name, frame, out _).jumpDirection.x * transform.root.localScale.x, (DemonFloat)GetEvent(name, frame, out _).jumpDirection.y);
        }
        return jumpDirection;
    }
    public DemonVector2 GetGrabPoint(string name, int frame)
    {
        Vector2 grabPoint = GetEvent(name, frame, out _).grabPoint;
        return new DemonVector2((DemonFloat)grabPoint.x, (DemonFloat)grabPoint.y);
    }

    public override void SetAnimation(string name, int frame)
    {
        if (name == "Wallsplat")
            transform.SetLocalPositionAndRotation(new Vector2(10 * -transform.localScale.x, 0), Quaternion.Euler(0, 0, -90));
        else
            transform.SetLocalPositionAndRotation(Vector2.zero, Quaternion.identity);
        base.SetAnimation(name, frame);
    }

    protected override void CheckAnimationBoxes()
    {
        base.CheckAnimationBoxes();
    }

    public void SetInvinsible(bool state)
    {
        _spriteRenderer.enabled = !state;
        _shadow.SetInvinsible(state);
    }

    public bool InRecovery(string name, int frame)
    {
        _group = _animation.GetGroupId(name);
        _cel = GetCellByFrame(frame);
        for (int i = 0; i < _animation.animationCelsGroup[_group].animationCel.Count; i++)
            if (i < _cel)
                if (_animation.animationCelsGroup[_group].animationCel[i].hitboxes.Count > 0
                 || _animation.GetCel(_group, i).animationEvent.projectile
                 || _animation.GetCel(_group, i).animationEvent.parry)
                    return true;
        return false;
    }

    public FramedataTypesEnum GetFramedata(string name, int frame)
    {
        _group = _animation.GetGroupId(name);
        _cel = GetCellByFrame(frame);
        if (_animation.animationCelsGroup[_group].animationCel[_cel].hitboxes?.Count > 0 || GetProjectile(name, frame))
            return FramedataTypesEnum.Active;
        else if (GetParrying(name, frame))
            return FramedataTypesEnum.Parry;
        else
        {
            if (InRecovery(name, frame))
                return FramedataTypesEnum.Recovery;
            else
                return FramedataTypesEnum.StartUp;
        }
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
