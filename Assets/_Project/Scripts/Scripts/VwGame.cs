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
public struct Bullet
{
    public bool active;
    public Vector2 position;
    public Vector2 velocity;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(active);
        bw.Write(position.x);
        bw.Write(position.y);
        bw.Write(velocity.x);
        bw.Write(velocity.y);
    }

    public void Deserialize(BinaryReader br)
    {
        active = br.ReadBoolean();
        position.x = br.ReadSingle();
        position.y = br.ReadSingle();
        velocity.x = br.ReadSingle();
        velocity.y = br.ReadSingle();
    }
};

[Serializable]
public class Ship
{
    public Vector2 position;
    public Vector2 velocity;
    public float radius;
    public float heading;
    public int light;
    public int health;
    public int cooldown;
    public int score;
    public Bullet[] bullets = new Bullet[MAX_BULLETS];

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(position.x);
        bw.Write(position.y);
        bw.Write(velocity.x);
        bw.Write(velocity.y);
        bw.Write(radius);
        bw.Write(heading);
        bw.Write(health);
        bw.Write(cooldown);
        bw.Write(score);
        for (int i = 0; i < MAX_BULLETS; ++i)
        {
            bullets[i].Serialize(bw);
        }
    }

    public void Deserialize(BinaryReader br)
    {
        position.x = br.ReadSingle();
        position.y = br.ReadSingle();
        velocity.x = br.ReadSingle();
        velocity.y = br.ReadSingle();
        radius = br.ReadSingle();
        heading = br.ReadSingle();
        health = br.ReadInt32();
        cooldown = br.ReadInt32();
        score = br.ReadInt32();
        for (int i = 0; i < MAX_BULLETS; ++i)
        {
            bullets[i].Deserialize(br);
        }
    }

    // @LOOK Not hashing bullets.
    public override int GetHashCode()
    {
        int hashCode = 1858597544;
        hashCode = hashCode * -1521134295 + position.GetHashCode();
        hashCode = hashCode * -1521134295 + velocity.GetHashCode();
        hashCode = hashCode * -1521134295 + radius.GetHashCode();
        hashCode = hashCode * -1521134295 + heading.GetHashCode();
        hashCode = hashCode * -1521134295 + health.GetHashCode();
        hashCode = hashCode * -1521134295 + cooldown.GetHashCode();
        hashCode = hashCode * -1521134295 + score.GetHashCode();
        return hashCode;
    }
}

[Serializable]
public struct VwGame : IGame
{
    public int Framenumber { get; private set; }

    public int Checksum => GetHashCode();

    public Ship[] _ships;
    public PlayerNetwork[] _players;

    public static Rect _bounds = new Rect(0, 0, 640, 480);

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(Framenumber);
        bw.Write(_ships.Length);
        for (int i = 0; i < _ships.Length; ++i)
        {
            _ships[i].Serialize(bw);
        }
        for (int i = 0; i < _players.Length; ++i)
        {
            _players[i].Serialize(bw);
        }
    }

    public void Deserialize(BinaryReader br)
    {
        Framenumber = br.ReadInt32();
        int length = br.ReadInt32();
        if (length != _ships.Length)
        {
            _ships = new Ship[length];
        }
        for (int i = 0; i < _ships.Length; ++i)
        {
            _ships[i].Deserialize(br);
        }
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
        _ships = new Ship[num_players];
        _players = new PlayerNetwork[num_players];
        for (int i = 0; i < _ships.Length; i++)
        {
            _ships[i] = new Ship();
            int heading = i * 360 / num_players;
            float cost, sint, theta;

            theta = (float)heading * PI / 180;
            cost = Mathf.Cos(theta);
            sint = Mathf.Sin(theta);

            _ships[i].position.x = (w / 2) + r * cost;
            _ships[i].position.y = (h / 2) + r * sint;
            _ships[i].heading = (heading + 180) % 360;
            _ships[i].health = STARTING_HEALTH;
            _ships[i].radius = SHIP_RADIUS;
        }
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i] = new PlayerNetwork();
            _players[i].position.x = 0;
            _players[i].position.y = 0;
        }
    }

    public void GetShipAI(int i, out float heading, out float thrust, out int fire)
    {
        heading = (_ships[i].heading + 5) % 360;
        thrust = 0;
        fire = 0;
    }

    public void ParseShipInputs(long inputs, int i, out float heading, out float thrust, out int fire)
    {
        var ship = _ships[i];

        GGPORunner.LogGame($"parsing ship {i} inputs: {inputs}.");

        if ((inputs & INPUT_ROTATE_RIGHT) != 0)
        {
            heading = 0.1f;
        }
        else if ((inputs & INPUT_ROTATE_LEFT) != 0)
        {
            heading = -0.1f;
        }
        else
        {
            heading = 0;
        }


        if ((inputs & INPUT_THRUST) != 0)
        {
            thrust = SHIP_THRUST;
        }
        else if ((inputs & INPUT_BREAK) != 0)
        {
            thrust = -SHIP_THRUST;
        }
        else
        {
            thrust = 0;
        }
        fire = (int)(inputs & INPUT_FIRE);
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

    public void MoveShip(int index, float heading, float thrust, int fire, bool up, bool down, bool left, bool right, bool light, bool medium, bool heavy, bool arcana, bool grab, bool shadow, bool blueFrenzy, bool redFrenzy)
    {
        var ship = _ships[index];
        var player = _players[index];

        GGPORunner.LogGame($"calculation of new ship coordinates: (thrust:{thrust} heading:{heading}).");

        ship.heading = heading;
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
        _players[index].position = new Vector2(heading, 0);
        if (ship.cooldown == 0)
        {
            if (fire != 0)
            {
                GGPORunner.LogGame("firing bullet.");
                for (int i = 0; i < ship.bullets.Length; i++)
                {
                    float dx = Mathf.Cos(DegToRad(ship.heading));
                    float dy = Mathf.Sin(DegToRad(ship.heading));
                    if (!ship.bullets[i].active)
                    {
                        ship.bullets[i].active = true;
                        ship.bullets[i].position.x = ship.position.x + (ship.radius * dx);
                        ship.bullets[i].position.y = ship.position.y + (ship.radius * dy);
                        ship.bullets[i].velocity.x = ship.velocity.x + (BULLET_SPEED * dx);
                        ship.bullets[i].velocity.y = ship.velocity.y + (BULLET_SPEED * dy);
                        ship.cooldown = BULLET_COOLDOWN;
                        break;
                    }
                }
            }
        }

        if (thrust != 0)
        {
            float dx = thrust * Mathf.Cos(DegToRad(heading));
            float dy = thrust * Mathf.Sin(DegToRad(heading));

            ship.velocity.x += dx;
            ship.velocity.y += dy;
            float mag = Mathf.Sqrt(ship.velocity.x * ship.velocity.x +
                             ship.velocity.y * ship.velocity.y);
            if (mag > SHIP_MAX_THRUST)
            {
                ship.velocity.x = (ship.velocity.x * SHIP_MAX_THRUST) / mag;
                ship.velocity.y = (ship.velocity.y * SHIP_MAX_THRUST) / mag;
            }
        }
        GGPORunner.LogGame($"new ship velocity: (dx:{ship.velocity.x} dy:{ship.velocity.y}).");

        ship.position.x += ship.velocity.x;
        ship.position.y += ship.velocity.y;
        GGPORunner.LogGame($"new ship position: (dx:{ship.position.x} dy:{ship.position.y}).");

        if (ship.position.x - ship.radius < _bounds.xMin ||
            ship.position.x + ship.radius > _bounds.xMax)
        {
            ship.velocity.x *= -1;
            ship.position.x += (ship.velocity.x * 2);
        }
        if (ship.position.y - ship.radius < _bounds.yMin ||
            ship.position.y + ship.radius > _bounds.yMax)
        {
            ship.velocity.y *= -1;
            ship.position.y += (ship.velocity.y * 2);
        }
        for (int i = 0; i < ship.bullets.Length; i++)
        {
            if (ship.bullets[i].active)
            {
                ship.bullets[i].position.x += ship.bullets[i].velocity.x;
                ship.bullets[i].position.y += ship.bullets[i].velocity.y;
                if (ship.bullets[i].position.x < _bounds.xMin ||
                    ship.bullets[i].position.y < _bounds.yMin ||
                    ship.bullets[i].position.x > _bounds.xMax ||
                    ship.bullets[i].position.y > _bounds.yMax)
                {
                    ship.bullets[i].active = false;
                }
                else
                {
                    for (int j = 0; j < _ships.Length; j++)
                    {
                        var other = _ships[j];
                        if (Distance(ship.bullets[i].position, other.position) < other.radius)
                        {
                            ship.score++;
                            other.health -= BULLET_DAMAGE;
                            ship.bullets[i].active = false;
                            break;
                        }
                    }
                }
            }
        }
    }

    public void LogInfo(string filename)
    {
        string fp = "";
        fp += "GameState object.\n";
        fp += string.Format("  bounds: {0},{1} x {2},{3}.\n", _bounds.xMin, _bounds.yMin, _bounds.xMax, _bounds.yMax);
        fp += string.Format("  num_ships: {0}.\n", _ships.Length);
        for (int i = 0; i < _ships.Length; i++)
        {
            var ship = _ships[i];
            fp += string.Format("  ship {0} position:  %.4f, %.4f\n", i, ship.position.x, ship.position.y);
            fp += string.Format("  ship {0} velocity:  %.4f, %.4f\n", i, ship.velocity.x, ship.velocity.y);
            fp += string.Format("  ship {0} radius:    %d.\n", i, ship.radius);
            fp += string.Format("  ship {0} heading:   %d.\n", i, ship.heading);
            fp += string.Format("  ship {0} health:    %d.\n", i, ship.health);
            fp += string.Format("  ship {0} cooldown:  %d.\n", i, ship.cooldown);
            fp += string.Format("  ship {0} score:     {1}.\n", i, ship.score);
            for (int j = 0; j < ship.bullets.Length; j++)
            {
                fp += string.Format("  ship {0} bullet {1}: {2} {3} -> {4} {5}.\n", i, j,
                        ship.bullets[j].position.x, ship.bullets[j].position.y,
                        ship.bullets[j].velocity.x, ship.bullets[j].velocity.y);
            }
        }
        File.WriteAllText(filename, fp);
    }

    public void Update(long[] inputs, int disconnect_flags)
    {
        Framenumber++;
        for (int i = 0; i < _players.Length; i++)
        {
            float thrust, heading;
            int fire;
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
                GetShipAI(i, out heading, out thrust, out fire);
            }
            else
            {
                ParseShipInputs(inputs[i], i, out heading, out thrust, out fire);
                ParseInputs(inputs[i], i, out up, out down, out left, out right, out light, out medium, out heavy, out arcana, out grab, out shadow, out blueFrenzy, out redFrenzy);
            }
            MoveShip(i, heading, thrust, fire, up, down, left, right, light, medium, heavy, arcana, grab, shadow, blueFrenzy, redFrenzy);

            if (_ships[i].cooldown != 0)
            {
                _ships[i].cooldown--;
            }
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
                NetworkInput.UP_INPUT = false;
            }
            if (NetworkInput.DOWN_INPUT)
            {
                input |= NetworkInput.DOWN_BYTE;
                NetworkInput.DOWN_INPUT = false;
            }
            if (NetworkInput.LEFT_INPUT)
            {
                input |= NetworkInput.LEFT_BYTE;
                NetworkInput.LEFT_INPUT = false;
            }
            if (NetworkInput.RIGHT_INPUT)
            {
                input |= NetworkInput.RIGHT_BYTE;
                NetworkInput.RIGHT_INPUT = false;
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
}