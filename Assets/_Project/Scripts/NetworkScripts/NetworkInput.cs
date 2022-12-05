using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkInput
{
    public const bool IS_LOCAL = true;
    public const int UP_BYTE = (1 << 0);
    public const int DOWN_BYTE = (1 << 1);
    public const int LEFT_BYTE = (1 << 2);
    public const int RIGHT_BYTE = (1 << 3);
    public const int LIGHT_BYTE = (1 << 4);
    public const int MEDIUM_BYTE = (1 << 5);
    public const int HEAVY_BYTE = (1 << 6);
    public const int ARCANA_BYTE = (1 << 7);
    public const int GRAB_BYTE = (1 << 8);
    public const int SHADOW_BYTE = (1 << 9);
    public const int BLUE_FRENZY_BYTE = (1 << 10);
    public const int RED_FRENZY_BYTE = (1 << 11);


    public static bool UP_INPUT { get; set; }
    public static bool DOWN_INPUT { get; set; }
    public static bool LEFT_INPUT { get; set; }
    public static bool RIGHT_INPUT { get; set; }
    public static bool LIGHT_INPUT { get; set; }
    public static bool MEDIUM_INPUT { get; set; }
    public static bool HEAVY_INPUT { get; set; }
    public static bool ARCANA_INPUT { get; set; }
    public static bool GRAB_INPUT { get; set; }
    public static bool SHADOW_INPUT { get; set; }
    public static bool BLUE_FRENZY_INPUT { get; set; }
    public static bool RED_FRENZY_INPUT { get; set; }
}
