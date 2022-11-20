using UnityEngine;

public class Shadowbreak : Hitbox
{
    [SerializeField] private DemonicsPhysics _physics = default;
    [SerializeField] DemonicsAnimator _animator = default;


    protected override void EnterCollision(DemonicsCollider collider)
    {
        if (!HitConfirm)
        {
            if (collider.transform.root.TryGetComponent(out Player player))
            {
                player.PlayerStateManager.TryToKnockbackState();
                HitConfirm = true;
            }
        }
    }
    public override void SetSourceTransform(Transform sourceTransform)
    {
        base.SetSourceTransform(sourceTransform);
        base.InitializeCollisionList();

    }

    public void SetPosition(DemonicsVector2 position)
    {
        _physics.SetPositionWithRender(position);
    }
    void OnEnable()
    {
        _animator.SetAnimation("Idle");
        _animator.OnCurrentAnimationFinished.AddListener(() => gameObject.SetActive(false));
    }
}
