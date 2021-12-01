using Demonics.Utility;
using UnityEngine;

public class PlayerGhost : MonoBehaviour
{
	[SerializeField] private float _ghostTime = default;
	private SpriteRenderer _spriteRenderer = default;


	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	async void OnEnable()
	{
		await UpdateTimer.WaitFor(_ghostTime);
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
	}

	public void SetSprite(Sprite sprite, float flipSpriteValue, Color color)
	{
		_spriteRenderer.color = color;
		_spriteRenderer.sprite = sprite;
		transform.localScale = new Vector2(flipSpriteValue, 1.0f);
	}
}
