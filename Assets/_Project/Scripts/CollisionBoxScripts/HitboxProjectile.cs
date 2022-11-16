using UnityEngine;

public class HitboxProjectile : Hitbox
{
    [SerializeField] private Projectile _projectile = default;

    public Projectile Projectile { get { return _projectile; } private set { } }
    void Awake()
    {
        GizmoColor = Color.red;
        SourceTransform = _projectile.SourceTransform;
    }

    protected override void EnterCollision(DemonicsCollider collider)
    {
        if (collider.TryGetComponent(out HitboxProjectile hitboxProjectile))
        {
            if (hitboxProjectile.Projectile.ProjectilePriority > Projectile.ProjectilePriority)
            {
                Projectile.DestroyProjectile();
            }
            else if (hitboxProjectile.Projectile.ProjectilePriority == Projectile.ProjectilePriority)
            {
                Projectile.DestroyProjectile();
                hitboxProjectile.Projectile.gameObject.SetActive(false);
            }
        }
        else
        {
            GameManager.Instance.AddHitstop(_projectile);
            base.EnterCollision(collider);
        }
    }

    public override void SetSourceTransform(Transform sourceTransform)
    {
        SourceTransform = sourceTransform;
        InitializeCollisionList();
    }

    protected override void InitializeCollisionList()
    {
        _demonicsColliders.Clear();
        DemonicsCollider[] demonicsCollidersArray = FindObjectsOfType<DemonicsCollider>();
        for (int i = 0; i < demonicsCollidersArray.Length; i++)
        {
            if (!demonicsCollidersArray[i].transform.IsChildOf(SourceTransform))
            {
                if (demonicsCollidersArray[i].TryGetComponent(out HitboxProjectile hitboxProjectile))
                {
                    if (hitboxProjectile.SourceTransform != SourceTransform)
                    {
                        _demonicsColliders.Add(demonicsCollidersArray[i]);
                    }
                }
                if (demonicsCollidersArray[i].TryGetComponent(out Hurtbox hurtbox))
                {
                    _demonicsColliders.Add(demonicsCollidersArray[i]);
                }
            }
        }
        _demonicsColliders.Remove(this);
    }
}
