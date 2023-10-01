#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace SpriteShadersUltimate
{
    [CustomEditor(typeof(MaterialInstancerSSU))]
    [CanEditMultipleObjects]
    public class MaterialInstancerSSUEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.richText = true;

            GUI.enabled = false;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("runtimeMaterial"));
            GUI.enabled = true;

            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical("Helpbox");
            GUI.color = new Color(1, 1, 1, 0.7f);
            EditorGUILayout.LabelField("Will <b>instantiate</b> materials at runtime.", labelStyle);
            EditorGUILayout.LabelField("Fixes shaders requiring a <b>unique material instance</b>.", labelStyle);
            GUI.color = Color.white;
            EditorGUILayout.EndVertical();

            //Check:
            MaterialInstancerSSU materialInstancer = (MaterialInstancerSSU)target;
            if(materialInstancer.GetComponent<Renderer>() == null && materialInstancer.GetComponent<Graphic>() == null)
            {

                EditorGUILayout.Space();
                GUI.color = Color.red;
                EditorGUILayout.BeginVertical("Helpbox");
                EditorGUILayout.LabelField("Requires a <b>UI Graphic</b> or <b>Renderer</b> with a material.", labelStyle);
                EditorGUILayout.EndVertical();
                GUI.color = Color.white;
            }
            else
            {
                EditorGUILayout.Space();
                GUI.color = Color.green;
                EditorGUILayout.BeginVertical("Helpbox");
                EditorGUILayout.LabelField("<b>Material</b> will be instanced on <b>Awake()</b>.", labelStyle);
                EditorGUILayout.EndVertical();
                GUI.color = Color.white;
            }
        }

    }
}

#endif