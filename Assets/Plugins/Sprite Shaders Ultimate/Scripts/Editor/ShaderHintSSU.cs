#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteShadersUltimate
{
    [CreateAssetMenu(fileName = "ShaderName", menuName = "Shader/SSU Shader Hint (ignore this)")]
    public class ShaderHintSSU : ScriptableObject
    {
        [Header("Main:")]
        [TextArea(6, 10)]
        public string shaderDescription;
        public List<HintText> hints = new List<HintText>();
        public List<string> lines = new List<string>();
        public string spaceHint = "";

        [Header("Extra Help:")]
        public bool requiresFullRectMesh = false;
        public bool requiresSpriteSheetFix = false;
        public bool requiresInstancing = false;
        public bool requiresTiling = false;

        [Header("Performance:")]
        public float benchmarkValue = 0f;
        public int textureSamples = 0;
        public string textureToggle = "";
        public string textureToggleExtra = "";
    }

    [System.Serializable]
    public class HintText
    {
        public string property = "";
        public string text = "";
    }
}

#endif