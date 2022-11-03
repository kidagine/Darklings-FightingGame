using System.Collections;
using System.Collections.Generic;
using FixMath.NET;
using UnityEngine;

public class DemonicsPhysics : MonoBehaviour
{
    public FixVector2 Velocity { get; set; }
    public FixVector2 Position { get; set; }
    private Fix64 _gravity = (Fix64)0.7;
    public static Fix64 GROUND_POINT = (Fix64)(-4.485);
    public static Fix64 CELLING_POINT = (Fix64)(7);
    public static Fix64 WALL_POINT = (Fix64)(10.75);
    public bool Freeze { get; set; }

    public void OnCollision(DemonicsPhysics otherPhysics)
    {
        if (Fix64.Abs(Velocity.x) > Fix64.Abs(otherPhysics.Velocity.x))
        {
            if (Position.x < otherPhysics.Position.x && Velocity.x > (Fix64)0 || Position.x > otherPhysics.Position.x && Velocity.x < (Fix64)0)
            {
                Velocity = new FixVector2(Velocity.x / (Fix64)2, Velocity.y);
                otherPhysics.Position = new FixVector2(otherPhysics.Position.x + Velocity.x / (Fix64)50, otherPhysics.Position.y);
            }
        }
        else if (Fix64.Abs(Velocity.x) < Fix64.Abs(otherPhysics.Velocity.x))
        {
            if (otherPhysics.Position.x < Position.x && otherPhysics.Velocity.x > (Fix64)0 || otherPhysics.Position.x > Position.x && otherPhysics.Velocity.x < (Fix64)0)
            {
                otherPhysics.Velocity = new FixVector2(otherPhysics.Velocity.x / (Fix64)2, otherPhysics.Velocity.y);
                Position = new FixVector2(Position.x + otherPhysics.Velocity.x / (Fix64)50, Position.y);
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //Sets physics
        Velocity = new FixVector2(Velocity.x, Velocity.y - _gravity);
        if (Freeze)
        {
            Velocity = FixVector2.Zero;
        }
        Position = new FixVector2(Position.x + Velocity.x / (Fix64)50, Position.y + Velocity.y / (Fix64)50);

        Bounds();
        //Sets rendering
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
            _gravity = (Fix64)0.7;
        }
        else
        {
            _gravity = (Fix64)0;
        }
    }
}
