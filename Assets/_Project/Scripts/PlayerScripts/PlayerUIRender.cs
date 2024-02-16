using UnityEngine;
using UnityEngine.UI;

public class PlayerUIRender : DemonicsAnimator
{
    private Image _image;

    public PlayerStatsSO PlayerStats { get; set; }

    void Awake()
    {
        _image = GetComponent<Image>();
    }

    protected override void FixedUpdate()
    {
        PlayAnimation();
        _image.sprite = _spriteRenderer.sprite;
    }

    public void SetPlayerStat(PlayerStatsSO playerStats)
    {
        _cel = 0;
        PlayerStats = playerStats;
    }

    public void Taunt()
    {
        SetAnimation("Taunt");
    }

    public int SetSpriteLibraryAsset(int skinNumber)
    {
        if (skinNumber > _animation.spriteAtlas.Length - 1)
        {
            skinNumber = 0;
        }
        else if (skinNumber < 0)
        {
            skinNumber = _animation.spriteAtlas.Length - 1;
        }
        _skin = skinNumber;
        return skinNumber;
    }

    // void OnDisable()
    // {
    //     gameObject.SetActive(false);
    // }
}
