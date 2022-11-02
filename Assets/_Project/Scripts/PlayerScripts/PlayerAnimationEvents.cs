using Demonics.Sounds;
using System;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private Player _player = default;
    [SerializeField] private PlayerMovement _playerMovement = default;
    [SerializeField] private Audio _audio = default;

    public void UnlockMovementNoFramedata()
    {
        _player.IsAttacking = false;
        _player.CanShadowbreak = true;
    }

    public void ThrowEnd()
    {
        _player.OtherPlayerStateManager.TryToKnockdownState();
    }

    public void ThrowArcanaEnd()
    {
        _player.OtherPlayerStateManager.TryToHurtState(_player.CurrentAttack);
    }

    public void CreateEffectAnimationEvent(int isProjectile)
    {
        _player.CreateEffect(Convert.ToBoolean(isProjectile));
    }

    public void SetParryingAnimationEvent(int state)
    {
        if (state == 0)
        {
            _player.Parrying = false;
        }
        else
        {
            _player.Parrying = true;
        }
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
}
