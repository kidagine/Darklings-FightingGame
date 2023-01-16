using UnityEngine;

public class Assist : DemonicsAnimator, IHitboxResponder
{
    [SerializeField] private AssistStatsSO _assistStatsSO = default;
    [SerializeField] private GameObject _smokePrefab = default;
    private Audio _audio;
    private Transform _player;
    private GameObject _hitEffect;

    public AssistStatsSO AssistStats { get { return _assistStatsSO; } private set { } }


    private void Awake()
    {
        _player = transform.root;
        _audio = GetComponent<Audio>();
        transform.SetParent(null);
    }

    public void SetAssist(AssistStatsSO assistStats)
    {
        _assistStatsSO = assistStats;
    }

    public void Attack(int frame)
    {
        gameObject.SetActive(true);
        _audio.Sound("Attack").Play();
        transform.SetParent(_player);
        transform.localPosition = AssistStats.assistPosition;
        transform.localScale = _player.localScale;
    }
    public void Simulate(ShadowNetwork shadow)
    {
        gameObject.SetActive(shadow.isOnScreen);
        SetAnimation("Attack", shadow.animationFrames);
        transform.position = new Vector2((float)shadow.position.x, (float)shadow.position.y);
        transform.localScale = new Vector2(shadow.flip, 1);
    }
    public bool GetProjectile(string name, int frame)
    {
        return GetEvent(name, frame).projectile;
    }
    protected override void AnimationEnded()
    {
        base.AnimationEnded();
        gameObject.SetActive(false);
    }

    public void Recall()
    {
        if (_hitEffect != null)
        {
            _hitEffect.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void Projectile()
    {
        // _hitEffect = Instantiate(_assistStatsSO.assistPrefab, transform);
        // _hitEffect.GetComponent<Projectile>().SetSourceTransform(_player, transform.position, true);
        // _hitEffect.transform.GetChild(0).GetChild(0).GetComponent<Hitbox>().SetSourceTransform(_player);
        // _hitEffect.transform.localRotation = Quaternion.Euler(0, 0, AssistStats.assistRotation);
        // _hitEffect.transform.GetChild(0).GetChild(0).GetComponent<Hitbox>().SetHitboxResponder(transform);
        // _hitEffect.transform.SetParent(null);
    }

    public bool HitboxCollided(Vector2 hurtPosition, Hurtbox hurtbox = null)
    {
        AssistStats.attackSO.hurtEffectPosition = hurtPosition;
        return hurtbox.TakeDamage(AssistStats.attackSO);
    }

    public void HitboxCollidedGround(RaycastHit2D hit)
    {
        _audio.Sound("Destroyed").Play();
        GameObject effect = Instantiate(_smokePrefab);
        effect.transform.localPosition = hit.point;
    }
}
