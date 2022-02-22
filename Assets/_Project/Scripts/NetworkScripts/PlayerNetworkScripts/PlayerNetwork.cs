using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : Player, IHurtboxResponder, IHitboxResponder
{
	public override void CreateEffect(bool isProjectile = false)
	{
		if (CurrentAttack.hitEffect != null)
		{
			GameObject hitEffect;
			hitEffect = Instantiate(CurrentAttack.hitEffect, _effectsParent);
			hitEffect.GetComponent<NetworkObject>().Spawn();
			hitEffect.transform.localPosition = CurrentAttack.hitEffectPosition;
			hitEffect.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, CurrentAttack.hitEffectRotation);
			if (isProjectile)
			{
				hitEffect.transform.SetParent(null);
				hitEffect.GetComponent<MoveInDirection>().Direction = new Vector2(transform.localScale.x, 0.0f);
				hitEffect.transform.GetChild(0).GetChild(0).GetComponent<Hitbox>().SetHitboxResponder(transform);
			}
		}
	}

	[ClientRpc]
	public void ArcanaClientRpc()
	{
		_arcana--;
		_playerUI.DecreaseArcana();
		_playerUI.SetArcana(_arcana);
		CurrentAttack = _playerComboSystem.GetArcana();
	}

	[ClientRpc]
	public void AttackClientRpc()
	{
		CurrentAttack = _playerComboSystem.GetComboAttack();
	}
}
