using UnityEngine;

public class InputBufferAction : MonoBehaviour
{
    public enum InputAction { Punch, Kick, Jump, Block };
    public InputAction Action;
    public float Timestamp;

    public static float TimeBeforeActionsExpire = 2f;


    public InputBufferAction(InputAction ia, float stamp)
    {
        Action = ia;
        Timestamp = stamp;
    }

    public bool CheckIfValid()
    {
        bool returnValue = false;
        if (Timestamp + TimeBeforeActionsExpire >= Time.time)
        {
            returnValue = true;
        }
        return returnValue;
    }
}
