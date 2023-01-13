using UnityEngine;

public class PlayerSimulation : MonoBehaviour
{
    [SerializeField] private Player _player = default;
    [SerializeField] private PlayerAnimator _playerAnimator = default;
    [SerializeField] private Audio _audio = default;
    [SerializeField] private CollisionVisualizer _hurtBoxVisualizer = default;
    [SerializeField] private CollisionVisualizer _hitBoxVisualizer = default;
    [SerializeField] private CollisionVisualizer _pushBoxVisualizer = default;
    [SerializeField] private DisconnectMenu _disconnectMenu = default;


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
        if (info.state == PlayerConnectState.Disconnected)
        {
            _player.PlayerUI.Disconnected();
        }
        _player.Simulate(playerGs, info);
        _player.PlayerUI.SetArcana(playerGs.arcanaGauge);
        if (playerGs.shadow.isOnScreen)
        {
            if (playerGs.shadow.animationFrames >= 55)
            {
                playerGs.shadow.isOnScreen = false;
                _player.Assist.Recall();
            }
            else
            {
                _player.Assist.Attack(playerGs.shadow.animationFrames);
                _player.Assist.SetAnimation("Attack", playerGs.shadow.animationFrames);
                bool isProjectile = _player.Assist.GetProjectile("Attack", playerGs.shadow.animationFrames);
                if (isProjectile)
                {
                    playerGs.SetAssist(playerGs.shadow.projectile.name, new DemonicsVector2(playerGs.position.x + playerGs.shadow.position.x, playerGs.position.y + playerGs.shadow.position.y), false);
                }
            }
        }
        _playerAnimator.SetAnimation(playerGs.animation, playerGs.animationFrames);
        _playerAnimator.SetInvinsible(playerGs.invinsible);
        _playerAnimator.SetSpriteOrder(playerGs.spriteOrder);
        _hitBoxVisualizer.ShowBox(playerGs.hitbox);
        _hurtBoxVisualizer.ShowBox(playerGs.hurtbox);
        _pushBoxVisualizer.ShowBox(playerGs.pushbox);
    }
}
