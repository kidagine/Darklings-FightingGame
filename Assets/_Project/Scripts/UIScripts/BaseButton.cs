using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Audio))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]
public class BaseButton : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public UnityEvent _onClickedAnimationEnd = default;
    [SerializeField] public UnityEvent _onSelected = default;
    [SerializeField] public RectTransform _scrollView = default;
    [SerializeField] public float _scrollUpAmount = default;
    [SerializeField] public float _scrollDownAmount = default;
    [SerializeField] private bool _ignoreFirstSelectSound = default;
    [SerializeField] private bool _allowMultiplePresses = default;
    protected Audio _audio;
    protected Button _button;
    protected Animator _animator;
    private RectTransform _rect;
    private bool _isIgnoringFirstSelectSound;
    private bool _wasClicked;
    private Coroutine _moveVerticalCoroutine;
    private Vector2 _defaultPosition;
    private readonly int _moveVerticalAmount = 7;


    void Awake()
    {
        _audio = GetComponent<Audio>();
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
        _rect = GetComponent<RectTransform>();
        _defaultPosition = _rect.anchoredPosition;
    }

    public void OnSelect(BaseEventData eventData)
    {
        _onSelected?.Invoke();
        if (!_isIgnoringFirstSelectSound)
        {
            // _audio.Sound("Selected").Play();
        }
        _animator.SetBool("IsSelected", true);
        _isIgnoringFirstSelectSound = false;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _animator.SetBool("IsSelected", false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _audio.Sound("Selected").Play();
        Cursor.SetCursor(MouseSetup.Instance.HoverCursor, Vector2.zero, CursorMode.Auto);
        _button.Select();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_wasClicked)
        {
            _wasClicked = true;
            _audio.Sound("Pressed").Play();
            _animator.SetTrigger("Pressed");
        }
    }

    public virtual void OnPress()
    {
        if (!_wasClicked)
        {
            EventSystem.current.sendNavigationEvents = false;
            if (!_allowMultiplePresses)
            {
                _wasClicked = true;
            }
            _audio.Sound("Pressed").Play();
            _animator.SetTrigger("Pressed");
        }
    }

    public void OnClickedEndAnimationEvent()
    {
        _animator.Rebind();
        if (_allowMultiplePresses)
        {
            _animator.SetBool("IsSelected", true);
        }
        EventSystem.current.sendNavigationEvents = true;
        _onClickedAnimationEnd?.Invoke();
    }

    public void MoveScrollDown(BaseEventData eventData)
    {
        AxisEventData axisEventData = eventData as AxisEventData;
        if (axisEventData.moveDir == MoveDirection.Down)
        {
            _scrollView.anchoredPosition += new Vector2(0.0f, _scrollDownAmount);
        }
    }

    public void MoveScrollUp(BaseEventData eventData)
    {
        AxisEventData axisEventData = eventData as AxisEventData;
        if (axisEventData.moveDir == MoveDirection.Up)
        {
            _scrollView.anchoredPosition += new Vector2(0.0f, _scrollUpAmount);
        }
    }

    public void Activate()
    {
        _button.enabled = true;
        _animator.SetBool("IsDeactivated", false);
    }

    public void Deactivate()
    {
        _button.enabled = false;
        _animator.SetBool("IsDeactivated", true);
    }

    void OnEnable()
    {
        _isIgnoringFirstSelectSound = _ignoreFirstSelectSound;
    }

    void OnDisable()
    {
        _wasClicked = false;
        if (_ignoreFirstSelectSound)
        {
            _isIgnoringFirstSelectSound = true;
        }
        //EventSystem.current.SetSelectedGameObject(null);
        _animator.Rebind();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_moveVerticalCoroutine != null)
            StopCoroutine(_moveVerticalCoroutine);
        _rect.anchoredPosition = _defaultPosition;
        _moveVerticalCoroutine = StartCoroutine(MoveVerticalCoroutine(-_moveVerticalAmount));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_moveVerticalCoroutine != null)
            StopCoroutine(_moveVerticalCoroutine);
        _rect.anchoredPosition = _defaultPosition + (-_moveVerticalAmount * Vector2.up);
        _moveVerticalCoroutine = StartCoroutine(MoveVerticalCoroutine(_moveVerticalAmount));
    }

    IEnumerator MoveVerticalCoroutine(int moveY)
    {
        float elapsedTime = 0f;
        float waitTime = 0.08f;
        Vector2 currentPosition = _rect.anchoredPosition;
        Vector2 endPosition = _rect.anchoredPosition + (moveY * Vector2.up);
        while (elapsedTime < waitTime)
        {
            _rect.anchoredPosition = Vector2.Lerp(currentPosition, endPosition, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _rect.anchoredPosition = endPosition;
        yield return null;
    }
}
