using UnityEngine;

public class StateSimulation
{
    public static void SetState(PlayerNetwork player)
    {
        if (player.state != "Run")
            player.CurrentState.Exit(player);
        if (player.state == "Attack")
            player.CurrentState = new AttackState();
        if (player.state == "Arcana")
            player.CurrentState = new ArcanaState();
        if (player.state == "ArcanaEnd")
            player.CurrentState = new ArcanaEndState();
        if (player.state == "Hurt")
            player.CurrentState = new HurtState();
        if (player.state == "DashAir")
            player.CurrentState = new DashAirState();
        if (player.state == "Dash")
            player.CurrentState = new DashState();
        if (player.state == "Idle")
            player.CurrentState = new IdleState();
        if (player.state == "Walk")
            player.CurrentState = new WalkState();
        if (player.state == "PreJump")
            player.CurrentState = new PreJumpState();
        if (player.state == "Run")
            player.CurrentState = new RunState();
        if (player.state == "JumpForward")
            player.CurrentState = new JumpForwardState();
        if (player.state == "Crouch")
            player.CurrentState = new CrouchState();
        if (player.state == "Jump")
            player.CurrentState = new JumpState();
        if (player.state == "Fall")
            player.CurrentState = new FallState();
        if (player.state == "Block")
            player.CurrentState = new BlockState();
        if (player.state == "BlockLow")
            player.CurrentState = new BlockLowState();
        if (player.state == "BlockAir")
            player.CurrentState = new BlockAirState();
        if (player.state == "HurtAir")
            player.CurrentState = new HurtAirState();
        if (player.state == "Airborne")
            player.CurrentState = new HurtAirborneState();
        if (player.state == "WallSplat")
            player.CurrentState = new WallSplatState();
        if (player.state == "SoftKnockdown")
            player.CurrentState = new KnockdownSoftState();
        if (player.state == "HardKnockdown")
            player.CurrentState = new KnockdownHardState();
        if (player.state == "Knockback")
            player.CurrentState = new KnockbackState();
        if (player.state == "WakeUp")
            player.CurrentState = new WakeUpState();
        if (player.state == "Death")
            player.CurrentState = new DeathState();
        if (player.state == "GiveUp")
            player.CurrentState = new GiveUpState();
        if (player.state == "Taunt")
            player.CurrentState = new TauntState();
        if (player.state == "BlueFrenzy")
            player.CurrentState = new BlueFrenzyState();
        if (player.state == "RedFrenzy")
            player.CurrentState = new RedFrenzyState();
        if (player.state == "Grab")
            player.CurrentState = new GrabState();
        if (player.state == "Grabbed")
            player.CurrentState = new GrabbedState();
        if (player.state == "Throw")
            player.CurrentState = new ThrowState();
        if (player.state == "Shadowbreak")
            player.CurrentState = new ShadowbreakState();
    }
}
