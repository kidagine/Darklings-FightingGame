using UnityEngine;

public class Assist : DemonicsAnimator
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
    public void Simulate(PlayerNetwork player)
    {
        gameObject.SetActive(player.shadow.isOnScreen);
        SetAnimation("Attack", player.shadow.animationFrames);
        transform.position = new Vector2((float)player.shadow.position.x, (float)player.shadow.position.y);
        transform.localScale = new Vector2(player.shadow.flip, 1);
    }
    public bool GetProjectile(string name, int frame)
    {
        return GetEvent(name, frame, out _).projectile;
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

    public void HitboxCollidedGround(RaycastHit2D hit)
    {
        _audio.Sound("Destroyed").Play();
        GameObject effect = Instantiate(_smokePrefab);
        effect.transform.localPosition = hit.point;
    }
}
