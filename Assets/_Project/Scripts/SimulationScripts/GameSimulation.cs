using SharedGame;
using System;
using System.IO;
using Unity.Collections;
using UnityEngine;

[Serializable]
public struct EffectGroupNetwork
{
    public DemonicsVector2 position;
    public int animationFrames;
    public bool active;
    public bool flip;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write((float)position.x);
        bw.Write((float)position.y);
        bw.Write(animationFrames);
        bw.Write(active);
        bw.Write(flip);
    }

    public void Deserialize(BinaryReader br)
    {
        position.x = (DemonicsFloat)br.ReadSingle();
        position.y = (DemonicsFloat)br.ReadSingle();
        animationFrames = br.ReadInt32();
        active = br.ReadBoolean();
        flip = br.ReadBoolean();
    }
};
[Serializable]
public struct EffectNetwork
{
    public string name;
    public int animationMaxFrames;
    public EffectGroupNetwork[] effectGroups;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(name);
        bw.Write(animationMaxFrames);
        for (int i = 0; i < effectGroups.Length; ++i)
        {
            effectGroups[i].Serialize(bw);
        }
    }

    public void Deserialize(BinaryReader br)
    {
        name = br.ReadString();
        animationMaxFrames = br.ReadInt32();
        for (int i = 0; i < effectGroups.Length; ++i)
        {
            effectGroups[i].Deserialize(br);
        }
    }
};
[Serializable]
public struct InputItemNetwork
{
    public int frame;
    public InputEnum inputEnum;
    public Vector2Int inputDirection;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(frame);
        bw.Write((int)inputEnum);
        bw.Write((int)inputDirection.x);
        bw.Write((int)inputDirection.y);
    }

    public void Deserialize(BinaryReader br)
    {
        frame = br.ReadInt32();
        inputEnum = (InputEnum)br.ReadInt32();
        inputDirection.x = br.ReadInt32();
        inputDirection.y = br.ReadInt32();
    }
};
[Serializable]
public struct InputBufferNetwork
{
    public InputItemNetwork[] inputItems;

    public void Serialize(BinaryWriter bw)
    {
        for (int i = 0; i < inputItems.Length; ++i)
        {
            inputItems[i].Serialize(bw);
        }
    }

    public void Deserialize(BinaryReader br)
    {
        for (int i = 0; i < inputItems.Length; ++i)
        {
            inputItems[i].Deserialize(br);
        }
    }
};
[Serializable]
public struct AttackNetwork
{
    public CameraShakerNetwork cameraShakerNetwork;

    public DemonicsVector2 travelDistance;
    public DemonicsFloat knockbackForce;
    public AttackTypeEnum attackType;
    public ComboTimerStarterEnum comboTimerStarter;
    public int knockbackDuration;
    public int knockbackArc;
    public int hitstop;
    public int damage;
    public int hitStun;
    public string name;
    public string attackSound;
    public string impactSound;
    public string hurtEffect;
    public bool jumpCancelable;
    public bool softKnockdown;
    public bool hardKnockdown;
    public bool superArmor;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write((float)travelDistance.x);
        bw.Write((float)travelDistance.y);
        bw.Write((float)knockbackForce);
        bw.Write((int)comboTimerStarter);
        bw.Write((int)attackType);
        bw.Write(knockbackDuration);
        bw.Write(hitstop);
        bw.Write(damage);
        bw.Write(knockbackArc);
        bw.Write(hitStun);
        bw.Write(name);
        bw.Write(attackSound);
        bw.Write(impactSound);
        bw.Write(hurtEffect);
        bw.Write(jumpCancelable);
        bw.Write(softKnockdown);
        bw.Write(hardKnockdown);
        bw.Write(superArmor);
        cameraShakerNetwork.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        travelDistance.x = (DemonicsFloat)br.ReadSingle();
        travelDistance.y = (DemonicsFloat)br.ReadSingle();
        knockbackForce = (DemonicsFloat)br.ReadSingle();
        comboTimerStarter = (ComboTimerStarterEnum)br.ReadInt32();
        attackType = (AttackTypeEnum)br.ReadInt32();
        knockbackDuration = br.ReadInt32();
        hitstop = br.ReadInt32();
        damage = br.ReadInt32();
        knockbackArc = br.ReadInt32();
        hitStun = br.ReadInt32();
        name = br.ReadString();
        attackSound = br.ReadString();
        impactSound = br.ReadString();
        hurtEffect = br.ReadString();
        jumpCancelable = br.ReadBoolean();
        softKnockdown = br.ReadBoolean();
        hardKnockdown = br.ReadBoolean();
        superArmor = br.ReadBoolean();
        cameraShakerNetwork.Deserialize(br);
    }
};
[Serializable]
public struct CameraShakerNetwork
{
    public float intensity;
    public float timer;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(intensity);
        bw.Write(timer);
    }

    public void Deserialize(BinaryReader br)
    {
        intensity = br.ReadSingle();
        timer = br.ReadSingle();
    }
};
[Serializable]
public struct ColliderNetwork
{
    public DemonicsVector2 position;
    public DemonicsVector2 size;
    public DemonicsVector2 offset;
    public bool active;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write((float)position.x);
        bw.Write((float)position.y);
        bw.Write((float)size.x);
        bw.Write((float)size.y);
        bw.Write((float)offset.x);
        bw.Write((float)offset.y);
        bw.Write(active);
    }

    public void Deserialize(BinaryReader br)
    {
        position.x = (DemonicsFloat)br.ReadSingle();
        position.y = (DemonicsFloat)br.ReadSingle();
        size.x = (DemonicsFloat)br.ReadSingle();
        size.y = (DemonicsFloat)br.ReadSingle();
        offset.x = (DemonicsFloat)br.ReadSingle();
        offset.y = (DemonicsFloat)br.ReadSingle();
        active = br.ReadBoolean();
    }

};
[Serializable]
public class PlayerNetwork
{
    public Player player;
    public PlayerNetwork otherPlayer;
    public PlayerStatsSO playerStats;
    public DemonicsVector2 position;
    public DemonicsVector2 velocity;
    public DemonicsVector2 pushbackStart;
    public DemonicsVector2 pushbackEnd;
    public AttackSO attack;
    public AttackNetwork attackNetwork;
    public AttackNetwork attackHurtNetwork;
    public Vector2Int direction;
    public string animation;
    public int animationFrames;
    public int attackFrames;
    public int stunFrames;
    public int combo;
    public int comboTimer;
    public ComboTimerStarterEnum comboTimerStarter;
    public InputEnum attackInput;
    public int health;
    public int healthRecoverable;
    public int arcana;
    public int shadow;
    public int flip;
    public int knockback;
    public string sound;
    public string soundStop;
    public float jump;
    public bool isCrouch;
    public bool isAir;
    public int dashDirection;
    public int jumpDirection;
    public int dashFrames;
    public int pushbackDuration;
    public bool invincible;
    public bool inPushback;
    public bool canDash;
    public bool hasJumped;
    public bool canJump;
    public bool canDoubleJump;
    public bool attackPress;
    public bool arcanaPress;
    public bool grabPress;
    public bool blueFrenzyPress;
    public bool redFrenzyPress;
    public bool enter;
    public bool wasWallSplatted;
    public bool canChainAttack;
    public string state;
    public int spriteOrder;
    public State CurrentState;
    public ColliderNetwork hurtbox;
    public ColliderNetwork hitbox;
    public ColliderNetwork pushbox;
    public InputBufferNetwork inputBuffer;
    public EffectNetwork[] effects;
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
        bw.Write(direction.x);
        bw.Write(direction.y);
        bw.Write(animation);
        bw.Write(animationFrames);
        bw.Write(attackFrames);
        bw.Write(stunFrames);
        bw.Write((int)attackInput);
        bw.Write(health);
        bw.Write(healthRecoverable);
        bw.Write(arcana);
        bw.Write(shadow);
        bw.Write(sound);
        bw.Write(soundStop);
        bw.Write(canDash);
        bw.Write(jump);
        bw.Write(knockback);
        bw.Write(combo);
        bw.Write(comboTimer);
        bw.Write((int)comboTimerStarter);
        bw.Write(isCrouch);
        bw.Write(isAir);
        bw.Write(hasJumped);
        bw.Write(pushbackDuration);
        bw.Write(canJump);
        bw.Write(invincible);
        bw.Write(canDoubleJump);
        bw.Write(dashDirection);
        bw.Write(jumpDirection);
        bw.Write(dashFrames);
        bw.Write(attackPress);
        bw.Write(arcanaPress);
        bw.Write(grabPress);
        bw.Write(blueFrenzyPress);
        bw.Write(redFrenzyPress);
        bw.Write(wasWallSplatted);
        bw.Write(enter);
        bw.Write(inPushback);
        bw.Write(canChainAttack);
        bw.Write(flip);
        bw.Write(spriteOrder);
        bw.Write(state);
        inputBuffer.Serialize(bw);
        hurtbox.Serialize(bw);
        hitbox.Serialize(bw);
        attackNetwork.Serialize(bw);
        attackHurtNetwork.Serialize(bw);
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
        direction.x = br.ReadInt32();
        direction.y = br.ReadInt32();
        animation = br.ReadString();
        animationFrames = br.ReadInt32();
        attackFrames = br.ReadInt32();
        stunFrames = br.ReadInt32();
        attackInput = (InputEnum)br.ReadInt32();
        health = br.ReadInt32();
        healthRecoverable = br.ReadInt32();
        arcana = br.ReadInt32();
        shadow = br.ReadInt32();
        sound = br.ReadString();
        soundStop = br.ReadString();
        canDash = br.ReadBoolean();
        jump = br.ReadSingle();
        knockback = br.ReadInt32();
        combo = br.ReadInt32();
        comboTimer = br.ReadInt32();
        comboTimerStarter = (ComboTimerStarterEnum)br.ReadInt32();
        isCrouch = br.ReadBoolean();
        isAir = br.ReadBoolean();
        hasJumped = br.ReadBoolean();
        pushbackDuration = br.ReadInt32();
        canJump = br.ReadBoolean();
        invincible = br.ReadBoolean();
        canDoubleJump = br.ReadBoolean();
        dashDirection = br.ReadInt32();
        jumpDirection = br.ReadInt32();
        dashFrames = br.ReadInt32();
        attackPress = br.ReadBoolean();
        arcanaPress = br.ReadBoolean();
        grabPress = br.ReadBoolean();
        blueFrenzyPress = br.ReadBoolean();
        redFrenzyPress = br.ReadBoolean();
        wasWallSplatted = br.ReadBoolean();
        enter = br.ReadBoolean();
        inPushback = br.ReadBoolean();
        canChainAttack = br.ReadBoolean();
        flip = br.ReadInt32();
        spriteOrder = br.ReadInt32();
        state = br.ReadString();
        inputBuffer.Deserialize(br);
        hurtbox.Deserialize(br);
        hitbox.Deserialize(br);
        attackNetwork.Deserialize(br);
        attackHurtNetwork.Deserialize(br);
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
};

[Serializable]
public struct GameSimulation : IGame
{
    public int Framenumber { get; private set; }
    public static int Hitstop { get; set; }
    public static int IntroFrame { get; set; }
    public static bool Start { get; set; }
    public static bool Run { get; set; }
    public int Checksum => GetHashCode();
    public static bool _introPlayed;
    public static PlayerNetwork[] _players;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(Framenumber);
        bw.Write(Hitstop);
        bw.Write(IntroFrame);
        bw.Write(Start);
        bw.Write(Run);
        bw.Write(_players.Length);
        for (int i = 0; i < _players.Length; ++i)
        {
            _players[i].Serialize(bw);
        }
    }

    public void Deserialize(BinaryReader br)
    {
        Framenumber = br.ReadInt32();
        Hitstop = br.ReadInt32();
        IntroFrame = br.ReadInt32();
        Start = br.ReadBoolean();
        Run = br.ReadBoolean();
        int length = br.ReadInt32();
        if (length != _players.Length)
        {
            _players = new PlayerNetwork[length];
        }
        for (int i = 0; i < _players.Length; ++i)
        {
            _players[i].Deserialize(br);
        }
    }

    public NativeArray<byte> ToBytes()
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
                Serialize(writer);
            }
            return new NativeArray<byte>(memoryStream.ToArray(), Allocator.Persistent);
        }
    }

    public void FromBytes(NativeArray<byte> bytes)
    {
        using (var memoryStream = new MemoryStream(bytes.ToArray()))
        {
            using (var reader = new BinaryReader(memoryStream))
            {
                Deserialize(reader);
            }
        }
    }

    public GameSimulation(PlayerStatsSO[] playerStats)
    {
        Framenumber = 0;
        Hitstop = 0;
        IntroFrame = -1000;
        _players = new PlayerNetwork[playerStats.Length];
        ObjectPoolingManager.Instance.PoolInitialize(playerStats[0]._effectsLibrary, playerStats[1]._effectsLibrary);
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i] = new PlayerNetwork();
            _players[i].inputBuffer.inputItems = new InputItemNetwork[20];
            _players[i].state = "Idle";
            _players[i].flip = 1;
            _players[i].position = new DemonicsVector2((DemonicsFloat)GameplayManager.Instance.GetSpawnPositions()[i], DemonicsPhysics.GROUND_POINT);
            _players[i].playerStats = playerStats[i];
            _players[i].health = playerStats[i].maxHealth;
            _players[i].healthRecoverable = playerStats[i].maxHealth;
            _players[i].attackInput = InputEnum.Direction;
            _players[i].animation = "Idle";
            _players[i].sound = "";
            _players[i].soundStop = "";
            _players[i].canJump = true;
            _players[i].canDoubleJump = true;
            _players[i].attackNetwork = new AttackNetwork() { name = "", attackSound = "", hurtEffect = "", impactSound = "" };
            _players[i].attackHurtNetwork = new AttackNetwork() { name = "", attackSound = "", hurtEffect = "", impactSound = "" };
            _players[i].effects = new EffectNetwork[playerStats[i]._effectsLibrary._objectPools.Count];
            _players[i].hitbox = new ColliderNetwork() { active = false };
            _players[i].hurtbox = new ColliderNetwork() { active = true };
            _players[i].pushbox = new ColliderNetwork() { active = true, size = new DemonicsVector2(22, 25), offset = new DemonicsVector2((DemonicsFloat)0, (DemonicsFloat)12.5) };

            for (int j = 0; j < _players[i].effects.Length; j++)
            {
                _players[i].effects[j] = new EffectNetwork();
                _players[i].effects[j].name = playerStats[i]._effectsLibrary._objectPools[j].groupName;
                _players[i].effects[j].animationMaxFrames = ObjectPoolingManager.Instance.GetObjectAnimation(i, _players[i].effects[j].name).GetMaxAnimationFrames();
                _players[i].effects[j].effectGroups = new EffectGroupNetwork[playerStats[i]._effectsLibrary._objectPools[j].size];
            }
        }
        _players[0].spriteOrder = 1;
        _players[1].spriteOrder = 0;
        _players[0].otherPlayer = _players[1];
        _players[1].otherPlayer = _players[0];
    }

    public void ParseInputs(long inputs, int i, out bool skip, out bool up, out bool down, out bool left, out bool right, out bool light, out bool medium,
    out bool heavy, out bool arcana, out bool grab, out bool shadow, out bool blueFrenzy, out bool redFrenzy, out bool dashForward, out bool dashBackward)
    {
        if ((inputs & NetworkInput.SKIP_BYTE) != 0)
        {
            skip = true;
        }
        else
        {
            skip = false;
        }
        if ((inputs & NetworkInput.UP_BYTE) != 0)
        {
            up = true;
        }
        else
        {
            up = false;
        }
        if ((inputs & NetworkInput.DOWN_BYTE) != 0)
        {
            down = true;
        }
        else
        {
            down = false;
        }
        if ((inputs & NetworkInput.LEFT_BYTE) != 0)
        {
            left = true;
        }
        else
        {
            left = false;
        }
        if ((inputs & NetworkInput.RIGHT_BYTE) != 0)
        {
            right = true;
        }
        else
        {
            right = false;
        }
        if ((inputs & NetworkInput.LIGHT_BYTE) != 0)
        {
            light = true;
        }
        else
        {
            light = false;
        }
        if ((inputs & NetworkInput.MEDIUM_BYTE) != 0)
        {
            medium = true;
        }
        else
        {
            medium = false;
        }
        if ((inputs & NetworkInput.HEAVY_BYTE) != 0)
        {
            heavy = true;
        }
        else
        {
            heavy = false;
        }
        if ((inputs & NetworkInput.ARCANA_BYTE) != 0)
        {
            arcana = true;
        }
        else
        {
            arcana = false;
        }
        if ((inputs & NetworkInput.GRAB_BYTE) != 0)
        {
            grab = true;
        }
        else
        {
            grab = false;
        }
        if ((inputs & NetworkInput.SHADOW_BYTE) != 0)
        {
            shadow = true;
        }
        else
        {
            shadow = false;
        }
        if ((inputs & NetworkInput.BLUE_FRENZY_BYTE) != 0)
        {
            blueFrenzy = true;
        }
        else
        {
            blueFrenzy = false;
        }
        if ((inputs & NetworkInput.RED_FRENZY_BYTE) != 0)
        {
            redFrenzy = true;
        }
        else
        {
            redFrenzy = false;
        }
        if ((inputs & NetworkInput.DASH_FORWARD_BYTE) != 0)
        {
            dashForward = true;
        }
        else
        {
            dashForward = false;
        }
        if ((inputs & NetworkInput.DASH_BACKWARD_BYTE) != 0)
        {
            dashBackward = true;
        }
        else
        {
            dashBackward = false;
        }
    }
    public void PlayerLogic(int index, bool skip, bool up, bool down, bool left, bool right, bool light, bool medium, bool heavy,
    bool arcana, bool grab, bool shadow, bool blueFrenzy, bool redFrenzy, bool dashForward, bool dashBackward)
    {
        if (GameSimulation.Run)
        {
            if (up)
            {
                _players[index].direction = new Vector2Int(0, 1);
            }
            if (down)
            {
                _players[index].direction = new Vector2Int(0, -1);
            }
            if (right)
            {
                _players[index].direction = new Vector2Int(1, _players[index].direction.y);
            }
            if (left)
            {
                _players[index].direction = new Vector2Int(-1, _players[index].direction.y);
            }
            if (dashForward)
            {
                _players[index].dashDirection = 1;
            }
            if (dashBackward)
            {
                _players[index].dashDirection = -1;
            }
            if (!left && !right)
            {
                _players[index].direction = new Vector2Int(0, _players[index].direction.y);
            }
            if (!up && !down)
            {
                _players[index].direction = new Vector2Int(_players[index].direction.x, 0);
            }
            if (light)
            {
                _players[index].inputBuffer.inputItems[0] = new InputItemNetwork() { inputEnum = InputEnum.Light, inputDirection = _players[index].direction, frame = Framenumber };
                _players[index].attackPress = true;
            }
            if (medium)
            {
                _players[index].inputBuffer.inputItems[0] = new InputItemNetwork() { inputEnum = InputEnum.Medium, inputDirection = _players[index].direction, frame = Framenumber };
                _players[index].attackPress = true;
            }
            if (heavy)
            {
                _players[index].inputBuffer.inputItems[0] = new InputItemNetwork() { inputEnum = InputEnum.Heavy, inputDirection = _players[index].direction, frame = Framenumber };
                _players[index].attackPress = true;
            }
            if (arcana)
            {
                _players[index].inputBuffer.inputItems[0] = new InputItemNetwork() { inputEnum = InputEnum.Special, inputDirection = _players[index].direction, frame = DemonicsWorld.Frame };
                _players[index].arcanaPress = true;
            }
            if (blueFrenzy)
            {
                _players[index].blueFrenzyPress = true;
            }
            if (redFrenzy)
            {
                _players[index].redFrenzyPress = true;
            }
            if (grab)
            {
                _players[index].inputBuffer.inputItems[0] = new InputItemNetwork() { inputEnum = InputEnum.Throw, inputDirection = _players[index].direction, frame = DemonicsWorld.Frame };
                _players[index].grabPress = true;
            }
            if (shadow)
            {
                if (_players[index].shadow > 1000)
                {
                    _players[index].shadow -= 1000;
                }
            }
            if (_players[index].arcana >= _players[index].playerStats.Arcana)
            {
                _players[index].arcana = _players[index].playerStats.Arcana;
            }
            else
            {
                _players[index].arcana += _players[index].playerStats.arcanaRecharge;
            }
            if (_players[index].shadow >= 2000)
            {
                _players[index].shadow = 2000;
            }
            else
            {
                _players[index].shadow += 3;
            }
        }
        else
        {
            _players[index].direction = Vector2Int.zero;
        }

        SetState(index);
        _players[index].CurrentState.UpdateLogic(_players[index]);
        _players[index].player.Flip(_players[index].flip);
        if (GameSimulation.Hitstop <= 0)
        {
            _players[index].player.PlayerAnimator.SpriteNormalEffect();
            if (!DemonicsPhysics.Collision(_players[index], _players[index].otherPlayer))
            {
                _players[index].position = new DemonicsVector2(_players[index].position.x + _players[index].velocity.x, _players[index].position.y + _players[index].velocity.y);
            }
        }
        _players[index].position = DemonicsPhysics.Bounds(_players[index]);
        DemonicsPhysics.CameraHorizontalBounds(_players[0], _players[1]);
        if (_players[index].player.IsAnimationFinished(_players[index].animation, _players[index].animationFrames))
        {
            if (_players[index].player.IsAnimationLoop(_players[index].animation))
            {
                _players[index].animationFrames = 0;
            }
            else
            {
                _players[index].animationFrames = _players[index].animationFrames + 1;
            }
        }
        for (int i = 0; i < _players[index].effects.Length; i++)
        {
            for (int j = 0; j < _players[index].effects[i].effectGroups.Length; j++)
            {
                if (_players[index].effects[i].effectGroups[j].active)
                {
                    _players[index].effects[i].effectGroups[j].animationFrames++;
                    if (_players[index].effects[i].effectGroups[j].animationFrames >= _players[index].effects[i].animationMaxFrames)
                    {
                        _players[index].effects[i].effectGroups[j].animationFrames = 0;
                        _players[index].effects[i].effectGroups[j].active = false;
                    }
                }
            }
        }
        AnimationBox[] animationHitboxes = _players[index].player.PlayerAnimator.GetHitboxes(_players[index].animation, _players[index].animationFrames);
        if (animationHitboxes.Length == 0)
        {
            _players[index].hitbox.active = false;
        }
        else
        {
            _players[index].hitbox.size = new DemonicsVector2((DemonicsFloat)animationHitboxes[0].size.x, (DemonicsFloat)animationHitboxes[0].size.y);
            _players[index].hitbox.offset = new DemonicsVector2((DemonicsFloat)animationHitboxes[0].offset.x, (DemonicsFloat)animationHitboxes[0].offset.y);
            _players[index].hitbox.position = new DemonicsVector2(_players[index].position.x + (_players[index].hitbox.offset.x * _players[index].flip), _players[index].position.y + _players[index].hitbox.offset.y);
            _players[index].hitbox.active = true;
        }
        AnimationBox[] animationHurtboxes = _players[index].player.PlayerAnimator.GetHurtboxes(_players[index].animation, _players[index].animationFrames);
        if (animationHurtboxes.Length > 0)
        {
            _players[index].hurtbox.active = true;
            _players[index].hurtbox.size = new DemonicsVector2((DemonicsFloat)animationHurtboxes[0].size.x, (DemonicsFloat)animationHurtboxes[0].size.y);
            _players[index].hurtbox.offset = new DemonicsVector2((DemonicsFloat)animationHurtboxes[0].offset.x, (DemonicsFloat)animationHurtboxes[0].offset.y);
        }
        _players[index].hurtbox.position = _players[index].position + _players[index].hurtbox.offset;
        _players[index].pushbox.position = _players[index].position + _players[index].pushbox.offset;
        _players[index].attackPress = false;
        _players[index].arcanaPress = false;
        _players[index].grabPress = false;
        _players[index].blueFrenzyPress = false;
        _players[index].redFrenzyPress = false;
    }
    public void Update(long[] inputs, int disconnect_flags)
    {
        if (Time.timeScale > 0 && GameplayManager.Instance)
        {
            if (Framenumber == 0)
            {
                GameSimulation.Start = true;
            }
            Framenumber++;
            DemonicsWorld.Frame = Framenumber;
            if (IntroFrame < 0 && IntroFrame > -1000)
            {
                GameSimulation.Run = true;
            }
            if (!GameSimulation.Run)
            {
                if ((inputs[0] & NetworkInput.SKIP_BYTE) != 0 && Framenumber > 200 && !_introPlayed)
                {
                    _introPlayed = true;
                    _players[0].enter = false;
                    _players[1].enter = false;
                    _players[0].state = "Taunt";
                    _players[1].state = "Taunt";
                    GameplayManager.Instance.SkipIntro();
                }
                IntroFrame--;
            }
            if (_players[0].player != null)
            {
                for (int i = 0; i < _players.Length; i++)
                {
                    bool skip = false;
                    bool up = false;
                    bool down = false;
                    bool left = false;
                    bool right = false;
                    bool light = false;
                    bool medium = false;
                    bool heavy = false;
                    bool arcana = false;
                    bool grab = false;
                    bool shadow = false;
                    bool blueFrenzy = false;
                    bool redFrenzy = false;
                    bool dashForward = false;
                    bool dashBackward = false;
                    if ((disconnect_flags & (1 << i)) != 0)
                    {
                        //AI
                    }
                    else
                    {
                        ParseInputs(inputs[i], i, out skip, out up, out down, out left, out right, out light, out medium, out heavy, out arcana,
                         out grab, out shadow, out blueFrenzy, out redFrenzy, out dashForward, out dashBackward);
                    }
                    PlayerLogic(i, skip, up, down, left, right, light, medium, heavy, arcana, grab, shadow, blueFrenzy, redFrenzy, dashForward, dashBackward);
                }
                Hitstop--;
            }
        }
    }

    private void SetState(int index)
    {
        if (_players[index].state == "Attack")
        {
            _players[index].CurrentState = new AttackState();
        }
        if (_players[index].state == "Arcana")
        {
            _players[index].CurrentState = new ArcanaState();
        }
        if (_players[index].state == "Hurt")
        {
            _players[index].CurrentState = new HurtState();
        }
        if (_players[index].state == "DashAir")
        {
            _players[index].CurrentState = new DashAirState();
        }
        if (_players[index].state == "Dash")
        {
            _players[index].CurrentState = new DashState();
        }
        if (_players[index].state == "Idle")
        {
            _players[index].CurrentState = new IdleState();
        }
        if (_players[index].state == "Walk")
        {
            _players[index].CurrentState = new WalkState();
        }
        if (_players[index].state == "Run")
        {
            _players[index].CurrentState = new RunState();
        }
        if (_players[index].state == "JumpForward")
        {
            _players[index].CurrentState = new JumpForwardState();
        }
        if (_players[index].state == "Crouch")
        {
            _players[index].CurrentState = new CrouchState();
        }
        if (_players[index].state == "Jump")
        {
            _players[index].CurrentState = new JumpState();
        }
        if (_players[index].state == "Fall")
        {
            _players[index].CurrentState = new FallState();
        }
        if (_players[index].state == "Block")
        {
            _players[index].CurrentState = new BlockState();
        }
        if (_players[index].state == "BlockLow")
        {
            _players[index].CurrentState = new BlockLowState();
        }
        if (_players[index].state == "BlockAir")
        {
            _players[index].CurrentState = new BlockAirState();
        }
        if (_players[index].state == "HurtAir")
        {
            _players[index].CurrentState = new HurtAirState();
        }
        if (_players[index].state == "Airborne")
        {
            _players[index].CurrentState = new HurtAirborneState();
        }
        if (_players[index].state == "WallSplat")
        {
            _players[index].CurrentState = new WallSplatState();
        }
        if (_players[index].state == "SoftKnockdown")
        {
            _players[index].CurrentState = new KnockdownSoftState();
        }
        if (_players[index].state == "HardKnockdown")
        {
            _players[index].CurrentState = new KnockdownHardState();
        }
        if (_players[index].state == "Knockback")
        {
            _players[index].CurrentState = new KnockbackState();
        }
        if (_players[index].state == "WakeUp")
        {
            _players[index].CurrentState = new WakeUpState();
        }
        if (_players[index].state == "Death")
        {
            _players[index].CurrentState = new DeathState();
        }
        if (_players[index].state == "Taunt")
        {
            _players[index].CurrentState = new TauntState();
        }
        if (_players[index].state == "BlueFrenzy")
        {
            _players[index].CurrentState = new BlueFrenzyState();
        }
        if (_players[index].state == "RedFrenzy")
        {
            _players[index].CurrentState = new RedFrenzyState();
        }
        if (_players[index].state == "Grab")
        {
            _players[index].CurrentState = new GrabState();
        }
        if (_players[index].state == "Grabbed")
        {
            _players[index].CurrentState = new GrabbedState();
        }
        if (_players[index].state == "Throw")
        {
            _players[index].CurrentState = new ThrowState();
        }
    }

    public long ReadInputs(int id)
    {
        long input = 0;
        if (id == 0)
        {
            if (Input.anyKeyDown)
            {
                input |= NetworkInput.SKIP_BYTE;
            }
            if (NetworkInput.ONE_UP_INPUT)
            {
                input |= NetworkInput.UP_BYTE;
            }
            if (NetworkInput.ONE_DOWN_INPUT)
            {
                input |= NetworkInput.DOWN_BYTE;
            }
            if (NetworkInput.ONE_LEFT_INPUT)
            {
                input |= NetworkInput.LEFT_BYTE;
            }
            if (NetworkInput.ONE_RIGHT_INPUT)
            {
                input |= NetworkInput.RIGHT_BYTE;
            }
            if (NetworkInput.ONE_LIGHT_INPUT)
            {
                input |= NetworkInput.LIGHT_BYTE;
                NetworkInput.ONE_LIGHT_INPUT = false;
            }
            if (NetworkInput.ONE_MEDIUM_INPUT)
            {
                input |= NetworkInput.MEDIUM_BYTE;
                NetworkInput.ONE_MEDIUM_INPUT = false;
            }
            if (NetworkInput.ONE_HEAVY_INPUT)
            {
                input |= NetworkInput.HEAVY_BYTE;
                NetworkInput.ONE_HEAVY_INPUT = false;
            }
            if (NetworkInput.ONE_ARCANA_INPUT)
            {
                input |= NetworkInput.ARCANA_BYTE;
                NetworkInput.ONE_ARCANA_INPUT = false;
            }
            if (NetworkInput.ONE_SHADOW_INPUT)
            {
                input |= NetworkInput.SHADOW_BYTE;
                NetworkInput.ONE_SHADOW_INPUT = false;
            }
            if (NetworkInput.ONE_GRAB_INPUT)
            {
                input |= NetworkInput.GRAB_BYTE;
                NetworkInput.ONE_GRAB_INPUT = false;
            }
            if (NetworkInput.ONE_BLUE_FRENZY_INPUT)
            {
                input |= NetworkInput.BLUE_FRENZY_BYTE;
                NetworkInput.ONE_BLUE_FRENZY_INPUT = false;
            }
            if (NetworkInput.ONE_RED_FRENZY_INPUT)
            {
                input |= NetworkInput.RED_FRENZY_BYTE;
                NetworkInput.ONE_RED_FRENZY_INPUT = false;
            }
            if (NetworkInput.ONE_DASH_FORWARD_INPUT)
            {
                input |= NetworkInput.DASH_FORWARD_BYTE;
                NetworkInput.ONE_DASH_FORWARD_INPUT = false;
            }
            if (NetworkInput.ONE_DASH_BACKWARD_INPUT)
            {
                input |= NetworkInput.DASH_BACKWARD_BYTE;
                NetworkInput.ONE_DASH_BACKWARD_INPUT = false;
            }
        }
        if (id == 1)
        {
            if (Input.anyKeyDown)
            {
                input |= NetworkInput.SKIP_BYTE;
            }
            if (NetworkInput.TWO_UP_INPUT)
            {
                input |= NetworkInput.UP_BYTE;
            }
            if (NetworkInput.TWO_DOWN_INPUT)
            {
                input |= NetworkInput.DOWN_BYTE;
            }
            if (NetworkInput.TWO_LEFT_INPUT)
            {
                input |= NetworkInput.LEFT_BYTE;
            }
            if (NetworkInput.TWO_RIGHT_INPUT)
            {
                input |= NetworkInput.RIGHT_BYTE;
            }
            if (NetworkInput.TWO_LIGHT_INPUT)
            {
                input |= NetworkInput.LIGHT_BYTE;
                NetworkInput.TWO_LIGHT_INPUT = false;
            }
            if (NetworkInput.TWO_MEDIUM_INPUT)
            {
                input |= NetworkInput.MEDIUM_BYTE;
                NetworkInput.TWO_MEDIUM_INPUT = false;
            }
            if (NetworkInput.TWO_HEAVY_INPUT)
            {
                input |= NetworkInput.HEAVY_BYTE;
                NetworkInput.TWO_HEAVY_INPUT = false;
            }
            if (NetworkInput.TWO_ARCANA_INPUT)
            {
                input |= NetworkInput.ARCANA_BYTE;
                NetworkInput.TWO_ARCANA_INPUT = false;
            }
            if (NetworkInput.TWO_SHADOW_INPUT)
            {
                input |= NetworkInput.SHADOW_BYTE;
                NetworkInput.TWO_SHADOW_INPUT = false;
            }
            if (NetworkInput.TWO_GRAB_INPUT)
            {
                input |= NetworkInput.GRAB_BYTE;
                NetworkInput.TWO_GRAB_INPUT = false;
            }
            if (NetworkInput.TWO_BLUE_FRENZY_INPUT)
            {
                input |= NetworkInput.BLUE_FRENZY_BYTE;
                NetworkInput.TWO_BLUE_FRENZY_INPUT = false;
            }
            if (NetworkInput.TWO_RED_FRENZY_INPUT)
            {
                input |= NetworkInput.RED_FRENZY_BYTE;
                NetworkInput.TWO_RED_FRENZY_INPUT = false;
            }
            if (NetworkInput.TWO_DASH_FORWARD_INPUT)
            {
                input |= NetworkInput.DASH_FORWARD_BYTE;
                NetworkInput.TWO_DASH_FORWARD_INPUT = false;
            }
            if (NetworkInput.TWO_DASH_BACKWARD_INPUT)
            {
                input |= NetworkInput.DASH_BACKWARD_BYTE;
                NetworkInput.TWO_DASH_BACKWARD_INPUT = false;
            }
        }
        return input;
    }

    public void FreeBytes(NativeArray<byte> data)
    {
        if (data.IsCreated)
        {
            data.Dispose();
        }
    }

    public override int GetHashCode()
    {
        int hashCode = -1214587014;
        hashCode = hashCode * -1521134295 + Framenumber.GetHashCode();
        foreach (var player in _players)
        {
            hashCode = hashCode * -1521134295 + player.GetHashCode();
        }
        return hashCode;
    }

    public void LogInfo(string filename)
    {
        //Log
    }
}
