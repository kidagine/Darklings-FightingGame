using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpriteShadersUltimate
{
    [CustomEditor(typeof(WindManagerSSU)), CanEditMultipleObjects]
    public class WindManagerSSUEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //Draw Inspector:
            base.OnInspectorGUI();

            //References:
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.richText = true;

            //Info Box:
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical("Helpbox");
            EditorGUILayout.LabelField("<b><size=14>Information</size></b>", style);
            GUI.color = new Color(1, 1, 1, 0.7f);
            EditorGUILayout.LabelField("- This component will handle the global wind settings.", style);
            EditorGUILayout.LabelField("- Only have one active <b>WindManager</b> at a time.", style);
            EditorGUILayout.LabelField("- This updates the global variables used by my <b>wind shader</b>.", style);
            GUI.color = new Color(1, 1, 1, 1f);
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}