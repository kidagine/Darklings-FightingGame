using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private SpriteLibraryAsset[] _spriteLibraryAssets = default;
    private Animator animator;
    private SpriteLibrary _spriteLibrary;
    private SpriteRenderer _spriteRenderer;
    private int _currentLibraryAsset;

    void Awake()
    {
        animator = GetComponent<Animator>();
        _spriteLibrary = GetComponent<SpriteLibrary>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetMove(bool state)
    {
        animator.SetBool("IsMoving", state);
    }

    public void SetMovementX(float value)
    {
        animator.SetFloat("MovementInputX", value);
    }

    public void IsCrouching(bool state)
    {
        animator.SetBool("IsCrouching", state);
    }

    public void IsJumping(bool state)
    {
        animator.SetBool("IsJumping", state);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
    public void Arcana()
    {
        animator.SetTrigger("Arcana");
    }

    public void IsHurt(bool state)
    {
        animator.SetBool("IsHurt", state);
    }

    public void IsBlocking(bool state)
    {
        animator.SetBool("IsBlocking", state);
    }

    public void IsBlockingLow(bool state)
    {
        animator.SetBool("IsBlockingLow", state);
    }
    public void IsBlockingAir(bool state)
    {
        animator.SetBool("IsBlockingAir", state);
    }

    public void IsDashing(bool state)
    {
        animator.SetBool("IsDashing", state);
    }

    public void IsRunning(bool state)
    {
        animator.SetBool("IsRunning", state);
    }

    public void Taunt()
    {
        animator.SetTrigger("Taunt");
    }

    public void Death()
    {
        animator.SetTrigger("Death");
    }

    public void Rebind()
    {
        animator.Rebind();
    }

    public Sprite GetCurrentSprite()
    {
        return _spriteRenderer.sprite;
    }

    public int SetSpriteLibraryAsset(int skinNumber)
    {
        if (skinNumber > _spriteLibraryAssets.Length - 1)
        {
            skinNumber = 0;
        }
        else if (skinNumber < 0)
        {
            skinNumber = _spriteLibraryAssets.Length - 1;
        }
        _spriteLibrary.spriteLibraryAsset = _spriteLibraryAssets[skinNumber];
        return skinNumber;
    }
}
