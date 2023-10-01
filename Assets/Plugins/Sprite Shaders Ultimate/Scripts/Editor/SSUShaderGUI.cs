#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SpriteShadersUltimate
{
    public class SSUShaderGUI : ShaderGUI
    {
        //Toggles:
        static bool showMasking = false;
        static bool showNoise = false;
        static bool showTime = false;

        //GUI Resources:
        GUIStyle labelStyle;
        GUIStyle labelCenterStyle;
        GUIStyle buttonStyle;
        static Color warnColor = new Color(1, 0.7f, 0.65f, 0.7f);
        static Color hintColor = new Color(1, 1, 1, 0.7f);
        Shader shader;
        Material defaultMaterial;
        GameObject gameObject;
        Dictionary<string, ShaderHintSSU> allHints;
        ShaderHintSSU currentHint;

        //Internal:
        HashSet<string> enabledShaders;
        HashSet<string> toggleProperties;
        HashSet<string> hidingProperties;
        HashSet<string> enableProperties;
        HashSet<string> alwaysHiddenProperties;
        HashSet<string> linesAboveProperties;
        List<string> shaderList;
        Dictionary<string, int> shaderDictionary;
        Shader lastShader;
        bool newCategory; //Used to avoid drawing lines at the beginning of a new category.
        bool isDisabled; //Skip shaders.
        bool isHidden; //Used to skip hidden properties.
        bool skipGUI; //Skip GUI refresh after changing the shader.
        float shaderSpace; //Used to skip hidden properties.
        string spaceName; //Used to skip hidden properties.
        float shaderFading; //Used to skip hidden properties.
        int currentBoxColor; //Shader Rainbow Color
        int shaderAmount; //Counter
        int enabledAmount; //Counter
        int textureAmount; //Samples
        float performanceBenchmark; 
        float lastTitleHeight; //For Category Lines
        Sprite lastSSF; //Sprite for last SSF.
        static int bakeStackAmount = 1;

        //Status:
        static Material[] materials;
        bool wasWarned;
        bool requiresFullRectMesh, requiresSpriteSheetFix, requiresInstancing, requiresTiling;

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            Initialize(materialEditor, properties);
            DrawStatus(materialEditor);

            if(skipGUI)
            {
                skipGUI = false;
                return;
            }

            //Properties:
            enabledShaders = new HashSet<string>();
            isDisabled = false;
            isHidden = false;
            EditorGUILayout.BeginVertical();
            for (int n = 0; n < properties.Length; n++)
            {
                newCategory = false;
                MaterialProperty prop = properties[n];

                //Always Skipped:
                if(alwaysHiddenProperties.Contains(prop.name))
                {
                    continue;
                }

                //Space Skipped:
                if (prop.name == "_PixelsPerUnit" && !(shaderSpace < 0.5f || (shaderSpace > 4.5f && shaderSpace < 5.5f))) continue;
                if (prop.name == "_ScreenWidthUnits" && !(shaderSpace > 5.5f)) continue;
                if ((prop.name == "_RectHeight" || prop.name == "_RectWidth") && !(shaderSpace > 4.5f && shaderSpace < 5.5f)) continue;

                //Fading Skipped:
                if (prop.name == "_FadingFade" && shaderFading < 0.5f) continue;
                if (prop.name == "_FadingMask" && Mathf.RoundToInt(shaderFading) != 2) continue;
                if ((prop.name == "_FadingPosition" || prop.name == "_FadingNoiseFactor") && Mathf.RoundToInt(shaderFading) != 4) continue;
                if ((prop.name == "_FadingWidth" || prop.name == "_FadingNoiseScale") && Mathf.RoundToInt(shaderFading) != 4 && Mathf.RoundToInt(shaderFading) != 3) continue;

                //Basic:
                if (prop.name == "_MetallicMap" && materials[0].GetFloat("_MetallicMapToggle") < 0.5f) continue;

                bool isKeyword = toggleProperties.Contains(prop.name);

                //Enable:
                if (enableProperties.Contains(prop.name))
                {
                    isDisabled = DrawShaderToggle(materialEditor, prop);
                    isHidden = false;

                    continue; //Dont display shader toggle twice.
                }
                else
                {
                    if (isDisabled)
                    {
                        continue; //Skip disabled properties.
                    }
                }

                string displayName = prop.displayName;

                //Categories:
                if (prop.name == "_MainTex")
                {
                    DrawShaderCategory("Main");
                    displayName = "Sprite Texture";
                }
                else if (prop.name == "_ShaderSpace")
                {
                    DrawShaderCategory("Space");
                    shaderSpace = prop.floatValue;

                    switch (Mathf.RoundToInt(shaderSpace))
                    {
                        case (0):
                            spaceName = "UV";
                            break;
                        case (1):
                            spaceName = "UV Raw";
                            break;
                        case (2):
                            spaceName = "Object";
                            requiresInstancing = true;
                            break;
                        case (3):
                            spaceName = "Scaled Object";
                            requiresInstancing = true;
                            break;
                        case (4):
                            spaceName = "World";
                            break;
                        case (5):
                            spaceName = "UI Rect";
                            break;
                        case (6):
                            spaceName = "Screen";
                            break;
                    }

                }
                else if (prop.name == "_ToggleUnscaledTime")
                {
                    //Space Hints:
                    switch (Mathf.RoundToInt(shaderSpace))
                    {
                        case (0):
                            DisplayHint("- Uses the <b>UV</b> position with size adjustment.");
                            break;
                        case (1):
                            DisplayHint("- Uses the raw <b>UV</b> position.");
                            break;
                        case (2):
                            DisplayHint("- Uses the <b>local position</b> of the object.");
                            break;
                        case (3):
                            DisplayHint("- Uses the <b>local position</b> with scale adjustment.");
                            break;
                        case (4):
                            DisplayHint("- Uses the <b>world position</b> (good for scene effects).");
                            break;
                        case (5):
                            DisplayHint("- Use this for <b>UI images</b>.");
                            DisplayHint("- The <b>ImageSSU</b> component auto-updates <b>rect size</b>.");

                            //Add component:
                            if (gameObject != null && gameObject.GetComponent<RectTransform>() != null && gameObject.GetComponent<Image>() != null && gameObject.GetComponent<ImageSSU>() == null)
                            {
                                EditorGUILayout.Space();
                                GUI.color = new Color(0.8f, 1f, 0.8f);
                                if (GUILayout.Button("Add Helper Component"))
                                {
                                    if (gameObject.GetComponent<InstancerSSU>() != null)
                                    {
                                        MonoBehaviour.DestroyImmediate(gameObject.GetComponent<InstancerSSU>());
                                    }
                                    gameObject.AddComponent<ImageSSU>();
                                }
                                GUI.color = Color.white;
                            }
                            break;
                        case (6):
                            DisplayHint("- Use this for non-moving <b>UI Images</b> or <b>fullscreen</b> effects..");
                            break;
                    }

                    DrawShaderCategory("Time", ref showTime);
                }
                else if (prop.name == "_ShaderFading")
                {
                    DrawShaderCategory("Fading");
                    shaderFading = prop.floatValue;
                }
                else if (prop.name == "_UberNoiseTexture")
                {
                    DrawShaderCategory("Noise", ref showNoise);
                }
                else if (prop.name == "_VertexTintFirst")
                {
                    DrawShaderCategory("Settings");
                }
                else if (prop.name == "_IsText")
                {
                    DrawShaderCategory("GUI Settings");
                }
                else if (prop.name == "_StencilComp")
                {
                    DrawShaderCategory("UI Masking", ref showMasking);
                }

                if (isKeyword && !newCategory && prop.name != "PixelSnap" && prop.name != "_UseUIAlphaClip" && prop.name != "_MetallicMapToggle")
                {
                    //Space:
                    if (prop.name == "_TogglePixelArt" && shaderSpace > 1.1f)
                    {
                        continue;
                    }

                    //GUI:
                    if (prop.name == "_IsTextMeshPro" && materials[0].GetFloat("_IsText") < 0.5f)
                    {
                        isHidden = true;
                        continue;
                    }

                    //Time:
                    if(showTime == false)
                    {
                        if(prop.name == "_ToggleCustomTime" || prop.name == "_ToggleTimeSpeed" || prop.name == "_ToggleTimeFPS" || prop.name == "_ToggleTimeFrequency")
                        {
                            continue;
                        }
                    }

                    //Lines:
                    Lines();

                    //Hiding:
                    if (hidingProperties.Contains(prop.name))
                    {
                        isHidden = prop.floatValue < 0.5f;
                    }
                }
                else if (isHidden)
                {
                    if (prop.name == "_EnchantedLowColor" || prop.name == "_EnchantedHighColor" || prop.name == "_ShiftingColorA" || prop.name == "_ShiftingColorB")
                    {
                        //No Skipping
                    }
                    else
                    {
                        continue; //Skip hidden properties.
                    }
                }
                else
                {
                    if (prop.name == "_EnchantedLowColor" || prop.name == "_EnchantedHighColor" || prop.name == "_ShiftingColorA" || prop.name == "_ShiftingColorB")
                    {
                        continue; //Skipping if not Hidden
                    }
                }

                //Lines Above:
                if (linesAboveProperties.Contains(prop.name))
                {
                    Lines();
                }

                //Name:
                string[] splitName = displayName.Split(':');
                if (splitName.Length > 1)
                {
                    displayName = splitName[1];
                    if (displayName[0] == ' ')
                    {
                        displayName = displayName.Substring(1);
                    }
                }
                GUIContent displayContent = new GUIContent(displayName, isKeyword ? "" : prop.name + "  (C#)");

                //Display:
                EditorGUILayout.BeginHorizontal();
                if (prop.type == MaterialProperty.PropType.Texture)
                {
                    prop.textureValue = (Texture)EditorGUILayout.ObjectField(displayContent, prop.textureValue, typeof(Texture), false);
                }
                else if (prop.type == MaterialProperty.PropType.Vector)
                {
                    if(prop.name == "_SpriteSheetRect")
                    {
                        prop.vectorValue = EditorGUI.Vector4Field(PropertyLabel(displayContent), GUIContent.none, prop.vectorValue);
                    }
                    else
                    {
                        prop.vectorValue = EditorGUI.Vector2Field(PropertyLabel(displayContent), GUIContent.none, prop.vectorValue);
                    }
                }
                else if (prop.type == MaterialProperty.PropType.Range)
                {
                    bool isSlider = true;
                    if (prop.name == "_FadingFade" && Mathf.RoundToInt(shaderFading) == 4)
                    {
                        isSlider = false;
                    }

                    if (isSlider)
                    {
                        prop.floatValue = EditorGUI.Slider(GetPropertySize(prop, isKeyword), displayContent, prop.floatValue, prop.rangeLimits.x, prop.rangeLimits.y);
                    }
                    else
                    {
                        prop.floatValue = EditorGUI.FloatField(GetPropertySize(prop, isKeyword), displayContent, prop.floatValue);
                    }
                }
                else if (prop.type == MaterialProperty.PropType.Color)
                {
                    EditorGUI.BeginChangeCheck();
                    EditorGUI.showMixedValue = prop.hasMixedValue;

                    bool showEyedropper = false;
                    bool showAlpha = false;
                    bool showContrast = false;
                    if (prop.name.StartsWith("_Recolor"))
                    {
                        showAlpha = true;
                        showContrast = true;
                    }
                    if (prop.name == "_ColorReplaceFromColor")
                    {
                        showEyedropper = true;
                    }

                    Color color = EditorGUILayout.ColorField(displayContent, prop.colorValue, showEyedropper, showAlpha, true);

                    if (showContrast)
                    {
                        Rect lastRect = GUILayoutUtility.GetLastRect();
                        lastRect.y -= 1;
                        GUI.color = new Color(1, 1, 1, 0.5f);
                        GUI.Label(lastRect, "<color=#00000000>" + displayContent.text + "</color>  <b><size=9>| " + Mathf.RoundToInt(prop.colorValue.a * 200) + "%</size></b>", labelStyle);
                        GUI.color = Color.white;
                    }

                    EditorGUI.showMixedValue = false;
                    if (EditorGUI.EndChangeCheck())
                    {
                        prop.colorValue = color;
                    }
                }
                else
                {
                    materialEditor.ShaderProperty(GetPropertySize(prop, isKeyword), prop, displayContent);
                }

                if (!isKeyword)
                {
                    ResetButton(prop);
                    HelpButton(prop, displayContent);
                }
                EditorGUILayout.EndHorizontal();

                //Hints:
                if(prop.name == "_ToggleUnscaledTime" && prop.floatValue > 0.5f)
                {
                    DisplayHint("- Requires <b>one</b> active <b>UnscaledTimeSSU</b> component in your scene.");
                }
                if (prop.name == "_TimeFPS")
                {
                    DisplayHint("- Limits shader animations to <b>" + Mathf.RoundToInt(prop.floatValue) + "</b> frames per second.");
                }
                if (prop.name == "_TimeRange")
                {
                    DisplayHint("- Time flows <b>back</b> and <b>forth</b>.");
                }
                if(prop.name == "_VertexTintFirst" && prop.floatValue > 0.5f)
                {
                    DisplayHint("- Applies the vertex tint <b>before</b> the applying shaders.");
                }
                if (prop.name == "_TilingFix" && prop.floatValue > 0.5f)
                {
                    DisplayHint("- Fixes issues with <b>tiling</b> shaders.");
                    DisplayHint("- This is only required on <b>sprite sheets</b>.");
                }
                if (prop.name == "_PixelPerfectSpace" && prop.floatValue > 0.5f)
                {
                    DisplayHint("- Snaps the <b>shader space</b> to the sprite's pixels.");
                    DisplayHint("- Only works with <b>UV</b> shader space.");
                }
                if (prop.name == "_PixelPerfectUV" && prop.floatValue > 0.5f)
                {
                    DisplayHint("- Snaps the <b>UV manipulation</b> to the sprite's pixels.");
                }
                if (prop.name == "_BakedMaterial" && prop.floatValue > 0.5f)
                {
                    DisplayHint("- Enabled while baking shaders to textures.");
                    DisplayHint("- You can <b>ignore</b> this.");
                }
                if (prop.name == "_ForceAlpha" && prop.floatValue > 0.5f)
                {
                    DisplayHint("- Forces the alpha to always be <b>100%</b>.");
                    DisplayHint("- You can <b>ignore</b> this.");
                }
                if(prop.name == "_WindLocalWind" && prop.floatValue < 0.5f)
                {
                    DisplayHint("- Uses and requires <b>WindManagerSSU</b> for <b>global</b> wind.");
                }
                if (prop.name == "_WindMaxIntensity")
                {
                    DisplayHint("- Uses <b>local</b> wind settings.");
                }
                if (prop.name == "_WindHighQualityNoise" && prop.floatValue > 0.5f)
                {
                    DisplayHint("- Generates less <b>repetitive</b> noise, but costs more performance.");
                }
                if (prop.name == "_SpriteSheetRect")
                {
                    DisplayHint("- Requires the <b>SpriteSheetSSU</b> component.");
                    DisplayHint("- Fixes sprite sheet issues for shaders like the <b>Wind</b> shader.");

                    Sprite targetSprite = null;
                    EditorGUILayout.BeginHorizontal();
                    GUI.color = new Color(1, 1, 1, 0.7f);
                    EditorGUILayout.PrefixLabel("- Manual Rect Calculation");
                    targetSprite = (Sprite) EditorGUILayout.ObjectField(targetSprite, typeof(Sprite), true);
                    if (targetSprite != null)
                    {
                        foreach(Material mat in materials)
                        {
                            mat.SetVector("_SpriteSheetRect", SpriteSheetSSU.GetSheetVector(targetSprite));
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    GUI.color = Color.white;

                    //Apply to Sprite Renderer:
                    if (Selection.activeGameObject != null)
                    {
                        SpriteRenderer spriteRenderer = Selection.activeGameObject.GetComponent<SpriteRenderer>();
                        if(spriteRenderer.sharedMaterial == materials[0])
                        {
                            if(spriteRenderer.HasPropertyBlock() && spriteRenderer.sprite != null && spriteRenderer.sprite != lastSSF)
                            {
                                lastSSF = spriteRenderer.sprite;
                                MaterialPropertyBlock mpb = new MaterialPropertyBlock();
                                spriteRenderer.GetPropertyBlock(mpb);
                                mpb.SetVector("_SpriteSheetRect", SpriteSheetSSU.GetSheetVector(lastSSF));
                                spriteRenderer.SetPropertyBlock(mpb);
                            }
                        }
                    }
                }
                if (prop.name == "_WiggleFixedGroundToggle" && prop.floatValue > 0.5f)
                {
                    DisplayHint("- Prevents the sprite from <b>sliding</b> on the ground.");
                }
                if(prop.name == "_TMPSmoothness")
                {
                    DisplayHint("- Many <b>distortion</b> shaders will not work.");
                    DisplayHint("- <b>Bold</b> markups will not work.");
                }

                //Other:
                if (currentHint != null)
                {
                    if(currentHint.hints != null)
                    {
                        for(int s = 0; s < currentHint.hints.Count; s++)
                        {
                            HintText suffix = currentHint.hints[s];
                            if(suffix.property == prop.name)
                            {
                                DisplayHint(suffix.text);
                                break;
                            }
                        }
                    }
                    if(currentHint.spaceHint != null && currentHint.spaceHint == prop.name)
                    {
                        DisplaySpaceHint();
                    }
                    if (currentHint.lines != null)
                    {
                        for (int l = 0; l < currentHint.lines.Count; l++)
                        {
                            string line = currentHint.lines[l];
                            if (line == prop.name)
                            {
                                Lines();
                            }
                        }
                    }
                }
            }

            EditorGUILayout.EndVertical();

            //Final Category Close:
            ShaderTitle("Close");

            //Additional:
            AdditionalMaterialProperties(materialEditor);
        }

        void Initialize(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            gameObject = Selection.activeGameObject;

            if(allHints == null)
            {
                allHints = new Dictionary<string, ShaderHintSSU>();
            }

            if(enabledShaders == null)
            {
                enabledShaders = new HashSet<string>();
            }

            if(shaderList == null || shaderList.Count == 0)
            {
                shaderList = new List<string>();
                shaderList.Add("Sprite Shaders Ultimate/Standard SSU");
                shaderList.Add("Sprite Shaders Ultimate/Additive SSU");
                shaderList.Add("Sprite Shaders Ultimate/GUI SSU");
                shaderList.Add("Sprite Shaders Ultimate/Additive GUI SSU");
                shaderList.Add("Sprite Shaders Ultimate/2D Lit URP SSU");
                shaderList.Add("Sprite Shaders Ultimate/3D Lit URP SSU");
                shaderList.Add("Sprite Shaders Ultimate/3D Lit BuiltIn SSU");
                shaderList.Add("Sprite Shaders Ultimate/Multiplicative SSU");
                shaderList.Add("Sprite Shaders Ultimate/3D Lit Cutout URP SSU");
                shaderList.Add("Sprite Shaders Ultimate/3D Lit Cutout BuiltIn SSU");

                shaderDictionary = new Dictionary<string, int>();
                for(int n = 0; n < shaderList.Count; n++)
                {
                    shaderDictionary.Add(shaderList[n], n);
                }
            }

            labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.richText = true;

            labelCenterStyle = new GUIStyle(labelStyle);
            labelCenterStyle.alignment = TextAnchor.MiddleCenter;

            buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.richText = true;
            buttonStyle.alignment = TextAnchor.MiddleCenter;

            shader = ((Material) materialEditor.target).shader;
            if (defaultMaterial == null || defaultMaterial.shader != shader)
            {
                defaultMaterial = new Material(shader);
            }

            if (lastShader != shader)
            {
                lastShader = shader;

                toggleProperties = new HashSet<string>();
                foreach (MaterialProperty prop in properties)
                {
                    if (IsKeyword(prop))
                    {
                        toggleProperties.Add(prop.name);
                    }
                }

                hidingProperties = new HashSet<string>();
                foreach (MaterialProperty prop in properties)
                {
                    if (prop.name.StartsWith("_Toggle") || prop.name.EndsWith("Toggle") || prop.name == "_IsTextMeshPro" || prop.name == "_WindLocalWind" || prop.name == "_WindIsParallax" || prop.name == "_SpriteSheetFix")
                    {
                        hidingProperties.Add(prop.name);
                    }
                }

                enableProperties = new HashSet<string>();
                foreach (MaterialProperty prop in properties)
                {
                    if (prop.name.StartsWith("_Enable"))
                    {
                        enableProperties.Add(prop.name);
                    }
                }

                alwaysHiddenProperties = new HashSet<string>();
                alwaysHiddenProperties.Add("_AlphaTex");
                alwaysHiddenProperties.Add("_texcoord");
                alwaysHiddenProperties.Add("_EmissionColor");
                alwaysHiddenProperties.Add("_AlphaCutoff");

                linesAboveProperties = new HashSet<string>();
                linesAboveProperties.Add("_MaskMap");
                linesAboveProperties.Add("_Smoothness");
                linesAboveProperties.Add("_NormalMap");
                linesAboveProperties.Add("_ShadowClip");
                linesAboveProperties.Add("_AlphaClip");
            }

            materials = new Material[materialEditor.targets.Length];
            for(int m = 0; m < materials.Length; m++)
            {
                materials[m] = (Material) materialEditor.targets[m];
            }
        }

        void DrawStatus(MaterialEditor materialEditor)
        {
            //Top:
            GUI.color = Color.white;
            EditorGUILayout.BeginVertical("Helpbox");

            //Shaders Toolbar:
            int selected = -1;
            if(shaderDictionary.ContainsKey(shader.name))
            {
                selected = shaderDictionary[shader.name];
            }
            int newSelection = GUILayout.Toolbar(selected, new string[] { "Standard", "Additive", "GUI", "Additive GUI" });
            int secondSelection = GUILayout.Toolbar(selected - 4, new string[] { "2D Lit URP", "3D Lit URP", "3D Lit Built-In", "Multiplicative" });
            int thirdSelection = GUILayout.Toolbar(selected - 8, new string[] { "3D Lit Cutout URP", "3D Lit Cutout Built-In"});
            if (secondSelection > -1 && secondSelection != selected - 4)
            {
                newSelection = secondSelection + 4;
            }
            if (thirdSelection > -1 && thirdSelection != selected - 8)
            {
                newSelection = thirdSelection + 8;
            }

            if (newSelection != selected)
            {
                Undo.RecordObjects(materials, "Changed SSU Shader");
                skipGUI = true;

                foreach(Material mat in materials)
                {
                    mat.shader = Shader.Find(shaderList[newSelection]);
                }
            }

            EditorGUILayout.Space();

            //Status:
            GUI.color = new Color(0.7f, 1f, 0.7f, EditorGUIUtility.isProSkin ? 1f : 0.7f);
            EditorGUILayout.BeginVertical(GUI.skin.box);
            GUI.color = hintColor;
            EditorGUILayout.LabelField("- Enabled <b>" + enabledAmount + "</b> out of <b>" + shaderAmount + "</b> shaders.", labelStyle);

            //Fading:
            switch(Mathf.RoundToInt(shaderFading))
            {
                case (0):
                    //No Cost
                    break;
                case (1):
                    performanceBenchmark += 400;
                        break;
                case (2):
                    performanceBenchmark += 600;
                    textureAmount++;
                        break;
                case (3):
                    performanceBenchmark += 950;
                    textureAmount++;
                    break;
                case (4):
                    performanceBenchmark += 1150;
                    textureAmount++;
                    break;
            }

            //Performance:
            if(performanceBenchmark <= 1)
            {
                EditorGUILayout.LabelField("- GPU Performance: " + "<b>" + Mathf.RoundToInt(performanceBenchmark * 0.1f) + "</b> (<size=11>same as unity's built-in shader</size>)", labelStyle);
            }
            else
            {
                EditorGUILayout.LabelField("- GPU Performance: " + "<b>" + Mathf.RoundToInt(performanceBenchmark * 0.1f) + "</b>", labelStyle);
            }

            if (textureAmount == 1)
            {
                EditorGUILayout.LabelField("- Texture Samples: <b>" + textureAmount + "</b>  (<size=11>the sprite texture</size>)", labelStyle);
            }
            else
            {
                EditorGUILayout.LabelField("- Texture Samples: <b>" + textureAmount + "</b>", labelStyle);
            }
            EditorGUILayout.EndVertical();

            wasWarned = false;

            //Color Space:
            if (PlayerSettings.colorSpace != ColorSpace.Linear)
            {
                StartWarning();
                EditorGUILayout.LabelField("- The <b>default</b> shader variables are <b>adjusted</b> for <b>Linear</b> color space.", labelStyle);
                EditorGUILayout.LabelField("- Switch to <b>Linear</b> for higher <b>quality</b> and better <b>default</b> settings.", labelStyle);

                if (GUILayout.Button("Switch to <b>Linear</b> (recommended)", buttonStyle))
                {
                    Undo.RecordObjects(materials, "Enabled Linear Color Space");

                    PlayerSettings.colorSpace = ColorSpace.Linear;
                }
                EditorGUILayout.EndVertical();
            }

            //Conflict Warnings:
            if (enabledShaders.Contains("World Tiling") && enabledShaders.Contains("Screen Tiling"))
            {
                StartWarning();
                EditorGUILayout.LabelField("- <b>World</b> and <b>Screen Tiling</b> aren't compatible.", labelStyle);
                EditorGUILayout.EndVertical();
            }
            if (enabledShaders.Contains("Smooth Pixel Art") && enabledShaders.Contains("Gaussian Blur"))
            {
                StartWarning();
                EditorGUILayout.LabelField("- <b>Smooth Pixel Art</b> and <b>Gaussian Blur</b> aren't compatible.", labelStyle);
                EditorGUILayout.EndVertical();
            }

            //Texture Warnings:
            Texture activeTexture = GetTexture(materialEditor);
            Sprite activeSprite = GetSprite();
            TextureImporter textureImporter = GetTextureImporter(activeTexture);
            if (textureImporter != null)
            {
                TextureImporterSettings textureSettings = new TextureImporterSettings();
                textureImporter.ReadTextureSettings(textureSettings);

                if (textureImporter != null)
                {
                    //Tiling and Sheet Conflict:
                    if(requiresTiling && textureImporter.spriteImportMode == SpriteImportMode.Multiple && materials[0].GetFloat("_TilingFix") < 0.5f)
                    {
                        StartWarning();
                        EditorGUILayout.LabelField("- <b>Tiling</b> shaders cause problems with <b>sprite sheets</b>.", labelStyle);
                        EditorGUILayout.LabelField("- Please enable <b>Tiling Fix</b> to fix these problems.", labelStyle);
                        EditorGUILayout.LabelField("- Also make sure <b>Sprite Sheet Fix</b> is enabled.", labelStyle);
                        if (GUILayout.Button("Enable <b>Tiling Fix</b>", buttonStyle))
                        {
                            Undo.RecordObjects(materials, "Enabled Tiling Fix Option");

                            foreach (Material mat in materials)
                            {
                                mat.EnableKeyword("_TILINGFIX_ON");
                                mat.SetFloat("_TilingFix", 1f);
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }

                    //Full Rect Mesh:
                    if (requiresFullRectMesh && textureSettings.spriteMeshType != SpriteMeshType.FullRect)
                    {
                        StartWarning();
                        EditorGUILayout.LabelField("- Set <b>Mesh Type</b> to <b>Full Rect</b> to avoid pixel clipping.", labelStyle);

                        if (GUILayout.Button("Set <b>Mesh Type</b> to <b>Full Rect</b>", buttonStyle))
                        {
                            textureSettings.spriteMeshType = SpriteMeshType.FullRect;

                            Undo.RecordObject(textureImporter, "Changed Mesh Type");

                            textureImporter.SetTextureSettings(textureSettings);
                            textureImporter.SaveAndReimport();
                        }
                        EditorGUILayout.EndVertical();
                    }

                    //Tiling Fix:
                    if (requiresTiling && textureSettings.wrapMode == TextureWrapMode.Clamp)
                    {
                        StartWarning();
                        EditorGUILayout.LabelField("- Some shaders require <b>Wrap Mode</b> set to <b>Repeat</b>.", labelStyle);
                        EditorGUILayout.LabelField("- <b>Tiling</b> won't work properly without this setting.", labelStyle);

                        if (GUILayout.Button("Set <b>Wrap Mode</b> to <b>Repeat</b>", buttonStyle))
                        {
                            textureSettings.wrapMode = TextureWrapMode.Repeat;

                            Undo.RecordObject(textureImporter, "Changed Wrap Mode");

                            textureImporter.SetTextureSettings(textureSettings);
                            textureImporter.SaveAndReimport();
                        }
                        EditorGUILayout.EndVertical();
                    }

                    //Sprite Sheet Fix:
                    if (enabledShaders.Contains("Wiggle") && materials[0].GetFloat("_WiggleFixedGroundToggle") > 0.5f)
                    {
                        requiresSpriteSheetFix = true;
                    }
                    if((requiresSpriteSheetFix || requiresTiling) && textureImporter.spriteImportMode == SpriteImportMode.Multiple && materials[0].GetFloat("_SpriteSheetFix") < 0.5f && activeSprite != null)
                    {
                        StartWarning();
                        EditorGUILayout.LabelField("- Some shaders require the <b>Sprite Sheet Fix</b> option.", labelStyle);
                        if (GUILayout.Button("Enable <b>Sprite Sheet Fix</b>", buttonStyle))
                        {
                            Undo.RecordObjects(materials, "Enabled Sprite Sheet Fix Option");

                            foreach (Material mat in materials)
                            {
                                mat.EnableKeyword("_SPRITESHEETFIX_ON");
                                mat.SetFloat("_SpriteSheetFix", 1f);

                                mat.SetVector("_SpriteSheetRect", SpriteSheetSSU.GetSheetVector(activeSprite));
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }

                    //Sprite Sheet Component:
                    if (materials[0].GetFloat("_SpriteSheetFix") > 0.5f && Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<SpriteSheetSSU>() == false)
                    {
                        StartWarning();
                        EditorGUILayout.LabelField("- You might need the <b>SpriteSheetSSU</b> component.", labelStyle);
                        EditorGUILayout.LabelField("- It updates the <b>Rect</b> variable depending on the <b>Sprite</b>.", labelStyle);

                        if (GUILayout.Button("Add <b>SpriteSheetSSU</b>", buttonStyle))
                        {
                            Undo.AddComponent(Selection.activeGameObject, typeof(SpriteSheetSSU));
                        }

                        Material mat = materials[0];
                        if(activeSprite != null)
                        {
                            mat.SetVector("_SpriteSheetRect", SpriteSheetSSU.GetSheetVector(activeSprite));
                        }

                        EditorGUILayout.EndVertical();
                    }

                    //Instance Required:
                    if (requiresInstancing && Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<InstancerSSU>() == null)
                    {
                        StartWarning();
                        if (Mathf.RoundToInt(shaderSpace) == 2 || Mathf.RoundToInt(shaderSpace) == 3)
                        {
                            EditorGUILayout.LabelField("- <b>Object</b> space requires <b>unique</b> material <b>instances</b>.", labelStyle);
                        }
                        else
                        {
                            EditorGUILayout.LabelField("- Some shaders require <b>unique</b> material <b>instances</b>.", labelStyle);
                        }
                        EditorGUILayout.LabelField("- <b>Instantiate</b> the material at <b>runtime</b> to fix this.", labelStyle);

                        if (Selection.activeGameObject.GetComponent<SpriteRenderer>() != null)
                        {
                            EditorGUILayout.LabelField("- <b>Modifying</b> the material at <b>runtime</b> will <b>instantiate</b> it.", labelStyle);
                        }

                        bool multiple = false;
                        if (enabledShaders.Contains("Wind"))
                        {
                            multiple = true;
                            if (GUILayout.Button("Add <b>InteractiveWindSSU</b>", buttonStyle))
                            {
                                Undo.AddComponent(Selection.activeGameObject, typeof(InteractiveWindSSU));
                            }
                        }
                        if (GUILayout.Button("Add <b>MaterialInstancerSSU</b>", buttonStyle))
                        {
                            Undo.AddComponent(Selection.activeGameObject, typeof(MaterialInstancerSSU));
                        }

                        if (multiple)
                        {
                            EditorGUILayout.LabelField("- You only need <b>one</b> of these components.", labelStyle);
                        }

                        EditorGUILayout.EndVertical();
                    }

                    //Bilinear:
                    if (enabledShaders.Contains("Smooth Pixel Art") && textureSettings.filterMode == FilterMode.Point)
                    {
                        StartWarning();
                        EditorGUILayout.LabelField("- <b>Smooth Pixel Art</b> requires <b>Bilinear</b> filtering.", labelStyle);

                        if (GUILayout.Button("Set <b>Filter Mode</b> to <b>Bilinear</b>", buttonStyle))
                        {
                            textureSettings.filterMode = FilterMode.Bilinear;

                            Undo.RecordObject(textureImporter, "Changed Filter Mode");

                            textureImporter.SetTextureSettings(textureSettings);
                            textureImporter.SaveAndReimport();
                        }

                        EditorGUILayout.EndVertical();
                    }

                    if(enabledShaders.Contains("Pixelate"))
                    {
                        if (textureSettings.mipmapEnabled)
                        {
                            StartWarning();
                            EditorGUILayout.LabelField("- <b>Pixelate</b> does not work with <b>mip maps</b>.", labelStyle);

                            if (GUILayout.Button("Disable <b>Mip Maps</b>", buttonStyle))
                            {
                                textureSettings.mipmapEnabled = false;

                                Undo.RecordObject(textureImporter, "Disabled Mip Maps");

                                textureImporter.SetTextureSettings(textureSettings);
                                textureImporter.SaveAndReimport();
                            }

                            EditorGUILayout.EndVertical();
                        }
                    }
                }
            }

            //WindParallaxSSU Required:
            if(enabledShaders.Contains("Wind") && materials[0].GetFloat("_WindIsParallax") > 0.5f && Selection.activeGameObject.GetComponent<WindParallaxSSU>() == null)
            {
                StartWarning();
                EditorGUILayout.LabelField("- <b>Is Parallax</b> requires the <b>WindParallaxSSU</b> component.", labelStyle);
                EditorGUILayout.LabelField("- This will fix any issues with <b>moving</b> the sprite.", labelStyle);

                if (GUILayout.Button("Add <b>WindParallaxSSU</b>", buttonStyle))
                {
                    Undo.AddComponent(Selection.activeGameObject, typeof(WindParallaxSSU));
                }

                EditorGUILayout.EndVertical();
            }

            if(!wasWarned)
            {
                GUI.color = new Color(0.7f, 1, 1f, EditorGUIUtility.isProSkin ? 1f : 0.7f);
                EditorGUILayout.BeginVertical(GUI.skin.box);
                GUI.color = hintColor;
                EditorGUILayout.LabelField("- No <b>issues</b> were detected.", labelStyle);
                EditorGUILayout.LabelField("- Detected issues will be <b>displayed here</b>.", labelStyle);
                EditorGUILayout.EndVertical();  
            }

            //Reset Requirements:
            requiresSpriteSheetFix = requiresFullRectMesh = requiresInstancing = requiresTiling = false;

            //Support Box:
            GUI.color = new Color(0.7f, 0.77f, 1f, EditorGUIUtility.isProSkin ? 1f : 0.7f);
            EditorGUILayout.BeginVertical(GUI.skin.box);
            GUI.color = Color.white;
            DisplaySupportInformation();
            EditorGUILayout.EndVertical();

            //Export Material Feature:
            GUI.color = new Color(0.9f, 0.7f, 1f, EditorGUIUtility.isProSkin ? 1f : 0.7f);
            EditorGUILayout.BeginVertical(GUI.skin.box);
            GUI.color = new Color(0.95f, 0.85f, 1, 1f);
            if (GUILayout.Button("<b>Bake Shader</b> to Texture and <b>Export</b>", buttonStyle))
            {
                ExportSprite(materialEditor, bakeStackAmount);
            }
            bakeStackAmount = Mathf.Clamp(EditorGUILayout.IntField("Stack Amount", bakeStackAmount), 1, 20);
            GUI.color = hintColor;
            EditorGUILayout.LabelField("- Use a <b>stack amount</b> of <b>10</b> for smooth <b>gaussian blur</b>.", labelStyle);
            GUI.color = Color.white;
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndVertical();
        }

        void StartWarning()
        {
            wasWarned = true;
            GUI.color = warnColor;
            EditorGUILayout.BeginVertical(GUI.skin.box);
        }

        void AdditionalMaterialProperties(MaterialEditor materialEditor)
        {
            if (UnityEngine.Rendering.SupportedRenderingFeatures.active.editableMaterialRenderQueue)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                materialEditor.RenderQueueField();
            }
        }

        void DrawShaderCategory(string title)
        {
            newCategory = true;
            isHidden = false;

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical("Helpbox");

            GUI.color = new Color(1, 1, 1, 0.9f);
            GUILayout.Label("<b><size=14>" + title + "</size></b>", labelStyle);
            Lines();
        }
        void DrawShaderCategory(string title, ref bool toggleVariable)
        {
            newCategory = true;
            isHidden = false;
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical("Helpbox");

            GUI.color = new Color(1, 1, 1, 0.9f);
            GUILayout.Label("<b><size=14>" + title + "</size></b>", labelStyle);

            //Toggle:
            Rect buttonRect = GUILayoutUtility.GetLastRect();
            buttonRect.x += buttonRect.width - 20;
            buttonRect.width = 20;

            if(GUI.Button(buttonRect, !toggleVariable ? "<size=12>▶</size>" : "<size=10>▼</size>", buttonStyle))
            {
                toggleVariable = !toggleVariable;
            }

            if(toggleVariable)
            {
                Lines();
            }
            else
            {
                isHidden = true;
            }
        }

        void DisplayHint(string text)
        {
            GUI.color = hintColor;
            EditorGUILayout.LabelField(text, labelStyle);
            GUI.color = Color.white;
        }
        void DisplaySpaceHint()
        {
            GUI.color = hintColor;
            EditorGUILayout.LabelField("- Uses <b>" + spaceName + " Space</b> as configured above.", labelStyle);
            GUI.color = Color.white;
        }

        bool DrawShaderToggle(MaterialEditor materialEditor, MaterialProperty prop)
        {
            newCategory = true;
            isHidden = false;
            EditorGUILayout.EndVertical();

            switch (prop.name)
            {
                case ("_EnableStrongTint"):
                    ShaderTitle("Color");
                    break;
                case ("_EnableHologram"):
                    ShaderTitle("Effect");
                    break;
                case ("_EnableFullAlphaDissolve"):
                    ShaderTitle("Fading");
                    break;
                case ("_EnablePixelate"):
                    ShaderTitle("Transformation");
                    break;
                case ("_EnableWind"):
                    ShaderTitle("Interactive");
                    break;
                case ("_EnableCheckerboard"):
                    ShaderTitle("Other");
                    break;
            }

            shaderAmount++;

            bool isEnabled = prop.floatValue > 0.5f;
            bool isCollapsed = prop.floatValue == 1.1f;

            //Box:
            Color color = Color.HSVToRGB((currentBoxColor * 0.05f) % 1, isEnabled ? 0.7f : 0.2f + (EditorGUIUtility.isProSkin ? 0.4f : 0), EditorGUIUtility.isProSkin ? (isEnabled ? 1.8f : 1f) : 1);
            color.a = isEnabled ? 1f : 0.7f;
            GUI.color = color;
            currentBoxColor++;
            EditorGUILayout.BeginVertical("Helpbox");

            string shaderName = prop.displayName.Replace("Enable ", "");
            EditorGUILayout.BeginHorizontal();

            GUI.color = new Color(1, 1, 1, 0.5f);
            GUILayout.Label(shaderAmount + ".", GUILayout.Width(24f));
            GUI.color = new Color(1, 1, 1, isEnabled ? 0.9f : 0.7f);
            GUILayout.Label("<b><size=14>" + shaderName + "</size></b>", labelStyle);

            //Track:
            if (isEnabled)
            {
                enabledShaders.Add(shaderName);
            }

            Rect toggleRect = GUILayoutUtility.GetLastRect();

            //Enabled:
            currentHint = null;
            if (isEnabled)
            {
                enabledAmount++;

                string resourcePath = "SSU/Hints/" + shaderName;
                if (allHints.ContainsKey(resourcePath))
                {
                    currentHint = allHints[resourcePath];
                }
                else
                {
                    currentHint = Resources.Load<ShaderHintSSU>(resourcePath);

                    if (currentHint != null)
                    {
                        allHints.Add(resourcePath, currentHint);
                    }
                }

                DrawShadow("<b><size=14>" + shaderName + "</size></b>");
            }

            toggleRect.x += toggleRect.width -= 37;
            toggleRect.width = 37;

            materialEditor.ShaderProperty(toggleRect, prop, GUIContent.none);

            if (isEnabled)
            {
                Rect lastRect = GUILayoutUtility.GetLastRect();
                lastRect.x += lastRect.width - 20;
                lastRect.width = 20;

                if (GUI.Button(lastRect, isCollapsed ? "<size=12>▶</size>" : "<size=10>▼</size>", buttonStyle))
                {
                    isCollapsed = !isCollapsed;

                    if (isCollapsed)
                    {
                        prop.floatValue = 1.1f;
                    }
                    else
                    {
                        prop.floatValue = 1f;
                    }
                }

                if (currentHint != null)
                {
                    if (currentHint.requiresFullRectMesh)
                    {
                        requiresFullRectMesh = true;
                    }
                    if (currentHint.requiresSpriteSheetFix)
                    {
                        requiresSpriteSheetFix = true;
                    }
                    if (currentHint.requiresTiling)
                    {
                        requiresTiling = true;
                    }
                    if (currentHint.requiresInstancing)
                    {
                        requiresInstancing = true;
                    }
                    if (currentHint.benchmarkValue > 0)
                    {
                        performanceBenchmark += currentHint.benchmarkValue;
                    }
                    if (currentHint.textureSamples > 0)
                    {
                        textureAmount += currentHint.textureSamples;
                    }
                    if (currentHint.textureToggle != "" && currentHint.textureToggle != null && materials[0].GetFloat(currentHint.textureToggle) > 0.5f)
                    {
                        textureAmount++;
                    }
                    if (currentHint.textureToggleExtra != "" && currentHint.textureToggleExtra != null && materials[0].GetFloat(currentHint.textureToggleExtra) > 0.5f)
                    {
                        textureAmount++;
                    }
                }
            }

            GUI.color = Color.white;
            EditorGUILayout.EndHorizontal();

            if (isEnabled && !isCollapsed)
            {
                if (currentHint != null && currentHint.shaderDescription != null && currentHint.shaderDescription != "")
                {
                    string[] descriptionLines = currentHint.shaderDescription.Split('\n');
                    for (int d = 0; d < descriptionLines.Length; d++)
                    {
                        DisplayHint(descriptionLines[d]);
                    }
                    DisplayHint("- GPU Performance: <b>" + Mathf.RoundToInt(currentHint.benchmarkValue * 0.1f) + "</b>");
                }

                if (prop.name == "_EnableSmoothPixelArt")
                {
                    Texture activeTexture = GetTexture(materialEditor);
                    TextureImporter textureImporter = GetTextureImporter(activeTexture);

                    if (textureImporter != null)
                    {
                        if (textureImporter.filterMode == FilterMode.Point)
                        {
                            GUI.color = warnColor;
                            if (GUILayout.Button("Set <b>" + activeTexture.name + "'s</b> filter mode to <b>Bilinear</b>", buttonStyle))
                            {
                                TextureImporterSettings textureSettings = new TextureImporterSettings();
                                textureImporter.ReadTextureSettings(textureSettings);

                                textureSettings.filterMode = FilterMode.Bilinear;

                                Undo.RecordObject(textureImporter, "Changed Filter Mode");

                                textureImporter.SetTextureSettings(textureSettings);
                                textureImporter.SaveAndReimport();
                            }
                            GUI.color = Color.white;
                        }
                    }
                }
                else
                {
                    Lines();

                    /*if (prop.name == "_EnableRecolorPalette")
                    {
                        EditorGUILayout.BeginHorizontal();
                        if (GUILayout.Button("Generate Source"))
                        {
                            Texture activeTexture = GetTexture(materialEditor);

                            if (activeTexture != null)
                            {
                                //Blit to Render Texture:
                                RenderTexture rt = new RenderTexture(activeTexture.width, activeTexture.height, 0, RenderTextureFormat.ARGB32);
                                Graphics.Blit(activeTexture, rt);

                                //Read RenderTexture to Texture:
                                Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
                                tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
                                tex.Apply();

                                //Get Colors:
                                Dictionary<Vector3, float> colorWeights = new Dictionary<Vector3, float>();
                                for (int x = 0; x < tex.width; x++)
                                {
                                    for (int y = 0; y < tex.height; y++)
                                    {
                                        Color pixel = paletteBilinear ? tex.GetPixelBilinear((float)x / (float)tex.width, (float)y / (float)tex.height) : tex.GetPixel(x, y);
                                        Vector3 colorKey = new Vector3(pixel.r, pixel.g, pixel.b);

                                        if (colorWeights.ContainsKey(colorKey))
                                        {
                                            colorWeights[colorKey] += pixel.a;
                                        }
                                        else
                                        {
                                            colorWeights.Add(colorKey, pixel.a);
                                        }
                                    }

                                    EditorUtility.DisplayProgressBar("Generating Palette Texture", "Loading Pixels", 0f);
                                }
                                
                                //Get Top Colors:
                                List<Vector3> paletteColors = new List<Vector3>();
                                Dictionary<Vector3, int> colorDictionary = new Dictionary<Vector3, int>(); ;
                                int totalColors = paletteSize * paletteSize;
                                if(colorWeights.Count > totalColors)
                                {
                                    while (paletteColors.Count < totalColors && colorWeights.Count > 0)
                                    {
                                        float highestValue = 0;
                                        Vector3 highestColor = Vector3.zero;
                                        foreach (KeyValuePair<Vector3, float> colorWeight in colorWeights)
                                        {
                                            if (highestValue < colorWeight.Value)
                                            {
                                                highestValue = colorWeight.Value;
                                                highestColor = colorWeight.Key;
                                            }
                                        }
                                        paletteColors.Add(highestColor);
                                        colorDictionary.Add(highestColor, paletteColors.Count - 1);
                                        colorWeights.Remove(highestColor);
                                        EditorUtility.DisplayProgressBar("Generating Palette Texture", "Sorting colors.", 0.2f);
                                    }
                                }
                                else
                                {
                                    foreach (KeyValuePair<Vector3, float> colorWeight in colorWeights)
                                    {
                                        paletteColors.Add(colorWeight.Key);
                                        colorDictionary.Add(colorWeight.Key, paletteColors.Count - 1);
                                    }
                                }
                                
                                
                                /*Sort Top Colors:
                                List<Vector3> sortedColors = new List<Vector3>();
                                sortedColors.Add(topColors[0]);
                                topColors.RemoveAt(0);
                                while (topColors.Count > 0)
                                {
                                    float closestDistance = 100f;
                                    int closestIndex = -1;
                                    for (int c = 0; c < topColors.Count; c++)
                                    {
                                        float distance = (sortedColors[sortedColors.Count - 1] - topColors[c]).sqrMagnitude;

                                        if (distance < closestDistance)
                                        {
                                            closestDistance = distance;
                                            closestIndex = c;
                                        }
                                    }

                                    sortedColors.Add(topColors[closestIndex]);
                                    topColors.RemoveAt(closestIndex);
                                    EditorUtility.DisplayProgressBar("Generating Palette Texture", "Sorting color palette.", 0.5f);
                                }

                                //Creating Texture:
                                for (int x = 0; x < tex.width; x++)
                                {
                                    for (int y = 0; y < tex.height; y++)
                                    {
                                        Color pixel = paletteBilinear ? tex.GetPixelBilinear((float)x / (float)tex.width, (float)y / (float)tex.height) : tex.GetPixel(x, y);
                                        Vector3 colorKey = new Vector3(pixel.r, pixel.g, pixel.b);

                                        int colorIndex = 0;

                                        if (colorDictionary.ContainsKey(colorKey))
                                        {
                                            colorIndex = colorDictionary[colorKey];
                                        }
                                        else
                                        {
                                            float closestDistance = 100f;
                                            for (int c = 0; c < paletteColors.Count; c++)
                                            {
                                                float distance = (paletteColors[c] - colorKey).sqrMagnitude;

                                                if (distance < closestDistance)
                                                {
                                                    closestDistance = distance;
                                                    colorIndex = c;
                                                }
                                            }
                                        }

                                        float rValue = (float) (colorIndex % paletteSize) / paletteSize;
                                        float gValue = Mathf.Floor(colorIndex / paletteSize) / paletteSize;
                                        tex.SetPixel(x, y, new Color(rValue, gValue, 0, 1));
                                    }

                                    EditorUtility.DisplayProgressBar("Generating Palette Texture", "Mapping colors.", 0.8f);
                                }

                                //Export:
                                EditorUtility.ClearProgressBar();
                                string oldPath = AssetDatabase.GetAssetPath(activeTexture);
                                if (oldPath == "")
                                {
                                    oldPath = Application.dataPath + "/";
                                }

                                string[] fileEnding = oldPath.Split('.');
                                string newPath = oldPath.Replace("." + fileEnding[fileEnding.Length - 1], "");
                                string folderPath = "";
                                string[] folders = oldPath.Split('/');
                                for (int n = 0; n < folders.Length - 1; n++)
                                {
                                    folderPath += folders[n] + "/";
                                }
                                string fileName = activeTexture.name;
                                newPath = EditorUtility.SaveFilePanel("Save Texture", folderPath, activeTexture.name + " (Source)", "png");

                                if (newPath != "")
                                {
                                    newPath = newPath.Substring(Application.dataPath.Length - 9);
                                    while (newPath.Length > 1 && newPath.StartsWith("Assets") == false)
                                    {
                                        newPath = newPath.Substring(1);
                                    }

                                    //Save Texture:
                                    byte[] bytes = tex.EncodeToPNG();
                                    bool newFile = AssetDatabase.LoadAssetAtPath(newPath, typeof(Texture)) == null;
                                    if (newFile)
                                    {
                                        AssetDatabase.CopyAsset(oldPath, newPath);
                                    }
                                    System.IO.File.WriteAllBytes(newPath, bytes);
                                    if (newFile)
                                    {
                                        AssetDatabase.ImportAsset(newPath);
                                    }

                                    //Import new Texture:
                                    AssetDatabase.Refresh();

                                    //Ping:
                                    EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath(newPath, typeof(Texture)));
                                }
                                
                                //Clear:
                                if (RenderTexture.active == rt)
                                {
                                    RenderTexture.active = null;
                                }
                                Object.DestroyImmediate(rt);
                                Object.DestroyImmediate(tex);
                            }
                            else
                            {
                                EditorUtility.DisplayDialog("No texture found.", "Select a SpriteRenderer or Image with this material.\nOr assign a texture to this material.", "Alright");
                            }
                        }
                        if (GUILayout.Button("Generate Palette"))
                        {
                            Texture activeTexture = GetTexture(materialEditor);

                            if (activeTexture != null)
                            {
                                //Open Source:
                                string currentFile = AssetDatabase.GetAssetPath(activeTexture);
                                string[] splitPath = currentFile.Split('/');
                                string openPath = EditorUtility.OpenFilePanel("Open Source Texture", currentFile.Substring(0, currentFile.Length - splitPath[splitPath.Length - 1].Length), "png");
                                string[] openPathSplit = openPath.Split('/');
                                openPath = "";

                                bool ignore = true;
                                for(int i = 0; i < openPathSplit.Length; i++)
                                {
                                    if(openPathSplit[i] == "Assets")
                                    {
                                        ignore = false;
                                    }

                                    if(!ignore)
                                    {
                                        openPath += openPathSplit[i] + (i == openPathSplit.Length - 1 ? "" : "/");
                                    }
                                }


                                Texture sourceTexture = AssetDatabase.LoadAssetAtPath<Texture>(openPath);

                                //Source Texture:
                                if (sourceTexture != null)
                                {
                                    RenderTexture sourceRT = new RenderTexture(sourceTexture.width, sourceTexture.height, 0, RenderTextureFormat.ARGB32);
                                    Graphics.Blit(sourceTexture, sourceRT);
                                    Texture2D sourceTex = new Texture2D(sourceRT.width, sourceRT.height, TextureFormat.ARGB32, false);
                                    sourceTex.ReadPixels(new Rect(0, 0, sourceRT.width, sourceRT.height), 0, 0);
                                    sourceTex.Apply();

                                    //Recolor Texture
                                    RenderTexture rt = new RenderTexture(activeTexture.width, activeTexture.height, 0, RenderTextureFormat.ARGB32);
                                    Graphics.Blit(activeTexture, rt);
                                    Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
                                    tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
                                    tex.Apply();

                                    //Creating Palette:
                                    Texture2D paletteTex = new Texture2D(paletteSize, paletteSize);
                                    for (int x = 0; x < tex.width; x++)
                                    {
                                        for (int y = 0; y < tex.height; y++)
                                        {
                                            Color pixel = paletteBilinear ? tex.GetPixelBilinear((float)x / (float)tex.width, (float)y / (float)tex.height) : tex.GetPixel(x, y);
                                            Color pixelSource = paletteBilinear ? sourceTex.GetPixelBilinear((float)x / (float)tex.width, (float)y / (float)tex.height) : tex.GetPixel(x, y);
                                            paletteTex.SetPixel(Mathf.RoundToInt(pixelSource.r * paletteSize), Mathf.RoundToInt(pixelSource.g * paletteSize), pixel);
                                        }
                                        EditorUtility.DisplayProgressBar("Generating Palette Texture", "Saving Pixels", 0f);
                                    }
                                    paletteTex.Apply();

                                    //Export:
                                    EditorUtility.ClearProgressBar();
                                    string oldPath = AssetDatabase.GetAssetPath(activeTexture);
                                    if (oldPath == "")
                                    {
                                        oldPath = Application.dataPath + "/";
                                    }

                                    string[] fileEnding = oldPath.Split('.');
                                    string newPath = oldPath.Replace("." + fileEnding[fileEnding.Length - 1], "");
                                    string folderPath = "";
                                    string[] folders = oldPath.Split('/');
                                    for (int n = 0; n < folders.Length - 1; n++)
                                    {
                                        folderPath += folders[n] + "/";
                                    }
                                    string fileName = activeTexture.name;
                                    newPath = EditorUtility.SaveFilePanel("Save Texture", folderPath, activeTexture.name + " (Source)", "png");
                                    if (newPath != "")
                                    {
                                        newPath = newPath.Substring(Application.dataPath.Length - 9);
                                        while (newPath.Length > 1 && newPath.StartsWith("Assets") == false)
                                        {
                                            newPath = newPath.Substring(1);
                                        }

                                        //Save Texture:
                                        byte[] bytes = paletteTex.EncodeToPNG();
                                        bool newFile = AssetDatabase.LoadAssetAtPath(newPath, typeof(Texture)) == null;
                                        if (newFile)
                                        {
                                            AssetDatabase.CopyAsset(oldPath, newPath);
                                        }
                                        System.IO.File.WriteAllBytes(newPath, bytes);
                                        if (newFile)
                                        {
                                            AssetDatabase.ImportAsset(newPath);
                                        }

                                        //Import new Texture:
                                        AssetDatabase.Refresh();

                                        //Ping:
                                        EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath(newPath, typeof(Texture)));
                                    }

                                    //Clear:
                                    if (RenderTexture.active == rt || RenderTexture.active == sourceRT)
                                    {
                                        RenderTexture.active = null;
                                    }
                                    Object.DestroyImmediate(rt);
                                    Object.DestroyImmediate(tex);
                                    Object.DestroyImmediate(sourceRT);
                                    Object.DestroyImmediate(sourceTex);
                                    Object.DestroyImmediate(paletteTex);
                                }
                            }
                            else
                            {
                                EditorUtility.DisplayDialog("No texture found.", "Select a SpriteRenderer or Image with this material.\nOr assign a texture to this material.", "Alright");
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                        paletteSize = EditorGUILayout.IntField("Palette Colors", paletteSize);
                        paletteBilinear = EditorGUILayout.Toggle("Bilinear Filter", paletteBilinear);
                        GUI.color = hintColor;
                        EditorGUILayout.LabelField("- Use these to easily generate palette textures.", labelStyle);
                        Lines();
                    }*/
                }
            }

            return !isEnabled || isCollapsed;
        }

        static Texture GetTexture(MaterialEditor materialEditor)
        {
            Texture activeTexture = null;
            if (Selection.activeGameObject != null)
            {
                SpriteRenderer spriteRenderer = Selection.activeGameObject.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    if(spriteRenderer.sprite != null)
                    {
                        activeTexture = spriteRenderer.sprite.texture;

                    }
                }
                else
                {
                    Image image = Selection.activeGameObject.GetComponent<Image>();

                    if (image != null && image.sprite != null)
                    {
                        activeTexture = image.sprite.texture;
                    }
                }
            }
            else
            {
                foreach(Material mat in materials)
                {
                    if (mat != null)
                    {
                        activeTexture = mat.GetTexture("_MainTex");

                        if(activeTexture != null)
                        {
                            break;
                        }
                    }
                }
            }

            return activeTexture;
        }
        public static TextureImporter GetTextureImporter(Texture activeTexture)
        {
            if (activeTexture != null)
            {
                string path = AssetDatabase.GetAssetPath(activeTexture);

                if (path != null)
                {
                    TextureImporter textureImporter = (TextureImporter)AssetImporter.GetAtPath(path);
                    return textureImporter;
                }
            }

            return null;
        }

        Sprite GetSprite()
        {
            if (Selection.activeGameObject != null)
            {
                SpriteRenderer spriteRenderer = Selection.activeGameObject.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    return spriteRenderer.sprite;
                }
                else
                {
                    Image image = Selection.activeGameObject.GetComponent<Image>();

                    if (image != null)
                    {
                        return image.sprite;
                    }
                }
            }

            return null;
        }

        void DrawShadow(string textContent)
        {
            for (int x = -1; x <= 1; x += 2)
            {
                for (int y = -1; y <= 1; y += 2)
                {
                    Rect lastRect = GUILayoutUtility.GetLastRect();
                    lastRect.x += x;
                    lastRect.y += y;
                    GUI.color = new Color(1, 1, 1, 0.12f);
                    EditorGUI.LabelField(lastRect, new GUIContent(textContent), labelStyle);
                }
            }
            GUI.color = Color.white;
        }

        void ShaderTitle(string title)
        {
            if(title != "Close")
            {
                if(title != "Color")
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                GUI.color = new Color(1, 1, 1, 0.55f);
                EditorGUILayout.LabelField("<size=18><b>" + title + "</b></size>", labelStyle, GUILayout.Height(22));

                GUI.color = new Color(1, 1, 1, 0.1f);
                for (int x = -1; x <= 1; x += 2)
                {
                    for (int y = -1; y <= 1; y += 2)
                    {
                        Rect lastRect = GUILayoutUtility.GetLastRect();
                        lastRect.y += x;
                        lastRect.x += y;
                        EditorGUI.LabelField(lastRect, "<size=18><b>" + title + "</b></size>", labelStyle);
                    }
                }
                GUI.color = Color.white;
            }

            float currentHeight = (title != "Close") ? GUILayoutUtility.GetLastRect().y : GUILayoutUtility.GetLastRect().y + 59;

            if (title == "Color")
            {
                shaderAmount = enabledAmount = currentBoxColor = 0;
                performanceBenchmark = 0f;
                textureAmount = 1;
            }
            else
            {
                Rect lineRect = GUILayoutUtility.GetLastRect();
                lineRect.x -= 8;
                lineRect.width = 2;
                lineRect.y = lastTitleHeight + 4;
                lineRect.height = currentHeight - lastTitleHeight - 32;

                if(EditorGUIUtility.isProSkin)
                {
                    GUI.color = new Color(1, 1, 1, 0.5f);
                    lineRect.x -= 3;
                    GUI.Box(lineRect, GUIContent.none, GUI.skin.verticalSlider);
                    lineRect.x -= 1;
                    GUI.Box(lineRect, GUIContent.none, GUI.skin.verticalSlider);
                }
                else
                {
                    GUI.color = new Color(0, 0, 0, 0.3f);
                    GUI.Box(lineRect, GUIContent.none);
                    lineRect.x += 1;
                    GUI.Box(lineRect, GUIContent.none);
                    GUI.color = Color.white;
                }
            }

            lastTitleHeight = currentHeight;
            EditorGUILayout.Space();
        }

        void ResetButton(MaterialProperty prop)
        {
            GUIContent resetButton = new GUIContent();
            resetButton.text = "R";
            resetButton.tooltip = "Resets this property.";

            if (prop.type == MaterialProperty.PropType.Color)
            {
                Color defaultValue = defaultMaterial.GetColor(prop.name);

                if (prop.colorValue == defaultValue)
                {
                    GUI.enabled = false;
                }

                if (GUILayout.Button(resetButton, GUILayout.Width(20)))
                {
                    prop.colorValue = defaultValue;
                }
            }
            else if (prop.type == MaterialProperty.PropType.Vector)
            {
                Vector4 defaultValue = defaultMaterial.GetVector(prop.name);

                if (prop.vectorValue == defaultValue)
                {
                    GUI.enabled = false;
                }

                if (GUILayout.Button(resetButton, GUILayout.Width(20)))
                {
                    prop.vectorValue = defaultValue;
                }
            }
            else if (prop.type == MaterialProperty.PropType.Float || prop.type == MaterialProperty.PropType.Range)
            {
                float defaultValue = defaultMaterial.GetFloat(prop.name);

                if (prop.floatValue == defaultValue)
                {
                    GUI.enabled = false;
                }

                if (GUILayout.Button(resetButton, GUILayout.Width(20)))
                {
                    prop.floatValue = defaultValue;
                }
            }

            GUI.enabled = true;
        }

        void HelpButton(MaterialProperty prop, GUIContent displayContent)
        {
            if(prop.type == MaterialProperty.PropType.Texture)
            {
                return;
            }

            GUIContent infoButton = new GUIContent();
            infoButton.text = "<size=1> </size>C<size=10>#</size>";
            infoButton.tooltip = "Useful information for programmers.";

            if (GUILayout.Button(infoButton, buttonStyle, GUILayout.Width(26)))
            {
                CodingHelper.Open(displayContent, prop, shader, EditorGUIUtility.currentViewWidth);
            }
        }

        Rect PropertyLabel(GUIContent labelContent)
        {
            EditorGUILayout.LabelField(" ");

            Rect labelRect = GUILayoutUtility.GetLastRect();
            Rect fieldRect = new Rect(labelRect);

            if (labelRect.width > 320)
            {
                labelRect.width -= 160;
            }
            else
            {
                labelRect.width = labelRect.width / 2;
            }

            fieldRect.width -= labelRect.width;
            fieldRect.x += labelRect.width;

            EditorGUI.PrefixLabel(labelRect, labelContent);

            return fieldRect;
        }
        Rect GetPropertySize(MaterialProperty prop, bool isKeyword)
        {
            EditorGUILayout.LabelField(" ");

            Rect fullRect = GUILayoutUtility.GetLastRect();
            Rect labelRect = new Rect(fullRect);
            Rect fieldRect = new Rect(labelRect);

            if (labelRect.width > 320)
            {
                labelRect.width -= 160;
            }
            else
            {
                labelRect.width = labelRect.width / 2;
            }

            if(isKeyword) {
                labelRect.width -= 52;
            }

            fieldRect.width -= labelRect.width;
            fieldRect.x += labelRect.width;

            EditorGUIUtility.fieldWidth = fieldRect.width;
            EditorGUIUtility.labelWidth = labelRect.width;

            if (prop.type == MaterialProperty.PropType.Range)
            {
                EditorGUIUtility.fieldWidth = 50;
                EditorGUIUtility.labelWidth = labelRect.width;
            }

            return fullRect;
        }

        bool IsKeyword(MaterialProperty prop)
        {
            return IsKeyword(prop.name);
        }

        public static bool IsKeyword(string propertyName)
        {
            if (propertyName.StartsWith("_Toggle") || propertyName.EndsWith("Toggle") || propertyName.EndsWith("Invert") || propertyName == "PixelSnap" || propertyName == "_ShaderSpace" || propertyName == "_ShaderFading" || propertyName == "_BakedMaterial" || propertyName == "_SmokeVertexSeed" || propertyName == "_SpriteSheetFix" || propertyName == "_UseUIAlphaClip" || propertyName == "_IsText" || propertyName == "_IsTextMeshPro" || propertyName == "_TilingFix" || propertyName == "_ForceAlpha" || propertyName == "_VertexTintFirst" || propertyName == "_PixelPerfectSpace" || propertyName == "_PixelPerfectUV" || propertyName == "_WindLocalWind" || propertyName == "_WindHighQualityNoise" || propertyName == "_WindIsParallax" || propertyName == "_WindFlip" || propertyName == "_SquishFlip")
            {
                return true;
            }

            return false;
        }

        public static void Lines()
        {
            GUI.color = new Color(1, 1, 1, 0.5f);
            EditorGUILayout.LabelField("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ", GUILayout.Height(9));
            GUI.color = new Color(1, 1, 1, 1);
        }

        public static void DisplaySupportInformation()
        {
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.richText = true;

            GUIStyle linkStyle = new GUIStyle(GUI.skin.label);
            linkStyle.richText = true;
            linkStyle.alignment = TextAnchor.MiddleLeft;
            linkStyle.normal.textColor = linkStyle.focused.textColor = linkStyle.hover.textColor = EditorGUIUtility.isProSkin ? new Color(0.8f, 0.9f, 1f, 1) : new Color(0.1f, 0.2f, 0.4f, 1);
            linkStyle.active.textColor = EditorGUIUtility.isProSkin ? new Color(0.6f, 0.8f, 1f, 1) : new Color(0.15f, 0.4f, 0.6f, 1);

            DisplayLink("Documentation", "https://ekincantas.com/sprite-shaders-ultimate/", "https://ekincantas.com/sprite-shaders-ultimate/", labelStyle, linkStyle);
            DisplayLink("Discord", "https://discord.gg/nWbRkN8Zxr", "https://discord.gg/nWbRkN8Zxr", labelStyle, linkStyle);
            DisplayLink("Email", "ekincantascontact@gmail.com", "", labelStyle, linkStyle);
        }

        private static void DisplayLink(string title, string link, string url, GUIStyle labelStyle, GUIStyle linkStyle)
        {
            GUI.color = new Color(1, 1, 1, 0.7f);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("<b>" + title + ": </b>", labelStyle);
            Rect lastRect = GUILayoutUtility.GetLastRect();
            lastRect.x += 100;
            lastRect.width -= 100;

            GUI.color = Color.white;

            if (url == "")
            {
                EditorGUI.SelectableLabel(lastRect, link, linkStyle);
            }
            else
            {
                if(GUI.Button(lastRect,link, linkStyle))
                {
                    Application.OpenURL(url);
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        public static void ExportSprite(MaterialEditor materialEditor, int count = 1)
        {
            Material material = (Material)materialEditor.target;
            Texture texture = GetTexture(materialEditor);

            if (material != null && texture != null)
            {
                SaveShadedTexture(material, texture, count);
            }
            else
            {
                EditorUtility.DisplayDialog("No texture found.", "Select a SpriteRenderer or Image with this material.\nOr assign a texture to this material.", "Alright");
            }
        }
        public static void SaveShadedTexture(Material spriteMaterial, Texture spriteTexture, int count = 1)
        {
            //Enable Baked Material:
            Undo.RecordObject(spriteMaterial, "Switched baked material setting.");
            spriteMaterial.EnableKeyword("_BAKEDMATERIAL_ON");
            spriteMaterial.SetFloat("_BakedMaterial", 1f);

            Texture2D tex = (Texture2D) spriteTexture;
            bool first = true;
            for (int n = 0; n < count; n++)
            {
                //Blit to Render Texture:
                RenderTexture rt = new RenderTexture(tex.width, tex.height, 0, RenderTextureFormat.ARGB32);
                Graphics.Blit(tex, rt, spriteMaterial);

                //Destroy Previous Texture:
                if (!first)
                {
                    Object.DestroyImmediate(tex);
                }
                first = false;

                //Read RenderTexture to Texture:
                tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
                tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
                tex.Apply();

                //Clear RenderTexture:
                if (RenderTexture.active == rt)
                {
                    RenderTexture.active = null;
                }
                Object.DestroyImmediate(rt);
            }

            //Disable Baked Material:
            spriteMaterial.DisableKeyword("_BAKEDMATERIAL_ON");
            spriteMaterial.SetFloat("_BakedMaterial", 0f);

            //Get Paths:
            string oldPath = AssetDatabase.GetAssetPath(spriteTexture);
            if (oldPath == "")
            {
                oldPath = Application.dataPath + "/";
            }

            string[] fileEnding = oldPath.Split('.');
            string newPath = oldPath.Replace("." + fileEnding[fileEnding.Length - 1], "");
            string folderPath = "";
            string[] folders = oldPath.Split('/');
            for (int n = 0; n < folders.Length - 1; n++)
            {
                folderPath += folders[n] + "/";
            }
            string fileName = spriteTexture.name;
            int number = 1;
            while (AssetDatabase.LoadAssetAtPath(newPath + " (Baked " + number + ").png", typeof(Texture)) != null)
            {
                number++;
            }
            newPath = EditorUtility.SaveFilePanel("Save Texture", folderPath, spriteTexture.name + " (Baked " + number + ")", "png");

            if (newPath != "")
            {
                newPath = newPath.Substring(Application.dataPath.Length - 9);
                while (newPath.Length > 1 && newPath.StartsWith("Assets") == false)
                {
                    newPath = newPath.Substring(1);
                }

                //Save Texture:
                byte[] bytes = tex.EncodeToPNG();
                bool newFile = AssetDatabase.LoadAssetAtPath(newPath, typeof(Texture)) == null;
                if (newFile)
                {
                    AssetDatabase.CopyAsset(oldPath, newPath);
                }
                System.IO.File.WriteAllBytes(newPath, bytes);
                if (newFile)
                {
                    AssetDatabase.ImportAsset(newPath);
                }

                //Import new Texture:
                AssetDatabase.Refresh();

                //Ping:
                EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath(newPath, typeof(Texture)));
            }

            //Clean Up:
            Object.DestroyImmediate(tex);
        }
    }
}

#endif