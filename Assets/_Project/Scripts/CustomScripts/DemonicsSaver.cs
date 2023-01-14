using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public static class DemonicsSaver
{
    [DllImport("__Internal")]
    private static extern void FunctionImplementedInJavaScriptLibraryFile(string str);
    [DllImport("__Internal")]
    private static extern void SaveData(string strKey, string strData);
    [DllImport("__Internal")]
    private static extern string LoadData(string str);

    //If the current build is Webgl then we save using our custom jsLibrary plugin, otherwise save to player Prefs
    public static void Save(string key, string value, bool addOn = false)
    {
        string keyLower = key.ToLower();
#if UNITY_WEBGL && !UNITY_EDITOR
        if (addOn)
        {        
            SaveData(keyLower, LoadData(keyLower) + value);
        }
        else
        {
            SaveData(keyLower, value);
        }
#endif
#if !UNITY_WEBGL || UNITY_EDITOR
        if (addOn)
        {
            PlayerPrefs.SetString(keyLower, PlayerPrefs.GetString(keyLower) + value);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetString(keyLower, value);
            PlayerPrefs.Save();
        }
#endif
    }

    //If the current build is Webgl then we load using our custom jsLibrary plugin, otherwise load to player Prefs
    public static string Load(string key, string defaultValue = "")
    {
        string keyLower = key.ToLower();
#if UNITY_WEBGL && !UNITY_EDITOR
        string value = LoadData(keyLower);
        if (string.IsNullOrEmpty(value))
        {
            value = defaultValue;
        }
        return value;
#endif
#if !UNITY_WEBGL || UNITY_EDITOR
        return PlayerPrefs.GetString(keyLower, defaultValue);
#endif
    }
}
