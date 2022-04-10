using Demonics.Sounds;
using System;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
	[SerializeField] private Player _player = default;
	[SerializeField] private PlayerMovement _playerMovement = default;
	[SerializeField] private InputBuffer _inputBuffer = default;
	[SerializeField] private Audio _audio = default;
	private TrainingMenu _trainingMenu;


	public void SetTrainingMenu(TrainingMenu trainingMenu)
	{
		_trainingMenu = trainingMenu;
	}

	public void UnlockMovement()
	{
		_inputBuffer.CheckForInputBufferItem();
		_player.IsAttacking = false;
		_player.CanCancelAttack = false;
		_playerMovement.SetLockMovement(false);
		SetFramedata();
	}

	public void CreateEffectAnimationEvent(int isProjectile)
	{
		//_player.AttackTravel();
		_player.CreateEffect(Convert.ToBoolean(isProjectile));
	}

	public void ResetTimeScale()
	{
		Time.timeScale = GameManager.Instance.GameSpeed;
	}

	public void ZeroGravity()
	{
		_playerMovement.ZeroGravity();
	}

	public void ResetGravity()
	{
		_playerMovement.ResetGravity();
	}

	public void PlayerFootstepAnimationEvent()
	{
		_audio.SoundGroup("Footsteps").PlayInRandom();
	}

	public void PlayerSoundAnimationEvent(string soundName)
	{
		_audio.Sound(soundName).Play();
	}

	public void SetFramedata()
	{
		_trainingMenu.FramedataValue(_player.IsPlayerOne, _player.CurrentAttack);
	}
}
