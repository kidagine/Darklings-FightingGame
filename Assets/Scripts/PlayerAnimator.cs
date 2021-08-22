using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    public void SetMove(bool state)
    {
        animator.SetBool("IsMoving", state);
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

    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }

    public void Death()
    {
        animator.SetTrigger("Death");
    }

    public void Rebind()
    {
        animator.Rebind();
    }
}
