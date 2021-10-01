using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer _spriteRenderer;


    void Awake()
    {
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetMove(bool state)
    {
        animator.SetBool("IsMoving", state);
    }

    public void SetMovement(float value)
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
    public void IsDashing(bool state)
    {
        animator.SetBool("IsDashing", state);
    }

    public void IsRunning(bool state)
    {
        animator.SetBool("IsRunning", state);
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
}
