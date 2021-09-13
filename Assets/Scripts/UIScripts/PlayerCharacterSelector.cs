using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerCharacterSelector : MonoBehaviour
{
    [SerializeField] private Button _currentButton;
    private RectTransform _rectTransform;
    private Vector2 _directionInput;
    private string _controllerInputName;
    private float _moveDistance = 180.0f;
    private bool _canGoRight;
    private bool _canGoLeft;
    private bool _canGoUp;
    private bool _canGoDown;
    private bool _inputDeactivated;

	public bool HasSelected { get; private set; }

	private void Start()
	{
        _currentButton.Select();
        _rectTransform = GetComponent<RectTransform>();
        _controllerInputName = "Keyboard";
    }

	void Update()
    {
        if (!HasSelected)
        {
            if (!_inputDeactivated)
            {
                _directionInput = new Vector2(Input.GetAxisRaw(_controllerInputName + "Horizontal"), Input.GetAxisRaw(_controllerInputName + "Vertical"));
                if (_directionInput.x == 1.0f)
                {
                    if (_canGoRight)
                    {
                        _currentButton.Select();
                        _rectTransform.anchoredPosition += new Vector2(_moveDistance, 0.0f);
                        StartCoroutine(ResetInput());
                    }
                }
                if (_directionInput.x == -1.0f)
                {
                    if (_canGoLeft)
                    {
                        _currentButton.Select();
                        _rectTransform.anchoredPosition += new Vector2(-_moveDistance, 0.0f);
                        StartCoroutine(ResetInput());
                    }
                }
                if (_directionInput.y == 1.0f)
                {
                    if (_canGoUp)
                    {
                        _currentButton.Select();
                        _rectTransform.anchoredPosition += new Vector2(0.0f, _moveDistance);
                        StartCoroutine(ResetInput());
                    }
                }
                if (_directionInput.y == -1.0f)
                {
                    if (_canGoDown)
                    {
                        _currentButton.Select();
                        _rectTransform.anchoredPosition += new Vector2(0.0f, -_moveDistance);
                        StartCoroutine(ResetInput());
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                HasSelected = true;
                _currentButton.onClick.Invoke();
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

	IEnumerator ResetInput()
	{
        _inputDeactivated = true;
        _canGoRight = false;
        _canGoLeft = false;
        _canGoUp = false;
        _canGoDown = false; 
        _directionInput = Vector2.zero;
        yield return new WaitForSeconds(0.15f);
        _inputDeactivated = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
        Vector2 currentPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 190.0f);
        if (collision.transform.localPosition.x > currentPosition.x)
        {
            _canGoRight = true;
            _currentButton = collision.GetComponent<Button>();
        }
        if (collision.transform.localPosition.x < currentPosition.x)
        {
            _canGoLeft = true;
            _currentButton = collision.GetComponent<Button>();
        }
        if (collision.transform.localPosition.y > currentPosition.y)
        {
            _canGoUp = true;
            _currentButton = collision.GetComponent<Button>();
            Debug.Log(collision.name + " | " + transform.localPosition + " | " + collision.transform.localPosition);
        }
        if (collision.transform.localPosition.y > currentPosition.y)
        {
            _canGoDown = true;
            _currentButton = collision.GetComponent<Button>();
        }
    }
}
