using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkInput
{
    public const int SKIP_BYTE = (1 << 0);
    public const int UP_BYTE = (1 << 1);
    public const int DOWN_BYTE = (1 << 2);
    public const int LEFT_BYTE = (1 << 3);
    public const int RIGHT_BYTE = (1 << 4);
    public const int LIGHT_BYTE = (1 << 5);
    public const int MEDIUM_BYTE = (1 << 6);
    public const int HEAVY_BYTE = (1 << 7);
    public const int ARCANA_BYTE = (1 << 8);
    public const int GRAB_BYTE = (1 << 9);
    public const int SHADOW_BYTE = (1 << 10);
    public const int BLUE_FRENZY_BYTE = (1 << 11);
    public const int RED_FRENZY_BYTE = (1 << 12);
    public const int DASH_FORWARD_BYTE = (1 << 13);
    public const int DASH_BACKWARD_BYTE = (1 << 14);

    public static bool IS_LOCAL { get; set; } = true;
    public static bool ONE_UP_INPUT { get; set; }
    public static bool ONE_DOWN_INPUT { get; set; }
    public static bool ONE_LEFT_INPUT { get; set; }
    public static bool ONE_RIGHT_INPUT { get; set; }
    public static bool ONE_LIGHT_INPUT { get; set; }
    public static bool ONE_MEDIUM_INPUT { get; set; }
    public static bool ONE_HEAVY_INPUT { get; set; }
    public static bool ONE_ARCANA_INPUT { get; set; }
    public static bool ONE_GRAB_INPUT { get; set; }
    public static bool ONE_SHADOW_INPUT { get; set; }
    public static bool ONE_BLUE_FRENZY_INPUT { get; set; }
    public static bool ONE_RED_FRENZY_INPUT { get; set; }
    public static bool ONE_DASH_FORWARD_INPUT { get; set; }
    public static bool ONE_DASH_BACKWARD_INPUT { get; set; }
    public static bool TWO_UP_INPUT { get; set; }
    public static bool TWO_DOWN_INPUT { get; set; }
    public static bool TWO_LEFT_INPUT { get; set; }
    public static bool TWO_RIGHT_INPUT { get; set; }
    public static bool TWO_LIGHT_INPUT { get; set; }
    public static bool TWO_MEDIUM_INPUT { get; set; }
    public static bool TWO_HEAVY_INPUT { get; set; }
    public static bool TWO_ARCANA_INPUT { get; set; }
    public static bool TWO_GRAB_INPUT { get; set; }
    public static bool TWO_SHADOW_INPUT { get; set; }
    public static bool TWO_BLUE_FRENZY_INPUT { get; set; }
    public static bool TWO_RED_FRENZY_INPUT { get; set; }
    public static bool TWO_DASH_FORWARD_INPUT { get; set; }
    public static bool TWO_DASH_BACKWARD_INPUT { get; set; }

}
