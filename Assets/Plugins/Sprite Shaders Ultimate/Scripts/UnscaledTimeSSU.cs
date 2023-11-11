using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteShadersUltimate
{
    public class UnscaledTimeSSU : MonoBehaviour
    {
        public bool dontDestroyOnLoad;

        void Awake()
        {
            if(dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        void Update()
        {
            Shader.SetGlobalFloat("UnscaledTime", Time.unscaledTime);
        }
    }
}
