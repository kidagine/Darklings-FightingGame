#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace SpriteShadersUltimate
{
    [CustomEditor(typeof(ImageSSU))]
    [CanEditMultipleObjects]
    public class ImageSSUEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            SerializedProperty updateChanges = serializedObject.FindProperty("updateChanges");
            EditorGUILayout.PropertyField(updateChanges);
            GUI.enabled = false;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("runtimeMaterial"));
            GUI.enabled = true;

            serializedObject.ApplyModifiedProperties();

            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.richText = true;

            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical("Helpbox");
            GUI.color = new Color(1, 1, 1, 0.7f);
            EditorGUILayout.LabelField("Requires the <b>UI_Graphic</b> shader space.", labelStyle);
            EditorGUILayout.LabelField("Sets the material's <b>Rect Width</b> and <b>Rect Height</b>.", labelStyle);
            EditorGUILayout.LabelField("Will also <b>instantiate</b> the material at runtime.", labelStyle);
            GUI.color = Color.white;
            EditorGUILayout.EndVertical();

            //Check:
            ImageSSU image = (ImageSSU) target;
            if(image.GetComponent<RectTransform>() == null)
            {
                EditorGUILayout.Space();
                GUI.color = Color.red;
                EditorGUILayout.BeginVertical("Helpbox");
                EditorGUILayout.LabelField("Requires a <b>RectTransform</b>.", labelStyle);
                EditorGUILayout.EndVertical();
                GUI.color = Color.white;
            }
            if (image.GetComponent<Image>() == null)
            {
                EditorGUILayout.Space();
                GUI.color = Color.red;
                EditorGUILayout.BeginVertical("Helpbox");
                EditorGUILayout.LabelField("Requires an <b>Image</b>.", labelStyle);
                EditorGUILayout.EndVertical();
                GUI.color = Color.white;
            }
            else if(Application.isPlaying == false)
            {
                Material mat = image.GetComponent<Image>().material;

                if (mat.shader.name.StartsWith("Sprite Shaders Ultimate") == false)
                {
                    EditorGUILayout.Space();
                    GUI.color = Color.red;
                    EditorGUILayout.BeginVertical("Helpbox");
                    EditorGUILayout.LabelField("Requires a <b>Sprite Shaders Ultimate</b> shader.", labelStyle);
                    EditorGUILayout.EndVertical();
                    GUI.color = Color.white;
                }
                else if (Mathf.RoundToInt(mat.GetFloat("_ShaderSpace")) != 5)
                {
                    EditorGUILayout.Space();
                    GUI.color = Color.red;
                    EditorGUILayout.BeginVertical("Helpbox");
                    EditorGUILayout.LabelField("Requires <b>UI_Graphic</b> shader space.", labelStyle);
                    EditorGUILayout.EndVertical();
                    GUI.color = Color.white;
                }
                else
                {
                    EditorGUILayout.Space();
                    GUI.color = Color.green;
                    EditorGUILayout.BeginVertical("Helpbox");
                    if (updateChanges.hasMultipleDifferentValues)
                    {
                        EditorGUILayout.LabelField("<b>Rect Width</b> and <b>Rect Height</b> will be updated on <b>Awake()</b> or <b>Update()</b>.", labelStyle);
                    }
                    else if (updateChanges.boolValue)
                    {
                        EditorGUILayout.LabelField("<b>Rect Width</b> and <b>Rect Height</b> will be updated on <b>Awake()</b> and <b>Update()</b>.", labelStyle);
                        EditorGUILayout.LabelField("Material is only updated if the RectTransform's <b>Width</b> or <b>Height</b> is <b>changed</b>.", labelStyle);
                    }
                    else
                    {
                        EditorGUILayout.LabelField("<b>Rect Width</b> and <b>Rect Height</b> will be updated on <b>Awake()</b>.", labelStyle);
                    }
                    EditorGUILayout.EndVertical();
                    GUI.color = Color.white;
                }
            }
        }
    }
}

#endif