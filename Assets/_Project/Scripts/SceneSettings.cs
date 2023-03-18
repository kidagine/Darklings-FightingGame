using UnityEngine.InputSystem;

public class SceneSettings
{
    //Fight
    public static int StageIndex = 0;
    public static string MusicName = "Random";
    public static bool OnlineStageRandom = false;
    public static bool OnlineBit1 = false;
    public static int OnlineStageIndex = 0;
    public static string OnlineMusicName = "Random";
    public static bool IsOnline = false;
    public static int OnlineIndex = -1;
    public static string OnlineOneIp = "127.0.0.1";
    public static string OnlineTwoIp = "127.0.0.1";
    public static int PortOne = 7000;
    public static int PortTwo = 7001;
    public static string PrivateOneIp = "127.0.0.1";
    public static string PrivateTwoIp = "127.0.0.1";
    public static InputDevice ControllerOne;
    public static InputDevice ControllerTwo;
    public static string ControllerOneScheme = "Keyboard";
    public static string ControllerTwoScheme = "Keyboard";
    public static int PlayerOne = 0;
    public static int PlayerTwo = 0;
    public static int ColorOne = 0;
    public static int ColorTwo = 0;
    public static int AssistOne = 0;
    public static int AssistTwo = 0;
    public static string NameOne = "";
    public static string NameTwo = "";
    public static bool Bit1 = false;
    public static bool RandomStage = false;
    public static bool SceneSettingsDecide = false;
    //Local Fight
    public static bool IsTrainingMode = false;
    //Main Menu
    public static bool ChangeCharacter = false;
    //Replay
    public static bool ReplayMode = false;
    public static int ReplayIndex = 0;
}
