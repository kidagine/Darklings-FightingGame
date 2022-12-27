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
    }

    public void SetAnimator(AnimationSO animation)
    {
        _animation = animation;
        SetAnimation("Idle");
    }

    public int GetMaxAnimationFrames(string name = "Idle")
    {
        int maxFrames = 0;
        _group = _animation.GetGroupId(name);
        for (int i = 0; i < _animation.GetGroup(_group).animationCel.Count; i++)
        {
            maxFrames += _animation.GetGroup(_group).animationCel[i].frames;
        }
        return maxFrames;
    }
    public static int GetMaxAnimationFrames(AnimationSO animation, string name = "Idle")
    {
        int maxFrames = 0;
        int group = animation.GetGroupId(name);
        for (int i = 0; i < animation.GetGroup(group).animationCel.Count; i++)
        {
            maxFrames += animation.GetGroup(group).animationCel[i].frames;
        }
        return maxFrames;
    }

    public virtual void SetAnimation(string name)
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

    public bool IsAnimationFinished()
    {
        return _finished;
    }
    private bool _finished;
    public void SetAnimation(string name, int frame)
    {
        _group = _animation.GetGroupId(name);
        for (int i = 0; i < _animation.GetGroup(_group).animationCel.Count; i++)
        {
            if (frame > 0)
            {
                frame -= _animation.GetGroup(_group).animationCel[i].frames;
                if (frame < 0)
                {
                    _finished = false;
                    _frame = Mathf.Abs(frame);
                    _cel = i;
                    break;
                }
                if (frame > 0 && _animation.GetGroup(_group).loop)
                {
                    _finished = true;
                    _frame = 0;
                    _cel = 0;
                }
            }
            else
            {
                _finished = false;
                _frame = Mathf.Abs(frame);
                _cel = i;
                break;
            }
        }
        CheckAnimationBoxes();
        CheckEvents();
        _spriteRenderer.sprite = _animation.GetSprite(_skin, _group, _cel);
    }

    public void PlayAnimation()
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

    public AnimationBox[] GetHurtboxes()
    {
        return _animation.GetCel(_group, _cel).hurtboxes.ToArray();
    }

    public AnimationBox[] GetHitboxes()
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
