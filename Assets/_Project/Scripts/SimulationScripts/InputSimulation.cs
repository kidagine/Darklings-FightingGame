using UnityEngine;

public class InputSimulation
{
    public static long GetInput(int id)
    {
        long input = 0;
        if (id == 0)
        {
            if (Input.anyKeyDown)
            {
                input |= NetworkInput.SKIP_BYTE;
            }
            if (NetworkInput.ONE_UP_INPUT)
            {
                input |= NetworkInput.UP_BYTE;
            }
            if (NetworkInput.ONE_DOWN_INPUT)
            {
                input |= NetworkInput.DOWN_BYTE;
            }
            if (NetworkInput.ONE_LEFT_INPUT)
            {
                input |= NetworkInput.LEFT_BYTE;
            }
            if (NetworkInput.ONE_RIGHT_INPUT)
            {
                input |= NetworkInput.RIGHT_BYTE;
            }
            if (NetworkInput.ONE_LIGHT_INPUT)
            {
                input |= NetworkInput.LIGHT_BYTE;
                NetworkInput.ONE_LIGHT_INPUT = false;
            }
            if (NetworkInput.ONE_MEDIUM_INPUT)
            {
                input |= NetworkInput.MEDIUM_BYTE;
                NetworkInput.ONE_MEDIUM_INPUT = false;
            }
            if (NetworkInput.ONE_HEAVY_INPUT)
            {
                input |= NetworkInput.HEAVY_BYTE;
                NetworkInput.ONE_HEAVY_INPUT = false;
            }
            if (NetworkInput.ONE_ARCANA_INPUT)
            {
                input |= NetworkInput.ARCANA_BYTE;
                NetworkInput.ONE_ARCANA_INPUT = false;
            }
            if (NetworkInput.ONE_SHADOW_INPUT)
            {
                input |= NetworkInput.SHADOW_BYTE;
                NetworkInput.ONE_SHADOW_INPUT = false;
            }
            if (NetworkInput.ONE_GRAB_INPUT)
            {
                input |= NetworkInput.GRAB_BYTE;
                NetworkInput.ONE_GRAB_INPUT = false;
            }
            if (NetworkInput.ONE_BLUE_FRENZY_INPUT)
            {
                input |= NetworkInput.BLUE_FRENZY_BYTE;
                NetworkInput.ONE_BLUE_FRENZY_INPUT = false;
            }
            if (NetworkInput.ONE_RED_FRENZY_INPUT)
            {
                input |= NetworkInput.RED_FRENZY_BYTE;
                NetworkInput.ONE_RED_FRENZY_INPUT = false;
            }
            if (NetworkInput.ONE_DASH_FORWARD_INPUT)
            {
                input |= NetworkInput.DASH_FORWARD_BYTE;
                NetworkInput.ONE_DASH_FORWARD_INPUT = false;
            }
            if (NetworkInput.ONE_DASH_BACKWARD_INPUT)
            {
                input |= NetworkInput.DASH_BACKWARD_BYTE;
                NetworkInput.ONE_DASH_BACKWARD_INPUT = false;
            }
        }
        if (id == 1)
        {
            Debug.Log("bbbb");
            if (Input.anyKeyDown)
            {
                input |= NetworkInput.SKIP_BYTE;
            }
            if (NetworkInput.TWO_UP_INPUT)
            {
                input |= NetworkInput.UP_BYTE;
            }
            if (NetworkInput.TWO_DOWN_INPUT)
            {
                input |= NetworkInput.DOWN_BYTE;
            }
            if (NetworkInput.TWO_LEFT_INPUT)
            {
                input |= NetworkInput.LEFT_BYTE;
            }
            if (NetworkInput.TWO_RIGHT_INPUT)
            {
                input |= NetworkInput.RIGHT_BYTE;
            }
            if (NetworkInput.TWO_LIGHT_INPUT)
            {
                input |= NetworkInput.LIGHT_BYTE;
                NetworkInput.TWO_LIGHT_INPUT = false;
            }
            if (NetworkInput.TWO_MEDIUM_INPUT)
            {
                input |= NetworkInput.MEDIUM_BYTE;
                NetworkInput.TWO_MEDIUM_INPUT = false;
            }
            if (NetworkInput.TWO_HEAVY_INPUT)
            {
                input |= NetworkInput.HEAVY_BYTE;
                NetworkInput.TWO_HEAVY_INPUT = false;
            }
            if (NetworkInput.TWO_ARCANA_INPUT)
            {
                input |= NetworkInput.ARCANA_BYTE;
                NetworkInput.TWO_ARCANA_INPUT = false;
            }
            if (NetworkInput.TWO_SHADOW_INPUT)
            {
                input |= NetworkInput.SHADOW_BYTE;
                NetworkInput.TWO_SHADOW_INPUT = false;
            }
            if (NetworkInput.TWO_GRAB_INPUT)
            {
                input |= NetworkInput.GRAB_BYTE;
                NetworkInput.TWO_GRAB_INPUT = false;
            }
            if (NetworkInput.TWO_BLUE_FRENZY_INPUT)
            {
                input |= NetworkInput.BLUE_FRENZY_BYTE;
                NetworkInput.TWO_BLUE_FRENZY_INPUT = false;
            }
            if (NetworkInput.TWO_RED_FRENZY_INPUT)
            {
                input |= NetworkInput.RED_FRENZY_BYTE;
                NetworkInput.TWO_RED_FRENZY_INPUT = false;
            }
            if (NetworkInput.TWO_DASH_FORWARD_INPUT)
            {
                input |= NetworkInput.DASH_FORWARD_BYTE;
                NetworkInput.TWO_DASH_FORWARD_INPUT = false;
            }
            if (NetworkInput.TWO_DASH_BACKWARD_INPUT)
            {
                input |= NetworkInput.DASH_BACKWARD_BYTE;
                NetworkInput.TWO_DASH_BACKWARD_INPUT = false;
            }
        }
        return input;
    }

    public static void ParseInputs(long inputs, out bool skip, out bool up, out bool down, out bool left, out bool right, out bool light, out bool medium,
    out bool heavy, out bool arcana, out bool grab, out bool shadow, out bool blueFrenzy, out bool redFrenzy, out bool dashForward, out bool dashBackward)
    {
        if ((inputs & NetworkInput.SKIP_BYTE) != 0)
        {
            skip = true;
        }
        else
        {
            skip = false;
        }
        if ((inputs & NetworkInput.UP_BYTE) != 0)
        {
            up = true;
        }
        else
        {
            up = false;
        }
        if ((inputs & NetworkInput.DOWN_BYTE) != 0)
        {
            down = true;
        }
        else
        {
            down = false;
        }
        if ((inputs & NetworkInput.LEFT_BYTE) != 0)
        {
            left = true;
        }
        else
        {
            left = false;
        }
        if ((inputs & NetworkInput.RIGHT_BYTE) != 0)
        {
            right = true;
        }
        else
        {
            right = false;
        }
        if ((inputs & NetworkInput.LIGHT_BYTE) != 0)
        {
            light = true;
        }
        else
        {
            light = false;
        }
        if ((inputs & NetworkInput.MEDIUM_BYTE) != 0)
        {
            medium = true;
        }
        else
        {
            medium = false;
        }
        if ((inputs & NetworkInput.HEAVY_BYTE) != 0)
        {
            heavy = true;
        }
        else
        {
            heavy = false;
        }
        if ((inputs & NetworkInput.ARCANA_BYTE) != 0)
        {
            arcana = true;
        }
        else
        {
            arcana = false;
        }
        if ((inputs & NetworkInput.GRAB_BYTE) != 0)
        {
            grab = true;
        }
        else
        {
            grab = false;
        }
        if ((inputs & NetworkInput.SHADOW_BYTE) != 0)
        {
            shadow = true;
        }
        else
        {
            shadow = false;
        }
        if ((inputs & NetworkInput.BLUE_FRENZY_BYTE) != 0)
        {
            blueFrenzy = true;
        }
        else
        {
            blueFrenzy = false;
        }
        if ((inputs & NetworkInput.RED_FRENZY_BYTE) != 0)
        {
            redFrenzy = true;
        }
        else
        {
            redFrenzy = false;
        }
        if ((inputs & NetworkInput.DASH_FORWARD_BYTE) != 0)
        {
            dashForward = true;
        }
        else
        {
            dashForward = false;
        }
        if ((inputs & NetworkInput.DASH_BACKWARD_BYTE) != 0)
        {
            dashBackward = true;
        }
        else
        {
            dashBackward = false;
        }
    }
}
