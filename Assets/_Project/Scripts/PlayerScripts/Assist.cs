using UnityEngine;

public class Assist : MonoBehaviour, IHitboxResponder
{
    [SerializeField] private Animator _animator = default;
    [SerializeField] private AssistStatsSO _assistStatsSO = default;
	[SerializeField] private GameObject _projectilePrefab = default;
	[SerializeField] private GameObject _smokePrefab = default;
	private Transform _player;

	public AssistStatsSO AssistStats { get { return _assistStatsSO; } private set { } }


	private void Awake()
	{
		_player = transform.root;
	}

	public void Attack()
	{
		transform.SetParent(_player);
		_animator.SetTrigger("Attack");
		transform.localPosition = AssistStats.assistPosition;
		transform.SetParent(null);
		transform.localScale = _player.localScale;
	}

	public void Projectile()
	{
		GameObject hitEffect;
		hitEffect = Instantiate(_projectilePrefab, transform);
		hitEffect.transform.localPosition = Vector2.zero;
		hitEffect.transform.GetChild(0).GetChild(0).GetComponent<Hitbox>().SetSourceTransform(_player);
		if (_player.transform.localScale.x == 1.0f)
		{
			hitEffect.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, AssistStats.assistRotation);
		}
		else
		{
			hitEffect.transform.localScale = new Vector2(-1.0f, 1.0f);
			hitEffect.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, AssistStats.assistRotation * 5);
		}
		hitEffect.transform.SetParent(null);
		hitEffect.transform.GetChild(0).GetChild(0).GetComponent<Hitbox>().SetHitboxResponder(transform);
	}

	public void HitboxCollided(RaycastHit2D hit, Hurtbox hurtbox = null)
	{
		AssistStats.attackSO.hurtEffectPosition = hit.point;
		hurtbox.TakeDamage(AssistStats.attackSO);
	}

	public void HitboxCollidedGround(RaycastHit2D hit)
	{
		GameObject effect = Instantiate(_smokePrefab);
		effect.transform.localPosition = hit.point;
	}
}
