using UnityEngine;

public class PlayerSimulation : MonoBehaviour
{
    [SerializeField] private Player _player = default;
    [SerializeField] private PlayerAnimator _playerAnimator = default;
    [SerializeField] private Audio _audio = default;
    [SerializeField] private CollisionVisualizer _hurtBoxVisualizer = default;
    [SerializeField] private CollisionVisualizer _hitBoxVisualizer = default;
    [SerializeField] private CollisionVisualizer _pushBoxVisualizer = default;


    public void Simulate(PlayerNetwork playerGs, PlayerConnectionInfo info)
    {
        if (!string.IsNullOrEmpty(playerGs.sound))
        {
            _audio.Sound(playerGs.sound).Play();
            playerGs.sound = "";
        }
        if (!string.IsNullOrEmpty(playerGs.soundStop))
        {
            _audio.Sound(playerGs.soundStop).Stop();
            playerGs.soundStop = "";
        }
        _player.Simulate(playerGs, info);
        _playerAnimator.SetAnimation(playerGs.animation, playerGs.animationFrames);
        _playerAnimator.SetSpriteOrder(playerGs.spriteOrder);
        _hitBoxVisualizer.ShowBox(playerGs.hitbox);
        _hurtBoxVisualizer.ShowBox(playerGs.hurtbox);
    }
}
