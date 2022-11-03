using System.Collections;
using System.Collections.Generic;
using FixMath.NET;
using UnityEngine;

public class DemonicsPhysics : MonoBehaviour
{
    public Fix64 VelocityX { get; set; }
    public Fix64 VelocityY { get; set; }
    public Fix64 PositionX { get; set; }
    public Fix64 PositionY { get; set; }
    private int _forceFrames;
    private Fix64 _gravity = (Fix64)0.7;
    public static Fix64 GROUND_POINT = (Fix64)(-4.485);
    public static Fix64 CELLING_POINT = (Fix64)(0);
    public static Fix64 WALL_POINT = (Fix64)(10.75);

    public void OnCollision(DemonicsPhysics otherPhysics)
    {
        if (Fix64.Abs(VelocityX) > Fix64.Abs(otherPhysics.VelocityX))
        {
            if (PositionX < otherPhysics.PositionX && VelocityX > (Fix64)0 || PositionX > otherPhysics.PositionX && VelocityX < (Fix64)0)
            {
                VelocityX /= (Fix64)2;
                otherPhysics.PositionX += VelocityX / (Fix64)50;
            }
        }
        else if (Fix64.Abs(VelocityX) < Fix64.Abs(otherPhysics.VelocityX))
        {
            if (otherPhysics.PositionX < PositionX && otherPhysics.VelocityX > (Fix64)0 || otherPhysics.PositionX > PositionX && otherPhysics.VelocityX < (Fix64)0)
            {
                otherPhysics.VelocityX /= (Fix64)2;
                PositionX += otherPhysics.VelocityX / (Fix64)50;
            }
        }
    }

    public void ExitCollision()
    {

    }

    public void Velocity(Fix64 forceX, Fix64 forceY)
    {
        VelocityX = forceX;
        VelocityY = forceY;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //Sets physics
        VelocityY -= _gravity;
        PositionX += VelocityX / (Fix64)50;
        PositionY += VelocityY / (Fix64)50;

        Bounds();
        //Sets rendering
        transform.position = new Vector2((float)PositionX, (float)PositionY);
    }

    private void Bounds()
    {
        if (PositionY <= GROUND_POINT)
        {
            PositionY = GROUND_POINT;
        }
        if (PositionY >= CELLING_POINT && VelocityY > (Fix64)0)
        {
            VelocityY = (Fix64)0;
        }
        if (PositionX >= WALL_POINT && VelocityX > (Fix64)0)
        {
            PositionX = WALL_POINT;
        }
        if (PositionX <= -WALL_POINT && VelocityX < (Fix64)0)
        {
            PositionX = -WALL_POINT;
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
