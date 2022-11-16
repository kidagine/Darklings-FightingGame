using UnityEngine;

public class HitboxProjectile : Hitbox
{
    [SerializeField] private Projectile _projectile = default;


    void Awake()
    {
        GizmoColor = Color.red;
        _sourceTransform = _projectile.SourceTransform;
    }

    protected override void EnterCollision(DemonicsCollider collider)
    {
        GameManager.Instance.AddHitstop(_projectile);
        base.EnterCollision(collider);
    }

    public override void SetSourceTransform(Transform sourceTransform)
    {
        _sourceTransform = sourceTransform;
        base.InitializeCollisionList();
    }
}
