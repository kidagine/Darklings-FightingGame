#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Rendering;

namespace SpriteShadersUltimate
{
    [CustomEditor(typeof(ShaderFaderSSU))]
    [CanEditMultipleObjects]
    public class ShaderFaderSSUEditor : Editor
    {
        List<string> shaderProperties;
        float previewValue;

        public override void OnInspectorGUI()
        {
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.richText = true;

            EditorGUI.BeginChangeCheck();
            serializedObject.UpdateIfRequiredOrScript();

            EditorGUILayout.BeginVertical();

            SerializedProperty property = serializedObject.GetIterator();
            bool expanded = true;
            bool displayObjectLists = true;
            bool automaticFading = true;
            while (property.NextVisible(expanded))
            {
                using (new EditorGUI.DisabledScope("m_Script" == property.propertyPath))
                {
                    bool draw = true;

                    //Hide fade value.
                    if (property.name == "fadeValue")
                    {
                        //GUI.enabled = false;
                    }

                    //Hide object lists.
                    if (property.name == "getChildObjects")
                    {
                        displayObjectLists = property.boolValue;
                    }
                    else if ((property.name == "renderers" || property.name == "graphics") && displayObjectLists)
                    {
                        draw = false;
                    }

                    //Hide fading variables.
                    if (property.name == "automaticFading")
                    {
                        automaticFading = property.boolValue;
                    }
                    else if ((property.name == "isFaded" || property.name == "duration" || property.name == "unscaledTime") && !automaticFading)
                    {
                        draw = false;
                    }
                    else if ((property.name == "fadeValue") && automaticFading)
                    {
                        draw = false;
                    }

                    //Create box groups.
                    if (property.name == "getChildObjects" || property.name == "automaticFading" || property.name == "floatProperties")
                    {
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.Space();
                        EditorGUILayout.BeginVertical("Helpbox");
                    }

                    //Fix box arrow overlap.
                    if (property.hasVisibleChildren)
                    {
                        EditorGUI.indentLevel += 1;
                    }

                    //DRAW PROPERTY:
                    if (draw)
                    {
                        if (property.name == "automaticFading")
                        {
                            int selected = property.boolValue ? 0 : 1;
                            selected = GUILayout.Toolbar(selected, new string[] { "Automatic Fading", "Manual Fading" });
                            property.boolValue = selected == 0 ? true : false;
                        }
                        else if (property.name == "getChildObjects")
                        {
                            int selected = property.boolValue ? 0 : 1;
                            selected = GUILayout.Toolbar(selected, new string[] { "Get From Children", "Manual Objects" });
                            property.boolValue = selected == 0 ? true : false;
                        }
                        else
                        {
                            EditorGUILayout.PropertyField(property, true);
                        }
                    }

                    //Fix box arrow overlap.
                    if (property.hasVisibleChildren)
                    {
                        EditorGUI.indentLevel -= 1;
                    }

                    //Reset stuff.
                    GUI.enabled = true;
                    expanded = false;
                }
            }

            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
            EditorGUI.EndChangeCheck();

            //Utility:
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical("Helpbox");
            EditorGUILayout.BeginHorizontal();

            #region Utility Buttons
            if (GUILayout.Button("Copy From"))
            {
                ShaderFaderSSU sf = (ShaderFaderSSU)target;
                foreach (Material mat in GetMaterials())
                {
                    Shader shader = mat.shader;

                    for(int i = 0; i < shader.GetPropertyCount(); i++)
                    {
                        ShaderPropertyType propertyType = shader.GetPropertyType(i);
                        string propertyName = shader.GetPropertyName(i);

                        if (SSUShaderGUI.IsKeyword(propertyName) || propertyName.StartsWith("_Enable"))
                        {
                            continue;
                        }

                        if (propertyType == ShaderPropertyType.Float || propertyType == ShaderPropertyType.Range)
                        {
                            //Float:
                            float propertyValue = mat.GetFloat(propertyName);

                            bool foundProperty = false;
                            foreach (FloatFaderSSU propertyFader in sf.floatProperties)
                            {
                                if (propertyFader.propertyName == shader.GetPropertyName(i))
                                {
                                    propertyFader.fromValue = propertyValue;
                                    foundProperty = true;
                                    break;
                                }
                            }

                            if (!foundProperty)
                            {
                                sf.floatProperties.Add(new FloatFaderSSU(propertyName, propertyValue, propertyValue));
                            }
                        }
                        else if (propertyType == ShaderPropertyType.Vector)
                        {
                            //Vector:
                            Vector4 propertyValue = mat.GetVector(propertyName);

                            bool foundProperty = false;
                            foreach (VectorFaderSSU propertyFader in sf.vectorProperties)
                            {
                                if (propertyFader.propertyName == shader.GetPropertyName(i))
                                {
                                    propertyFader.fromValue = propertyValue;
                                    foundProperty = true;
                                    break;
                                }
                            }

                            if (!foundProperty)
                            {
                                sf.vectorProperties.Add(new VectorFaderSSU(propertyName, propertyValue, propertyValue));
                            }
                        }
                        else if (propertyType == ShaderPropertyType.Color)
                        {
                            //Color:
                            Color propertyValue = mat.GetColor(propertyName);

                            bool foundProperty = false;
                            foreach (ColorFaderSSU propertyFader in sf.colorProperties)
                            {
                                if (propertyFader.propertyName == shader.GetPropertyName(i))
                                {
                                    propertyFader.fromValue = propertyValue;
                                    foundProperty = true;
                                    break;
                                }
                            }

                            if (!foundProperty)
                            {
                                sf.colorProperties.Add(new ColorFaderSSU(propertyName, propertyValue, propertyValue));
                            }
                        }
                    }
                }
            }
            if (GUILayout.Button("Copy To"))
            {
                ShaderFaderSSU sf = (ShaderFaderSSU)target;
                foreach (Material mat in GetMaterials())
                {
                    Shader shader = mat.shader;

                    for (int i = 0; i < shader.GetPropertyCount(); i++)
                    {
                        ShaderPropertyType propertyType = shader.GetPropertyType(i);
                        string propertyName = shader.GetPropertyName(i);

                        if (SSUShaderGUI.IsKeyword(propertyName) || propertyName.StartsWith("_Enable"))
                        {
                            continue;
                        }

                        if (propertyType == ShaderPropertyType.Float || propertyType == ShaderPropertyType.Range)
                        {
                            //Float:
                            float propertyValue = mat.GetFloat(propertyName);

                            bool foundProperty = false;
                            foreach (FloatFaderSSU propertyFader in sf.floatProperties)
                            {
                                if (propertyFader.propertyName == shader.GetPropertyName(i))
                                {
                                    propertyFader.toValue = propertyValue;
                                    foundProperty = true;
                                    break;
                                }
                            }

                            if (!foundProperty)
                            {
                                sf.floatProperties.Add(new FloatFaderSSU(propertyName, propertyValue, propertyValue));
                            }
                        }
                        else if (propertyType == ShaderPropertyType.Vector)
                        {
                            //Vector:
                            Vector4 propertyValue = mat.GetVector(propertyName);

                            bool foundProperty = false;
                            foreach (VectorFaderSSU propertyFader in sf.vectorProperties)
                            {
                                if (propertyFader.propertyName == shader.GetPropertyName(i))
                                {
                                    propertyFader.toValue = propertyValue;
                                    foundProperty = true;
                                    break;
                                }
                            }

                            if (!foundProperty)
                            {
                                sf.vectorProperties.Add(new VectorFaderSSU(propertyName, propertyValue, propertyValue));
                            }
                        }
                        else if (propertyType == ShaderPropertyType.Color)
                        {
                            //Color:
                            Color propertyValue = mat.GetColor(propertyName);

                            bool foundProperty = false;
                            foreach (ColorFaderSSU propertyFader in sf.colorProperties)
                            {
                                if (propertyFader.propertyName == shader.GetPropertyName(i))
                                {
                                    propertyFader.toValue = propertyValue;
                                    foundProperty = true;
                                    break;
                                }
                            }

                            if (!foundProperty)
                            {
                                sf.colorProperties.Add(new ColorFaderSSU(propertyName, propertyValue, propertyValue));
                            }
                        }
                    }
                }
            }
            if (GUILayout.Button("Cleanup"))
            {
                ShaderFaderSSU sf = (ShaderFaderSSU)target;

                //Float:
                List<FloatFaderSSU> removedFloatProperties = new List<FloatFaderSSU>();
                foreach (FloatFaderSSU propertyFader in sf.floatProperties)
                {
                    if(propertyFader.fromValue == propertyFader.toValue)
                    {
                        removedFloatProperties.Add(propertyFader);
                    }
                }
                foreach (FloatFaderSSU propertyFader in removedFloatProperties)
                {
                    sf.floatProperties.Remove(propertyFader);
                }

                //Vector:
                List<VectorFaderSSU> removedVectorProperties = new List<VectorFaderSSU>();
                foreach (VectorFaderSSU propertyFader in sf.vectorProperties)
                {
                    if (propertyFader.fromValue == propertyFader.toValue)
                    {
                        removedVectorProperties.Add(propertyFader);
                    }
                }
                foreach (VectorFaderSSU propertyFader in removedVectorProperties)
                {
                    sf.vectorProperties.Remove(propertyFader);
                }

                //Color:
                List<ColorFaderSSU> removedColorProperties = new List<ColorFaderSSU>();
                foreach (ColorFaderSSU propertyFader in sf.colorProperties)
                {
                    if (propertyFader.fromValue == propertyFader.toValue)
                    {
                        removedColorProperties.Add(propertyFader);
                    }
                }
                foreach (ColorFaderSSU propertyFader in removedColorProperties)
                {
                    sf.colorProperties.Remove(propertyFader);
                }
            }
            #endregion

            EditorGUILayout.EndHorizontal();

            #region Preview
            float lastPreview = previewValue;
            previewValue = EditorGUILayout.Slider(new GUIContent("Preview", "This will modify the materials."), previewValue, 0, 1);
            if (previewValue != lastPreview)
            {
                ShaderFaderSSU sf = (ShaderFaderSSU)target;
                float fadeFactor = sf.fadeCurve.Evaluate(previewValue);
                foreach (Material mat in GetMaterials())
                {
                    sf.UpdateSingleMaterial(mat, fadeFactor);
                }
            }
            #endregion

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical("Helpbox");

            GUI.color = new Color(1, 1, 1, 0.7f);
            EditorGUILayout.LabelField("Fades material <b>properties</b> between two values.", labelStyle);
            EditorGUILayout.LabelField("", labelStyle);
            EditorGUILayout.LabelField("<b>Objects:</b>", labelStyle);
            EditorGUILayout.LabelField("First <b>assign</b> the objects, whose <b>materials</b> should be faded.", labelStyle);
            EditorGUILayout.LabelField("These can either be <b>children</b> of this gameobject or <b>manually</b> assigned.", labelStyle);
            EditorGUILayout.LabelField("", labelStyle);
            EditorGUILayout.LabelField("<b>Properties:</b>", labelStyle);
            EditorGUILayout.LabelField("Next you need to add the material <b>properties</b>, which you want to fade.", labelStyle);
            EditorGUILayout.LabelField("These can be <b>added</b> manually or <b>setup</b> using the <b>utility buttons</b>.", labelStyle);
            EditorGUILayout.LabelField("Only <b>floats</b>, <b>colors</b> and <b>vectors</b> can be faded. Do no try to fade <b>toggles</b>.", labelStyle);
            EditorGUILayout.LabelField("", labelStyle);
            EditorGUILayout.LabelField("<b>Quick Setup:</b>", labelStyle);
            EditorGUILayout.LabelField("First <b>modify</b> the materials to their <b>faded out</b> state and press <b>[Copy From]</b>.", labelStyle);
            EditorGUILayout.LabelField("Then <b>modify</b> the materials to their <b>faded in</b> state and press <b>[Copy To]</b>.", labelStyle);
            EditorGUILayout.LabelField("Finally press <b>[Cleanup]</b> to <b>remove</b> all <b>unmodified</b> properties.", labelStyle);
            EditorGUILayout.LabelField("", labelStyle);
            EditorGUILayout.LabelField("<b>Scripting:</b>", labelStyle);
            EditorGUILayout.LabelField("For <b>automatic fading</b> simply toggle the <b>isFaded</b> boolean at runtime.", labelStyle);
            EditorGUILayout.LabelField("For <b>manual fading</b> modify the <b>fadeValue</b> float at runtime.", labelStyle);
            EditorGUILayout.LabelField("Materials are only <b>updated</b>, when the <b>fadeValue</b> changes.", labelStyle);
            GUI.color = Color.white;
            EditorGUILayout.EndVertical();
        }

        HashSet<Material> GetMaterials()
        {
            HashSet<Material> materials = new HashSet<Material>();
            ShaderFaderSSU sf = (ShaderFaderSSU)target;

            if (sf.getChildObjects)
            {
                //Auto Renderers:
                foreach (Renderer renderer in sf.gameObject.GetComponentsInChildren<Renderer>(true))
                {
                    if(!materials.Contains(renderer.sharedMaterial))
                    {
                        materials.Add(renderer.sharedMaterial);
                    }
                }

                //Auto Graphics:
                foreach (Graphic graphic in sf.gameObject.GetComponentsInChildren<Graphic>(true))
                {
                    if (!materials.Contains(graphic.material))
                    {
                        materials.Add(graphic.material);
                    }
                }
            }
            else
            {
                //Manual Renderers:
                if (sf.renderers != null)
                {
                    foreach (Renderer renderer in sf.renderers)
                    {
                        if (renderer != null)
                        {
                            if (!materials.Contains(renderer.sharedMaterial))
                            {
                                materials.Add(renderer.sharedMaterial);
                            }
                        }
                    }
                }

                //Manual Graphics:
                if (sf.graphics != null)
                {
                    foreach (Graphic graphic in sf.graphics)
                    {
                        if (graphic != null)
                        {
                            if (!materials.Contains(graphic.material))
                            {
                                materials.Add(graphic.material);
                            }
                        }
                    }
                }
            }

            return materials;
        }
    }
}

#endif