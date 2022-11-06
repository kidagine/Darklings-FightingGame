using System.Collections;
using System.Collections.Generic;
using FixMath.NET;
using UnityEngine;

public class DemonicsPhysics : MonoBehaviour
{
    public FixVector2 Velocity { get; set; }
    public FixVector2 Position { get; set; }
    private FixVector2 _freezePosition;
    private Fix64 _gravity;
    private bool _freeze;
    public static Fix64 GROUND_POINT = (Fix64)(-4.485);
    public static Fix64 CELLING_POINT = (Fix64)(7);
    public static Fix64 WALL_POINT = (Fix64)(10.75);


    public void OnCollision(DemonicsPhysics otherPhysics)
    {
        if (Fix64.Abs(Velocity.x) > Fix64.Abs(otherPhysics.Velocity.x))
        {
            if (Position.x < otherPhysics.Position.x && Velocity.x > (Fix64)0 || Position.x > otherPhysics.Position.x && Velocity.x < (Fix64)0)
            {
                Velocity = new FixVector2(Velocity.x / (Fix64)2, Velocity.y);
                otherPhysics.Position = new FixVector2(otherPhysics.Position.x + Velocity.x, otherPhysics.Position.y);
            }
        }
        else if (Fix64.Abs(Velocity.x) < Fix64.Abs(otherPhysics.Velocity.x))
        {
            if (otherPhysics.Position.x < Position.x && otherPhysics.Velocity.x > (Fix64)0 || otherPhysics.Position.x > Position.x && otherPhysics.Velocity.x < (Fix64)0)
            {
                otherPhysics.Velocity = new FixVector2(otherPhysics.Velocity.x / (Fix64)2, otherPhysics.Velocity.y);
                Position = new FixVector2(Position.x + otherPhysics.Velocity.x, Position.y);
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
        Velocity = new FixVector2(Velocity.x, Velocity.y - _gravity);

        Position = new FixVector2(Position.x + Velocity.x, Position.y + Velocity.y);

        Bounds();
        //Sets rendering
        transform.position = new Vector2((float)Position.x, (float)Position.y);
    }

    public void SetPositionWithRender(FixVector2 position)
    {
        Position = position;
        transform.position = new Vector2((float)Position.x, (float)Position.y);
    }

    private void Bounds()
    {
        if (Position.y <= GROUND_POINT)
        {
            Position = new FixVector2(Position.x, GROUND_POINT);
        }
        if (Position.y >= CELLING_POINT && Velocity.y > (Fix64)0)
        {
            Velocity = new FixVector2(Velocity.x, (Fix64)0);
        }
        if (Position.x >= WALL_POINT && Velocity.x > (Fix64)0)
        {
            Position = new FixVector2(WALL_POINT, Position.y);
        }
        if (Position.x <= -WALL_POINT && Velocity.x < (Fix64)0)
        {
            Position = new FixVector2(-WALL_POINT, Position.y);
        }
    }

    public void EnableGravity(bool state)
    {
        if (state)
        {
            _gravity = (Fix64)0.018;
        }
        else
        {
            _gravity = (Fix64)0;
        }
    }
}
