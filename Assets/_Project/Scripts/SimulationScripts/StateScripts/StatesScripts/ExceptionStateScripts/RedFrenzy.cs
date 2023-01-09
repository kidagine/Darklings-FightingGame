public class RedFrenzyState : State
{
    public override void UpdateLogic(PlayerNetwork player)
    {
        if (!player.enter)
        {
            SetTopPriority(player);
            CheckFlip(player);
            player.enter = true;
            player.sound = "Vanish";
            player.animationFrames = 0;
            player.animation = "RedFrenzy";
            player.canChainAttack = true;
            player.attackFrames = DemonicsAnimator.GetMaxAnimationFrames(player.playerStats._animation, player.animation);
        }
        player.velocity = DemonicsVector2.Zero;
        if (GameSimulation.Hitstop <= 0)
        {
            player.animationFrames++;
            player.attackFrames--;
        }
        // if (player.animationFrames == 7)
        // {
        //     player.SetEffect("Parry", player.position);
        //     // GameSimulation.Hitstop = 26;
        // }
        // if (player.animationFrames == 22)
        // {
        //     player.position = new DemonicsVector2(player.otherPlayer.position.x, player.otherPlayer.position.y);
        //     player.SetEffect("Parry", player.position);
        // }
        // if (player.animationFrames == 33)
        // {
        //     player.sound = "Vanish";
        //     player.SetEffect("Parry", player.position);
        // }
        ToIdleState(player);
    }
    private void ToIdleState(PlayerNetwork player)
    {
        if (player.attackFrames <= 0)
        {
            player.enter = false;
            player.state = "Idle";
        }
    }
}