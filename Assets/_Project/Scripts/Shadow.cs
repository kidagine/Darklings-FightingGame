using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private GameObject _playerShadowPrefab = default;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _shadowSpriteRenderer;
    private Transform _shadow;
    private bool _activated;


    void OnEnable()
    {
        if (!_activated)
        {
            _activated = true;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _shadow = Instantiate(_playerShadowPrefab).transform.GetChild(0);
            _shadowSpriteRenderer = _shadow.GetComponent<SpriteRenderer>();
            _shadow.localRotation = Quaternion.Euler(-45.0f, 0.0f, -180.0f - transform.root.eulerAngles.z);
        }
    }

    void LateUpdate()
    {
        _shadow.localPosition = new Vector2(transform.position.x, transform.position.y * -1);
        _shadow.localScale = new Vector2(transform.root.localScale.x * -1, 1.0f);
        _shadowSpriteRenderer.sprite = _spriteRenderer.sprite;
    }

    public void SetInvinsible(bool state)
    {
        _shadowSpriteRenderer.enabled = !state;
    }

    private void OnDestroy()
    {
        if (_shadowSpriteRenderer != null)
        {
            Destroy(_shadow.root.gameObject);
        }
    }

    private void OnDisable()
    {
        if (_shadowSpriteRenderer != null)
        {
            _shadow.root.gameObject.SetActive(false);
        }
    }
}
