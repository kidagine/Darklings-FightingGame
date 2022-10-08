using System.Collections;
using UnityEngine;

public class Projectile : DemonicsAnimator, IHurtboxResponder, IHitstop
{
    [SerializeField] private Hitbox _hitbox = default;
    [SerializeField] private GameObject _dustPrefab = default;
    [SerializeField] private int _projectilePriority = default;
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private bool _isFixed = default;
    private Rigidbody2D _rigidbody;
    private float _originalSpeed;
    public int ProjectilePriority { get { return _projectilePriority; } private set { } }
    public Transform SourceTransform { get; private set; }


    public Vector2 Direction { get; set; }
    public bool BlockingLow { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool BlockingHigh { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool BlockingMiddair { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    protected override void Awake()
    {
        base.Awake();
        _originalSpeed = _speed;
        _rigidbody = GetComponent<Rigidbody2D>();
        _hitbox.OnPlayerCollision += () => GameManager.Instance.AddHitstop(this);
        _hitbox.OnPlayerCollision += () => ExitHitstop();
        _hitbox.OnGroundCollision += () => gameObject.SetActive(false);
    }

    void OnEnable()
    {
        OnCurrentAnimationFinished.AddListener(() => gameObject.SetActive(false));
        SetAnimation("Idle");
        _speed = _originalSpeed;
    }

    void Update()
    {
        if (_isFixed)
        {
            _rigidbody.velocity = (transform.right * transform.root.localScale.x) * _speed;
        }
        else
        {
            _rigidbody.velocity = Direction * _speed;
        }
    }

    void OnDisable()
    {
        if (_hitbox.HitPoint != null)
        {
            if (_hitbox.HitPoint.gameObject.layer == 6)
            {
                Instantiate(_dustPrefab, transform.position, Quaternion.identity);
            }
            _hitbox.OnPlayerCollision -= () => GameManager.Instance.AddHitstop(this);
            _hitbox.OnGroundCollision -= () => gameObject.SetActive(false);
        }
    }

    protected override void CheckAnimationBoxes()
    {
        if (GetHitboxes().Length > 0)
        {
            _hitbox.SetBox(GetHitboxes()[0].size, GetHitboxes()[0].offset);
        }
    }

    public void SetSourceTransform(Transform sourceTransform)
    {
        SourceTransform = sourceTransform;
        _hitbox.SetSourceTransform(sourceTransform);
    }

    public bool TakeDamage(AttackSO attackSO)
    {
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Projectile projectile))
        {
            if (projectile.SourceTransform != SourceTransform)
            {
                if (projectile.ProjectilePriority > ProjectilePriority)
                {
                    gameObject.SetActive(false);
                    Instantiate(_dustPrefab, transform.position, Quaternion.identity);
                }
                else if (projectile.ProjectilePriority == ProjectilePriority)
                {
                    gameObject.SetActive(false);
                    projectile.gameObject.SetActive(false);
                    Instantiate(_dustPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }

    public void EnterHitstop()
    {
        Pause();
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        _rigidbody.isKinematic = true;
    }

    public void ExitHitstop()
    {
        Resume();
        _rigidbody.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        _rigidbody.isKinematic = false;
        if (gameObject.activeSelf)
            StartCoroutine(DisableCoroutine());
    }

    public bool IsInHitstop()
    {
        return _rigidbody.isKinematic;
    }

    IEnumerator DisableCoroutine()
    {
        _speed = 0;
        yield return new WaitForSecondsRealtime(0.05f);
        gameObject.SetActive(false);
    }
}
