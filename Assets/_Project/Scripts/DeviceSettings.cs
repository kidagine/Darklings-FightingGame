using System;
using System.Collections.Generic;
using UnityEngine;

namespace Demonics
{
    [Serializable]
    public struct PromptIcon
    {
        public string customInputContextString;
        public Sprite customInputContextIcon;
    }

    [CreateAssetMenu(fileName = "Device Settings", menuName = "Scriptable Objects/Device Settings", order = 1)]
    public class DeviceSettings : ScriptableObject
    {
        public List<PromptIcon> promptIcons = new List<PromptIcon>();
    }
}