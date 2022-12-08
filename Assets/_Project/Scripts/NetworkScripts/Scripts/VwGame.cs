using SharedGame;
using System;
using System.IO;
using Unity.Collections;
using UnityEngine;


[Serializable]
public struct PlayerNetwork
{
    public Vector2 position;
    public bool start;
    public bool skip;
    public bool up;
    public bool down;
    public bool left;
    public bool right;
    public bool light;
    public bool medium;
    public bool heavy;
    public bool arcana;
    public bool shadow;
    public bool grab;
    public bool blueFrenzy;
    public bool redFrenzy;
    public bool dashForward;
    public bool dashBackward;
    public void Serialize(BinaryWriter bw)
    {
        bw.Write(position.x);
        bw.Write(position.y);
        bw.Write(start);
        bw.Write(skip);
        bw.Write(up);
        bw.Write(down);
        bw.Write(left);
        bw.Write(right);
        bw.Write(light);
        bw.Write(medium);
        bw.Write(heavy);
        bw.Write(arcana);
        bw.Write(shadow);
        bw.Write(grab);
        bw.Write(blueFrenzy);
        bw.Write(redFrenzy);
        bw.Write(dashForward);
        bw.Write(dashBackward);
    }

    public void Deserialize(BinaryReader br)
    {
        position.x = br.ReadSingle();
        position.y = br.ReadSingle();
        start = br.ReadBoolean();
        skip = br.ReadBoolean();
        up = br.ReadBoolean();
        down = br.ReadBoolean();
        left = br.ReadBoolean();
        right = br.ReadBoolean();
        light = br.ReadBoolean();
        medium = br.ReadBoolean();
        heavy = br.ReadBoolean();
        arcana = br.ReadBoolean();
        shadow = br.ReadBoolean();
        grab = br.ReadBoolean();
        blueFrenzy = br.ReadBoolean();
        redFrenzy = br.ReadBoolean();
        dashForward = br.ReadBoolean();
        dashBackward = br.ReadBoolean();
    }
};

[Serializable]
public struct VwGame : IGame
{
    public int Framenumber { get; private set; }

    public int Checksum => GetHashCode();

    public PlayerNetwork[] _players;

    public static Rect _bounds = new Rect(0, 0, 640, 480);

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(Framenumber);
        bw.Write(_players.Length);
        for (int i = 0; i < _players.Length; ++i)
        {
            _players[i].Serialize(bw);
        }
    }

    public void Deserialize(BinaryReader br)
    {
        Framenumber = br.ReadInt32();
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

    public VwGame(int num_players)
    {
        Debug.Log("b");
        Framenumber = 0;
        _players = new PlayerNetwork[num_players];
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i] = new PlayerNetwork();
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
        var player = _players[index];
        _players[index].skip = skip;
        _players[index].up = up;
        _players[index].down = down;
        _players[index].left = left;
        _players[index].right = right;
        _players[index].light = light;
        _players[index].medium = medium;
        _players[index].heavy = heavy;
        _players[index].arcana = arcana;
        _players[index].grab = grab;
        _players[index].shadow = shadow;
        _players[index].blueFrenzy = blueFrenzy;
        _players[index].redFrenzy = redFrenzy;
        _players[index].dashForward = dashForward;
        _players[index].dashBackward = dashBackward;
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
        if (id == 0)
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