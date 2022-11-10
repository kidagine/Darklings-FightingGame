using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicsPhysics : MonoBehaviour
{
    public DemonicsVector2 Velocity { get; set; }
    public DemonicsVector2 Position { get; set; }
    public bool OnGround { get { return Position.y <= GROUND_POINT ? true : false; } private set { } }
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
    public DemonicsPhysics otherPhysics;
    void Awake()
    {
        _camera = Camera.main;
    }

    public void OnCollision(DemonicsPhysics otherPhysics)
    {
        this.otherPhysics = otherPhysics;
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
        _skipWallFrame = 1;
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
        //Sets physics
        Velocity = new DemonicsVector2(Velocity.x, Velocity.y - _gravity);
        //Check collision
        if (!Collision())
        {
            //Set physical Position
            SetPositionWithRender(new DemonicsVector2(Position.x + Velocity.x, Position.y + Velocity.y));
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
        Bounds();
        transform.position = new Vector2((float)Position.x, (float)Position.y);
    }

    private bool Collision()
    {
        if (otherPhysics != null)
        {
            if (Position.y > otherPhysics.Position.y)
            {
                if (Velocity.y < otherPhysics.Velocity.y)
                {
                    DemonicsFloat difference = DemonicsFloat.Abs(Position.x - otherPhysics.Position.x);
                    DemonicsFloat pushDistance = (1.3 - difference);
                    if (Position.x > otherPhysics.Position.x)
                    {
                        Debug.Log("A");
                        if (Position.x >= WALL_RIGHT_POINT)
                        {
                            otherPhysics.Position = new DemonicsVector2(otherPhysics.Position.x - (pushDistance / 2), otherPhysics.Position.y);
                        }
                        else
                        {
                            Position = new DemonicsVector2(Position.x + (pushDistance / 2), Position.y);
                        }
                    }
                    else if (Position.x <= otherPhysics.Position.x)
                    {
                        if (otherPhysics.Position.x <= WALL_LEFT_POINT)
                        {
                            Position = new DemonicsVector2(Position.x + (pushDistance / 2), Position.y);
                        }
                        else if (Position.x <= WALL_LEFT_POINT)
                        {
                            otherPhysics.Position = new DemonicsVector2(otherPhysics.Position.x + (pushDistance / 2), otherPhysics.Position.y);
                        }
                        else
                        {
                            Position = new DemonicsVector2(Position.x - (pushDistance / 2), Position.y);
                        }
                    }
                }
            }
            DemonicsVector2 main = Velocity;
            DemonicsVector2 second = otherPhysics.Velocity;
            if (otherPhysics.Position.x >= WALL_RIGHT_POINT && Velocity.x >= (DemonicsFloat)0 || otherPhysics.Position.x <= WALL_LEFT_POINT && Velocity.x <= (DemonicsFloat)0)
            {
                main = new DemonicsVector2((DemonicsFloat)0, Velocity.y);
                second = new DemonicsVector2((DemonicsFloat)0, otherPhysics.Velocity.y);
                otherPhysics.SetPositionWithRender(new DemonicsVector2(otherPhysics.Position.x + second.x, otherPhysics.Position.y + second.y));
                SetPositionWithRender(new DemonicsVector2(Position.x + main.x, Position.y + main.y));
                return true;
            }
            if (DemonicsFloat.Abs(Velocity.x) > DemonicsFloat.Abs(otherPhysics.Velocity.x))
            {
                DemonicsFloat totalVelocity;
                if (Velocity.x > (DemonicsFloat)0 && otherPhysics.Velocity.x < (DemonicsFloat)0)
                {
                    totalVelocity = DemonicsFloat.Abs(Velocity.x) - DemonicsFloat.Abs(otherPhysics.Velocity.x);
                }
                else
                {
                    totalVelocity = DemonicsFloat.Abs(Velocity.x);
                }
                if (Position.x < otherPhysics.Position.x && Velocity.x > (DemonicsFloat)0)
                {
                    main = new DemonicsVector2(totalVelocity, Velocity.y);
                    second = new DemonicsVector2(totalVelocity, otherPhysics.Velocity.y);
                    otherPhysics.SetPositionWithRender(new DemonicsVector2(otherPhysics.Position.x + second.x, otherPhysics.Position.y + second.y));
                    SetPositionWithRender(new DemonicsVector2(Position.x + main.x, Position.y + main.y));
                    return true;
                }
                else if (Position.x > otherPhysics.Position.x && Velocity.x < (DemonicsFloat)0)
                {
                    main = new DemonicsVector2(-totalVelocity, Velocity.y);
                    second = new DemonicsVector2(-totalVelocity, otherPhysics.Velocity.y);
                    otherPhysics.SetPositionWithRender(new DemonicsVector2(otherPhysics.Position.x + second.x, otherPhysics.Position.y + second.y));
                    SetPositionWithRender(new DemonicsVector2(Position.x + main.x, Position.y + main.y));
                    return true;
                }
                return false;
            }
            else if (DemonicsFloat.Abs(Velocity.x) == DemonicsFloat.Abs(otherPhysics.Velocity.x))
            {
                if (Position.x < otherPhysics.Position.x && Velocity.x > (DemonicsFloat)0 || Position.x > otherPhysics.Position.x && Velocity.x < (DemonicsFloat)0)
                {
                    main = new DemonicsVector2((DemonicsFloat)0, Velocity.y);
                    second = new DemonicsVector2((DemonicsFloat)0, otherPhysics.Velocity.y);
                    otherPhysics.SetPositionWithRender(new DemonicsVector2(otherPhysics.Position.x + second.x, otherPhysics.Position.y + second.y));

                    SetPositionWithRender(new DemonicsVector2(Position.x + main.x, Position.y + main.y));
                    return true;
                }
                return false;
            }
            return true;
        }
        return false;
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
        if (Position.x >= WALL_RIGHT_POINT && Velocity.x >= (DemonicsFloat)0)
        {
            Position = new DemonicsVector2(WALL_RIGHT_POINT, Position.y);
        }
        if (Position.x <= WALL_LEFT_POINT && Velocity.x <= (DemonicsFloat)0)
        {
            Position = new DemonicsVector2(WALL_LEFT_POINT, Position.y);
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
}
