using System;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerNetwork
{
    public Player player;
    public PlayerNetwork otherPlayer;
    public ShadowNetwork shadow;
    public ResultAttack resultAttack;
    public PlayerStatsSO playerStats;
    public DemonicsVector2 position;
    public DemonicsVector2 velocity;
    public DemonicsVector2 pushbackStart;
    public DemonicsVector2 pushbackEnd;
    public DemonicsVector2 hurtPosition;
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
    public bool hitstop;
    public bool juggleBounce;
    public string state;
    public int spriteOrder;
    public State CurrentState;
    public ColliderNetwork hurtbox;
    public ColliderNetwork hitbox;
    public ColliderNetwork pushbox;
    public InputBufferNetwork inputBuffer;
    public EffectNetwork[] effects;
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
        bw.Write(canDash);
        bw.Write(jump);
        bw.Write(knockback);
        bw.Write(combo);
        bw.Write(comboTimer);
        bw.Write((int)comboTimerStarter);
        bw.Write(isCrouch);
        bw.Write(isAir);
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
        bw.Write(juggleBounce);
        bw.Write(flip);
        bw.Write(spriteOrder);
        bw.Write(state);
        inputBuffer.Serialize(bw);
        hurtbox.Serialize(bw);
        hitbox.Serialize(bw);
        shadow.Serialize(bw);
        attackNetwork.Serialize(bw);
        attackHurtNetwork.Serialize(bw);
        for (int i = 0; i < projectiles.Length; ++i)
        {
            projectiles[i].Serialize(bw);
        }
        for (int i = 0; i < effects.Length; ++i)
        {
            effects[i].Serialize(bw);
        }
    }

    public void Deserialize(BinaryReader br)
    {
        position.x = (DemonicsFloat)br.ReadSingle();
        position.y = (DemonicsFloat)br.ReadSingle();
        velocity.x = (DemonicsFloat)br.ReadSingle();
        velocity.y = (DemonicsFloat)br.ReadSingle();
        pushbackStart.x = (DemonicsFloat)br.ReadSingle();
        pushbackStart.y = (DemonicsFloat)br.ReadSingle();
        pushbackEnd.x = (DemonicsFloat)br.ReadSingle();
        pushbackEnd.y = (DemonicsFloat)br.ReadSingle();
        hurtPosition.x = (DemonicsFloat)br.ReadSingle();
        hurtPosition.y = (DemonicsFloat)br.ReadSingle();
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
        canDash = br.ReadBoolean();
        jump = br.ReadSingle();
        knockback = br.ReadInt32();
        combo = br.ReadInt32();
        comboTimer = br.ReadInt32();
        comboTimerStarter = (ComboTimerStarterEnum)br.ReadInt32();
        isCrouch = br.ReadBoolean();
        isAir = br.ReadBoolean();
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
        juggleBounce = br.ReadBoolean();
        flip = br.ReadInt32();
        spriteOrder = br.ReadInt32();
        state = br.ReadString();
        inputBuffer.Deserialize(br);
        hurtbox.Deserialize(br);
        hitbox.Deserialize(br);
        shadow.Deserialize(br);
        attackNetwork.Deserialize(br);
        attackHurtNetwork.Deserialize(br);
        for (int i = 0; i < projectiles.Length; ++i)
        {
            projectiles[i].Deserialize(br);
        }
        for (int i = 0; i < effects.Length; ++i)
        {
            effects[i].Deserialize(br);
        }
    }

    public override int GetHashCode()
    {
        int hashCode = 1858597544;
        hashCode = hashCode * -1521134295 + position.GetHashCode();
        return hashCode;
    }

    public void SetEffect(string name, DemonicsVector2 position, bool flip = false)
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
    public void SetProjectile(string name, DemonicsVector2 position, bool flip = false)
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
    public void SetAssist(string name, DemonicsVector2 position, DemonicsFloat speed, bool flip = false)
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
    public void InitializeProjectile(string name, AttackNetwork attackNetwork, DemonicsFloat speed, int priority, bool destroyOnHit)
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
};
