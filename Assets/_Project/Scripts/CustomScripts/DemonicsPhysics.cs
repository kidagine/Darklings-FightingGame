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
    private Camera _camera;
    private bool _freeze;
    public static DemonicsFloat GROUND_POINT = (DemonicsFloat)(-4.485);
    public static DemonicsFloat CELLING_POINT = (DemonicsFloat)(7);
    public static DemonicsFloat WALL_RIGHT_POINT;
    public static DemonicsFloat WALL_LEFT_POINT;
    private int _skipWallFrame = 1;
    private readonly DemonicsFloat _wallPointOffset = (DemonicsFloat)0.6;
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
        // Stop physics if frozen
        if (_freeze)
        {
            Position = _freezePosition;
            return;
        }
        // Set horizontal wall points
        CameraHorizontalBounds();
        // Sets physics
        Velocity = new DemonicsVector2(Velocity.x, Velocity.y - _gravity);
        // Check collision


    }
    public static bool Collision(PlayerNetwork player, PlayerNetwork otherPlayer)
    {
        if (Colliding(player, otherPlayer))
        {
            if (player.position.y > otherPlayer.position.y)
            {
                if (player.velocity.y < otherPlayer.velocity.y)
                {
                    float difference = Mathf.Abs(player.position.x - otherPlayer.position.x);
                    float pushDistance = (1.35f - difference) / (2);
                    if (player.position.x <= (float)DemonicsPhysics.WALL_LEFT_POINT)
                    {
                        player.position = new Vector2(player.position.x + pushDistance, player.position.y);
                    }
                    else if (player.position.x >= (float)DemonicsPhysics.WALL_RIGHT_POINT)
                    {
                        player.position = new Vector2(player.position.x - pushDistance, player.position.y);
                    }
                }
            }
            Vector2 main = player.velocity;
            Vector2 second = otherPlayer.velocity;
            if (otherPlayer.position.x >= (float)DemonicsPhysics.WALL_RIGHT_POINT && player.velocity.x >= 0 || otherPlayer.position.x <= (float)DemonicsPhysics.WALL_LEFT_POINT && player.velocity.x <= 0)
            {
                main = new Vector2(0, player.velocity.y);
                second = new Vector2(0, otherPlayer.velocity.y);
                otherPlayer.position = (new Vector2(otherPlayer.position.x + second.x, otherPlayer.position.y + second.y));
                player.position = (new Vector2(player.position.x + main.x, player.position.y + main.y));
                Intersects(player, otherPlayer);
                return true;
            }
            if (Mathf.Abs(player.velocity.x) > Mathf.Abs(otherPlayer.velocity.x))
            {
                float totalVelocity;
                if (player.velocity.x > 0 && otherPlayer.velocity.x < 0)
                {
                    totalVelocity = Mathf.Abs(player.velocity.x) - Mathf.Abs(otherPlayer.velocity.x);
                }
                else
                {
                    totalVelocity = Mathf.Abs(player.velocity.x);
                }
                if (player.position.x < otherPlayer.position.x && player.velocity.x > 0)
                {
                    main = new Vector2(totalVelocity, player.velocity.y);
                    second = new Vector2(totalVelocity, otherPlayer.velocity.y);
                    otherPlayer.position = (new Vector2(otherPlayer.position.x + second.x, otherPlayer.position.y + second.y));
                    player.position = (new Vector2(player.position.x + main.x, player.position.y + main.y));
                    Intersects(player, otherPlayer);
                    return true;
                }
                else if (player.position.x > otherPlayer.position.x && player.velocity.x < 0)
                {
                    main = new Vector2(-totalVelocity, player.velocity.y);
                    second = new Vector2(-totalVelocity, otherPlayer.velocity.y);
                    otherPlayer.position = (new Vector2(otherPlayer.position.x + second.x, otherPlayer.position.y + second.y));
                    player.position = (new Vector2(player.position.x + main.x, player.position.y + main.y));
                    Intersects(player, otherPlayer);
                    return true;
                }
                if (player.velocity.x == 0 || otherPlayer.velocity.x == 0)
                {
                    Intersects(player, otherPlayer);
                }
                return false;
            }
            else if (Mathf.Abs(player.velocity.x) == Mathf.Abs(otherPlayer.velocity.x))
            {
                if (player.position.x < otherPlayer.position.x && player.velocity.x > 0 || player.position.x > otherPlayer.position.x && player.velocity.x < 0)
                {
                    main = new Vector2(0, player.velocity.y);
                    second = new Vector2(0, otherPlayer.velocity.y);
                    player.position = (new Vector2(player.position.x + main.x, player.position.y + main.y));
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
        if (player.position.x <= (float)DemonicsPhysics.WALL_LEFT_POINT || player.position.x >= (float)DemonicsPhysics.WALL_RIGHT_POINT)
        {
            return;
        }
        if (player.position.x < otherPlayer.position.x)
        {
            if (player.position.x + 1.35f >= otherPlayer.position.x)
            {
                float difference = Mathf.Abs(player.position.x - otherPlayer.position.x);
                float pushDistance = (1.35f - difference) / (2);
                player.position = new Vector2((player.position.x - pushDistance), player.position.y);
            }
        }
        else
        {
            if (player.position.x <= otherPlayer.position.x + 1.35f)
            {
                float difference = Mathf.Abs(player.position.x - otherPlayer.position.x);
                float pushDistance = (1.35f - difference) / (2);
                player.position = new Vector2((player.position.x + pushDistance), player.position.y);
            }
        }
    }
    private static bool valueInRange(float value, float min, float max)
    { return (value >= min) && (value <= max); }
    private static bool Colliding(PlayerNetwork a, PlayerNetwork b)
    {
        bool xOverlap = valueInRange(a.position.x - (1.35f / 2), b.position.x - (1.35f / 2), b.position.x + (1.35f / 2)) ||
                    valueInRange(b.position.x - (1.35f / 2), a.position.x - (1.35f / 2), a.position.x + (1.35f / 2));
        bool yOverlap = valueInRange(a.position.y - (1.5f / 2), b.position.y - (1.5f / 2), b.position.y + (1.5f / 2)) ||
                    valueInRange(b.position.y - (1.5f / 2), a.position.y - (1.5f / 2), a.position.y + (1.5f / 2));
        return xOverlap && yOverlap;
    }
    private void CameraHorizontalBounds()
    {
        if (_skipWallFrame > 0)
        {
            _skipWallFrame--;
            WALL_LEFT_POINT = (DemonicsFloat)(-1000);
            WALL_RIGHT_POINT = (DemonicsFloat)(1000);
        }
        else
        {
            WALL_LEFT_POINT = (DemonicsFloat)_camera.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x + _wallPointOffset;
            WALL_RIGHT_POINT = (DemonicsFloat)_camera.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, Camera.main.nearClipPlane)).x - _wallPointOffset;
        }
    }

    public void SetPositionWithRender(DemonicsVector2 position)
    {
        Position = position;
        //Bounds();
        transform.position = new Vector2((float)Position.x, (float)Position.y);
    }

    public void EnableGravity(bool state)
    {
        if (state)
        {
            _gravity = (DemonicsFloat)0.018;
        }
        else
        {
            _gravity = (DemonicsFloat)0;
        }
    }

    public void SetJuggleGravity(bool state)
    {
        if (state)
        {
            _gravity = (DemonicsFloat)0.013;
        }
        else
        {
            _gravity = (DemonicsFloat)0.018;
        }
    }
}
