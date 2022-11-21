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

    public static void Save(string key, string value)
    {
        string keyLower = key.ToLower();
#if UNITY_WEBGL && !UNITY_EDITOR
        SaveData(keyLower, value);
#endif
#if !UNITY_WEBGL || UNITY_EDITOR
        PlayerPrefs.SetString(keyLower, value);
        PlayerPrefs.Save();
#endif
    }

    public static string Load(string key, string defaultValue)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        string value = LoadData(key);
        if (string.IsNullOrEmpty(value))
        {
            value = defaultValue;
        }
        return value;
#endif
#if !UNITY_WEBGL || UNITY_EDITOR
        return PlayerPrefs.GetString(key);
#endif
    }

    public static int LoadInt(string key, int defaultValue)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        string value = LoadData(key);
        if (string.IsNullOrEmpty(value))
        {
            return defaultValue;
        }
        return int.Parse(value);
#endif
#if !UNITY_WEBGL || UNITY_EDITOR
        return PlayerPrefs.GetInt(key);
#endif
    }
}
