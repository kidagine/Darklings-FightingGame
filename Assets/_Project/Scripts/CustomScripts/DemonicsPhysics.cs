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
    private bool _freeze;
    public static DemonicsFloat GROUND_POINT = (DemonicsFloat)(-4.485);
    public static DemonicsFloat CELLING_POINT = (DemonicsFloat)(7);
    public static DemonicsFloat WALL_POINT = (DemonicsFloat)(10.75);


    public void OnCollision(DemonicsPhysics otherPhysics)
    {
        if (DemonicsFloat.Abs(Velocity.x) > DemonicsFloat.Abs(otherPhysics.Velocity.x))
        {
            if (Position.x < otherPhysics.Position.x && Velocity.x > (DemonicsFloat)0 || Position.x > otherPhysics.Position.x && Velocity.x < (DemonicsFloat)0)
            {
                Velocity = new DemonicsVector2(Velocity.x / (DemonicsFloat)2, Velocity.y);
                otherPhysics.Position = new DemonicsVector2(otherPhysics.Position.x + Velocity.x, otherPhysics.Position.y);
            }
        }
        else if (DemonicsFloat.Abs(Velocity.x) < DemonicsFloat.Abs(otherPhysics.Velocity.x))
        {
            if (otherPhysics.Position.x < Position.x && otherPhysics.Velocity.x > (DemonicsFloat)0 || otherPhysics.Position.x > Position.x && otherPhysics.Velocity.x < (DemonicsFloat)0)
            {
                otherPhysics.Velocity = new DemonicsVector2(otherPhysics.Velocity.x / (DemonicsFloat)2, otherPhysics.Velocity.y);
                Position = new DemonicsVector2(Position.x + otherPhysics.Velocity.x, Position.y);
            }
        }
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
        Move();
    }

    private void Move()
    {
        if (_freeze)
        {
            Position = _freezePosition;
            return;
        }
        //Sets physics
        Velocity = new DemonicsVector2(Velocity.x, Velocity.y - _gravity);

        Position = new DemonicsVector2(Position.x + Velocity.x, Position.y + Velocity.y);

        Bounds();
        //Sets rendering
        transform.position = new Vector2((float)Position.x, (float)Position.y);
    }

    public void SetPositionWithRender(DemonicsVector2 position)
    {
        Position = position;
        transform.position = new Vector2((float)Position.x, (float)Position.y);
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
        if (Position.x >= WALL_POINT && Velocity.x > (DemonicsFloat)0)
        {
            Position = new DemonicsVector2(WALL_POINT, Position.y);
        }
        if (Position.x <= -WALL_POINT && Velocity.x < (DemonicsFloat)0)
        {
            Position = new DemonicsVector2(-WALL_POINT, Position.y);
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
