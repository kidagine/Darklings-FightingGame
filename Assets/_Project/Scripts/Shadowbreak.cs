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
    private bool valueInRange(DemonicsFloat value, DemonicsFloat min, DemonicsFloat max)
    { return (value >= min) && (value <= max); }

    protected override bool Colliding(DemonicsCollider a, DemonicsCollider b)
    {
        bool xOverlap = valueInRange(a.Position.x - (a.Size.x), b.Position.x - (b.Size.x / (DemonicsFloat)2), b.Position.x + (b.Size.x / (DemonicsFloat)2)) ||
                    valueInRange(b.Position.x - (b.Size.x / (DemonicsFloat)2), a.Position.x - (a.Size.x), a.Position.x + (a.Size.x));
        bool yOverlap = valueInRange(a.Position.y - (a.Size.y / (DemonicsFloat)2), b.Position.y - (b.Size.y / (DemonicsFloat)2), b.Position.y + (b.Size.y / (DemonicsFloat)2)) ||
                    valueInRange(b.Position.y - (b.Size.y / (DemonicsFloat)2), a.Position.y - (a.Size.y / (DemonicsFloat)2), a.Position.y + (a.Size.y / (DemonicsFloat)2));
        return xOverlap && yOverlap;
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
