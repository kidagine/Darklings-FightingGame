using System;
using UnityEngine;

[Serializable]
public enum ComboTimerStarterEnum { Blue, Yellow, Red };

public class ComboTimerStarterTypes : MonoBehaviour
{
    [SerializeField] private ComboTimerStarterEnum _comboTimerStarterEnum = default;
    private static Color _red = new(1.0f, 0.3254902f, 0.3254902f);
    private static Color _yellow = new(0.9647059f, 0.8941177f, 0.4078431f);
    private static Color _blue = new(1.0f, 1.0f, 1.0f);


    public static int GetComboTimerStarterValue(ComboTimerStarterEnum comboTimerStarter)
    {
        return comboTimerStarter switch
        {
            ComboTimerStarterEnum.Blue => 300,
            ComboTimerStarterEnum.Yellow => 250,
            ComboTimerStarterEnum.Red => 200,
            _ => 0,
        };
    }

    public static Color GetComboTimerStarterColor(ComboTimerStarterEnum comboTimerStarter)
    {
        return comboTimerStarter switch
        {
            ComboTimerStarterEnum.Blue => _blue,
            ComboTimerStarterEnum.Yellow => _yellow,
            ComboTimerStarterEnum.Red => _red,
            _ => Color.white,
        };
    }

    public ComboTimerStarterEnum ComboTimerStarterEnum { get { return _comboTimerStarterEnum; } private set { } }
}
