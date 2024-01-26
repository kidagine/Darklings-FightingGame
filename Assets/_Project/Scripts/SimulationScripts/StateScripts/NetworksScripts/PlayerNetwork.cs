using System;
using System.IO;
using Demonics;
using UnityEngine;

[Serializable]
public class PlayerNetwork
{
    public Player player;
    public PlayerNetwork otherPlayer;
    public ShadowNetwork shadow;
    public ResultAttack resultAttack;
    public PlayerStatsSO playerStats;
    public DemonVector2 position;
    public DemonVector2 velocity;
    public DemonVector2 pushbackStart;
    public DemonVector2 pushbackEnd;
    public DemonVector2 hurtPosition;
    public AttackNetwork attackNetwork;
    public AttackNetwork attackHurtNetwork;
    public Vector2Int direction;
    public InputDirectionEnum inputDirection;
    public string animation;
    public int animationFrames;
    public int cel;
    public int attackFrames;
    public int stunFrames;
    public int combo;
    public int comboTimer;
    public ComboTimerStarterEnum comboTimerStarter;
    public InputEnum attackInput;
    public int health;
    public int healthRecoverable;
    public int arcanaGauge;
    public int shadowGauge;
    public int flip;
    public int knockback;
    public string soundGroup;
    public string sound;
    public string soundStop;
    public float jump;
    public bool isCrouch;
    public bool isAir;
    public bool upHold;
    public bool downHold;
    public bool leftHold;
    public bool rightHold;
    public bool isAi;
    public bool gotHit;
    public int dashDirection;
    public int jumpDirection;
    public int dashFrames;
    public int pushbackDuration;
    public bool invisible;
    public bool invincible;
    public bool inPushback;
    public bool canDash;
    public bool comboLocked;
    public bool hasJumped;
    public bool canJump;
    public bool canDoubleJump;
    public bool enter;
    public bool wasWallSplatted;
    public bool canChainAttack;
    public bool usedShadowbreak;
    public bool hitstop;
    public bool juggleBounce;
    public bool runJump;
    public string state;
    public int spriteOrder;
    public State CurrentState;
    public InputList inputList;
    public ColliderNetwork hurtbox;
    public ColliderNetwork hitbox;
    public ColliderNetwork pushbox;
    public InputBufferNetwork inputBuffer;
    public InputHistoryNetwork inputHistory;
    public FramedataTypesEnum framedataEnum;
    public EffectNetwork[] effects;
    public EffectNetwork[] particles;
    public ProjectileNetwork[] projectiles;
    public void Serialize(BinaryWriter bw)
    {
        bw.Write((float)position.x);
        bw.Write((float)position.y);
        bw.Write((float)velocity.x);
        bw.Write((float)velocity.y);
        bw.Write((float)pushbackStart.x);
        bw.Write((float)pushbackStart.y);
        bw.Write((float)pushbackEnd.x);
        bw.Write((float)pushbackEnd.y);
        bw.Write((float)hurtPosition.x);
        bw.Write((float)hurtPosition.y);
        bw.Write((int)inputDirection);
        bw.Write(direction.x);
        bw.Write(direction.y);
        bw.Write(animation);
        bw.Write(animationFrames);
        bw.Write(cel);
        bw.Write(attackFrames);
        bw.Write(stunFrames);
        bw.Write((int)attackInput);
        bw.Write(health);
        bw.Write(healthRecoverable);
        bw.Write(arcanaGauge);
        bw.Write(shadowGauge);
        bw.Write(sound);
        bw.Write(soundGroup);
        bw.Write(soundStop);
        bw.Write(jump);
        bw.Write(knockback);
        bw.Write(combo);
        bw.Write(comboTimer);
        bw.Write((int)comboTimerStarter);
        bw.Write(isCrouch);
        bw.Write(isAir);
        bw.Write(gotHit);
        bw.Write(upHold);
        bw.Write(downHold);
        bw.Write(leftHold);
        bw.Write(rightHold);
        bw.Write(hasJumped);
        bw.Write(pushbackDuration);
        bw.Write(canJump);
        bw.Write(invisible);
        bw.Write(invincible);
        bw.Write(canDoubleJump);
        bw.Write(dashDirection);
        bw.Write(jumpDirection);
        bw.Write(dashFrames);
        bw.Write(wasWallSplatted);
        bw.Write(enter);
        bw.Write(hitstop);
        bw.Write(inPushback);
        bw.Write(canChainAttack);
        bw.Write(usedShadowbreak);
        bw.Write(juggleBounce);
        bw.Write(flip);
        bw.Write(spriteOrder);
        bw.Write(state);
        bw.Write(runJump);
        inputBuffer.Serialize(bw);
        inputHistory.Serialize(bw);
        hurtbox.Serialize(bw);
        hitbox.Serialize(bw);
        shadow.Serialize(bw);
        attackNetwork.Serialize(bw);
        attackHurtNetwork.Serialize(bw);
        for (int i = 0; i < projectiles.Length; ++i)
            projectiles[i].Serialize(bw);
        for (int i = 0; i < particles.Length; ++i)
            particles[i].Serialize(bw);
        for (int i = 0; i < effects.Length; ++i)
            effects[i].Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        position.x = (DemonFloat)br.ReadSingle();
        position.y = (DemonFloat)br.ReadSingle();
        velocity.x = (DemonFloat)br.ReadSingle();
        velocity.y = (DemonFloat)br.ReadSingle();
        pushbackStart.x = (DemonFloat)br.ReadSingle();
        pushbackStart.y = (DemonFloat)br.ReadSingle();
        pushbackEnd.x = (DemonFloat)br.ReadSingle();
        pushbackEnd.y = (DemonFloat)br.ReadSingle();
        hurtPosition.x = (DemonFloat)br.ReadSingle();
        hurtPosition.y = (DemonFloat)br.ReadSingle();
        direction.x = br.ReadInt32();
        direction.y = br.ReadInt32();
        inputDirection = (InputDirectionEnum)br.ReadInt32();
        animation = br.ReadString();
        animationFrames = br.ReadInt32();
        cel = br.ReadInt32();
        attackFrames = br.ReadInt32();
        stunFrames = br.ReadInt32();
        attackInput = (InputEnum)br.ReadInt32();
        health = br.ReadInt32();
        healthRecoverable = br.ReadInt32();
        arcanaGauge = br.ReadInt32();
        shadowGauge = br.ReadInt32();
        sound = br.ReadString();
        soundGroup = br.ReadString();
        soundStop = br.ReadString();
        jump = br.ReadSingle();
        knockback = br.ReadInt32();
        combo = br.ReadInt32();
        comboTimer = br.ReadInt32();
        comboTimerStarter = (ComboTimerStarterEnum)br.ReadInt32();
        isCrouch = br.ReadBoolean();
        isAir = br.ReadBoolean();
        gotHit = br.ReadBoolean();
        upHold = br.ReadBoolean();
        downHold = br.ReadBoolean();
        leftHold = br.ReadBoolean();
        rightHold = br.ReadBoolean();
        hasJumped = br.ReadBoolean();
        pushbackDuration = br.ReadInt32();
        canJump = br.ReadBoolean();
        invisible = br.ReadBoolean();
        invincible = br.ReadBoolean();
        canDoubleJump = br.ReadBoolean();
        dashDirection = br.ReadInt32();
        jumpDirection = br.ReadInt32();
        dashFrames = br.ReadInt32();
        wasWallSplatted = br.ReadBoolean();
        enter = br.ReadBoolean();
        hitstop = br.ReadBoolean();
        inPushback = br.ReadBoolean();
        canChainAttack = br.ReadBoolean();
        usedShadowbreak = br.ReadBoolean();
        juggleBounce = br.ReadBoolean();
        runJump = br.ReadBoolean();
        flip = br.ReadInt32();
        spriteOrder = br.ReadInt32();
        state = br.ReadString();
        inputBuffer.Deserialize(br);
        inputHistory.Deserialize(br);
        hurtbox.Deserialize(br);
        hitbox.Deserialize(br);
        shadow.Deserialize(br);

        attackNetwork.Deserialize(br);
        attackHurtNetwork.Deserialize(br);
        for (int i = 0; i < projectiles.Length; ++i)
            projectiles[i].Deserialize(br);
        for (int i = 0; i < particles.Length; ++i)
            particles[i].Deserialize(br);
        for (int i = 0; i < effects.Length; ++i)
            effects[i].Deserialize(br);
    }

    public override int GetHashCode()
    {
        int hashCode = 1858597544;
        hashCode = hashCode * -1521134295 + position.GetHashCode();
        return hashCode;
    }

    public void SetEffect(string name, DemonVector2 position, bool flip = false)
    {
        for (int i = 0; i < effects.Length; i++)
        {
            if (name == effects[i].name)
            {
                for (int j = 0; j < effects[i].effectGroups.Length; j++)
                {
                    if (!effects[i].effectGroups[j].active)
                    {
                        effects[i].effectGroups[j].flip = flip;
                        effects[i].effectGroups[j].active = true;
                        effects[i].effectGroups[j].position = position;
                        break;
                    }
                }
            }
        }
    }
    public void SetParticle(string name, DemonVector2 position, Vector3 flip = default)
    {
        for (int i = 0; i < particles.Length; i++)
            if (name == particles[i].name)
            {
                GameSimulationView.UpdateParticles(player.IsPlayerOne ? 0 : 1, particles[i], position, flip);
                return;
            }
    }
    public void SetProjectile(string name, DemonVector2 position, bool flip = false)
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (name == projectiles[i].name)
            {
                if (!projectiles[i].active)
                {
                    projectiles[i].flip = flip;
                    projectiles[i].active = true;
                    projectiles[i].position = position;
                    break;
                }
            }
        }
    }
    public void SetAssist(string name, DemonVector2 position, DemonFloat speed, bool flip = false)
    {
        if (name == shadow.projectile.name)
        {
            if (!shadow.projectile.active)
            {
                shadow.projectile.active = true;
                shadow.projectile.position = position;
                shadow.projectile.speed = speed;
            }
        }
    }
    public void InitializeProjectile(string name, AttackNetwork attackNetwork, DemonFloat speed, int priority, bool destroyOnHit)
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (name == projectiles[i].name)
            {
                projectiles[i].speed = speed;
                projectiles[i].priority = priority;
                projectiles[i].destroyOnHit = destroyOnHit;
                projectiles[i].attackNetwork = attackNetwork;
            }
        }
    }
    public void ArcanaGain(ArcanaGainTypes arcanaGainTypes)
    {
        DemonFloat meter = (DemonFloat)70;
        switch (arcanaGainTypes)
        {
            case ArcanaGainTypes.AttackOnHit:
                arcanaGauge += (int)DemonFloat.Floor(meter * 1.3);
                break;
            case ArcanaGainTypes.AttackOnBlock:
                arcanaGauge += (int)DemonFloat.Floor(meter * 0.8);
                break;
            case ArcanaGainTypes.DefendOnHit:
                arcanaGauge += (int)DemonFloat.Floor(meter * 0.6);
                break;
            case ArcanaGainTypes.DefendOnBlock:
                arcanaGauge += (int)DemonFloat.Floor(meter * 0.75);
                break;
        }
        int maxArcana = PlayerStatsSO.ARCANA_MULTIPLIER * playerStats.arcanaLevel;
        if (arcanaGauge > maxArcana)
            arcanaGauge = maxArcana;
    }
};
