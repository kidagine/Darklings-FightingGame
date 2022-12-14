using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Audio))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]
public class BaseButton : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerClickHandler
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
    private bool _isIgnoringFirstSelectSound;
    private bool _wasClicked;


    void Awake()
    {
        _audio = GetComponent<Audio>();
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        _onSelected?.Invoke();
        if (!_isIgnoringFirstSelectSound)
        {
            _audio.Sound("Selected").Play();
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
        _button.Select();
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
}
