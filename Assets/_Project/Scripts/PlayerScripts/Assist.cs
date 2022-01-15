using Demonics.Sounds;
using UnityEngine;

public class Assist : MonoBehaviour, IHitboxResponder
{
	[SerializeField] private Animator _animator = default;
	[SerializeField] private AssistStatsSO _assistStatsSO = default;
	[SerializeField] private GameObject _smokePrefab = default;
	private Audio _audio;
	private Transform _player;

	public AssistStatsSO AssistStats { get { return _assistStatsSO; } private set { } }
	public bool IsOnScreen { get; set; }


	private void Awake()
	{
		_player = transform.root;
		_audio = GetComponent<Audio>();
	}

	public void SetAssist(AssistStatsSO assistStats)
	{
		AssistStats = assistStats;
	}

	public void Attack()
	{
		IsOnScreen = true;
		_audio.Sound("Attack").Play();
		transform.SetParent(_player);
		_animator.SetTrigger("Attack");
		transform.localPosition = AssistStats.assistPosition;
		transform.SetParent(null);
		transform.localScale = _player.localScale;
	}

	public void Projectile()
	{
		GameObject hitEffect;
		hitEffect = Instantiate(_assistStatsSO.assistPrefab, transform);
		hitEffect.transform.localPosition = Vector2.zero;
		hitEffect.transform.GetChild(0).GetChild(0).GetComponent<Hitbox>().SetSourceTransform(_player);
		hitEffect.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, AssistStats.assistRotation);
		hitEffect.transform.GetChild(0).GetChild(0).GetComponent<Hitbox>().SetHitboxResponder(transform);
		hitEffect.transform.SetParent(null);
	}

	public void HitboxCollided(RaycastHit2D hit, Hurtbox hurtbox = null)
	{
		AssistStats.attackSO.hurtEffectPosition = hit.point;
		hurtbox.TakeDamage(AssistStats.attackSO);
	}

	public void HitboxCollidedGround(RaycastHit2D hit)
	{
		_audio.Sound("Destroyed").Play();
		GameObject effect = Instantiate(_smokePrefab);
		effect.transform.localPosition = hit.point;
	}
}
