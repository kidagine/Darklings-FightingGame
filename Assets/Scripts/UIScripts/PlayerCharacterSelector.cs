using System.Collections;
using UnityEngine;

public class PlayerCharacterSelector : MonoBehaviour
{
    [SerializeField] private CharacterMenu _characterMenu = default;
    [SerializeField] private bool _isPlayerOne = default;
    private readonly float _moveDistance = 180.0f;
    private RectTransform _rectTransform;
    private Vector2 _directionInput;
    private Audio _audio;
    private string _controllerInputName;
    private bool _canGoRight;
    private bool _canGoLeft;
    private bool _canGoUp;
    private bool _canGoDown;
    private bool _inputDeactivated;


    public bool HasSelected { get; set; }


    void Awake()
    {
        _audio = GetComponent<Audio>();
    }

    private void OnEnable()
	{
        _rectTransform = GetComponent<RectTransform>();
        if (_isPlayerOne)
        {
            if (SceneSettings.ControllerOne == "")
            {
                _controllerInputName = "Keyboard";
            }
            else
            {
                _controllerInputName = SceneSettings.ControllerOne;
            }
        }
        else
        {
            if (SceneSettings.ControllerTwo == "")
            {
                if (SceneSettings.ControllerOne == "")
                {
                    _controllerInputName = "Keyboard";
                }
                else
                {
                    _controllerInputName = SceneSettings.ControllerOne;
                }
            }
            else
            {
                _controllerInputName = SceneSettings.ControllerTwo;
            }
        }
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
                        _rectTransform.anchoredPosition += new Vector2(_moveDistance, 0.0f);
                        StartCoroutine(ResetInput());
                    }
                }
                if (_directionInput.x == -1.0f)
                {
                    if (_canGoLeft)
                    {
                        _rectTransform.anchoredPosition += new Vector2(-_moveDistance, 0.0f);
                        StartCoroutine(ResetInput());
                    }
                }
                if (_directionInput.y == 1.0f)
                {
                    if (_canGoUp)
                    {
                        _rectTransform.anchoredPosition += new Vector2(0.0f, _moveDistance);
                        StartCoroutine(ResetInput());
                    }
                }
                if (_directionInput.y == -1.0f)
                {
                    if (_canGoDown)
                    {
                        _rectTransform.anchoredPosition += new Vector2(0.0f, -_moveDistance);
                        StartCoroutine(ResetInput());
                    }
                }
            }
            if (Input.GetButtonDown(_controllerInputName + "Confirm"))
            {
                Debug.Log(_controllerInputName);
                _audio.Sound("Pressed").Play();
                HasSelected = true;
                _characterMenu.SelectCharacterOneImage(_isPlayerOne);
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
        if (currentPosition == new Vector2(collision.transform.localPosition.x, collision.transform.localPosition.y))
        {
            _audio.Sound("Selected").Play();
            RuntimeAnimatorController animatorController = collision.GetComponent<CharacterButton>().CharacterAnimatorController;
            string characterName = collision.GetComponent<CharacterButton>().CharacterName;
            _characterMenu.SetCharacterOneImage(_isPlayerOne, animatorController, characterName);
        }
        if (collision.transform.localPosition.x > currentPosition.x)
        {
            _canGoRight = true;
        }
        if (collision.transform.localPosition.x < currentPosition.x)
        {
            _canGoLeft = true;
        }
        if (collision.transform.localPosition.y > currentPosition.y)
        {
            _canGoUp = true;
        }
        if (collision.transform.localPosition.y > currentPosition.y)
        {
            _canGoDown = true;
        }
    }
}
