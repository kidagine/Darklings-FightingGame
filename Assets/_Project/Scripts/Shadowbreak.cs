using UnityEngine;

public class Shadowbreak : Hitbox
{
    [SerializeField] DemonicsAnimator _animator = default;

    protected override void Start()
    {
        base.Start();
        _animator.OnCurrentAnimationFinished.AddListener(() => gameObject.SetActive(false));
    }

    protected override void EnterCollision(DemonicsCollider collider)
    {
        if (collider.transform.root.TryGetComponent(out Player player))
        {
            player.PlayerStateManager.TryToKnockbackState();
        }
    }
}
