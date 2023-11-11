using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpriteShadersUltimate
{
    public class ShaderFaderSSU : MonoBehaviour
    {
        //Fading:
        public bool automaticFading = true;
        public bool isFaded;
        [Range(0, 1)]
        public float fadeValue = 0f;
        [Min(0.01f)]
        public float duration = 2f;
        public bool unscaledTime;
        public AnimationCurve fadeCurve = new AnimationCurve(new Keyframe(0, 0, 0, 0, 0.55f, 0.55f), new Keyframe(1, 1, 0, 0, 0.55f, 0.55f));

        //Materials:
        public bool getChildObjects = true;
        public bool poolMaterials = true;
        public List<Renderer> renderers = new List<Renderer>();
        public List<Graphic> graphics = new List<Graphic>();

        //Properties:
        public List<FloatFaderSSU> floatProperties = new List<FloatFaderSSU>();
        public List<VectorFaderSSU> vectorProperties = new List<VectorFaderSSU>();
        public List<ColorFaderSSU> colorProperties = new List<ColorFaderSSU>();

        //Internal:
        HashSet<Material> materials;
        Dictionary<Material, Material> materialPool;
        float lastFadeValue;

        void Start()
        {
            ReloadMaterials();
        }

        void Update()
        {
            //Fade:
            if(automaticFading)
            {
                if(isFaded)
                {
                    float speed = 1f / duration;
                    fadeValue += unscaledTime ? Time.unscaledDeltaTime * speed : Time.deltaTime * speed;
                    if(fadeValue > 1f)
                    {
                        fadeValue = 1f;
                    }
                }
                else
                {
                    float speed = 1f / duration;
                    fadeValue -= unscaledTime ? Time.unscaledDeltaTime * speed : Time.deltaTime * speed;
                    if (fadeValue < 0f)
                    {
                        fadeValue = 0f;
                    }
                }
            }

            //Update Materials:
            if(lastFadeValue != fadeValue)
            {
                lastFadeValue = fadeValue;
                UpdateMaterials();
            }
        }

        public void UpdateMaterials()
        {
            foreach (Material mat in materials)
            {
                UpdateSingleMaterial(mat, fadeValue);
            }
        }

        public void UpdateSingleMaterial(Material mat, float fadeFactor)
        {
            foreach (FloatFaderSSU floatProperty in floatProperties)
            {
                mat.SetFloat(floatProperty.propertyName, Mathf.LerpUnclamped(floatProperty.fromValue, floatProperty.toValue, ApplyTimeRange(fadeFactor, floatProperty.fromTime, floatProperty.toTime)));
            }
            foreach (VectorFaderSSU vectorProperty in vectorProperties)
            {
                mat.SetColor(vectorProperty.propertyName, Vector4.LerpUnclamped(vectorProperty.fromValue, vectorProperty.toValue, ApplyTimeRange(fadeFactor, vectorProperty.fromTime, vectorProperty.toTime)));
            }
            foreach (ColorFaderSSU colorProperty in colorProperties)
            {
                mat.SetColor(colorProperty.propertyName, Color.LerpUnclamped(colorProperty.fromValue, colorProperty.toValue, ApplyTimeRange(fadeFactor, colorProperty.fromTime, colorProperty.toTime)));
            }
        }

        float ApplyTimeRange(float fadeFactor, float fromTime, float toTime)
        {
            return fadeCurve.Evaluate(Mathf.Clamp01((fadeFactor - fromTime) / (toTime - fromTime)));
        }

        #region Get Materials
        public void ReloadMaterials()
        {
            //Initialize:
            materials = new HashSet<Material>();
            materialPool = new Dictionary<Material, Material>();
            lastFadeValue = -1;

            //Get Materials:
            if (getChildObjects)
            {
                //Auto Renderers:
                foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>(true))
                {
                    GetMaterialFromRenderer(renderer);
                }

                //Auto Graphics:
                foreach (Graphic graphic in gameObject.GetComponentsInChildren<Graphic>(true))
                {
                    GetMaterialFromGraphic(graphic);
                }
            }
            else
            {
                //Manual Renderers:
                if (renderers != null)
                {
                    foreach (Renderer renderer in renderers)
                    {
                        if (renderer != null)
                        {
                            GetMaterialFromRenderer(renderer);
                        }
                    }
                }

                //Manual Graphics:
                if (graphics != null)
                {
                    foreach (Graphic graphic in graphics)
                    {
                        if (graphic != null)
                        {
                            GetMaterialFromGraphic(graphic);
                        }
                    }
                }
            }
        }

        void GetMaterialFromRenderer(Renderer renderer)
        {
            InstancerSSU instancer = renderer.GetComponent<InstancerSSU>();
            if (instancer != null)
            {
                materials.Add(instancer.runtimeMaterial);
            }
            else
            {
                Material mat = renderer.material = InstantiateMaterial(renderer.material);
                materials.Add(mat);
                renderer.gameObject.AddComponent<InstancerSSU>().runtimeMaterial = mat;
            }
        }

        void GetMaterialFromGraphic(Graphic graphic)
        {
            InstancerSSU instancer = graphic.GetComponent<InstancerSSU>();
            if (instancer != null)
            {
                materials.Add(instancer.runtimeMaterial);
            }
            else
            {
                Material mat = graphic.material = InstantiateMaterial(graphic.material);
                materials.Add(mat);
                graphic.gameObject.AddComponent<InstancerSSU>().runtimeMaterial = mat;
            }
        }

        Material InstantiateMaterial(Material sharedMaterial)
        {
            if(poolMaterials)
            {
                if (materialPool.ContainsKey(sharedMaterial))
                {
                    return materialPool[sharedMaterial];
                }
                else
                {
                    Material newMaterial = Instantiate<Material>(sharedMaterial);
                    newMaterial.name = sharedMaterial + " (Instance)";
                    materialPool.Add(sharedMaterial, newMaterial);
                    return newMaterial;
                }
            }
            else
            {
                return Instantiate<Material>(sharedMaterial);
            }
        }
        #endregion
    }

    [System.Serializable]
    public class BaseFaderSSU
    {
        public BaseFaderSSU()
        {
            toTime = 1;
        }

        [Header("Property Name:")]
        public string propertyName;

        [Header("Time:")]
        [Range(0, 1)]
        public float fromTime = 0f;
        [Range(0, 1)]
        public float toTime = 1f;
    }

    [System.Serializable]
    public class FloatFaderSSU : BaseFaderSSU
    {
        [Header("Range:")]
        public float fromValue;
        public float toValue;

        public FloatFaderSSU(string newName, float newFrom, float newTo)
        {
            propertyName = newName;
            fromValue = newFrom;
            toValue = newTo;

            fromTime = 0f;
            toTime = 1f;
        }
    }

    [System.Serializable]
    public class VectorFaderSSU : BaseFaderSSU
    {
        [Header("Range:")]
        public Vector4 fromValue;
        public Vector4 toValue;

        public VectorFaderSSU(string newName, Vector4 newFrom, Vector4 newTo)
        {
            propertyName = newName;
            fromValue = newFrom;
            toValue = newTo;

            fromTime = 0f;
            toTime = 1f;
        }
    }

    [System.Serializable]
    public class ColorFaderSSU : BaseFaderSSU
    {
        [Header("Range:")]
        [ColorUsage(true,true)]
        public Color fromValue;
        [ColorUsage(true, true)]
        public Color toValue;

        public ColorFaderSSU(string newName, Color newFrom, Color newTo)
        {
            propertyName = newName;
            fromValue = newFrom;
            toValue = newTo;

            fromTime = 0f;
            toTime = 1f;
        }
    }
}

