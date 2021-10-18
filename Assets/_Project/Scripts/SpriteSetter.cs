using UnityEngine;
using UnityEngine.U2D.Animation;

[ExecuteInEditMode]
public class SpriteSetter : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField] private SpriteLibraryAsset _spriteLibraryAsset = default;
    [SerializeField] private int _category = default;
    [SerializeField] private int _label = default;
    private SpriteRenderer _spriteRenderer;


	void Awake()
	{
        _spriteRenderer = GetComponent<SpriteRenderer>();
	}

    void LateUpdate()
    {
		//string category = _spriteLibraryAsset.GetCategoryNames();
  //      Sprite sprite = _spriteLibraryAsset.GetSprite(_category, $"Entry_{_label}");
  //      Debug.Log(sprite.name);
  //     _spriteRenderer.sprite = sprite;
    }

	public void OnBeforeSerialize()
	{
		throw new System.NotImplementedException();
	}

	public void OnAfterDeserialize()
	{
		throw new System.NotImplementedException();
	}
}
