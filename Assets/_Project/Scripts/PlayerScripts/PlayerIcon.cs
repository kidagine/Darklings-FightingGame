using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    private RectTransform _rectTransform;
    private float _originalPositionY;

	private void Awake()
	{
        _rectTransform = GetComponent<RectTransform>();
        _originalPositionY = _rectTransform.anchoredPosition.y;
    }

	void Update()
    {
        if (_rectTransform.anchoredPosition.x == -375.0f)
        {
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, 250.0f);
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (_rectTransform.anchoredPosition.x == 375.0f)
        {
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, 250.0f);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _originalPositionY);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
