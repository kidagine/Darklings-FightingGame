using SharedGame;
using System;
using System.IO;
using Unity.Collections;
using UnityEngine;

[Serializable]
public struct JumpEffectNetwork
{
    public Vector2 position;
    public int animationFrames;
    public int animationMaxFrames;
    public bool active;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(position.x);
        bw.Write(position.y);
        bw.Write(animationFrames);
        bw.Write(animationMaxFrames);
        bw.Write(active);
    }

    public void Deserialize(BinaryReader br)
    {
        position.x = br.ReadSingle();
        position.y = br.ReadSingle();
        animationFrames = br.ReadInt32();
        animationMaxFrames = br.ReadInt32();
        active = br.ReadBoolean();
    }
};
[Serializable]
public struct PlayerNetwork
{
    public PlayerStatsSO playerStats;
    public Vector2 position;
    public Vector2 velocity;
    public Vector2 direction;
    public string animation;
    public int animationFrames;
    public string sound;
    public float speed;
    public float gravity;
    public float jump;
    public int dashFrames;
    public bool jumped;
    public bool start;
    public bool skip;
    public States CurrentState;
    public JumpEffectNetwork jumpEffect;
    public void Serialize(BinaryWriter bw)
    {
        bw.Write(position.x);
        bw.Write(position.y);
        bw.Write(velocity.x);
        bw.Write(velocity.y);
        bw.Write(direction.x);
        bw.Write(direction.y);
        bw.Write(animation);
        bw.Write(animationFrames);
        bw.Write(sound);
        bw.Write(speed);
        bw.Write(gravity);
        bw.Write(jump);
        bw.Write(jumped);
        bw.Write(dashFrames);
        bw.Write(start);
        bw.Write(skip);
        CurrentState.Serialize(bw);
        jumpEffect.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        position.x = br.ReadSingle();
        position.y = br.ReadSingle();
        velocity.x = br.ReadSingle();
        velocity.y = br.ReadSingle();
        direction.x = br.ReadSingle();
        direction.y = br.ReadSingle();
        animation = br.ReadString();
        animationFrames = br.ReadInt32();
        sound = br.ReadString();
        speed = br.ReadSingle();
        gravity = br.ReadSingle();
        jump = br.ReadSingle();
        jumped = br.ReadBoolean();
        dashFrames = br.ReadInt32();
        start = br.ReadBoolean();
        skip = br.ReadBoolean();
        CurrentState.Deserialize(br);
        jumpEffect.Deserialize(br);
    }

    public override int GetHashCode()
    {
        int hashCode = 1858597544;
        hashCode = hashCode * -1521134295 + position.GetHashCode();
        hashCode = hashCode * -1521134295 + start.GetHashCode();
        hashCode = hashCode * -1521134295 + animation.GetHashCode();
        hashCode = hashCode * -1521134295 + sound.GetHashCode();
        hashCode = hashCode * -1521134295 + skip.GetHashCode();
        return hashCode;
    }
};

[Serializable]
public struct VwGame : IGame
{
    public int Framenumber { get; private set; }
    public int NextFramenumber { get; private set; }
    public int Checksum => GetHashCode();

    public PlayerNetwork[] _players;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(Framenumber);
        bw.Write(NextFramenumber);
        bw.Write(_players.Length);
        for (int i = 0; i < _players.Length; ++i)
        {
            _players[i].Serialize(bw);
        }
    }

    public void Deserialize(BinaryReader br)
    {
        Framenumber = br.ReadInt32();
        NextFramenumber = br.ReadInt32();
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

    /*
     * InitGameState --
     *
     * Initialize our game state.
     */

    public VwGame(PlayerStatsSO[] playerStats)
    {
        ObjectPoolingManager.Instance.PoolInitialize();
        Framenumber = 0;
        NextFramenumber = 1;
        _players = new PlayerNetwork[playerStats.Length];
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i] = new PlayerNetwork();
            _players[i].CurrentState = new IdleStates();
            _players[i].position = new Vector2(GameplayManager.Instance.GetSpawnPositions()[i], (float)DemonicsPhysics.GROUND_POINT);
            _players[i].playerStats = playerStats[i];
            _players[i].animation = "Idle";
            _players[i].sound = "";
            _players[i].gravity = 0.018f;
            _players[i].jumpEffect.animationMaxFrames = ObjectPoolingManager.Instance.GetObjectAnimation("s").GetMaxAnimationFrames("s");
        }
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
        if (up)
        {
            _players[index].direction = new Vector2(0, 1);
        }
        if (down)
        {
            _players[index].direction = new Vector2(0, -1);
        }
        if (right)
        {
            _players[index].direction = new Vector2(1, _players[index].direction.y);
        }
        if (left)
        {
            _players[index].direction = new Vector2(-1, _players[index].direction.y);
        }
        if (!left && !right)
        {
            _players[index].direction = new Vector2(0, _players[index].direction.y);
        }
        if (!up && !down)
        {
            _players[index].direction = new Vector2(_players[index].direction.x, 0);
        }
        if (dashForward)
        {
            if (_players[index].CurrentState.ToDashState())
            {
                _players[index].dashFrames = 15;
                _players[index].direction = new Vector2(1, 0);
                _players[index].CurrentState = _players[index].CurrentState.NextState;
                _players[index].CurrentState.Enter(_players[index]);
                _players[index].gravity = _players[index].CurrentState.Gravity;
                _players[index].animation = _players[index].CurrentState.Animation;
                _players[index].velocity = _players[index].CurrentState.velocity;
                _players[index].sound = _players[index].CurrentState.Sound;
                NextFramenumber = Framenumber + 1;
            }
        }
        if (dashBackward)
        {
            if (_players[index].CurrentState.ToDashState())
            {
                _players[index].dashFrames = 15;
                _players[index].direction = new Vector2(-1, 0);
                _players[index].CurrentState = _players[index].CurrentState.NextState;
                _players[index].CurrentState.Enter(_players[index]);
                _players[index].gravity = _players[index].CurrentState.Gravity;
                _players[index].animation = _players[index].CurrentState.Animation;
                _players[index].velocity = _players[index].CurrentState.velocity;
                _players[index].sound = _players[index].CurrentState.Sound;
                NextFramenumber = Framenumber + 1;
            }
        }
        _players[index].CurrentState.UpdateLogic(_players[index]);
        if (_players[index].CurrentState.NextState != null && _players[index].CurrentState.NextState != _players[index].CurrentState)
        {
            _players[index].CurrentState = _players[index].CurrentState.NextState;
            _players[index].CurrentState.Enter(_players[index]);
            _players[index].animation = _players[index].CurrentState.Animation;
            _players[index].velocity = _players[index].CurrentState.velocity;
            _players[index].gravity = _players[index].CurrentState.Gravity;
            if (_players[index].CurrentState.Sound != "")
            {
                _players[index].sound = _players[index].CurrentState.Sound;
                NextFramenumber = Framenumber + 1;
            }
        }
        if (!string.IsNullOrEmpty(_players[index].CurrentState.Effect))
        {
            _players[index].jumpEffect.active = true;
            _players[index].jumpEffect.position = _players[index].CurrentState.EffectPosition;
        }
        if (_players[index].CurrentState.AnimationFrames >= 0)
        {
            _players[index].animationFrames = _players[index].CurrentState.AnimationFrames;
        }
        _players[index].velocity = new Vector2(_players[index].CurrentState.velocity.x, _players[index].velocity.y);
        _players[index].velocity = new Vector2(_players[index].velocity.x, _players[index].velocity.y - _players[index].gravity);

        _players[index].position = new Vector2(_players[index].position.x + _players[index].velocity.x, _players[index].position.y + _players[index].velocity.y);
        if ((DemonicsFloat)_players[index].position.y <= DemonicsPhysics.GROUND_POINT)
        {
            _players[index].position = new Vector2(_players[index].position.x, (float)DemonicsPhysics.GROUND_POINT);
        }
        if ((DemonicsFloat)_players[index].position.x >= DemonicsPhysics.WALL_RIGHT_POINT && (DemonicsFloat)_players[index].velocity.x >= (DemonicsFloat)0)
        {
            _players[index].position = new Vector2((float)DemonicsPhysics.WALL_RIGHT_POINT, _players[index].position.y);
        }
        if ((DemonicsFloat)_players[index].position.x <= DemonicsPhysics.WALL_LEFT_POINT && (DemonicsFloat)_players[index].velocity.x <= (DemonicsFloat)0)
        {
            _players[index].position = new Vector2((float)DemonicsPhysics.WALL_LEFT_POINT, _players[index].position.y);
        }
        _players[index].dashFrames--;
        if (GameplayManager.Instance.PlayerOne)
        {
            if (GameplayManager.Instance.PlayerOne.IsAnimationFinished())
            {
                _players[0].CurrentState.AnimationFrames = 0;
                _players[0].animationFrames = 0;
            }
        }
        if (GameplayManager.Instance.PlayerTwo)
        {
            if (GameplayManager.Instance.PlayerTwo.IsAnimationFinished())
            {
                _players[1].CurrentState.AnimationFrames = 0;
                _players[1].animationFrames = 0;
            }
        }
        if (_players[index].jumpEffect.active)
        {
            _players[index].jumpEffect.animationFrames++;
        }
        if (_players[index].jumpEffect.animationFrames >= _players[index].jumpEffect.animationMaxFrames)
        {
            _players[index].CurrentState.Effect = "";
            _players[index].jumpEffect.animationFrames = 0;
            _players[index].jumpEffect.active = false;
        }
        NextFrameReset(index);
    }

    private void NextFrameReset(int index)
    {
        if (NextFramenumber <= Framenumber)
        {
            if (!string.IsNullOrEmpty(_players[index].sound))
            {
                if (index == 0)
                {
                    GameplayManager.Instance.PlayerOne.PlaySound(_players[index].sound);
                    _players[index].sound = "";
                    NextFramenumber = 0;
                }
                else
                {
                    GameplayManager.Instance.PlayerTwo.PlaySound(_players[index].sound);
                    _players[index].sound = "";
                    NextFramenumber = 0;
                }
            }
        }
    }

    public void Update(long[] inputs, int disconnect_flags)
    {
        Framenumber++;
        DemonicsWorld.Frame = Framenumber;
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
            _players[i].start = true;
        }
    }

    public long ReadInputs(int id)
    {
        long input = 0;
        if (id == 2)
        {
            if (Input.anyKeyDown)
            {
                input |= NetworkInput.SKIP_BYTE;
            }
            if (NetworkInput.UP_INPUT)
            {
                input |= NetworkInput.UP_BYTE;
            }
            if (NetworkInput.DOWN_INPUT)
            {
                input |= NetworkInput.DOWN_BYTE;
            }
            if (NetworkInput.LEFT_INPUT)
            {
                input |= NetworkInput.LEFT_BYTE;
            }
            if (NetworkInput.RIGHT_INPUT)
            {
                input |= NetworkInput.RIGHT_BYTE;
            }
            if (NetworkInput.LIGHT_INPUT)
            {
                input |= NetworkInput.LIGHT_BYTE;
                NetworkInput.LIGHT_INPUT = false;
            }
            if (NetworkInput.MEDIUM_INPUT)
            {
                input |= NetworkInput.MEDIUM_BYTE;
                NetworkInput.MEDIUM_INPUT = false;
            }
            if (NetworkInput.HEAVY_INPUT)
            {
                input |= NetworkInput.HEAVY_BYTE;
                NetworkInput.HEAVY_INPUT = false;
            }
            if (NetworkInput.ARCANA_INPUT)
            {
                input |= NetworkInput.ARCANA_BYTE;
                NetworkInput.ARCANA_INPUT = false;
            }
            if (NetworkInput.SHADOW_INPUT)
            {
                input |= NetworkInput.SHADOW_BYTE;
                NetworkInput.SHADOW_INPUT = false;
            }
            if (NetworkInput.GRAB_INPUT)
            {
                input |= NetworkInput.GRAB_BYTE;
                NetworkInput.GRAB_INPUT = false;
            }
            if (NetworkInput.BLUE_FRENZY_INPUT)
            {
                input |= NetworkInput.BLUE_FRENZY_BYTE;
                NetworkInput.BLUE_FRENZY_INPUT = false;
            }
            if (NetworkInput.RED_FRENZY_INPUT)
            {
                input |= NetworkInput.RED_FRENZY_BYTE;
                NetworkInput.RED_FRENZY_INPUT = false;
            }
            if (NetworkInput.DASH_FORWARD_INPUT)
            {
                input |= NetworkInput.DASH_FORWARD_BYTE;
                NetworkInput.DASH_FORWARD_INPUT = false;
            }
            if (NetworkInput.DASH_BACKWARD_INPUT)
            {
                input |= NetworkInput.DASH_BACKWARD_BYTE;
                NetworkInput.DASH_BACKWARD_INPUT = false;
            }
        }
        if (id == 1)
        {
            if (Input.anyKeyDown)
            {
                input |= NetworkInput.SKIP_BYTE;
            }
            if (NetworkInput.UP_INPUT)
            {
                input |= NetworkInput.UP_BYTE;
            }
            if (NetworkInput.DOWN_INPUT)
            {
                input |= NetworkInput.DOWN_BYTE;
            }
            if (NetworkInput.LEFT_INPUT)
            {
                input |= NetworkInput.LEFT_BYTE;
            }
            if (NetworkInput.RIGHT_INPUT)
            {
                input |= NetworkInput.RIGHT_BYTE;
            }
            if (NetworkInput.LIGHT_INPUT)
            {
                input |= NetworkInput.LIGHT_BYTE;
                NetworkInput.LIGHT_INPUT = false;
            }
            if (NetworkInput.MEDIUM_INPUT)
            {
                input |= NetworkInput.MEDIUM_BYTE;
                NetworkInput.MEDIUM_INPUT = false;
            }
            if (NetworkInput.HEAVY_INPUT)
            {
                input |= NetworkInput.HEAVY_BYTE;
                NetworkInput.HEAVY_INPUT = false;
            }
            if (NetworkInput.ARCANA_INPUT)
            {
                input |= NetworkInput.ARCANA_BYTE;
                NetworkInput.ARCANA_INPUT = false;
            }
            if (NetworkInput.SHADOW_INPUT)
            {
                input |= NetworkInput.SHADOW_BYTE;
                NetworkInput.SHADOW_INPUT = false;
            }
            if (NetworkInput.GRAB_INPUT)
            {
                input |= NetworkInput.GRAB_BYTE;
                NetworkInput.GRAB_INPUT = false;
            }
            if (NetworkInput.BLUE_FRENZY_INPUT)
            {
                input |= NetworkInput.BLUE_FRENZY_BYTE;
                NetworkInput.BLUE_FRENZY_INPUT = false;
            }
            if (NetworkInput.RED_FRENZY_INPUT)
            {
                input |= NetworkInput.RED_FRENZY_BYTE;
                NetworkInput.RED_FRENZY_INPUT = false;
            }
            if (NetworkInput.DASH_FORWARD_INPUT)
            {
                input |= NetworkInput.DASH_FORWARD_BYTE;
                NetworkInput.DASH_FORWARD_INPUT = false;
            }
            if (NetworkInput.DASH_BACKWARD_INPUT)
            {
                input |= NetworkInput.DASH_BACKWARD_BYTE;
                NetworkInput.DASH_BACKWARD_INPUT = false;
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
public class States
{
    public States NextState;
    public string Animation;
    public int AnimationFrames;
    public float Gravity;
    public string Sound;
    public string Effect;
    public Vector2 EffectPosition;
    public Vector2 velocity;
    public virtual void Enter(PlayerNetwork player)
    {
        Sound = "";
        Gravity = 0.018f;
    }
    public virtual void UpdateLogic(PlayerNetwork player) { }
    public virtual void Exit() { }
    public virtual Vector2 GetVelocity() { return Vector2.zero; }
    public virtual bool ToAttackState() { return false; }
    public virtual bool ToDashState() { return false; }
    public void Serialize(BinaryWriter bw)
    {
        bw.Write(velocity.x);
        bw.Write(velocity.y);
    }

    public void Deserialize(BinaryReader br)
    {
        velocity.x = br.ReadSingle();
        velocity.y = br.ReadSingle();
    }
};
public class GroundParentStates : States
{
    //TODO
}
public class IdleStates : GroundParentStates
{
    public override void Enter(PlayerNetwork player)
    {
        base.Enter(player);
        Animation = "Idle";
    }

    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        velocity = Vector2.zero;
        ToWalkState(player.direction.x);
        ToJumpState(player.direction.y);
        ToJumpForwardState(player);
        ToCrouchState(player.direction.y);
        AnimationFrames++;
    }

    private void ToWalkState(float directionX)
    {
        if (directionX != 0)
        {
            NextState = new WalkStates();
        }
    }

    private void ToJumpState(float directionY)
    {
        if (directionY > 0)
        {
            NextState = new JumpStates();
        }
    }
    private void ToJumpForwardState(PlayerNetwork player)
    {
        if (player.direction.y > 0 && player.direction.x != 0)
        {
            NextState = new JumpForwardStates();
        }
    }
    private void ToCrouchState(float directionY)
    {
        if (directionY < 0)
        {
            NextState = new CrouchStates();
        }
    }
    public override bool ToAttackState()
    {
        NextState = new AttackStates();
        return true;
    }
    public override bool ToDashState()
    {
        NextState = new DashStates();
        return true;
    }
}
public class CrouchStates : GroundParentStates
{
    public override void Enter(PlayerNetwork player)
    {
        base.Enter(player);
        Animation = "Crouch";
    }

    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        velocity = Vector2.zero;
        ToIdleState(player.direction.y);
    }

    private void ToIdleState(float directionY)
    {
        if (directionY >= 0)
        {
            NextState = new IdleStates();
        }
    }
}
public class WalkStates : GroundParentStates
{
    public override void Enter(PlayerNetwork player)
    {
        base.Enter(player);
        Animation = "Walk";
    }

    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        velocity = new Vector2(player.direction.x * (float)player.playerStats.SpeedWalk, 0);
        ToIdleState(player.direction.x);
        ToJumpForwardState(player.direction.y);
        ToCrouchState(player.direction.y);
        AnimationFrames++;
    }

    private void ToIdleState(float directionX)
    {
        if (directionX == 0)
        {
            NextState = new IdleStates();
        }
    }

    private void ToJumpForwardState(float directionY)
    {
        if (directionY > 0)
        {
            NextState = new JumpForwardStates();
        }
    }

    private void ToCrouchState(float directionY)
    {
        if (directionY < 0)
        {
            NextState = new CrouchStates();
        }
    }
}

public class DashStates : States
{
    public override void Enter(PlayerNetwork player)
    {
        base.Enter(player);
        Animation = "Dash";
        Sound = "Dash";
        velocity = new Vector2(player.direction.x * (float)player.playerStats.DashForce, 0);
        NextState = null;
    }

    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        Dash(player);
    }

    private void Dash(PlayerNetwork player)
    {
        if (player.dashFrames == 0)
        {
            if (player.direction.x != 0)
            {
                NextState = new RunStates();
            }
            else
            {
                NextState = new IdleStates();
            }
        }
    }
}

public class DashAirState : States
{
    public override void Enter(PlayerNetwork player)
    {
        Animation = "Dash";
        Sound = "Dash";
        velocity = new Vector2(player.direction.x * (float)player.playerStats.DashForce, 0);
        Gravity = 0;
        NextState = null;
    }

    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        Dash(player);
    }

    private void Dash(PlayerNetwork player)
    {
        if (player.dashFrames == 0)
        {
            if (player.direction.x != 0)
            {
                NextState = new RunStates();
            }
            else
            {
                NextState = new IdleStates();
            }
        }
    }
}

public class RunStates : States
{
    public override void Enter(PlayerNetwork player)
    {
        base.Enter(player);
        Animation = "Run";
    }
    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        velocity = new Vector2(player.direction.x * (float)player.playerStats.SpeedRun, 0);
        ToIdleState(player.direction.x);
    }

    private void ToIdleState(float directionX)
    {
        if (directionX == 0)
        {
            NextState = new IdleStates();
        }
    }
}

public class JumpStates : States
{
    public override void Enter(PlayerNetwork player)
    {
        base.Enter(player);
        Animation = "Jump";
        Sound = "Jump";
        Effect = "Jump";
        EffectPosition = player.position;
        velocity = new Vector2(player.velocity.x, (float)player.playerStats.JumpForce);
    }

    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        ToIdleState(player.position.y);
        //ToJumpState(player.direction.y);
        ToFallState(player.velocity.y);
        AnimationFrames++;
    }
    public override Vector2 GetVelocity() { return velocity; }

    private void ToIdleState(float positionY)
    {
        if ((DemonicsFloat)positionY == DemonicsPhysics.GROUND_POINT)
        {
            NextState = new IdleStates();
        }
    }
    private void ToJumpState(float directionY)
    {
        if (directionY > 0)
        {
            NextState = new JumpStates();
        }
    }
    private void ToFallState(float velocityY)
    {
        if (velocityY <= 0)
        {
            NextState = new FallStates();
        }
    }

    public override bool ToDashState()
    {
        NextState = new DashAirState();
        return true;
    }
}

public class JumpForwardStates : States
{
    public override void Enter(PlayerNetwork player)
    {
        base.Enter(player);
        Animation = "JumpForward";
        Sound = "Jump";
        Effect = "Jump";
        EffectPosition = player.position;
        velocity = new Vector2(0.14f * player.direction.x, (float)player.playerStats.JumpForce);
    }

    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        ToIdleState(player.position.y);
        AnimationFrames++;
    }

    private void ToIdleState(float positionY)
    {
        if ((DemonicsFloat)positionY == DemonicsPhysics.GROUND_POINT)
        {
            NextState = new IdleStates();
        }
    }
}
public class FallStates : States
{
    public override void Enter(PlayerNetwork player)
    {
        base.Enter(player);
        Animation = "Jump";
        AnimationFrames = -1;
    }

    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        ToIdleState(player);
    }

    private void ToIdleState(PlayerNetwork player)
    {
        if ((DemonicsFloat)player.position.y == DemonicsPhysics.GROUND_POINT && (DemonicsFloat)player.velocity.y <= (DemonicsFloat)0)
        {
            Sound = "Jump";
            NextState = new IdleStates();
        }
    }
    public override bool ToDashState()
    {
        NextState = new DashAirState();
        return true;
    }
}
public class AttackStates : States
{
    public override void Enter(PlayerNetwork player)
    {
        base.Enter(player);
        Animation = "5M";
        AnimationFrames = 0;
    }

    public override void UpdateLogic(PlayerNetwork player)
    {
        base.UpdateLogic(player);
        ToIdleState(player);
        AnimationFrames++;
    }

    private void ToIdleState(PlayerNetwork player)
    {
        if (AnimationFrames >= 50)
        {
            NextState = new IdleStates();
        }
    }
}