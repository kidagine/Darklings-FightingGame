using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Audio))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]
public class BaseButton : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public UnityEvent _onClickedAnimationEnd = default;
    [SerializeField] public UnityEvent _onSelected = default;
    [SerializeField] public UnityEvent _onDeselected = default;
    [SerializeField] public RectTransform _scrollView = default;
    [SerializeField] public float _scrollUpAmount = default;
    [SerializeField] public float _scrollDownAmount = default;
    [SerializeField] private bool _selectOnHover = default;
    [SerializeField] private bool _clickAnimation = true;
    [SerializeField] private bool _ignoreFirstSelectSound = default;
    [SerializeField] private bool _allowMultiplePresses = default;
    [SerializeField] private Selectable selectableParent = default;
    [SerializeField] protected bool _resetToDefault = false;
    protected Audio _audio;
    protected Button _button;
    protected Animator _animator;
    protected RectTransform _rect;
    private bool _isIgnoringFirstSelectSound;
    private bool _wasClicked;
    private Coroutine _moveVerticalCoroutine;
    protected Vector2 _defaultPosition;
    private readonly int _moveVerticalAmount = 5;
    protected bool _isPressed;


    protected virtual void Awake()
    {
#if UNITY_EDITOR
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
#endif
        _audio = GetComponent<Audio>();
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
        _rect = GetComponent<RectTransform>();
        _defaultPosition = _rect.anchoredPosition;
    }

    public virtual void OnSelect(BaseEventData eventData)
    {
        _onSelected?.Invoke();
        if (!_isIgnoringFirstSelectSound)
        {
        }
        _animator.SetBool("IsHover", true);
        _isIgnoringFirstSelectSound = false;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (_selectOnHover)
            _button.Select();
        _animator.SetBool("IsHover", true);
        _audio.Sound("Selected").Play();
        Cursor.SetCursor(MouseSetup.Instance.HoverCursor, Vector2.zero, CursorMode.Auto);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (!_selectOnHover)
            _animator.SetBool("IsHover", false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void SelectParent() => selectableParent?.Select();

    public virtual void OnPress()
    {
        if (!_wasClicked)
        {
            _animator.SetBool("IsPress", true);
            EventSystem.current.sendNavigationEvents = false;
            if (!_allowMultiplePresses)
                _wasClicked = true;
        }
    }

    public void OnClickedEndAnimationEvent()
    {
        _animator.Rebind();
        if (_allowMultiplePresses)
            _animator.SetBool("IsSelected", true);
        EventSystem.current.sendNavigationEvents = true;
        if (_onClickedAnimationEnd != null)
            _onClickedAnimationEnd?.Invoke();
    }

    public void MoveScrollDown(BaseEventData eventData)
    {
        AxisEventData axisEventData = eventData as AxisEventData;
        if (axisEventData.moveDir == MoveDirection.Down)
            _scrollView.anchoredPosition += new Vector2(0.0f, _scrollDownAmount);
    }

    public void MoveScrollUp(BaseEventData eventData)
    {
        AxisEventData axisEventData = eventData as AxisEventData;
        if (axisEventData.moveDir == MoveDirection.Up)
            _scrollView.anchoredPosition += new Vector2(0.0f, _scrollUpAmount);
    }

    public virtual void Activate()
    {
        _button.enabled = true;
        gameObject.SetActive(true);
        _animator.SetBool("IsDeactivated", false);
    }

    public virtual void Deactivate()
    {
        _button.enabled = false;
        gameObject.SetActive(false);
        _animator.SetBool("IsDeactivated", true);
    }

    void OnEnable()
    {
        _isIgnoringFirstSelectSound = _ignoreFirstSelectSound;
    }

    void OnDisable()
    {
        if (_moveVerticalCoroutine != null)
        {
            StopCoroutine(_moveVerticalCoroutine);
            _rect.anchoredPosition = _defaultPosition;
        }
        _isPressed = false;
        _wasClicked = false;
        if (_ignoreFirstSelectSound)
            _isIgnoringFirstSelectSound = true;
        _animator.Rebind();
        _animator.SetBool("IsHover", false);
        _animator.SetBool("IsPress", false);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (_isPressed)
            return;
        _audio.Sound("Pressed").Play();
        _animator.SetBool("IsPress", true);
        if (!_clickAnimation)
            return;
        if (_moveVerticalCoroutine != null)
            StopCoroutine(_moveVerticalCoroutine);
        _rect.anchoredPosition = _defaultPosition;
        _moveVerticalCoroutine = StartCoroutine(MoveVerticalCoroutine(-_moveVerticalAmount));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isPressed)
            return;
        if (_resetToDefault)
        {
            _isPressed = false;
            _animator.SetBool("IsPress", false);
        }
        if (!_clickAnimation)
            return;
        if (_moveVerticalCoroutine != null)
            StopCoroutine(_moveVerticalCoroutine);
        _rect.anchoredPosition = _defaultPosition + (-_moveVerticalAmount * Vector2.up);
        _moveVerticalCoroutine = StartCoroutine(MoveVerticalCoroutine(_moveVerticalAmount));
    }

    IEnumerator MoveVerticalCoroutine(int moveY)
    {
        float elapsedTime = 0f;
        float waitTime = 0.05f;
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

    public virtual void OnDeselect(BaseEventData eventData)
    {
        _onDeselected?.Invoke();
        _animator.SetBool("IsHover", false);
    }
}
