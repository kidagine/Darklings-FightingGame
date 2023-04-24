using System.Collections;
using Demonics;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAnimator : DemonicsAnimator
{
    [SerializeField] private Player _player = default;
    private Shadow _shadow;
    private int _celPrevious = -1;


    void Awake()
    {
        _shadow = GetComponent<Shadow>();
    }

    void Start()
    {
        _animation = _player.playerStats._animation;
    }

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
    public DemonicsVector2 GetJump(string name, int frame)
    {
        DemonicsVector2 jumpDirection = DemonicsVector2.Zero;
        if (GetEvent(name, frame, out _).jump)
        {
            jumpDirection = new DemonicsVector2((DemonicsFloat)GetEvent(name, frame, out _).jumpDirection.x * transform.root.localScale.x, (DemonicsFloat)GetEvent(name, frame, out _).jumpDirection.y);
        }
        return jumpDirection;
    }
    public DemonicsVector2 GetGrabPoint(string name, int frame)
    {
        Vector2 grabPoint = GetEvent(name, frame, out _).grabPoint;
        return new DemonicsVector2((DemonicsFloat)grabPoint.x, (DemonicsFloat)grabPoint.y);
    }

    public override void SetAnimation(string name, int frame)
    {
        if (name == "Wallsplat")
        {
            transform.localPosition = new Vector2(10 * -transform.localScale.x, 0);
            transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            transform.localPosition = Vector2.zero;
            transform.localRotation = Quaternion.identity;
        }
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
