using UnityEngine;

public class DemonicsPhysics : MonoBehaviour
{
    [SerializeField] private bool _ignoreWalls = default;
    public DemonVector2 Velocity { get; set; }
    public DemonVector2 Position { get; set; }
    public bool OnGround { get { return Position.y <= GROUND_POINT ? true : false; } private set { } }
    public bool OnWall { get { return Position.x >= WALL_RIGHT_POINT || Position.x <= WALL_LEFT_POINT ? true : false; } private set { } }
    private DemonVector2 _freezePosition;
    private bool _freeze;
    public static DemonFloat FOOT_POINT = (DemonFloat)(-80);
    public static DemonFloat GROUND_POINT = (DemonFloat)(-72);
    public static DemonFloat CELLING_POINT = (DemonFloat)120;
    public static DemonFloat WALL_RIGHT_POINT;
    public static DemonFloat WALL_LEFT_POINT;
    public static DemonFloat GRAVITY = (DemonFloat)0.235f;
    public static DemonFloat JUGGLE_GRAVITY = (DemonFloat)0.208f;
    public DemonicsPhysics OtherPhysics { get; set; }
    public bool IgnoreWalls { get { return _ignoreWalls; } set { _ignoreWalls = value; } }


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
                    DemonFloat difference = DemonFloat.Abs(player.position.x - otherPlayer.position.x);
                    DemonFloat pushDistance = (player.pushbox.size.x - difference) / (2);
                    if (player.position.x <= DemonicsPhysics.WALL_LEFT_POINT)
                    {
                        player.position = new DemonVector2(player.position.x + pushDistance, player.position.y);
                    }
                    else if (player.position.x >= DemonicsPhysics.WALL_RIGHT_POINT)
                    {
                        player.position = new DemonVector2(player.position.x - pushDistance, player.position.y);
                    }
                }
            }
            DemonVector2 main = player.velocity;
            DemonVector2 second = otherPlayer.velocity;
            if (otherPlayer.position.x >= DemonicsPhysics.WALL_RIGHT_POINT && player.velocity.x >= (DemonFloat)0 || otherPlayer.position.x <= DemonicsPhysics.WALL_LEFT_POINT && player.velocity.x <= (DemonFloat)0)
            {
                main = new DemonVector2((DemonFloat)0, player.velocity.y);
                second = new DemonVector2((DemonFloat)0, otherPlayer.velocity.y);
                otherPlayer.position = (new DemonVector2(otherPlayer.position.x + second.x, otherPlayer.position.y + second.y));
                player.position = (new DemonVector2(player.position.x + main.x, player.position.y + main.y));
                Intersects(player, otherPlayer);
                return true;
            }
            if (DemonFloat.Abs(player.velocity.x) > DemonFloat.Abs(otherPlayer.velocity.x))
            {
                DemonFloat totalVelocity;
                if (player.velocity.x > 0 && otherPlayer.velocity.x < 0)
                {
                    totalVelocity = DemonFloat.Abs(player.velocity.x) - DemonFloat.Abs(otherPlayer.velocity.x);
                }
                else
                {
                    totalVelocity = DemonFloat.Abs(player.velocity.x);
                }
                if (player.position.x < otherPlayer.position.x && player.velocity.x > 0)
                {
                    main = new DemonVector2(totalVelocity, player.velocity.y);
                    second = new DemonVector2(totalVelocity, otherPlayer.velocity.y);
                    otherPlayer.position = (new DemonVector2(otherPlayer.position.x + second.x, otherPlayer.position.y + second.y));
                    player.position = (new DemonVector2(player.position.x + main.x, player.position.y + main.y));
                    Intersects(player, otherPlayer);
                    return true;
                }
                else if (player.position.x > otherPlayer.position.x && player.velocity.x < 0)
                {
                    main = new DemonVector2(-totalVelocity, player.velocity.y);
                    second = new DemonVector2(-totalVelocity, otherPlayer.velocity.y);
                    otherPlayer.position = (new DemonVector2(otherPlayer.position.x + second.x, otherPlayer.position.y + second.y));
                    player.position = (new DemonVector2(player.position.x + main.x, player.position.y + main.y));
                    Intersects(player, otherPlayer);
                    return true;
                }
                if (player.velocity.x == (DemonFloat)0 || otherPlayer.velocity.x == (DemonFloat)0)
                {
                    Intersects(player, otherPlayer);
                }
                return false;
            }
            else if (DemonFloat.Abs(player.velocity.x) == DemonFloat.Abs(otherPlayer.velocity.x))
            {
                if (player.position.x < otherPlayer.position.x && player.velocity.x > 0 || player.position.x > otherPlayer.position.x && player.velocity.x < 0)
                {
                    main = new DemonVector2((DemonFloat)0, player.velocity.y);
                    second = new DemonVector2((DemonFloat)0, otherPlayer.velocity.y);
                    player.position = (new DemonVector2(player.position.x + main.x, player.position.y + main.y));
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
                DemonFloat difference = DemonFloat.Abs(player.position.x - otherPlayer.position.x);
                int pushDistance = (int)(player.pushbox.size.x - difference) / (2);
                player.position = new DemonVector2((player.position.x - pushDistance), player.position.y);
            }
        }
        else
        {
            if (player.position.x <= otherPlayer.position.x + player.pushbox.size.x)
            {
                DemonFloat difference = DemonFloat.Abs(player.position.x - otherPlayer.position.x);
                int pushDistance = (int)(player.pushbox.size.x - difference) / (2);
                player.position = new DemonVector2((player.position.x + pushDistance), player.position.y);
            }
        }
    }
    private static bool valueInRange(DemonFloat value, DemonFloat min, DemonFloat max)
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
        DemonFloat distance = DemonFloat.Abs((DemonFloat)player.position.x - (DemonFloat)otherPlayer.position.x);
        if (distance >= (DemonFloat)220)
        {
            if (player.position.x > otherPlayer.position.x)
            {
                WALL_LEFT_POINT = (DemonFloat)(otherPlayer.position.x);
                WALL_RIGHT_POINT = (DemonFloat)(player.position.x);
            }
            else
            {
                WALL_LEFT_POINT = (DemonFloat)(player.position.x);
                WALL_RIGHT_POINT = (DemonFloat)(otherPlayer.position.x);
            }
        }
        else
        {
            WALL_LEFT_POINT = (DemonFloat)(-169);
            WALL_RIGHT_POINT = (DemonFloat)(169);
        }
    }

    public void SetPositionWithRender(DemonVector2 position)
    {
        Position = position;
        transform.position = new Vector2((float)Position.x, (float)Position.y);
    }
    public static DemonVector2 Bounds(PlayerNetwork player)
    {
        DemonVector2 position = player.position;
        if (player.position.y >= CELLING_POINT && player.velocity.y > 0)
        {
            //CHECK FOR CELLING
        }
        if (player.position.x >= WALL_RIGHT_POINT && player.velocity.x > (DemonFloat)0)
        {
            if (player.position.y <= GROUND_POINT)
            {
                position = new DemonVector2(WALL_RIGHT_POINT, GROUND_POINT);
            }
            else
            {
                position = new DemonVector2(WALL_RIGHT_POINT, player.position.y);
            }
        }
        else if (player.position.x <= WALL_LEFT_POINT && player.velocity.x < (DemonFloat)0)
        {
            if (player.position.y <= GROUND_POINT)
            {
                position = new DemonVector2(WALL_LEFT_POINT, GROUND_POINT);
            }
            else
            {
                position = new DemonVector2(WALL_LEFT_POINT, player.position.y);
            }
        }
        else
        {
            if (player.position.y <= GROUND_POINT)
            {
                position = new DemonVector2(player.position.x, GROUND_POINT);
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
