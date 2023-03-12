using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicsPhysics : MonoBehaviour
{
    [SerializeField] private bool _ignoreWalls = default;
    public DemonicsVector2 Velocity { get; set; }
    public DemonicsVector2 Position { get; set; }
    public bool OnGround { get { return Position.y <= GROUND_POINT ? true : false; } private set { } }
    public bool OnWall { get { return Position.x >= WALL_RIGHT_POINT || Position.x <= WALL_LEFT_POINT ? true : false; } private set { } }
    private DemonicsVector2 _freezePosition;
    private DemonicsFloat _gravity;
    private static Camera _camera;
    private bool _freeze;
    public static DemonicsFloat GROUND_POINT = (DemonicsFloat)(-72);
    public static DemonicsFloat CELLING_POINT = (DemonicsFloat)(120);
    public static DemonicsFloat WALL_RIGHT_POINT;
    public static DemonicsFloat WALL_LEFT_POINT;
    public static DemonicsFloat GRAVITY = (DemonicsFloat)0.288f;
    public static DemonicsFloat JUGGLE_GRAVITY = (DemonicsFloat)0.208f;
    private static DemonicsFloat WALL_OFFSET = (DemonicsFloat)10;
    private int _skipWallFrame = 1;
    public DemonicsPhysics OtherPhysics { get; set; }
    public bool IgnoreWalls { get { return _ignoreWalls; } set { _ignoreWalls = value; } }


    void Awake()
    {
        _camera = Camera.main;
    }

    public void OnCollision(DemonicsPhysics otherPhysics)
    {
        this.OtherPhysics = otherPhysics;
    }

    public void SetFreeze(bool state)
    {
        _freeze = state;
        if (_freeze)
        {
            _freezePosition = Position;
        }
    }

    public void ResetSkipWall()
    {
        _skipWallFrame = 2;
    }

    void FixedUpdate()
    {
        if (_freeze)
        {
            Position = _freezePosition;
            return;
        }
    }
    public static bool Collision(PlayerNetwork player, PlayerNetwork otherPlayer)
    {
        if (!player.pushbox.active || !otherPlayer.pushbox.active)
        {
            return false;
        }
        if (Colliding(player, otherPlayer))
        {
            if (player.position.y > otherPlayer.position.y)
            {
                if (player.velocity.y < otherPlayer.velocity.y)
                {
                    DemonicsFloat difference = DemonicsFloat.Abs(player.position.x - otherPlayer.position.x);
                    DemonicsFloat pushDistance = (player.pushbox.size.x - difference) / (2);
                    if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT)
                    {
                        player.position = new DemonicsVector2(player.position.x + pushDistance, player.position.y);
                    }
                    else if (player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT)
                    {
                        player.position = new DemonicsVector2(player.position.x - pushDistance, player.position.y);
                    }
                }
            }
            DemonicsVector2 main = player.velocity;
            DemonicsVector2 second = otherPlayer.velocity;
            if (otherPlayer.position.x >= DemonicsPhysics.WALL_RIGHT_POINT && player.velocity.x >= (DemonicsFloat)0 || otherPlayer.position.x <= DemonicsPhysics.WALL_LEFT_POINT && player.velocity.x <= (DemonicsFloat)0)
            {
                main = new DemonicsVector2((DemonicsFloat)0, player.velocity.y);
                second = new DemonicsVector2((DemonicsFloat)0, otherPlayer.velocity.y);
                otherPlayer.position = (new DemonicsVector2(otherPlayer.position.x + second.x, otherPlayer.position.y + second.y));
                player.position = (new DemonicsVector2(player.position.x + main.x, player.position.y + main.y));
                Intersects(player, otherPlayer);
                return true;
            }
            if (DemonicsFloat.Abs(player.velocity.x) > DemonicsFloat.Abs(otherPlayer.velocity.x))
            {
                DemonicsFloat totalVelocity;
                if (player.velocity.x > 0 && otherPlayer.velocity.x < 0)
                {
                    totalVelocity = DemonicsFloat.Abs(player.velocity.x) - DemonicsFloat.Abs(otherPlayer.velocity.x);
                }
                else
                {
                    totalVelocity = DemonicsFloat.Abs(player.velocity.x);
                }
                if (player.position.x < otherPlayer.position.x && player.velocity.x > 0)
                {
                    main = new DemonicsVector2(totalVelocity, player.velocity.y);
                    second = new DemonicsVector2(totalVelocity, otherPlayer.velocity.y);
                    otherPlayer.position = (new DemonicsVector2(otherPlayer.position.x + second.x, otherPlayer.position.y + second.y));
                    player.position = (new DemonicsVector2(player.position.x + main.x, player.position.y + main.y));
                    Intersects(player, otherPlayer);
                    return true;
                }
                else if (player.position.x > otherPlayer.position.x && player.velocity.x < 0)
                {
                    main = new DemonicsVector2(-totalVelocity, player.velocity.y);
                    second = new DemonicsVector2(-totalVelocity, otherPlayer.velocity.y);
                    otherPlayer.position = (new DemonicsVector2(otherPlayer.position.x + second.x, otherPlayer.position.y + second.y));
                    player.position = (new DemonicsVector2(player.position.x + main.x, player.position.y + main.y));
                    Intersects(player, otherPlayer);
                    return true;
                }
                if (player.velocity.x == (DemonicsFloat)0 || otherPlayer.velocity.x == (DemonicsFloat)0)
                {
                    Intersects(player, otherPlayer);
                }
                return false;
            }
            else if (DemonicsFloat.Abs(player.velocity.x) == DemonicsFloat.Abs(otherPlayer.velocity.x))
            {
                if (player.position.x < otherPlayer.position.x && player.velocity.x > 0 || player.position.x > otherPlayer.position.x && player.velocity.x < 0)
                {
                    main = new DemonicsVector2((DemonicsFloat)0, player.velocity.y);
                    second = new DemonicsVector2((DemonicsFloat)0, otherPlayer.velocity.y);
                    player.position = (new DemonicsVector2(player.position.x + main.x, player.position.y + main.y));
                    Intersects(player, otherPlayer);
                    return true;
                }
                Intersects(player, otherPlayer);
                return false;
            }
            return true;
        }
        return false;
    }
    private static void Intersects(PlayerNetwork player, PlayerNetwork otherPlayer)
    {
        if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT || player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT)
        {
            return;
        }
        if (player.position.x < otherPlayer.position.x)
        {
            if (player.position.x + player.pushbox.size.x >= otherPlayer.position.x)
            {
                DemonicsFloat difference = DemonicsFloat.Abs(player.position.x - otherPlayer.position.x);
                int pushDistance = (int)(player.pushbox.size.x - difference) / (2);
                player.position = new DemonicsVector2((player.position.x - pushDistance), player.position.y);
            }
        }
        else
        {
            if (player.position.x <= otherPlayer.position.x + player.pushbox.size.x)
            {
                DemonicsFloat difference = DemonicsFloat.Abs(player.position.x - otherPlayer.position.x);
                int pushDistance = (int)(player.pushbox.size.x - difference) / (2);
                player.position = new DemonicsVector2((player.position.x + pushDistance), player.position.y);
            }
        }
    }
    private static bool valueInRange(DemonicsFloat value, DemonicsFloat min, DemonicsFloat max)
    { return (value >= min) && (value <= max); }
    private static bool Colliding(PlayerNetwork a, PlayerNetwork b)
    {
        bool xOverlap = valueInRange(a.position.x - (a.pushbox.size.x / 2), b.position.x - (b.pushbox.size.x / 2), b.position.x + (b.pushbox.size.x / 2)) ||
                    valueInRange(b.position.x - (b.pushbox.size.x / 2), a.position.x - (a.pushbox.size.x / 2), a.position.x + (a.pushbox.size.x / 2));
        bool yOverlap = valueInRange(a.position.y - (a.pushbox.size.y / 2), b.position.y - (b.pushbox.size.y / 2), b.position.y + (b.pushbox.size.y / 2)) ||
                    valueInRange(b.position.y - (b.pushbox.size.y / 2), a.position.y - (a.pushbox.size.y / 2), a.position.y + (a.pushbox.size.y / 2));
        return xOverlap && yOverlap;
    }
    public static void CameraHorizontalBounds(PlayerNetwork player, PlayerNetwork otherPlayer)
    {
        DemonicsFloat distance = DemonicsFloat.Abs((DemonicsFloat)player.position.x - (DemonicsFloat)otherPlayer.position.x);
        if (distance >= (DemonicsFloat)220)
        {
            if (player.position.x > otherPlayer.position.x)
            {
                WALL_LEFT_POINT = (DemonicsFloat)(otherPlayer.position.x);
                WALL_RIGHT_POINT = (DemonicsFloat)(player.position.x);
            }
            else
            {
                WALL_LEFT_POINT = (DemonicsFloat)(player.position.x);
                WALL_RIGHT_POINT = (DemonicsFloat)(otherPlayer.position.x);
            }
        }
        else
        {
            WALL_LEFT_POINT = (DemonicsFloat)(-169);
            WALL_RIGHT_POINT = (DemonicsFloat)(169);
        }
    }

    public void SetPositionWithRender(DemonicsVector2 position)
    {
        Position = position;
        transform.position = new Vector2((float)Position.x, (float)Position.y);
    }
    public static DemonicsVector2 Bounds(PlayerNetwork player)
    {
        DemonicsVector2 position = player.position;
        if (player.position.y >= CELLING_POINT && player.velocity.y > 0)
        {
            //CHECK FOR CELLING
        }
        if (player.position.x >= WALL_RIGHT_POINT && player.velocity.x > (DemonicsFloat)0)
        {
            if (player.position.y <= GROUND_POINT)
            {
                position = new DemonicsVector2(WALL_RIGHT_POINT, GROUND_POINT);
            }
            else
            {
                position = new DemonicsVector2(WALL_RIGHT_POINT, player.position.y);
            }
        }
        else if (player.position.x <= WALL_LEFT_POINT && player.velocity.x < (DemonicsFloat)0)
        {
            if (player.position.y <= GROUND_POINT)
            {
                position = new DemonicsVector2(WALL_LEFT_POINT, GROUND_POINT);
            }
            else
            {
                position = new DemonicsVector2(WALL_LEFT_POINT, player.position.y);
            }
        }
        else
        {
            if (player.position.y <= GROUND_POINT)
            {
                position = new DemonicsVector2(player.position.x, GROUND_POINT);
            }
        }
        return position;
    }
    public static bool IsInCorner(PlayerNetwork player)
    {
        if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT || player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT)
        {
            return true;
        }
        return false;
    }
}
