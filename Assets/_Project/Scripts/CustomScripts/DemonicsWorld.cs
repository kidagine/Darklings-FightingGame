using UnityEngine;

public class DemonicsWorld : MonoBehaviour
{
    [SerializeField] private int _frameRate = 60;
    [SerializeField] private bool _vSync = true;
    public static int Frame;


    void Awake()
    {
        Application.targetFrameRate = _frameRate;
        QualitySettings.vSyncCount = _vSync ? 1 : 0;
        Time.fixedDeltaTime = 0.01667f;
        Frame = 0;
    }

    public static bool WaitFrames(ref int frames)
    {
        frames--;
        if (frames <= 0)
            return true;
        return false;
    }

    public static bool WaitFramesOnce(ref int frames)
    {
        frames--;
        if (frames == 0)
            return true;
        return false;
    }

    public static void SetFramerate(int framerate, bool vSync = true)
    {
        Application.targetFrameRate = framerate;
        QualitySettings.vSyncCount = vSync ? 1 : 0;
    }
}
