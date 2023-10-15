using System.Numerics;
using Demonics;

public class KnockdownSoftState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        CheckFlip(player);
        if (!player.enter)
        {
            player.player.PlayerUI.DisplayNotification(NotificationTypeEnum.SoftKnockdown);
            player.enter = true;
            player.animationFrames = 0;
            player.sound = "Landed";
            player.SetParticle("KnockdownSoft", new DemonVector2(player.position.x, player.position.y - 15));
            player.velocity = new DemonVector2((DemonFloat)0, (DemonFloat)1.45f);
            player.framedataEnum = FramedataTypesEnum.Knockdown;
            return;
        }
        player.velocity = new DemonVector2(player.velocity.x, player.velocity.y - DemonicsPhysics.GRAVITY);
        player.animationFrames++;
        player.framedataEnum = FramedataTypesEnum.Knockdown;
        player.hurtbox.active = false;
        player.animation = "Knockdown";
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.animationFrames >= 30)
            EnterState(player, "WakeUp");
    }
}