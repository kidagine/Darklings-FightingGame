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

    // protected override void InitializeCollisionList()
    // {
    //     _demonicsColliders.Clear();
    //     DemonicsCollider[] demonicsCollidersArray = FindObjectsOfType<DemonicsCollider>();
    //     for (int i = 0; i < demonicsCollidersArray.Length; i++)
    //     {
    //         if (!demonicsCollidersArray[i].transform.IsChildOf(SourceTransform))
    //         {
    //             if (demonicsCollidersArray[i].TryGetComponent(out HitboxProjectile hitboxProjectile))
    //             {
    //                 if (hitboxProjectile.SourceTransform != SourceTransform)
    //                 {
    //                     _demonicsColliders.Add(demonicsCollidersArray[i]);
    //                 }
    //             }
    //             if (demonicsCollidersArray[i].TryGetComponent(out Hurtbox hurtbox))
    //             {
    //                 _demonicsColliders.Add(demonicsCollidersArray[i]);
    //             }
    //         }
    //     }
    //     _demonicsColliders.Remove(this);
    // }
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
