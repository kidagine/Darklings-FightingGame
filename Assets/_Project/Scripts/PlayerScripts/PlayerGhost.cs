using System.Collections;
using UnityEngine;

public class PlayerGhost : MonoBehaviour
{
    [SerializeField] private int _ghostFrameMax = default;
    private SpriteRenderer _spriteRenderer;
    private int _ghostFrame;
    private float _opacity;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        _ghostFrame = 0;
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1);
    }

    void FixedUpdate()
    {
        if (_ghostFrame < _ghostFrameMax)
        {
            _opacity = Mathf.Lerp(1, 0, ((float)_ghostFrame / (float)_ghostFrameMax));
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _opacity);
            _ghostFrame++;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
