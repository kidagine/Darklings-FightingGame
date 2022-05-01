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
		_player.IsAttacking = false;
		_playerMovement.SetLockMovement(false);
		_player.CanCancelAttack = false;
		_inputBuffer.CheckForInputBufferItem();
		_playerMovement.FullyLockMovement = false;
		SetFramedata();
	}

	public void FullyLockMovement()
	{
		_playerMovement.FullyLockMovement = true;
	}

	public void ThrowEnd()
	{
		_player.ThrowEnd();
	}

	public void CreateEffectAnimationEvent(int isProjectile)
	{
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

	public void PlayerFootstepHeavyAnimationEvent()
	{
		_audio.SoundGroup("FootstepsHeavy").PlayInRandom();
	}

	public void PlayerSoundAnimationEvent(string soundName)
	{
		_audio.Sound(soundName).Play();
	}

	public void SetFramedata()
	{
		_trainingMenu.FramedataValue(_player.IsPlayerOne, _player.CurrentAttack);
	}

	public void AddForce(int moveHorizontally)
	{
		_playerMovement.AddForce(moveHorizontally);
	}

	public void ShakeCamera(CameraShakerSO cameraShaker)
	{
		CameraShake.Instance.Shake(cameraShaker.intensity, cameraShaker.timer);
	}
}
