#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace SpriteShadersUltimate
{
    [CustomEditor(typeof(SpriteSheetSSU))]
    [CanEditMultipleObjects]
    public class SpriteSheetSSUEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            SerializedProperty updateChanges = serializedObject.FindProperty("updateChanges");
            EditorGUILayout.PropertyField(updateChanges);
            serializedObject.ApplyModifiedProperties();

            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.richText = true;

            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical("Helpbox");
            GUI.color = new Color(1, 1, 1, 0.7f);
            EditorGUILayout.LabelField("Only supports <b>images</b> and <b>sprite renderers</b>.", labelStyle);
            EditorGUILayout.LabelField("Requires the <b>Sprite Sheet Fix</b> option enabled.", labelStyle);
            EditorGUILayout.LabelField("Sets the material's <b>Sprite Sheet Rect</b> to fix shader issues.", labelStyle);
            EditorGUILayout.LabelField("Will also <b>instantiate</b> materials at runtime.", labelStyle);
            GUI.color = Color.white;
            EditorGUILayout.EndVertical();

            //Check:
            SpriteSheetSSU targetComponent = (SpriteSheetSSU) target;
            if(targetComponent.GetComponent<Image>() == null && targetComponent.GetComponent<SpriteRenderer>() == null)
            {
                EditorGUILayout.Space();
                GUI.color = Color.red;
                EditorGUILayout.BeginVertical("Helpbox");
                EditorGUILayout.LabelField("Requires a <b>Sprite Renderer</b> or <b>Image</b> component.", labelStyle);
                EditorGUILayout.EndVertical();
                GUI.color = Color.white;
            }
        }
    }
}

#endif