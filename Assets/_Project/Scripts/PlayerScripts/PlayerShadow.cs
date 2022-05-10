using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
	private SpriteRenderer _playerSpriteRenderer;
	private SpriteRenderer _shadowSpriteRenderer;
	private Transform _target;


	void Start()
	{
		_shadowSpriteRenderer = GetComponent<SpriteRenderer>();
		if (SceneSettings.Bit1)
		{
			_shadowSpriteRenderer.color = Color.black;
		}
	}

	public void Initialize(Transform target, SpriteRenderer spriteRenderer)
	{
		_playerSpriteRenderer = spriteRenderer;
		_target = target;
	}

	void LateUpdate()
	{
		transform.localPosition = new Vector2(_target.position.x, _target.position.y * -1);
		transform.localScale = new Vector2(_target.localScale.x * -1, 1.0f);
		_shadowSpriteRenderer.sprite = _playerSpriteRenderer.sprite;
	}
}
