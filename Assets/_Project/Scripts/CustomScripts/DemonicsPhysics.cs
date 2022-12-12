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
        if (!Collision())
        {
            // Set physical Position
            //SetPositionWithRender(new DemonicsVector2(Position.x + Velocity.x, Position.y + Velocity.y));
        }

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

    private bool Collision()
    {
        if (OtherPhysics != null)
        {
            if (Position.y > OtherPhysics.Position.y)
            {
                if (Velocity.y < OtherPhysics.Velocity.y)
                {
                    DemonicsFloat difference = DemonicsFloat.Abs(Position.x - OtherPhysics.Position.x);
                    DemonicsFloat pushDistance = ((DemonicsFloat)1.35 - difference) / ((DemonicsFloat)2);
                    if (Position.x > OtherPhysics.Position.x)
                    {
                        if (Position.x >= WALL_RIGHT_POINT)
                        {
                            OtherPhysics.Position = new DemonicsVector2(OtherPhysics.Position.x - pushDistance, OtherPhysics.Position.y);
                        }
                        else
                        {
                            Position = new DemonicsVector2(Position.x + pushDistance, Position.y);
                        }
                    }
                    else if (Position.x <= OtherPhysics.Position.x)
                    {
                        if (OtherPhysics.Position.x <= WALL_LEFT_POINT)
                        {
                            Position = new DemonicsVector2(Position.x + pushDistance, Position.y);
                        }
                        else if (Position.x <= WALL_LEFT_POINT)
                        {
                            OtherPhysics.Position = new DemonicsVector2(OtherPhysics.Position.x + pushDistance, OtherPhysics.Position.y);
                        }
                        else
                        {
                            Position = new DemonicsVector2(Position.x - pushDistance, Position.y);
                        }
                    }
                }
            }
            DemonicsVector2 main = Velocity;
            DemonicsVector2 second = OtherPhysics.Velocity;
            if (OtherPhysics.Position.x >= WALL_RIGHT_POINT && Velocity.x >= (DemonicsFloat)0 || OtherPhysics.Position.x <= WALL_LEFT_POINT && Velocity.x <= (DemonicsFloat)0)
            {
                main = new DemonicsVector2((DemonicsFloat)0, Velocity.y);
                second = new DemonicsVector2((DemonicsFloat)0, OtherPhysics.Velocity.y);
                OtherPhysics.SetPositionWithRender(new DemonicsVector2(OtherPhysics.Position.x + second.x, OtherPhysics.Position.y));
                SetPositionWithRender(new DemonicsVector2(Position.x + main.x, Position.y + main.y));
                Intersects();
                return true;
            }
            if (DemonicsFloat.Abs(Velocity.x) > DemonicsFloat.Abs(OtherPhysics.Velocity.x))
            {
                DemonicsFloat totalVelocity;
                if (Velocity.x > (DemonicsFloat)0 && OtherPhysics.Velocity.x < (DemonicsFloat)0)
                {
                    totalVelocity = DemonicsFloat.Abs(Velocity.x) - DemonicsFloat.Abs(OtherPhysics.Velocity.x);
                }
                else
                {
                    totalVelocity = DemonicsFloat.Abs(Velocity.x);
                }
                if (Position.x < OtherPhysics.Position.x && Velocity.x > (DemonicsFloat)0)
                {
                    main = new DemonicsVector2(totalVelocity, Velocity.y);
                    second = new DemonicsVector2(totalVelocity, OtherPhysics.Velocity.y);
                    OtherPhysics.SetPositionWithRender(new DemonicsVector2(OtherPhysics.Position.x + second.x, OtherPhysics.Position.y + second.y));
                    SetPositionWithRender(new DemonicsVector2(Position.x + main.x, Position.y + main.y));
                    Intersects();
                    return true;
                }
                else if (Position.x > OtherPhysics.Position.x && Velocity.x < (DemonicsFloat)0)
                {
                    main = new DemonicsVector2(-totalVelocity, Velocity.y);
                    second = new DemonicsVector2(-totalVelocity, OtherPhysics.Velocity.y);
                    OtherPhysics.SetPositionWithRender(new DemonicsVector2(OtherPhysics.Position.x + second.x, OtherPhysics.Position.y + second.y));
                    SetPositionWithRender(new DemonicsVector2(Position.x + main.x, Position.y + main.y)); Intersects();
                    Intersects();
                    return true;
                }
                return false;
            }
            else if (DemonicsFloat.Abs(Velocity.x) == DemonicsFloat.Abs(OtherPhysics.Velocity.x))
            {
                if (Position.x < OtherPhysics.Position.x && Velocity.x > (DemonicsFloat)0 || Position.x > OtherPhysics.Position.x && Velocity.x < (DemonicsFloat)0)
                {
                    main = new DemonicsVector2((DemonicsFloat)0, Velocity.y);
                    second = new DemonicsVector2((DemonicsFloat)0, OtherPhysics.Velocity.y);
                    OtherPhysics.SetPositionWithRender(new DemonicsVector2(OtherPhysics.Position.x + second.x, OtherPhysics.Position.y + second.y));
                    SetPositionWithRender(new DemonicsVector2(Position.x + main.x, Position.y + main.y));
                    Intersects();
                    return true;
                }
                if (!IgnoreWalls)
                {
                    Intersects();
                }
                return false;
            }
            return true;
        }
        return false;
    }

    private void Intersects()
    {
        if (OtherPhysics != null)
        {
            if (Position.x < OtherPhysics.Position.x)
            {
                if (Position.x + 1.35 > OtherPhysics.Position.x)
                {
                    DemonicsFloat difference = DemonicsFloat.Abs(Position.x - OtherPhysics.Position.x);
                    DemonicsFloat pushDistance = ((DemonicsFloat)1.35 - difference) / ((DemonicsFloat)2);
                    Position = new DemonicsVector2((Position.x - pushDistance), Position.y);
                }
            }
            else
            {
                if (Position.x < OtherPhysics.Position.x + 1.35)
                {
                    DemonicsFloat difference = DemonicsFloat.Abs(Position.x - OtherPhysics.Position.x);
                    DemonicsFloat pushDistance = ((DemonicsFloat)1.35 - difference) / ((DemonicsFloat)2);
                    Position = new DemonicsVector2((Position.x + pushDistance), Position.y);
                }
            }
        }
    }

    private void Bounds()
    {
        if (Position.y <= GROUND_POINT)
        {
            Position = new DemonicsVector2(Position.x, GROUND_POINT);
        }
        if (Position.y >= CELLING_POINT && Velocity.y > (DemonicsFloat)0)
        {
            Velocity = new DemonicsVector2(Velocity.x, (DemonicsFloat)0);
        }
        if (!IgnoreWalls)
        {
            if (Position.x >= WALL_RIGHT_POINT && Velocity.x >= (DemonicsFloat)0)
            {
                Position = new DemonicsVector2(WALL_RIGHT_POINT, Position.y);
            }
            if (Position.x <= WALL_LEFT_POINT && Velocity.x <= (DemonicsFloat)0)
            {
                Position = new DemonicsVector2(WALL_LEFT_POINT, Position.y);
            }
        }
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
