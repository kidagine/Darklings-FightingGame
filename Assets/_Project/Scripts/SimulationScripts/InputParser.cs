using UnityEngine;

public class InputParser
{
    public static long GetInput(int id)
    {
        long input = 0;
        if (id == 0)
        {
            if (UnityEngine.Input.anyKeyDown)
                input |= NetworkInput.SKIP_BYTE;
            if (NetworkInput.ONE_UP_INPUT)
                input |= NetworkInput.UP_BYTE;
            if (NetworkInput.ONE_DOWN_INPUT)
                input |= NetworkInput.DOWN_BYTE;
            if (NetworkInput.ONE_LEFT_INPUT)
                input |= NetworkInput.LEFT_BYTE;
            if (NetworkInput.ONE_RIGHT_INPUT)
                input |= NetworkInput.RIGHT_BYTE;
            if (NetworkInput.ONE_NEUTRAL_INPUT)
                input |= NetworkInput.NEUTRAL_BYTE;
            if (NetworkInput.ONE_UP_RIGHT_INPUT)
                input |= NetworkInput.UP_RIGHT_BYTE;
            if (NetworkInput.ONE_UP_LEFT_INPUT)
                input |= NetworkInput.UP_LEFT_BYTE;
            if (NetworkInput.ONE_DOWN_RIGHT_INPUT)
                input |= NetworkInput.DOWN_RIGHT_BYTE;
            if (NetworkInput.ONE_DOWN_LEFT_INPUT)
                input |= NetworkInput.DOWN_LEFT_BYTE;
            if (NetworkInput.ONE_NEUTRAL_INPUT)
                input |= NetworkInput.NEUTRAL_BYTE;

            if (NetworkInput.ONE_LIGHT_INPUT)
                input |= NetworkInput.LIGHT_BYTE;
            if (NetworkInput.ONE_MEDIUM_INPUT)
                input |= NetworkInput.MEDIUM_BYTE;
            if (NetworkInput.ONE_HEAVY_INPUT)
                input |= NetworkInput.HEAVY_BYTE;
            if (NetworkInput.ONE_ARCANA_INPUT)
                input |= NetworkInput.ARCANA_BYTE;
            if (NetworkInput.ONE_SHADOW_INPUT)
                input |= NetworkInput.SHADOW_BYTE;
            if (NetworkInput.ONE_GRAB_INPUT)
                input |= NetworkInput.GRAB_BYTE;
            if (NetworkInput.ONE_BLUE_FRENZY_INPUT)
                input |= NetworkInput.BLUE_FRENZY_BYTE;
            if (NetworkInput.ONE_RED_FRENZY_INPUT)
                input |= NetworkInput.RED_FRENZY_BYTE;
            if (NetworkInput.ONE_DASH_FORWARD_INPUT)
                input |= NetworkInput.DASH_FORWARD_BYTE;
            if (NetworkInput.ONE_DASH_BACKWARD_INPUT)
                input |= NetworkInput.DASH_BACKWARD_BYTE;
        }
        if (id == 1)
        {
            if (UnityEngine.Input.anyKeyDown)
                input |= NetworkInput.SKIP_BYTE;
            if (NetworkInput.TWO_UP_INPUT)
                input |= NetworkInput.UP_BYTE;
            if (NetworkInput.TWO_DOWN_INPUT)
                input |= NetworkInput.DOWN_BYTE;
            if (NetworkInput.TWO_LEFT_INPUT)
                input |= NetworkInput.LEFT_BYTE;
            if (NetworkInput.TWO_RIGHT_INPUT)
                input |= NetworkInput.RIGHT_BYTE;
            if (NetworkInput.TWO_UP_RIGHT_INPUT)
                input |= NetworkInput.UP_RIGHT_BYTE;
            if (NetworkInput.TWO_UP_LEFT_INPUT)
                input |= NetworkInput.UP_LEFT_BYTE;
            if (NetworkInput.TWO_DOWN_RIGHT_INPUT)
                input |= NetworkInput.DOWN_RIGHT_BYTE;
            if (NetworkInput.TWO_DOWN_LEFT_INPUT)
                input |= NetworkInput.DOWN_LEFT_BYTE;
            if (NetworkInput.TWO_NEUTRAL_INPUT)
                input |= NetworkInput.NEUTRAL_BYTE;

            if (NetworkInput.TWO_LIGHT_INPUT)
                input |= NetworkInput.LIGHT_BYTE;
            if (NetworkInput.TWO_MEDIUM_INPUT)
                input |= NetworkInput.MEDIUM_BYTE;
            if (NetworkInput.TWO_HEAVY_INPUT)
                input |= NetworkInput.HEAVY_BYTE;
            if (NetworkInput.TWO_ARCANA_INPUT)
                input |= NetworkInput.ARCANA_BYTE;
            if (NetworkInput.TWO_SHADOW_INPUT)
                input |= NetworkInput.SHADOW_BYTE;
            if (NetworkInput.TWO_GRAB_INPUT)
                input |= NetworkInput.GRAB_BYTE;
            if (NetworkInput.TWO_BLUE_FRENZY_INPUT)
                input |= NetworkInput.BLUE_FRENZY_BYTE;
            if (NetworkInput.TWO_RED_FRENZY_INPUT)
                input |= NetworkInput.RED_FRENZY_BYTE;
            if (NetworkInput.TWO_DASH_FORWARD_INPUT)
                input |= NetworkInput.DASH_FORWARD_BYTE;
            if (NetworkInput.TWO_DASH_BACKWARD_INPUT)
                input |= NetworkInput.DASH_BACKWARD_BYTE;
        }
        return input;
    }

    public static void ParseInput(long inputs, out bool skip, ref InputList inputTriggers)
    {
        if ((inputs & NetworkInput.SKIP_BYTE) != 0)
            skip = true;
        else
            skip = false;
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[0], NetworkInput.LIGHT_BYTE, NetworkInput.BLUE_FRENZY_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[1], NetworkInput.MEDIUM_BYTE, NetworkInput.BLUE_FRENZY_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[2], NetworkInput.SHADOW_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[3], NetworkInput.HEAVY_BYTE, NetworkInput.RED_FRENZY_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[4], NetworkInput.ARCANA_BYTE, NetworkInput.RED_FRENZY_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[5], NetworkInput.GRAB_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[6], NetworkInput.DASH_FORWARD_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[7], NetworkInput.DASH_BACKWARD_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[8], NetworkInput.UP_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[9], NetworkInput.DOWN_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[10], NetworkInput.LEFT_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[11], NetworkInput.RIGHT_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[12], NetworkInput.UP_RIGHT_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[13], NetworkInput.UP_LEFT_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[14], NetworkInput.DOWN_RIGHT_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[15], NetworkInput.DOWN_LEFT_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[16], NetworkInput.NEUTRAL_BYTE);
    }

    private static void ParseTrigger(long inputs, ref InputTrigger inputTrigger, int inputByte, int optionalInputByte = 0)
    {
        if ((inputs & inputByte) != 0 || (inputs & optionalInputByte) != 0)
        {
            if (!inputTrigger.held)
                inputTrigger.pressed = true;
            inputTrigger.held = true;
        }
        else
        {
            inputTrigger.held = false;
            inputTrigger.pressed = false;
        }
    }
}
