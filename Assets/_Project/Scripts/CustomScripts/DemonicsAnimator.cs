using UnityEngine;
using UnityEngine.Events;

public class DemonicsAnimator : MonoBehaviour
{
    [SerializeField] protected AnimationSO _animation = default;
    [SerializeField] protected SpriteRenderer _spriteRenderer = default;
    private int _frame;
    protected int _cel;
    protected int _group;
    protected int _skin;
    protected bool _isPaused;
    protected bool _frozen;
    [HideInInspector] public UnityEvent OnCurrentAnimationFinished;


    protected virtual void FixedUpdate()
    {
        PlayAnimation();
    }

    public void SetAnimator(AnimationSO animation)
    {
        _animation = animation;
        SetAnimation("Idle");
    }

    public void SetAnimation(string name)
    {
        if (_animation != null && _animation.GetGroup(_group).celName != name)
        {
            _frame = 0;
            _cel = 0;
            _group = _animation.GetGroupId(name);
            _isPaused = false;
            CheckAnimationBoxes();
            CheckEvents();
            _spriteRenderer.sprite = _animation.GetSprite(_skin, _group, _cel);
        }
    }
    public void SetAnimation(string name, int frame)
    {
        if (_animation != null)
        {
            _frame = 0;
            _cel = frame;
            _group = _animation.GetGroupId(name);
            _isPaused = false;
            CheckAnimationBoxes();
            CheckEvents();
            if (_cel < 3)
            {
                _spriteRenderer.sprite = _animation.GetSprite(_skin, _group, _cel);
            }
        }
    }

    private void PlayAnimation()
    {
        if (!_isPaused && !_frozen)
        {
            if (_frame == _animation.GetCel(_group, _cel).frames)
            {
                _cel++;
                if (_cel > _animation.GetGroup(_group).animationCel.Count - 1)
                {
                    AnimationEnded();
                    if (!_animation.GetGroup(_group).loop)
                    {
                        return;
                    }
                }
                CheckEvents();
                CheckAnimationBoxes();
                _frame = 0;
            }
            CheckGrab();
            _spriteRenderer.sprite = _animation.GetSprite(_skin, _group, _cel);
            _frame++;
        }
    }
    protected virtual void CheckGrab() { }
    protected virtual void CheckEvents() { }
    protected virtual void CheckAnimationBoxes() { }

    protected virtual void AnimationEnded()
    {
        if (!_animation.GetGroup(_group).loop)
        {
            _isPaused = true;
        }
        _frame = 0;
        _cel = 0;
        OnCurrentAnimationFinished?.Invoke();
        OnCurrentAnimationFinished.RemoveAllListeners();
    }

    protected AnimationBox[] GetHurtboxes()
    {
        return _animation.GetCel(_group, _cel).hurtboxes.ToArray();
    }

    protected AnimationBox[] GetHitboxes()
    {
        return _animation.GetCel(_group, _cel).hitboxes.ToArray();
    }

    protected AnimationEvent GetEvent()
    {
        return _animation.GetCel(_group, _cel).animationEvent;
    }

    public void Pause()
    {
        _frozen = true;
    }

    public void Resume()
    {
        _frozen = false;
    }
}
