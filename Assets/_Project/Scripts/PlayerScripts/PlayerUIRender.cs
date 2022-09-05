using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class PlayerUIRender : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _spriteRenderer = default;
	[SerializeField] private SpriteLibrary _spriteLibrary = default;
	private Image _image;

	public PlayerStatsSO PlayerStats { get; set; }

	private void Awake()
	{
		_image = GetComponent<Image>();
	}

	void Update()
	{
		_image.sprite = _spriteRenderer.sprite;
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
		if (_spriteLibrary != null)
		{
			_spriteLibrary.spriteLibraryAsset = PlayerStats.spriteLibraryAssets[skinNumber];
		}
		return skinNumber;
	}
}
