using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteShadersUltimate
{
    public class WindManagerSSU : MonoBehaviour
    {
        [Header("Settings:")]

        [Tooltip("Smaller values will result in more frequent changes in wind.")]
        public float windNoiseScale = 0.1f;
        private float lastWindNoiseScale;

        [Tooltip("Speed at which the wind pattern moves horizontally.")]
        public float windNoiseSpeed = 1;

        [Tooltip("The wind intensity will be between this value and 'Wind Intensity To'.")]
        public float windIntensityFrom = -0.4f;
        private float lastWindIntensityFrom;

        [Tooltip("The wind intensity will be between this value and 'Wind Intensity From'.")]
        public float windIntensityTo = 0.4f;
        private float lastWindIntensityTo;

        //Runtime:
        float currentTime;

        void Start()
        {
            currentTime = 0;
        }

        void FixedUpdate()
        {
            ModifyIfChanged(ref windNoiseScale,ref lastWindNoiseScale, "WindNoiseScale");
            ModifyIfChanged(ref windIntensityFrom, ref lastWindIntensityFrom, "WindMinIntensity");
            ModifyIfChanged(ref windIntensityTo, ref lastWindIntensityTo, "WindMaxIntensity");

            currentTime += Time.fixedDeltaTime * windNoiseSpeed;
            Shader.SetGlobalFloat("WindTime", currentTime);
        }

        public void ModifyIfChanged(ref float currentValue, ref float oldValue, string globalShaderName)
        {
            if(oldValue != currentValue)
            {
                oldValue = currentValue;
                Shader.SetGlobalFloat(globalShaderName, currentValue);
            }
        }
    }
}