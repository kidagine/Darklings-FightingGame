using Demonics.Sounds;
using UnityEngine;

public class Assist : DemonicsAnimator, IHitboxResponder
{
    [SerializeField] private AssistStatsSO _assistStatsSO = default;
    [SerializeField] private GameObject _smokePrefab = default;
    private Audio _audio;
    private Transform _player;
    private GameObject _hitEffect;

    public AssistStatsSO AssistStats { get { return _assistStatsSO; } private set { } }
    public bool IsOnScreen { get; set; }


    private void Awake()
    {
        _player = transform.root;
        _audio = GetComponent<Audio>();
    }

    public void SetAssist(AssistStatsSO assistStats)
    {
        _assistStatsSO = assistStats;
    }

    public void Attack()
    {
        gameObject.SetActive(true);
        IsOnScreen = true;
        _audio.Sound("Attack").Play();
        transform.SetParent(_player);
        SetAnimation("Attack");
        transform.localPosition = AssistStats.assistPosition;
        transform.SetParent(null);
        transform.localScale = _player.localScale;
    }

    protected override void CheckEvents()
    {
        base.CheckEvents();
        if (GetEvent().projectile)
        {
            Projectile();
        }
    }

    protected override void AnimationEnded()
    {
        base.AnimationEnded();
        IsOnScreen = false;
        gameObject.SetActive(false);
    }

    public void Recall()
    {
        if (IsOnScreen)
        {
            IsOnScreen = false;
            if (_hitEffect != null)
            {
                _hitEffect.SetActive(false);
            }
        }
    }

    public void Projectile()
    {
        _hitEffect = Instantiate(_assistStatsSO.assistPrefab, transform);
        _hitEffect.transform.localPosition = Vector2.zero;
        _hitEffect.GetComponent<Projectile>().SetSourceTransform(_player);
        _hitEffect.transform.GetChild(0).GetChild(0).GetComponent<Hitbox>().SetSourceTransform(_player);
        _hitEffect.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, AssistStats.assistRotation);
        _hitEffect.transform.GetChild(0).GetChild(0).GetComponent<Hitbox>().SetHitboxResponder(transform);
        _hitEffect.transform.SetParent(null);
    }

    public bool HitboxCollided(RaycastHit2D hit, Hurtbox hurtbox = null)
    {
        AssistStats.attackSO.hurtEffectPosition = hit.point;
        return hurtbox.TakeDamage(AssistStats.attackSO);
    }

    public void HitboxCollidedGround(RaycastHit2D hit)
    {
        _audio.Sound("Destroyed").Play();
        GameObject effect = Instantiate(_smokePrefab);
        effect.transform.localPosition = hit.point;
    }
}
