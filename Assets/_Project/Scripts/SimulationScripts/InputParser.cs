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

        ParseSequence(inputs, ref inputTriggers.inputSequence);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[0], NetworkInput.LIGHT_BYTE, NetworkInput.BLUE_FRENZY_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[1], NetworkInput.MEDIUM_BYTE, NetworkInput.BLUE_FRENZY_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[2], NetworkInput.SHADOW_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[3], NetworkInput.HEAVY_BYTE, NetworkInput.RED_FRENZY_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[4], NetworkInput.ARCANA_BYTE, NetworkInput.RED_FRENZY_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[5], NetworkInput.GRAB_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[6], NetworkInput.DASH_FORWARD_BYTE);
        ParseTrigger(inputs, ref inputTriggers.inputTriggers[7], NetworkInput.DASH_BACKWARD_BYTE);
    }

    private static void ParseSequence(long inputs, ref InputSequence inputSequence)
    {
        bool up = (inputs & NetworkInput.UP_BYTE) != 0;
        bool down = (inputs & NetworkInput.DOWN_BYTE) != 0;
        bool left = (inputs & NetworkInput.LEFT_BYTE) != 0;
        bool right = (inputs & NetworkInput.RIGHT_BYTE) != 0;
        if (left && right)
        {
            left = false;
            right = false;
        }
        if (up && down)
        {
            up = false;
            down = false;
        }

        if (up & left)
            inputSequence.inputDirectionEnum = InputDirectionEnum.UpLeft;
        else if (up & right)
            inputSequence.inputDirectionEnum = InputDirectionEnum.UpRight;
        else if (down & left)
            inputSequence.inputDirectionEnum = InputDirectionEnum.DownLeft;
        else if (down & right)
            inputSequence.inputDirectionEnum = InputDirectionEnum.DownRight;
        else if (up)
            inputSequence.inputDirectionEnum = InputDirectionEnum.Up;
        else if (down)
            inputSequence.inputDirectionEnum = InputDirectionEnum.Down;
        else if (left)
            inputSequence.inputDirectionEnum = InputDirectionEnum.Left;
        else if (right)
            inputSequence.inputDirectionEnum = InputDirectionEnum.Right;
        else
            inputSequence.inputDirectionEnum = InputDirectionEnum.Neutral;
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
