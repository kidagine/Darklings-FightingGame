using UnityEngine;

public class DemonicsWorld : MonoBehaviour
{
    public static int Frame;
    private float _tick;


    void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        Time.fixedDeltaTime = 0.01667f;
        Frame = 0;
    }

    void Update()
    {
        if (GameplayManager.Instance.HasGameStarted)
        {
            _tick += Time.deltaTime;
            if (_tick >= Time.fixedDeltaTime)
            {
                _tick -= Time.fixedDeltaTime;
                Physics2D.Simulate(Time.fixedDeltaTime);
            }
        }
    }

    void FixedUpdate()
    {
        if (GameplayManager.Instance.HasGameStarted)
        {
            Frame++;
        }
    }

    public static bool WaitFrames(ref int frames)
    {
        frames--;
        if (frames <= 0)
        {
            return true;
        }
        return false;
    }

    public static bool WaitFramesOnce(ref int frames)
    {
        frames--;
        if (frames == 0)
        {
            return true;
        }
        return false;
    }
}
