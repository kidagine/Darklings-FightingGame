using System.Collections;
using UnityEngine;

public class Projectile : DemonicsAnimator, IHurtboxResponder, IHitstop
{
    [SerializeField] private Hitbox _hitbox = default;
    [SerializeField] private GameObject _dustPrefab = default;
    [SerializeField] private int _projectilePriority = default;
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private bool _isFixed = default;
    [SerializeField] private bool _disableOnContact = true;
    private DemonicsPhysics _physics;
    public int ProjectilePriority { get { return _projectilePriority; } private set { } }
    public Transform SourceTransform { get; private set; }


    public Vector2 Direction { get; set; }
    public bool BlockingLow { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool BlockingHigh { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool BlockingMiddair { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    void OnEnable()
    {
        OnCurrentAnimationFinished.AddListener(() => gameObject.SetActive(false));
        SetAnimation("Idle");

        _physics = GetComponent<DemonicsPhysics>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_isFixed)
        {
            _physics.Velocity = (new DemonicsVector2((DemonicsFloat)transform.right.x, (DemonicsFloat)transform.right.y) * (DemonicsFloat)transform.root.localScale.x) * (DemonicsFloat)_speed;
        }
        else
        {
            _physics.Velocity = new DemonicsVector2((DemonicsFloat)Direction.x, (DemonicsFloat)Direction.y) * (DemonicsFloat)_speed;
        }
        if (_physics.OnGround && _disableOnContact)
        {
            DestroyProjectile();
        }
    }

    protected override void CheckAnimationBoxes()
    {
        if (GetHitboxes().Length > 0)
        {
            _hitbox.SetBox(GetHitboxes()[0].size, GetHitboxes()[0].offset);
        }
    }

    public void SetSourceTransform(Transform sourceTransform, Vector2 position, bool assist)
    {
        SourceTransform = sourceTransform;
        _hitbox.SetSourceTransform(sourceTransform);
        if (assist)
        {
            _physics.Position = new DemonicsVector2((DemonicsFloat)position.x, (DemonicsFloat)position.y);
        }
        else
        {
            _physics.Position = new DemonicsVector2((DemonicsFloat)sourceTransform.position.x + (sourceTransform.localScale.x * (DemonicsFloat)position.x), (DemonicsFloat)sourceTransform.position.y + (DemonicsFloat)position.y);
        }
    }

    public void DestroyProjectile()
    {
        Instantiate(_dustPrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    public bool TakeDamage(AttackSO attackSO)
    {
        return false;
    }

    public void EnterHitstop()
    {
        _physics.SetFreeze(true);
        Pause();
    }

    public void ExitHitstop()
    {
        gameObject.SetActive(false);
        _physics.SetFreeze(false);
        Resume();
    }

    public bool IsInHitstop()
    {
        return false;
    }
}
