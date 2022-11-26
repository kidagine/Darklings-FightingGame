using System;
using System.IO;
using System.Runtime.CompilerServices;

/// <summary>
/// Represents a Q31.32 fixed-point number.
/// </summary>
[System.Serializable]
public partial struct DemonicsFloat : IEquatable<DemonicsFloat>, IComparable<DemonicsFloat>
{
    readonly long m_rawValue;

    public static readonly decimal Precision = (decimal)(new DemonicsFloat(1L));
    public static readonly DemonicsFloat One = new DemonicsFloat(ONE);
    public static readonly DemonicsFloat Zero = new DemonicsFloat();
    public static readonly DemonicsFloat PI = new DemonicsFloat(Pi);
    public static readonly DemonicsFloat PITimes2 = new DemonicsFloat(PiTimes2);
    public static readonly DemonicsFloat PIOver180 = new DemonicsFloat((long)72);
    public static readonly DemonicsFloat Rad2Deg = DemonicsFloat.Pi * (DemonicsFloat)2 / (DemonicsFloat)360;
    public static readonly DemonicsFloat Deg2Rad = (DemonicsFloat)360 / (DemonicsFloat.Pi * (DemonicsFloat)2);

    const long Pi = 12868;
    const long PiTimes2 = 25736;

    public const int FRACTIONAL_PLACES = 12;
    const long ONE = 1L << FRACTIONAL_PLACES;

    public static int Sign(DemonicsFloat value)
    {
        return
            value.m_rawValue < 0 ? -1 :
            value.m_rawValue > 0 ? 1 :
            0;
    }

    public static DemonicsFloat Abs(DemonicsFloat value)
    {
        return new DemonicsFloat(value.m_rawValue > 0 ? value.m_rawValue : -value.m_rawValue);
    }

    public static DemonicsFloat Floor(DemonicsFloat value)
    {
        return new DemonicsFloat((long)((ulong)value.m_rawValue & 0xFFFFFFFFFFFFF000));
    }

    public static DemonicsFloat Ceiling(DemonicsFloat value)
    {
        var hasFractionalPart = (value.m_rawValue & 0x0000000000000FFF) != 0;
        return hasFractionalPart ? Floor(value) + One : value;
    }

    public static DemonicsFloat operator +(DemonicsFloat x, DemonicsFloat y)
    {
        return new DemonicsFloat(x.m_rawValue + y.m_rawValue);
    }

    public static DemonicsFloat operator +(DemonicsFloat x, int y)
    {
        return x + (DemonicsFloat)y;
    }

    public static DemonicsFloat operator +(int x, DemonicsFloat y)
    {
        return (DemonicsFloat)x + y;
    }

    public static DemonicsFloat operator +(DemonicsFloat x, float y)
    {
        return x + (DemonicsFloat)y;
    }

    public static DemonicsFloat operator +(float x, DemonicsFloat y)
    {
        return (DemonicsFloat)x + y;
    }

    public static DemonicsFloat operator +(DemonicsFloat x, double y)
    {
        return x + (DemonicsFloat)y;
    }

    public static DemonicsFloat operator +(double x, DemonicsFloat y)
    {
        return (DemonicsFloat)x + y;
    }

    public static DemonicsFloat operator -(DemonicsFloat x, DemonicsFloat y)
    {
        return new DemonicsFloat(x.m_rawValue - y.m_rawValue);
    }

    public static DemonicsFloat operator -(DemonicsFloat x, int y)
    {
        return x - (DemonicsFloat)y;
    }

    public static DemonicsFloat operator -(int x, DemonicsFloat y)
    {
        return (DemonicsFloat)x - y;
    }

    public static DemonicsFloat operator -(DemonicsFloat x, float y)
    {
        return x - (DemonicsFloat)y;
    }

    public static DemonicsFloat operator -(float x, DemonicsFloat y)
    {
        return (DemonicsFloat)x + y;
    }

    public static DemonicsFloat operator -(DemonicsFloat x, double y)
    {
        return x - (DemonicsFloat)y;
    }

    public static DemonicsFloat operator -(double x, DemonicsFloat y)
    {
        return (DemonicsFloat)x - y;
    }


    public static DemonicsFloat operator *(DemonicsFloat x, DemonicsFloat y)
    {
        return new DemonicsFloat((x.m_rawValue * y.m_rawValue) >> FRACTIONAL_PLACES);
    }

    public static DemonicsFloat operator *(DemonicsFloat x, int y)
    {
        return x * (DemonicsFloat)y;
    }

    public static DemonicsFloat operator *(int x, DemonicsFloat y)
    {
        return (DemonicsFloat)x * y;
    }

    public static DemonicsFloat operator *(DemonicsFloat x, float y)
    {
        return x * (DemonicsFloat)y;
    }

    public static DemonicsFloat operator *(float x, DemonicsFloat y)
    {
        return (DemonicsFloat)x * y;
    }

    public static DemonicsFloat operator *(DemonicsFloat x, double y)
    {
        return x * (DemonicsFloat)y;
    }

    public static DemonicsFloat operator *(double x, DemonicsFloat y)
    {
        return (DemonicsFloat)x * y;
    }

    public static DemonicsFloat operator /(DemonicsFloat x, DemonicsFloat y)
    {
        return new DemonicsFloat((x.m_rawValue << FRACTIONAL_PLACES) / y.m_rawValue);
    }

    public static DemonicsFloat operator /(DemonicsFloat x, int y)
    {
        return x / (DemonicsFloat)y;
    }

    public static DemonicsFloat operator /(int x, DemonicsFloat y)
    {
        return (DemonicsFloat)x / y;
    }

    public static DemonicsFloat operator /(DemonicsFloat x, float y)
    {
        return x / (DemonicsFloat)y;
    }

    public static DemonicsFloat operator /(float x, DemonicsFloat y)
    {
        return (DemonicsFloat)x / y;
    }

    public static DemonicsFloat operator /(double x, DemonicsFloat y)
    {
        return (DemonicsFloat)x / y;
    }

    public static DemonicsFloat operator /(DemonicsFloat x, double y)
    {
        return x / (DemonicsFloat)y;
    }

    public static DemonicsFloat operator %(DemonicsFloat x, DemonicsFloat y)
    {
        return new DemonicsFloat(x.m_rawValue % y.m_rawValue);
    }

    public static DemonicsFloat operator -(DemonicsFloat x)
    {
        return new DemonicsFloat(-x.m_rawValue);
    }

    public static bool operator ==(DemonicsFloat x, DemonicsFloat y)
    {
        return x.m_rawValue == y.m_rawValue;
    }

    public static bool operator !=(DemonicsFloat x, DemonicsFloat y)
    {
        return x.m_rawValue != y.m_rawValue;
    }

    public static bool operator >(DemonicsFloat x, DemonicsFloat y)
    {
        return x.m_rawValue > y.m_rawValue;
    }

    public static bool operator >(DemonicsFloat x, int y)
    {
        return x > (DemonicsFloat)y;
    }
    public static bool operator <(DemonicsFloat x, int y)
    {
        return x < (DemonicsFloat)y;
    }
    public static bool operator >(DemonicsFloat x, float y)
    {
        return x > (DemonicsFloat)y;
    }
    public static bool operator <(DemonicsFloat x, float y)
    {
        return x < (DemonicsFloat)y;
    }

    public static bool operator <(DemonicsFloat x, DemonicsFloat y)
    {
        return x.m_rawValue < y.m_rawValue;
    }

    public static bool operator >=(DemonicsFloat x, DemonicsFloat y)
    {
        return x.m_rawValue >= y.m_rawValue;
    }

    public static bool operator <=(DemonicsFloat x, DemonicsFloat y)
    {
        return x.m_rawValue <= y.m_rawValue;
    }

    public static DemonicsFloat operator >>(DemonicsFloat x, int amount)
    {
        return new DemonicsFloat(x.RawValue >> amount);
    }

    public static DemonicsFloat operator <<(DemonicsFloat x, int amount)
    {
        return new DemonicsFloat(x.RawValue << amount);
    }


    public static explicit operator DemonicsFloat(long value)
    {
        return new DemonicsFloat(value * ONE);
    }
    public static explicit operator long(DemonicsFloat value)
    {
        return value.m_rawValue >> FRACTIONAL_PLACES;
    }
    public static explicit operator DemonicsFloat(float value)
    {
        return new DemonicsFloat((long)(value * ONE));
    }
    public static explicit operator float(DemonicsFloat value)
    {
        return (float)value.m_rawValue / ONE;
    }
    public static explicit operator int(DemonicsFloat value)
    {
        return (int)((float)value);
    }
    public static explicit operator DemonicsFloat(double value)
    {
        return new DemonicsFloat((long)(value * ONE));
    }
    public static explicit operator double(DemonicsFloat value)
    {
        return (double)value.m_rawValue / ONE;
    }
    public static explicit operator DemonicsFloat(decimal value)
    {
        return new DemonicsFloat((long)(value * ONE));
    }
    public static explicit operator decimal(DemonicsFloat value)
    {
        return (decimal)value.m_rawValue / ONE;
    }

    public override bool Equals(object obj)
    {
        return obj is DemonicsFloat && ((DemonicsFloat)obj).m_rawValue == m_rawValue;
    }

    public override int GetHashCode()
    {
        return m_rawValue.GetHashCode();
    }

    public bool Equals(DemonicsFloat other)
    {
        return m_rawValue == other.m_rawValue;
    }

    public int CompareTo(DemonicsFloat other)
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

    public static DemonicsFloat FromRaw(long rawValue)
    {
        return new DemonicsFloat(rawValue);
    }

    public static DemonicsFloat Pow(DemonicsFloat x, int y)
    {
        if (y == 1) return x;
        DemonicsFloat result = DemonicsFloat.Zero;
        DemonicsFloat tmp = Pow(x, y / 2);
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


    DemonicsFloat(long rawValue)
    {
        m_rawValue = rawValue;
    }

    public DemonicsFloat(int value)
    {
        m_rawValue = value * ONE;
    }

    public static DemonicsFloat Sqrt(DemonicsFloat f, int numberIterations)
    {
        if (f.RawValue < 0)
        {
            throw new ArithmeticException("sqrt error");
        }

        if (f.RawValue == 0)
            return DemonicsFloat.Zero;

        DemonicsFloat k = f + DemonicsFloat.One >> 1;
        for (int i = 0; i < numberIterations; i++)
            k = (k + (f / k)) >> 1;

        if (k.RawValue < 0)
            throw new ArithmeticException("Overflow");
        else
            return k;
    }

    public static DemonicsFloat Sqrt(DemonicsFloat f)
    {
        byte numberOfIterations = 8;
        if (f.RawValue > 0x64000)
            numberOfIterations = 12;
        if (f.RawValue > 0x3e8000)
            numberOfIterations = 16;
        return Sqrt(f, numberOfIterations);
    }

    public static DemonicsFloat Lerp(DemonicsFloat from, DemonicsFloat to, DemonicsFloat factor)
    {
        return from * (1 - factor) + to * factor;
    }
    #region Sin
    public static DemonicsFloat Sin(DemonicsFloat i)
    {
        DemonicsFloat j = (DemonicsFloat)0;
        for (; i < DemonicsFloat.Zero; i += DemonicsFloat.FromRaw(PiTimes2)) ;
        if (i > DemonicsFloat.FromRaw(PiTimes2))
            i %= DemonicsFloat.FromRaw(PiTimes2);

        DemonicsFloat k = (i * DemonicsFloat.FromRaw(100000000)) / DemonicsFloat.FromRaw(7145244444);
        if (i != DemonicsFloat.Zero && i != DemonicsFloat.FromRaw(6434) && i != DemonicsFloat.FromRaw(Pi) &&
            i != DemonicsFloat.FromRaw(19302) && i != DemonicsFloat.FromRaw(PiTimes2))
            j = (i * DemonicsFloat.FromRaw(100000000)) / DemonicsFloat.FromRaw(7145244444) - k * DemonicsFloat.FromRaw(10);
        if (k <= DemonicsFloat.FromRaw(90))
            return sin_lookup(k, j);
        if (k <= DemonicsFloat.FromRaw(180))
            return sin_lookup(DemonicsFloat.FromRaw(180) - k, j);
        if (k <= DemonicsFloat.FromRaw(270))
            return -sin_lookup(k - DemonicsFloat.FromRaw(180), j);
        else
            return -sin_lookup(DemonicsFloat.FromRaw(360) - k, j);
    }

    private static DemonicsFloat sin_lookup(DemonicsFloat i, DemonicsFloat j)
    {
        if (j > 0 && j < DemonicsFloat.FromRaw(10) && i < DemonicsFloat.FromRaw(90))
            return DemonicsFloat.FromRaw(SIN_TABLE[i.RawValue]) +
                ((DemonicsFloat.FromRaw(SIN_TABLE[i.RawValue + 1]) - DemonicsFloat.FromRaw(SIN_TABLE[i.RawValue])) /
                DemonicsFloat.FromRaw(10)) * j;
        else
            return DemonicsFloat.FromRaw(SIN_TABLE[i.RawValue]);
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
    public static DemonicsFloat Cos(DemonicsFloat i)
    {
        return Sin(i + DemonicsFloat.FromRaw(6435));
    }

    public static DemonicsFloat Tan(DemonicsFloat i)
    {
        return Sin(i) / Cos(i);
    }

    public static DemonicsFloat Asin(DemonicsFloat F)
    {
        bool isNegative = F < 0;
        F = Abs(F);

        if (F > DemonicsFloat.One)
            throw new ArithmeticException("Bad Asin Input:" + (double)F);

        DemonicsFloat f1 = ((((DemonicsFloat.FromRaw(145103 >> FRACTIONAL_PLACES) * F) -
            DemonicsFloat.FromRaw(599880 >> FRACTIONAL_PLACES) * F) +
            DemonicsFloat.FromRaw(1420468 >> FRACTIONAL_PLACES) * F) -
            DemonicsFloat.FromRaw(3592413 >> FRACTIONAL_PLACES) * F) +
            DemonicsFloat.FromRaw(26353447 >> FRACTIONAL_PLACES);
        DemonicsFloat f2 = PI / (DemonicsFloat)2 - (Sqrt(DemonicsFloat.One - F) * f1);

        return isNegative ? -f2 : f2;
    }
    #endregion

    #region ATan, ATan2
    public static DemonicsFloat Atan(DemonicsFloat F)
    {
        return Asin(F / Sqrt(DemonicsFloat.One + (F * F)));
    }

    public static DemonicsFloat Atan2(DemonicsFloat F1, DemonicsFloat F2)
    {
        if (F2.RawValue == 0 && F1.RawValue == 0)
            return (DemonicsFloat)0;

        DemonicsFloat result = (DemonicsFloat)0;
        if (F2 > 0)
            result = Atan(F1 / F2);
        else if (F2 < 0)
        {
            if (F1 >= (DemonicsFloat)0)
                result = (PI - Atan(Abs(F1 / F2)));
            else
                result = -(PI - Atan(Abs(F1 / F2)));
        }
        else
            result = (F1 >= (DemonicsFloat)0 ? PI : -PI) / (DemonicsFloat)2;

        return result;
    }
    #endregion
}

public struct DemonicsVector3
{
    public DemonicsFloat x;
    public DemonicsFloat y;
    public DemonicsFloat z;

    public DemonicsVector3(DemonicsFloat x, DemonicsFloat y, DemonicsFloat z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public DemonicsVector3(DemonicsVector3 v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }

    public DemonicsFloat this[int index]
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

    public static DemonicsVector3 Zero
    {
        get { return new DemonicsVector3(DemonicsFloat.Zero, DemonicsFloat.Zero, DemonicsFloat.Zero); }
    }

    public static DemonicsVector3 operator +(DemonicsVector3 a, DemonicsVector3 b)
    {
        DemonicsFloat x = a.x + b.x;
        DemonicsFloat y = a.y + b.y;
        DemonicsFloat z = a.z + b.z;
        return new DemonicsVector3(x, y, z);
    }

    public static DemonicsVector3 operator -(DemonicsVector3 a, DemonicsVector3 b)
    {
        DemonicsFloat x = a.x - b.x;
        DemonicsFloat y = a.y - b.y;
        DemonicsFloat z = a.z - b.z;
        return new DemonicsVector3(x, y, z);
    }

    public static DemonicsVector3 operator *(DemonicsFloat d, DemonicsVector3 a)
    {
        DemonicsFloat x = a.x * d;
        DemonicsFloat y = a.y * d;
        DemonicsFloat z = a.z * d;
        return new DemonicsVector3(x, y, z);
    }

    public static DemonicsVector3 operator *(DemonicsVector3 a, DemonicsFloat d)
    {
        DemonicsFloat x = a.x * d;
        DemonicsFloat y = a.y * d;
        DemonicsFloat z = a.z * d;
        return new DemonicsVector3(x, y, z);
    }

    public static DemonicsVector3 operator /(DemonicsVector3 a, DemonicsFloat d)
    {
        DemonicsFloat x = a.x / d;
        DemonicsFloat y = a.y / d;
        DemonicsFloat z = a.z / d;
        return new DemonicsVector3(x, y, z);
    }

    public static bool operator ==(DemonicsVector3 lhs, DemonicsVector3 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
    }

    public static bool operator !=(DemonicsVector3 lhs, DemonicsVector3 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
    }

    public static DemonicsFloat SqrMagnitude(DemonicsVector3 a)
    {
        return a.x * a.x + a.y * a.y + a.z * a.z;
    }

    public static DemonicsFloat Distance(DemonicsVector3 a, DemonicsVector3 b)
    {
        return Magnitude(a - b);
    }

    public static DemonicsFloat Magnitude(DemonicsVector3 a)
    {
        return DemonicsFloat.Sqrt(DemonicsVector3.SqrMagnitude(a));
    }

    public void Normalize()
    {
        DemonicsFloat n = x * x + y * y + z * z;
        if (n == DemonicsFloat.Zero)
            return;

        n = DemonicsFloat.Sqrt(n);

        if (n < (DemonicsFloat)0.0001)
        {
            return;
        }

        n = 1 / n;
        x *= n;
        y *= n;
        z *= n;
    }

    public DemonicsVector3 GetNormalized()
    {
        DemonicsVector3 v = new DemonicsVector3(this);
        v.Normalize();
        return v;
    }

    public override string ToString()
    {
        return string.Format("x:{0} y:{1} z:{2}", x, y, z);
    }

    public override bool Equals(object obj)
    {
        return obj is DemonicsVector2 && ((DemonicsVector3)obj) == this;
    }

    public override int GetHashCode()
    {
        return this.x.GetHashCode() + this.y.GetHashCode() + this.z.GetHashCode();
    }

    public static DemonicsVector3 Lerp(DemonicsVector3 from, DemonicsVector3 to, DemonicsFloat factor)
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

public struct DemonicsVector2
{
    public DemonicsFloat x;
    public DemonicsFloat y;

    public DemonicsVector2(DemonicsFloat x, DemonicsFloat y)
    {
        this.x = x;
        this.y = y;
    }
    public DemonicsVector2(DemonicsFloat x, int y)
    {
        this.x = x;
        this.y = (DemonicsFloat)y;
    }

    public DemonicsVector2(int x, int y)
    {
        this.x = (DemonicsFloat)x;
        this.y = (DemonicsFloat)y;
    }
    public DemonicsVector2(DemonicsVector2 v)
    {
        this.x = v.x;
        this.y = v.y;
    }
    public static DemonicsVector2 operator -(DemonicsVector2 a, int b)
    {
        DemonicsFloat x = a.x - b;
        DemonicsFloat y = a.y - b;
        return new DemonicsVector2(x, y);
    }

    public DemonicsFloat this[int index]
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

    public static DemonicsVector2 Zero
    {
        get { return new DemonicsVector2(DemonicsFloat.Zero, DemonicsFloat.Zero); }
    }

    public static DemonicsVector2 operator +(DemonicsVector2 a, DemonicsVector2 b)
    {
        DemonicsFloat x = a.x + b.x;
        DemonicsFloat y = a.y + b.y;
        return new DemonicsVector2(x, y);
    }

    public static DemonicsVector2 operator -(DemonicsVector2 a, DemonicsVector2 b)
    {
        DemonicsFloat x = a.x - b.x;
        DemonicsFloat y = a.y - b.y;
        return new DemonicsVector2(x, y);
    }

    public static DemonicsVector2 operator *(DemonicsFloat d, DemonicsVector2 a)
    {
        DemonicsFloat x = a.x * d;
        DemonicsFloat y = a.y * d;
        return new DemonicsVector2(x, y);
    }

    public static DemonicsVector2 operator *(DemonicsVector2 a, DemonicsFloat d)
    {
        DemonicsFloat x = a.x * d;
        DemonicsFloat y = a.y * d;
        return new DemonicsVector2(x, y);
    }

    public static DemonicsVector2 operator /(DemonicsVector2 a, DemonicsFloat d)
    {
        DemonicsFloat x = a.x / d;
        DemonicsFloat y = a.y / d;
        return new DemonicsVector2(x, y);
    }

    public static bool operator ==(DemonicsVector2 lhs, DemonicsVector2 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }

    public static bool operator !=(DemonicsVector2 lhs, DemonicsVector2 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y;
    }

    public override bool Equals(object obj)
    {
        return obj is DemonicsVector2 && ((DemonicsVector2)obj) == this;
    }

    public override int GetHashCode()
    {
        return this.x.GetHashCode() + this.y.GetHashCode();
    }


    public static DemonicsFloat SqrMagnitude(DemonicsVector2 a)
    {
        return a.x * a.x + a.y * a.y;
    }

    public static DemonicsFloat Distance(DemonicsVector2 a, DemonicsVector2 b)
    {
        return Magnitude(a - b);
    }

    public static DemonicsFloat Magnitude(DemonicsVector2 a)
    {
        return DemonicsFloat.Sqrt(DemonicsVector2.SqrMagnitude(a));
    }

    public void Normalize()
    {
        DemonicsFloat n = x * x + y * y;
        if (n == DemonicsFloat.Zero)
            return;

        n = DemonicsFloat.Sqrt(n);

        if (n < (DemonicsFloat)0.0001)
        {
            return;
        }

        n = 1 / n;
        x *= n;
        y *= n;
    }

    public DemonicsVector2 GetNormalized()
    {
        DemonicsVector2 v = new DemonicsVector2(this);
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
