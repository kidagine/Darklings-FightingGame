using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demonics
{
    [CreateAssetMenu(fileName = "Device Configurator", menuName = "Scriptable Objects/Device Configurator", order = 1)]
    public class DeviceConfigurator : ScriptableObject
    {
        [System.Serializable]
        public struct DeviceSet
        {
            public string deviceRawPath;
            public DeviceSettings deviceDisplaySettings;
        }

        [System.Serializable]
        public struct DisconnectedSettings
        {
            public string disconnectedDisplayName;
            public Color disconnectedDisplayColor;
        }

        public List<DeviceSet> listDeviceSets = new List<DeviceSet>();

        public DisconnectedSettings disconnectedDeviceSettings;


        public Sprite GetDeviceBindingIcon(PlayerInput playerInput, string playerInputDeviceInputBinding)
        {
            string currentDeviceRawPath = playerInput.devices[0].ToString();
            Sprite displaySpriteIcon = null;
            for (int i = 0; i < listDeviceSets.Count; i++)
            {
                if (listDeviceSets[i].deviceRawPath.Equals(currentDeviceRawPath))
                {
                    displaySpriteIcon = FilterForDeviceInputBinding(listDeviceSets[i], playerInputDeviceInputBinding);
                }
            }
            return displaySpriteIcon;
        }

        private Sprite FilterForDeviceInputBinding(DeviceSet targetDeviceSet, string inputBinding)
        {
            Sprite spriteIcon = null;
            for (int i = 0; i < targetDeviceSet.deviceDisplaySettings.promptIcons.Count; i++)
            {
                if (targetDeviceSet.deviceDisplaySettings.promptIcons[i].customInputContextString == inputBinding)
                {
                    if (targetDeviceSet.deviceDisplaySettings.promptIcons[i].customInputContextIcon != null)
                    {
                        spriteIcon = targetDeviceSet.deviceDisplaySettings.promptIcons[i].customInputContextIcon;
                    }
                }
            }
            return spriteIcon;
        }
    }
}