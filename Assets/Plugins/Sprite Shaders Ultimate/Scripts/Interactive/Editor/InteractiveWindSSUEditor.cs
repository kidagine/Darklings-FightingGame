using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpriteShadersUltimate
{
    [CustomEditor(typeof(InteractiveWindSSU)), CanEditMultipleObjects]
    public class InteractiveWindSSUEditor : Editor
    {
        bool displaySetup;
        bool displayTroubleshooting;
        bool displayInformation;

        public override void OnInspectorGUI()
        {
            //References:
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.richText = true;
            InteractiveWindSSU wind = (InteractiveWindSSU)target;

            //Header:
            EditorGUILayout.BeginVertical("Helpbox");
            EditorGUILayout.LabelField("<size=14><b>Interactive Wind</b></size>", style);
            EditorGUILayout.LabelField(" ", GUILayout.Height(1));
            DisplayHints(ref displaySetup, "Setup",
                "- Attach this to a <b>SpriteRenderer</b> with the <b>Wind</b> shader.",
                "- Set <b>Mesh Type</b> to <b>Full Rect</b> in the sprite's settings.",
                "- Attach a <b>BoxCollider2D</b> to this gameobject.",
                "- Set <b>Is Trigger</b> to true in the <b>BoxCollider2D</b>.",
                "- Have a single active <b>WindManager</b> component in your scene.",
                " ",
                "- You can <b>flip</b> the shader for hanging objects.",
                "- You can combine the shader with <b>UV Distort</b>."
                );
            EditorGUILayout.LabelField(" ", GUILayout.Height(1));
            DisplayHints(ref displayTroubleshooting, "Troubleshooting",
                " ",
                "<b>Pixels are clipping out:</b>",
                "- Make sure the sprite's <b>Mesh Type</b> is <b>Full Rect</b>.",
                "- <b>Sprite Sheets</b> need the <b>Sprite Sheet Fix</b> option.",
                "- Expand the sprite's texture horizontally with empty pixels.",
                " ",
                "<b>Wind is not visible:</b>",
                "- Make sure the SpriteRenderer is using the <b>Wind</b> shader.",
                "- If using a <b>Uber Shader</b> you need to enable <b>Wind</b>.",
                "- Make sure you have a single active <b>WindManager</b> component.",
                "- Check your <b>WindManager's</b> and <b>material's</b> settings.",
                " ",
                "<b>Physical interaction is not happening:</b>",
                "- Check this <b>component's</b> and the <b>material's</b> settings.",
                "- Make sure the <b>BoxCollider2D</b> is positioned properly.",
                "- Set the <b>BoxCollider2D</b> to a <b>trigger</b>.",
                "- Make sure a collision with the <b>BoxCollider2D</b> is happening.",
                " "
                );
            EditorGUILayout.LabelField(" ", GUILayout.Height(1));
            DisplayHints(ref displayInformation, "Information",
                " ",
                "<b>Summary:</b>",
                "- Interaction will <b>bend</b> and <b>squish</b> the sprite.",
                "- Can be used for <b>Grass, Trees, Chains, Vines</b> and more.",
                " ",
                "<b>Temporary Interaction:</b>",
                "- Disable <b>Stay Bent</b> to have a temporary interaction.",
                "- Only objects moving faster than <b>" + Mathf.RoundToInt(wind.minBendSpeed) + " unit/s</b> will interact.",
                " "
                );
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();

            //Rotation:
            EditorGUILayout.BeginVertical("Helpbox");
            EditorGUILayout.LabelField("<b>Rotation:</b>", style);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationFactor"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bendInSpeed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bendOutSpeed"));
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();

            //Temporary:
            EditorGUILayout.BeginVertical("Helpbox");
            EditorGUILayout.LabelField("<b>Method:</b>", style);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("stayBent"));
            if(!wind.stayBent)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("minBendSpeed"));
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();

            //Hyper Performance:
            EditorGUILayout.BeginVertical("Helpbox");
            EditorGUILayout.LabelField("<b>Hyper Performance:</b>", style);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hyperPerformanceMode"));
            if (wind.hyperPerformanceMode)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("randomOffsetZ"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("customMaterial"));
                if (wind.customMaterial)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("inactiveMaterial"));
                }
                else
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("inactiveShader"));
                }

                //Hint:
                GUI.color = new Color(1, 1, 1, 0.7f);
                EditorGUILayout.LabelField("- GPU performance boost for <b>lowest-end</b> mobile phones.", style);
                if(!wind.customMaterial)
                {
                    EditorGUILayout.LabelField("- Sets the shader to <b>" + wind.inactiveShader + "</b> while inactive.", style);
                }
                else
                {
                    if(wind.inactiveMaterial != null)
                    {
                        EditorGUILayout.LabelField("- Sets the material to <b>" + wind.inactiveMaterial.name + "</b> while inactive.", style);
                    }
                    else
                    {
                        GUIStyle warningStyle = new GUIStyle(GUI.skin.label);
                        warningStyle.richText = true;

                        if (EditorGUIUtility.isProSkin)
                        {
                            warningStyle.normal.textColor = new Color(1, 0.7f, 0.7f, 1);
                        }
                        else
                        {
                            warningStyle.normal.textColor = new Color(0.3f, 0f, 0f, 1);
                        }

                        EditorGUILayout.LabelField("- Please reference a material in <b>Inactive Material</b>.", warningStyle);
                    }
                }
                EditorGUILayout.LabelField("- Use this if you want <b>physical interaction</b> but don't need <b>Wind</b>.", style);
                EditorGUILayout.LabelField("- You must set <b>Rotation Wind Factor</b> to <b>0</b> in the material.", style);
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("- Enabling <b>Random Offset Z</b> will prevent resorting of render order.", style);
                GUI.color = Color.white;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();

            //Other:
            EditorGUILayout.BeginVertical("Helpbox");
            EditorGUILayout.LabelField("<b>Other:</b>", style);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("randomizeWiggle"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("allowCustomLayer"));
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();

            //Fix Layer:
            if(wind.gameObject.layer != 2 && !wind.allowCustomLayer)
            {
                wind.gameObject.layer = 2;
            }

            //Fix BoxCollider2D:
            BoxCollider2D boxCollider = wind.gameObject.GetComponent<BoxCollider2D>();
            if(boxCollider == null)
            {
                //Create new BoxCollider2D:
                boxCollider = wind.gameObject.AddComponent<BoxCollider2D>();
                boxCollider.isTrigger = true;
            }

            //Fix Variables:
            if(wind.bendInSpeed < 0)
            {
                wind.bendInSpeed = 0;
            }
            if (wind.bendOutSpeed < 0)
            {
                wind.bendOutSpeed = 0;
            }
            if (wind.minBendSpeed < 0)
            {
                wind.minBendSpeed = 0;
            }
        }

        void DisplayHints(ref bool toggleVariable, string title,params string[] lines)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.richText = true;

            GUIStyle button = new GUIStyle(GUI.skin.button);
            button.richText = true;

            if (toggleVariable)
            {
                GUI.color = new Color(1, 1, 1, 0.7f);
            }
            else
            {
                GUI.color = new Color(1, 1, 1, 0.5f);
            }

            title = "<b>" + title + "</b>";

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.BeginHorizontal();
            GUI.color = Color.white;
            EditorGUILayout.LabelField(title, style);
            if (GUILayout.Button("<size=10>" + (toggleVariable ? "▼" : "▲") + "</size>", button, GUILayout.Width(20)))
            {
                toggleVariable = !toggleVariable;
            }
            EditorGUILayout.EndHorizontal();

            if (toggleVariable == true)
            {
                GUI.color = new Color(1, 1, 1, 0.7f);
                for(int l = 0; l < lines.Length; l++)
                {
                    if(lines[l] == " ")
                    {
                        EditorGUILayout.LabelField(lines[l], style,GUILayout.Height(6));
                    }
                    else
                    {
                        EditorGUILayout.LabelField(lines[l], style);
                    }
                }
            }

            GUI.color = Color.white;
            EditorGUILayout.EndVertical();
        }
    }
}