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
        base.FixedUpdate();
        _image.sprite = _spriteRenderer.sprite;
    }

    public void Taunt()
    {
        SetAnimation("Taunt");
    }

    public void SetAnimationController()
    {
    }

    public int SetSpriteLibraryAsset(int skinNumber)
    {
        if (skinNumber > PlayerStats.spriteLibraryAssets.Length - 1)
        {
            skinNumber = 0;
        }
        else if (skinNumber < 0)
        {
            skinNumber = PlayerStats.spriteLibraryAssets.Length - 1;
        }
        return skinNumber;
    }

    void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
