#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpriteShadersUltimate
{
    [CustomEditor(typeof(UnscaledTimeSSU))]
    [CanEditMultipleObjects]
    public class UnscaledTimeSSUEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //Properties:
            EditorGUILayout.PropertyField(serializedObject.FindProperty("dontDestroyOnLoad"));
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();

            //Additional Information:
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.richText = true;

            EditorGUILayout.BeginVertical("Helpbox");
            GUI.color = new Color(1, 1, 1, 0.7f);
            EditorGUILayout.LabelField("Allows you to use <b>unscaled time</b> in SSU shaders.", labelStyle);
            EditorGUILayout.LabelField("Attach this to <b>any</b> gameobject.", labelStyle);
            EditorGUILayout.LabelField("You only need <b>one</b> of this component in your scene.", labelStyle);
            GUI.color = Color.white;
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
            GUI.color = Color.green;
            EditorGUILayout.BeginVertical("Helpbox");
            EditorGUILayout.LabelField("<b>Unscaled Time</b> can be used.", labelStyle);
            if(serializedObject.FindProperty("dontDestroyOnLoad").boolValue)
            {
                EditorGUILayout.LabelField("This gameobject will <b>not</b> be <b>destroyed</b> when the scene <b>changes</b>.", labelStyle);
            }
            EditorGUILayout.EndVertical();
            GUI.color = Color.white;
        }
    }
}

#endif