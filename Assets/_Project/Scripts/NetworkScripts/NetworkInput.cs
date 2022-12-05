using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkInput
{
    public const int UP_BYTE = (1 << 0);
    public const int DOWN_BYTE = (1 << 1);
    public const int LEFT_BYTE = (1 << 2);
    public const int RIGHT_BYTE = (1 << 3);
    public const int LIGHT_BYTE = (1 << 4);
    public const int MEDIUM_BYTE = (1 << 5);
    public const int HEAVY_BYTE = (1 << 6);
    public const int ARCANA_BYTE = (1 << 7);

    public static bool LIGHT_INPUT { get; set; }
    public static bool MEDIUM_INPUT { get; set; }
    public static bool HEAVY_INPUT { get; set; }

}
