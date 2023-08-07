using System;
using System.IO;
using System.Runtime.CompilerServices;

/// <summary>
/// Represents a Q31.32 fixed-point number.
/// </summary>
[System.Serializable]
public partial struct DemonFloat : IEquatable<DemonFloat>, IComparable<DemonFloat>
{
    readonly long m_rawValue;

    public static readonly decimal Precision = (decimal)(new DemonFloat(1L));
    public static readonly DemonFloat One = new DemonFloat(ONE);
    public static readonly DemonFloat Zero = new DemonFloat();
    public static readonly DemonFloat PI = new DemonFloat(Pi);
    public static readonly DemonFloat PITimes2 = new DemonFloat(PiTimes2);
    public static readonly DemonFloat PIOver180 = new DemonFloat((long)72);
    public static readonly DemonFloat Rad2Deg = DemonFloat.Pi * (DemonFloat)2 / (DemonFloat)360;
    public static readonly DemonFloat Deg2Rad = (DemonFloat)360 / (DemonFloat.Pi * (DemonFloat)2);

    const long Pi = 12868;
    const long PiTimes2 = 25736;

    public const int FRACTIONAL_PLACES = 12;
    const long ONE = 1L << FRACTIONAL_PLACES;

    public static int Sign(DemonFloat value)
    {
        return
            value.m_rawValue < 0 ? -1 :
            value.m_rawValue > 0 ? 1 :
            0;
    }

    public static DemonFloat Abs(DemonFloat value)
    {
        return new DemonFloat(value.m_rawValue > 0 ? value.m_rawValue : -value.m_rawValue);
    }

    public static DemonFloat Floor(DemonFloat value)
    {
        return new DemonFloat((long)((ulong)value.m_rawValue & 0xFFFFFFFFFFFFF000));
    }

    public static DemonFloat Ceiling(DemonFloat value)
    {
        var hasFractionalPart = (value.m_rawValue & 0x0000000000000FFF) != 0;
        return hasFractionalPart ? Floor(value) + One : value;
    }

    public static DemonFloat operator +(DemonFloat x, DemonFloat y)
    {
        return new DemonFloat(x.m_rawValue + y.m_rawValue);
    }

    public static DemonFloat operator +(DemonFloat x, int y)
    {
        return x + (DemonFloat)y;
    }

    public static DemonFloat operator +(int x, DemonFloat y)
    {
        return (DemonFloat)x + y;
    }

    public static DemonFloat operator +(DemonFloat x, float y)
    {
        return x + (DemonFloat)y;
    }

    public static DemonFloat operator +(float x, DemonFloat y)
    {
        return (DemonFloat)x + y;
    }

    public static DemonFloat operator +(DemonFloat x, double y)
    {
        return x + (DemonFloat)y;
    }

    public static DemonFloat operator +(double x, DemonFloat y)
    {
        return (DemonFloat)x + y;
    }

    public static DemonFloat operator -(DemonFloat x, DemonFloat y)
    {
        return new DemonFloat(x.m_rawValue - y.m_rawValue);
    }

    public static DemonFloat operator -(DemonFloat x, int y)
    {
        return x - (DemonFloat)y;
    }

    public static DemonFloat operator -(int x, DemonFloat y)
    {
        return (DemonFloat)x - y;
    }

    public static DemonFloat operator -(DemonFloat x, float y)
    {
        return x - (DemonFloat)y;
    }

    public static DemonFloat operator -(float x, DemonFloat y)
    {
        return (DemonFloat)x + y;
    }

    public static DemonFloat operator -(DemonFloat x, double y)
    {
        return x - (DemonFloat)y;
    }

    public static DemonFloat operator -(double x, DemonFloat y)
    {
        return (DemonFloat)x - y;
    }


    public static DemonFloat operator *(DemonFloat x, DemonFloat y)
    {
        return new DemonFloat((x.m_rawValue * y.m_rawValue) >> FRACTIONAL_PLACES);
    }

    public static DemonFloat operator *(DemonFloat x, int y)
    {
        return x * (DemonFloat)y;
    }

    public static DemonFloat operator *(int x, DemonFloat y)
    {
        return (DemonFloat)x * y;
    }

    public static DemonFloat operator *(DemonFloat x, float y)
    {
        return x * (DemonFloat)y;
    }

    public static DemonFloat operator *(float x, DemonFloat y)
    {
        return (DemonFloat)x * y;
    }

    public static DemonFloat operator *(DemonFloat x, double y)
    {
        return x * (DemonFloat)y;
    }

    public static DemonFloat operator *(double x, DemonFloat y)
    {
        return (DemonFloat)x * y;
    }

    public static DemonFloat operator /(DemonFloat x, DemonFloat y)
    {
        return new DemonFloat((x.m_rawValue << FRACTIONAL_PLACES) / y.m_rawValue);
    }

    public static DemonFloat operator /(DemonFloat x, int y)
    {
        return x / (DemonFloat)y;
    }

    public static DemonFloat operator /(int x, DemonFloat y)
    {
        return (DemonFloat)x / y;
    }

    public static DemonFloat operator /(DemonFloat x, float y)
    {
        return x / (DemonFloat)y;
    }

    public static DemonFloat operator /(float x, DemonFloat y)
    {
        return (DemonFloat)x / y;
    }

    public static DemonFloat operator /(double x, DemonFloat y)
    {
        return (DemonFloat)x / y;
    }

    public static DemonFloat operator /(DemonFloat x, double y)
    {
        return x / (DemonFloat)y;
    }

    public static DemonFloat operator %(DemonFloat x, DemonFloat y)
    {
        return new DemonFloat(x.m_rawValue % y.m_rawValue);
    }

    public static DemonFloat operator -(DemonFloat x)
    {
        return new DemonFloat(-x.m_rawValue);
    }

    public static bool operator ==(DemonFloat x, DemonFloat y)
    {
        return x.m_rawValue == y.m_rawValue;
    }

    public static bool operator !=(DemonFloat x, DemonFloat y)
    {
        return x.m_rawValue != y.m_rawValue;
    }

    public static bool operator >(DemonFloat x, DemonFloat y)
    {
        return x.m_rawValue > y.m_rawValue;
    }

    public static bool operator >(DemonFloat x, int y)
    {
        return x > (DemonFloat)y;
    }
    public static bool operator <(DemonFloat x, int y)
    {
        return x < (DemonFloat)y;
    }
    public static bool operator >(DemonFloat x, float y)
    {
        return x > (DemonFloat)y;
    }
    public static bool operator <(DemonFloat x, float y)
    {
        return x < (DemonFloat)y;
    }

    public static bool operator <(DemonFloat x, DemonFloat y)
    {
        return x.m_rawValue < y.m_rawValue;
    }

    public static bool operator >=(DemonFloat x, DemonFloat y)
    {
        return x.m_rawValue >= y.m_rawValue;
    }

    public static bool operator <=(DemonFloat x, DemonFloat y)
    {
        return x.m_rawValue <= y.m_rawValue;
    }

    public static DemonFloat operator >>(DemonFloat x, int amount)
    {
        return new DemonFloat(x.RawValue >> amount);
    }

    public static DemonFloat operator <<(DemonFloat x, int amount)
    {
        return new DemonFloat(x.RawValue << amount);
    }


    public static explicit operator DemonFloat(long value)
    {
        return new DemonFloat(value * ONE);
    }
    public static explicit operator long(DemonFloat value)
    {
        return value.m_rawValue >> FRACTIONAL_PLACES;
    }
    public static explicit operator DemonFloat(float value)
    {
        return new DemonFloat((long)(value * ONE));
    }
    public static explicit operator float(DemonFloat value)
    {
        return (float)value.m_rawValue / ONE;
    }
    public static explicit operator int(DemonFloat value)
    {
        return (int)((float)value);
    }
    public static explicit operator DemonFloat(double value)
    {
        return new DemonFloat((long)(value * ONE));
    }
    public static explicit operator double(DemonFloat value)
    {
        return (double)value.m_rawValue / ONE;
    }
    public static explicit operator DemonFloat(decimal value)
    {
        return new DemonFloat((long)(value * ONE));
    }
    public static explicit operator decimal(DemonFloat value)
    {
        return (decimal)value.m_rawValue / ONE;
    }

    public override bool Equals(object obj)
    {
        return obj is DemonFloat && ((DemonFloat)obj).m_rawValue == m_rawValue;
    }

    public override int GetHashCode()
    {
        return m_rawValue.GetHashCode();
    }

    public bool Equals(DemonFloat other)
    {
        return m_rawValue == other.m_rawValue;
    }

    public int CompareTo(DemonFloat other)
    {
        return m_rawValue.CompareTo(other.m_rawValue);
    }

    public override string ToString()
    {
        return ((decimal)this).ToString();
    }
    public string ToStringRound(int round = 2)
    {
        return (float)Math.Round((float)this, round) + "";
    }

    public static DemonFloat FromRaw(long rawValue)
    {
        return new DemonFloat(rawValue);
    }

    public static DemonFloat Pow(DemonFloat x, int y)
    {
        if (y == 1) return x;
        DemonFloat result = DemonFloat.Zero;
        DemonFloat tmp = Pow(x, y / 2);
        if ((y & 1) != 0) //奇数    
        {
            result = x * tmp * tmp;
        }
        else
        {
            result = tmp * tmp;
        }

        return result;
    }


    public long RawValue { get { return m_rawValue; } }


    DemonFloat(long rawValue)
    {
        m_rawValue = rawValue;
    }

    public DemonFloat(int value)
    {
        m_rawValue = value * ONE;
    }

    public static DemonFloat Sqrt(DemonFloat f, int numberIterations)
    {
        if (f.RawValue < 0)
        {
            throw new ArithmeticException("sqrt error");
        }

        if (f.RawValue == 0)
            return DemonFloat.Zero;

        DemonFloat k = f + DemonFloat.One >> 1;
        for (int i = 0; i < numberIterations; i++)
            k = (k + (f / k)) >> 1;

        if (k.RawValue < 0)
            throw new ArithmeticException("Overflow");
        else
            return k;
    }

    public static DemonFloat Sqrt(DemonFloat f)
    {
        byte numberOfIterations = 8;
        if (f.RawValue > 0x64000)
            numberOfIterations = 12;
        if (f.RawValue > 0x3e8000)
            numberOfIterations = 16;
        return Sqrt(f, numberOfIterations);
    }

    public static DemonFloat Lerp(DemonFloat from, DemonFloat to, DemonFloat factor)
    {
        return from * (1 - factor) + to * factor;
    }
    #region Sin
    public static DemonFloat Sin(DemonFloat i)
    {
        DemonFloat j = (DemonFloat)0;
        for (; i < DemonFloat.Zero; i += DemonFloat.FromRaw(PiTimes2)) ;
        if (i > DemonFloat.FromRaw(PiTimes2))
            i %= DemonFloat.FromRaw(PiTimes2);

        DemonFloat k = (i * DemonFloat.FromRaw(100000000)) / DemonFloat.FromRaw(7145244444);
        if (i != DemonFloat.Zero && i != DemonFloat.FromRaw(6434) && i != DemonFloat.FromRaw(Pi) &&
            i != DemonFloat.FromRaw(19302) && i != DemonFloat.FromRaw(PiTimes2))
            j = (i * DemonFloat.FromRaw(100000000)) / DemonFloat.FromRaw(7145244444) - k * DemonFloat.FromRaw(10);
        if (k <= DemonFloat.FromRaw(90))
            return sin_lookup(k, j);
        if (k <= DemonFloat.FromRaw(180))
            return sin_lookup(DemonFloat.FromRaw(180) - k, j);
        if (k <= DemonFloat.FromRaw(270))
            return -sin_lookup(k - DemonFloat.FromRaw(180), j);
        else
            return -sin_lookup(DemonFloat.FromRaw(360) - k, j);
    }

    private static DemonFloat sin_lookup(DemonFloat i, DemonFloat j)
    {
        if (j > 0 && j < DemonFloat.FromRaw(10) && i < DemonFloat.FromRaw(90))
            return DemonFloat.FromRaw(SIN_TABLE[i.RawValue]) +
                ((DemonFloat.FromRaw(SIN_TABLE[i.RawValue + 1]) - DemonFloat.FromRaw(SIN_TABLE[i.RawValue])) /
                DemonFloat.FromRaw(10)) * j;
        else
            return DemonFloat.FromRaw(SIN_TABLE[i.RawValue]);
    }

    private static int[] SIN_TABLE = {
        0, 71, 142, 214, 285, 357, 428, 499, 570, 641,
        711, 781, 851, 921, 990, 1060, 1128, 1197, 1265, 1333,
        1400, 1468, 1534, 1600, 1665, 1730, 1795, 1859, 1922, 1985,
        2048, 2109, 2170, 2230, 2290, 2349, 2407, 2464, 2521, 2577,
        2632, 2686, 2740, 2793, 2845, 2896, 2946, 2995, 3043, 3091,
        3137, 3183, 3227, 3271, 3313, 3355, 3395, 3434, 3473, 3510,
        3547, 3582, 3616, 3649, 3681, 3712, 3741, 3770, 3797, 3823,
        3849, 3872, 3895, 3917, 3937, 3956, 3974, 3991, 4006, 4020,
        4033, 4045, 4056, 4065, 4073, 4080, 4086, 4090, 4093, 4095,
        4096
    };
    #endregion


    #region Cos, Tan, Asin
    public static DemonFloat Cos(DemonFloat i)
    {
        return Sin(i + DemonFloat.FromRaw(6435));
    }

    public static DemonFloat Tan(DemonFloat i)
    {
        return Sin(i) / Cos(i);
    }

    public static DemonFloat Asin(DemonFloat F)
    {
        bool isNegative = F < 0;
        F = Abs(F);

        if (F > DemonFloat.One)
            throw new ArithmeticException("Bad Asin Input:" + (double)F);

        DemonFloat f1 = ((((DemonFloat.FromRaw(145103 >> FRACTIONAL_PLACES) * F) -
            DemonFloat.FromRaw(599880 >> FRACTIONAL_PLACES) * F) +
            DemonFloat.FromRaw(1420468 >> FRACTIONAL_PLACES) * F) -
            DemonFloat.FromRaw(3592413 >> FRACTIONAL_PLACES) * F) +
            DemonFloat.FromRaw(26353447 >> FRACTIONAL_PLACES);
        DemonFloat f2 = PI / (DemonFloat)2 - (Sqrt(DemonFloat.One - F) * f1);

        return isNegative ? -f2 : f2;
    }
    #endregion

    #region ATan, ATan2
    public static DemonFloat Atan(DemonFloat F)
    {
        return Asin(F / Sqrt(DemonFloat.One + (F * F)));
    }

    public static DemonFloat Atan2(DemonFloat F1, DemonFloat F2)
    {
        if (F2.RawValue == 0 && F1.RawValue == 0)
            return (DemonFloat)0;

        DemonFloat result = (DemonFloat)0;
        if (F2 > 0)
            result = Atan(F1 / F2);
        else if (F2 < 0)
        {
            if (F1 >= (DemonFloat)0)
                result = (PI - Atan(Abs(F1 / F2)));
            else
                result = -(PI - Atan(Abs(F1 / F2)));
        }
        else
            result = (F1 >= (DemonFloat)0 ? PI : -PI) / (DemonFloat)2;

        return result;
    }
    #endregion
}

public struct DemonVector3
{
    public DemonFloat x;
    public DemonFloat y;
    public DemonFloat z;

    public DemonVector3(DemonFloat x, DemonFloat y, DemonFloat z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public DemonVector3(DemonVector3 v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }

    public DemonFloat this[int index]
    {
        get
        {
            if (index == 0)
                return x;
            else if (index == 1)
                return y;
            else
                return z;
        }
        set
        {
            if (index == 0)
                x = value;
            else if (index == 1)
                y = value;
            else
                y = value;
        }
    }

    public static DemonVector3 Zero
    {
        get { return new DemonVector3(DemonFloat.Zero, DemonFloat.Zero, DemonFloat.Zero); }
    }

    public static DemonVector3 operator +(DemonVector3 a, DemonVector3 b)
    {
        DemonFloat x = a.x + b.x;
        DemonFloat y = a.y + b.y;
        DemonFloat z = a.z + b.z;
        return new DemonVector3(x, y, z);
    }

    public static DemonVector3 operator -(DemonVector3 a, DemonVector3 b)
    {
        DemonFloat x = a.x - b.x;
        DemonFloat y = a.y - b.y;
        DemonFloat z = a.z - b.z;
        return new DemonVector3(x, y, z);
    }

    public static DemonVector3 operator *(DemonFloat d, DemonVector3 a)
    {
        DemonFloat x = a.x * d;
        DemonFloat y = a.y * d;
        DemonFloat z = a.z * d;
        return new DemonVector3(x, y, z);
    }

    public static DemonVector3 operator *(DemonVector3 a, DemonFloat d)
    {
        DemonFloat x = a.x * d;
        DemonFloat y = a.y * d;
        DemonFloat z = a.z * d;
        return new DemonVector3(x, y, z);
    }

    public static DemonVector3 operator /(DemonVector3 a, DemonFloat d)
    {
        DemonFloat x = a.x / d;
        DemonFloat y = a.y / d;
        DemonFloat z = a.z / d;
        return new DemonVector3(x, y, z);
    }

    public static bool operator ==(DemonVector3 lhs, DemonVector3 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
    }

    public static bool operator !=(DemonVector3 lhs, DemonVector3 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
    }

    public static DemonFloat SqrMagnitude(DemonVector3 a)
    {
        return a.x * a.x + a.y * a.y + a.z * a.z;
    }

    public static DemonFloat Distance(DemonVector3 a, DemonVector3 b)
    {
        return Magnitude(a - b);
    }

    public static DemonFloat Magnitude(DemonVector3 a)
    {
        return DemonFloat.Sqrt(DemonVector3.SqrMagnitude(a));
    }

    public void Normalize()
    {
        DemonFloat n = x * x + y * y + z * z;
        if (n == DemonFloat.Zero)
            return;

        n = DemonFloat.Sqrt(n);

        if (n < (DemonFloat)0.0001)
        {
            return;
        }

        n = 1 / n;
        x *= n;
        y *= n;
        z *= n;
    }

    public DemonVector3 GetNormalized()
    {
        DemonVector3 v = new DemonVector3(this);
        v.Normalize();
        return v;
    }

    public override string ToString()
    {
        return string.Format("x:{0} y:{1} z:{2}", x, y, z);
    }

    public override bool Equals(object obj)
    {
        return obj is DemonVector2 && ((DemonVector3)obj) == this;
    }

    public override int GetHashCode()
    {
        return this.x.GetHashCode() + this.y.GetHashCode() + this.z.GetHashCode();
    }

    public static DemonVector3 Lerp(DemonVector3 from, DemonVector3 to, DemonFloat factor)
    {
        return from * (1 - factor) + to * factor;
    }
#if _CLIENTLOGIC_
    public UnityEngine.Vector3 ToVector3()
    {
        return new UnityEngine.Vector3((float)x, (float)y, (float)z);
    }
#endif
}

public struct DemonVector2
{
    public DemonFloat x;
    public DemonFloat y;

    public DemonVector2(DemonFloat x, DemonFloat y)
    {
        this.x = x;
        this.y = y;
    }
    public DemonVector2(DemonFloat x, int y)
    {
        this.x = x;
        this.y = (DemonFloat)y;
    }

    public DemonVector2(int x, int y)
    {
        this.x = (DemonFloat)x;
        this.y = (DemonFloat)y;
    }
    public DemonVector2(DemonVector2 v)
    {
        this.x = v.x;
        this.y = v.y;
    }
    public static DemonVector2 operator -(DemonVector2 a, int b)
    {
        DemonFloat x = a.x - b;
        DemonFloat y = a.y - b;
        return new DemonVector2(x, y);
    }

    public DemonFloat this[int index]
    {
        get { return index == 0 ? x : y; }
        set
        {
            if (index == 0)
            {
                x = value;
            }
            else
            {
                y = value;
            }
        }
    }

    public static DemonVector2 Zero
    {
        get { return new DemonVector2(DemonFloat.Zero, DemonFloat.Zero); }
    }

    public static DemonVector2 operator +(DemonVector2 a, DemonVector2 b)
    {
        DemonFloat x = a.x + b.x;
        DemonFloat y = a.y + b.y;
        return new DemonVector2(x, y);
    }

    public static DemonVector2 operator -(DemonVector2 a, DemonVector2 b)
    {
        DemonFloat x = a.x - b.x;
        DemonFloat y = a.y - b.y;
        return new DemonVector2(x, y);
    }

    public static DemonVector2 operator *(DemonFloat d, DemonVector2 a)
    {
        DemonFloat x = a.x * d;
        DemonFloat y = a.y * d;
        return new DemonVector2(x, y);
    }

    public static DemonVector2 operator *(DemonVector2 a, DemonFloat d)
    {
        DemonFloat x = a.x * d;
        DemonFloat y = a.y * d;
        return new DemonVector2(x, y);
    }

    public static DemonVector2 operator /(DemonVector2 a, DemonFloat d)
    {
        DemonFloat x = a.x / d;
        DemonFloat y = a.y / d;
        return new DemonVector2(x, y);
    }

    public static bool operator ==(DemonVector2 lhs, DemonVector2 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }

    public static bool operator !=(DemonVector2 lhs, DemonVector2 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y;
    }

    public override bool Equals(object obj)
    {
        return obj is DemonVector2 && ((DemonVector2)obj) == this;
    }

    public override int GetHashCode()
    {
        return this.x.GetHashCode() + this.y.GetHashCode();
    }


    public static DemonFloat SqrMagnitude(DemonVector2 a)
    {
        return a.x * a.x + a.y * a.y;
    }

    public static DemonFloat Distance(DemonVector2 a, DemonVector2 b)
    {
        return Magnitude(a - b);
    }

    public static DemonFloat Magnitude(DemonVector2 a)
    {
        return DemonFloat.Sqrt(DemonVector2.SqrMagnitude(a));
    }

    public void Normalize()
    {
        DemonFloat n = x * x + y * y;
        if (n == DemonFloat.Zero)
            return;

        n = DemonFloat.Sqrt(n);

        if (n < (DemonFloat)0.0001)
        {
            return;
        }

        n = 1 / n;
        x *= n;
        y *= n;
    }

    public DemonVector2 GetNormalized()
    {
        DemonVector2 v = new DemonVector2(this);
        v.Normalize();
        return v;
    }

    public override string ToString()
    {
        return string.Format("x:{0} y:{1}", x, y);
    }

#if _CLIENTLOGIC_
    public UnityEngine.Vector2 ToVector2()
    {
        return new UnityEngine.Vector2((float)x, (float)y);
    }
#endif
}

public struct NormalVector2
{
    public float x;
    public float y;

    public NormalVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }


    public NormalVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public NormalVector2(NormalVector2 v)
    {
        this.x = v.x;
        this.y = v.y;
    }
    public static NormalVector2 operator -(NormalVector2 a, int b)
    {
        float x = a.x - b;
        float y = a.y - b;
        return new NormalVector2(x, y);
    }

    public float this[int index]
    {
        get { return index == 0 ? x : y; }
        set
        {
            if (index == 0)
            {
                x = value;
            }
            else
            {
                y = value;
            }
        }
    }

    public static NormalVector2 Zero
    {
        get { return new NormalVector2(0, 0); }
    }

    public static NormalVector2 operator +(NormalVector2 a, NormalVector2 b)
    {
        float x = a.x + b.x;
        float y = a.y + b.y;
        return new NormalVector2(x, y);
    }

    public static NormalVector2 operator -(NormalVector2 a, NormalVector2 b)
    {
        float x = a.x - b.x;
        float y = a.y - b.y;
        return new NormalVector2(x, y);
    }

    public static NormalVector2 operator *(float d, NormalVector2 a)
    {
        float x = a.x * d;
        float y = a.y * d;
        return new NormalVector2(x, y);
    }

    public static NormalVector2 operator *(NormalVector2 a, float d)
    {
        float x = a.x * d;
        float y = a.y * d;
        return new NormalVector2(x, y);
    }

    public static NormalVector2 operator /(NormalVector2 a, float d)
    {
        float x = a.x / d;
        float y = a.y / d;
        return new NormalVector2(x, y);
    }

    public static bool operator ==(NormalVector2 lhs, NormalVector2 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }

    public static bool operator !=(NormalVector2 lhs, NormalVector2 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y;
    }

    public override bool Equals(object obj)
    {
        return obj is NormalVector2 && ((NormalVector2)obj) == this;
    }

    public override int GetHashCode()
    {
        return this.x.GetHashCode() + this.y.GetHashCode();
    }


    public static float SqrMagnitude(NormalVector2 a)
    {
        return a.x * a.x + a.y * a.y;
    }

    public static float Distance(NormalVector2 a, NormalVector2 b)
    {
        return Magnitude(a - b);
    }

    public static float Magnitude(NormalVector2 a)
    {
        return NormalVector2.SqrMagnitude(a);
    }

    public void Normalize()
    {
        float n = x * x + y * y;
        if (n == 0)
            return;

        //n = float.Sqrt(n);

        if (n < (float)0.0001)
        {
            return;
        }

        n = 1 / n;
        x *= n;
        y *= n;
    }

    public NormalVector2 GetNormalized()
    {
        NormalVector2 v = new NormalVector2(this);
        v.Normalize();
        return v;
    }

    public override string ToString()
    {
        return string.Format("x:{0} y:{1}", x, y);
    }

#if _CLIENTLOGIC_
    public UnityEngine.Vector2 ToVector2()
    {
        return new UnityEngine.Vector2((float)x, (float)y);
    }
#endif
}

public struct IntVector2
{
    public int x;
    public int y;



    public IntVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public IntVector2(IntVector2 v)
    {
        this.x = v.x;
        this.y = v.y;
    }
    public static IntVector2 operator -(IntVector2 a, int b)
    {
        int x = a.x - b;
        int y = a.y - b;
        return new IntVector2(x, y);
    }

    public int this[int index]
    {
        get { return index == 0 ? x : y; }
        set
        {
            if (index == 0)
            {
                x = value;
            }
            else
            {
                y = value;
            }
        }
    }

    public static IntVector2 Zero
    {
        get { return new IntVector2(0, 0); }
    }

    public static IntVector2 operator +(IntVector2 a, IntVector2 b)
    {
        int x = a.x + b.x;
        int y = a.y + b.y;
        return new IntVector2(x, y);
    }

    public static IntVector2 operator -(IntVector2 a, IntVector2 b)
    {
        int x = a.x - b.x;
        int y = a.y - b.y;
        return new IntVector2(x, y);
    }

    public static IntVector2 operator *(int d, IntVector2 a)
    {
        int x = a.x * d;
        int y = a.y * d;
        return new IntVector2(x, y);
    }

    public static IntVector2 operator *(IntVector2 a, int d)
    {
        int x = a.x * d;
        int y = a.y * d;
        return new IntVector2(x, y);
    }

    public static IntVector2 operator /(IntVector2 a, int d)
    {
        int x = a.x / d;
        int y = a.y / d;
        return new IntVector2(x, y);
    }

    public static bool operator ==(IntVector2 lhs, IntVector2 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }

    public static bool operator !=(IntVector2 lhs, IntVector2 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y;
    }

    public override bool Equals(object obj)
    {
        return obj is IntVector2 && ((IntVector2)obj) == this;
    }

    public override int GetHashCode()
    {
        return this.x.GetHashCode() + this.y.GetHashCode();
    }


    public static int SqrMagnitude(IntVector2 a)
    {
        return a.x * a.x + a.y * a.y;
    }

    public static int Distance(IntVector2 a, IntVector2 b)
    {
        return Magnitude(a - b);
    }

    public static int Magnitude(IntVector2 a)
    {
        return IntVector2.SqrMagnitude(a);
    }

    public void Normalize()
    {
        int n = x * x + y * y;
        if (n == 0)
            return;

        //n = int.Sqrt(n);

        if (n < (int)0.0001)
        {
            return;
        }

        n = 1 / n;
        x *= n;
        y *= n;
    }

    public IntVector2 GetNormalized()
    {
        IntVector2 v = new IntVector2(this);
        v.Normalize();
        return v;
    }

    public override string ToString()
    {
        return string.Format("x:{0} y:{1}", x, y);
    }

#if _CLIENTLOGIC_
    public UnityEngine.Vector2 ToVector2()
    {
        return new UnityEngine.Vector2((int)x, (int)y);
    }
#endif
}
