using UnityEngine;

public class PlayerCharacterSelector : MonoBehaviour
{
    private RectTransform _rectTransform;


	private void Start()
	{
        _rectTransform = GetComponent<RectTransform>();	
	}

	void Update()
    {
        RaycastHit2D raycastHitUp = Raycast.Cast(_rectTransform.anchoredPosition, Vector2.up, 2.0f, LayerMaskEnum.UI, Color.blue);
        RaycastHit2D raycastHitDown = Raycast.Cast(_rectTransform.anchoredPosition, Vector2.down, 2.0f, LayerMaskEnum.UI, Color.blue);
        RaycastHit2D raycastHitRight = Raycast.Cast(_rectTransform.anchoredPosition, Vector2.right, 2.0f, LayerMaskEnum.UI, Color.blue);
        RaycastHit2D raycastHitLeft = Raycast.Cast(_rectTransform.anchoredPosition, Vector2.left, 2.0f, LayerMaskEnum.UI, Color.blue);
        if (raycastHitUp.collider != null)
        {
            Debug.Log("up");
        }
        if (raycastHitDown.collider != null)
        {
            Debug.Log("down");
        }
        if (raycastHitRight.collider != null)
        {
            Debug.Log("right");
        }
        if (raycastHitLeft.collider != null)
        {
            Debug.Log("left");
        }
    }
}
