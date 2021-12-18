using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PerformancePlayTest
{
    [UnityTest, Performance]
    public IEnumerator Loading_GameScene()
    {
        using (Measure.Scope("Setup.LoadScene"))
        {
            SceneManager.LoadScene("GameScene");
        }
        yield return null;

        yield return Measure.Frames().Run();
    }

    [UnityTest, Performance]
    public IEnumerator Player_Measure_AttackAction()
    {
        SceneManager.LoadScene("GameScene");
        yield return null;

        Player player = Object.FindObjectOfType<Player>();
        PlayerController playerController = Object.FindObjectOfType<PlayerController>();
        playerController.enabled = false;

        Measure.Method(() =>
        {
            player.AttackAction();
        })
        .MeasurementCount(10)
        .IterationsPerMeasurement(5)
        .Run();
    }
}
