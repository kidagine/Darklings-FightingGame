using FixMath.NET;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Vector2Fix64
{
	public Fix64 x;
	public Fix64 y; 
    public Fix64 this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return this.x;
                case 1:
                    return this.y;
                default:
                    throw new IndexOutOfRangeException("Invalid Vector2d index!");
            }
        }
        set
        {
            switch (index)
            {
                case 0:
                    this.x = value;
                    break;
                case 1:
                    this.y = value;
                    break;
                default:
                    throw new IndexOutOfRangeException("Invalid Vector2d index!");
            }
        }
    }
}
