using SharedGame;
using System;
using System.IO;
using Unity.Collections;
using UnityEngine;


using static VWConstants;

public static class VWConstants
{
    public const int MAX_SHIPS = 4;
    public const int MAX_PLAYERS = 2;

    public const int INPUT_THRUST = (1 << 0);
    public const int INPUT_BREAK = (1 << 1);
    public const int INPUT_ROTATE_LEFT = (1 << 2);
    public const int INPUT_ROTATE_RIGHT = (1 << 3);
    public const int INPUT_FIRE = (1 << 4);
    public const int INPUT_BOMB = (1 << 5);
    public const int MAX_BULLETS = 30;

    public const float PI = 3.1415926f;
    public const int STARTING_HEALTH = 100;
    public const float ROTATE_INCREMENT = 3f;
    public const float SHIP_RADIUS = 15f;
    public const float SHIP_THRUST = 0.06f;
    public const float SHIP_MAX_THRUST = 4.0f;
    public const float SHIP_BREAK_SPEED = 0.6f;
    public const float BULLET_SPEED = 5f;
    public const int BULLET_COOLDOWN = 8;
    public const int BULLET_DAMAGE = 10;
}

[Serializable]
public struct PlayerNetwork
{
    public Vector2 position;
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
    public void Serialize(BinaryWriter bw)
    {
        bw.Write(position.x);
        bw.Write(position.y);
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
    }

    public void Deserialize(BinaryReader br)
    {
        position.x = br.ReadSingle();
        position.y = br.ReadSingle();
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

    private static float DegToRad(float deg)
    {
        return PI * deg / 180;
    }

    private static float Distance(Vector2 lhs, Vector2 rhs)
    {
        float x = rhs.x - lhs.x;
        float y = rhs.y - lhs.y;
        return Mathf.Sqrt(x * x + y * y);
    }

    /*
     * InitGameState --
     *
     * Initialize our game state.
     */

    public VwGame(int num_players)
    {
        var w = _bounds.xMax - _bounds.xMin;
        var h = _bounds.yMax - _bounds.yMin;
        var r = h / 4;
        Framenumber = 0;
        _players = new PlayerNetwork[num_players];
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i] = new PlayerNetwork();
            _players[i].position.x = 0;
            _players[i].position.y = 0;
        }
    }

    public void ParseInputs(long inputs, int i, out bool up, out bool down, out bool left, out bool right, out bool light, out bool medium, out bool heavy, out bool arcana, out bool grab, out bool shadow, out bool blueFrenzy, out bool redFrenzy)
    {
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
    }

    public void MoveShip(int index, bool up, bool down, bool left, bool right, bool light, bool medium, bool heavy, bool arcana, bool grab, bool shadow, bool blueFrenzy, bool redFrenzy)
    {
        var player = _players[index];
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
    }

    public void Update(long[] inputs, int disconnect_flags)
    {
        Framenumber++;
        for (int i = 0; i < _players.Length; i++)
        {
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
            if ((disconnect_flags & (1 << i)) != 0)
            {
                //AI
            }
            else
            {
                ParseInputs(inputs[i], i, out up, out down, out left, out right, out light, out medium, out heavy, out arcana, out grab, out shadow, out blueFrenzy, out redFrenzy);
            }
            MoveShip(i, up, down, left, right, light, medium, heavy, arcana, grab, shadow, blueFrenzy, redFrenzy);
        }
    }

    public long ReadInputs(int id)
    {
        long input = 0;
        if (id == 0)
        {
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