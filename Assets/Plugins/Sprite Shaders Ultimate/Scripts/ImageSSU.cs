using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpriteShadersUltimate
{
    public class ImageSSU : InstancerSSU
    {
        //Updates material when RectTransform changes.
        [Tooltip("Enable this if the size of the RectTransform will change.")]
        public bool updateChanges = false;

        //References:
        RectTransform rectTransform;

        //Property IDs:
        int rectWidthID;
        int rectHeightID;

        //Previous SizeDelta:
        Vector2 lastSizeDelta;

        void Awake()
        {
            //Get RectTransform:
            rectTransform = GetComponent<RectTransform>();

            //Instantiate Material:
            Image image = GetComponent<Image>();
            image.material = Instantiate(image.material);
            runtimeMaterial = image.materialForRendering;

            //Shader IDs:
            rectWidthID = Shader.PropertyToID("_RectWidth");
            rectHeightID = Shader.PropertyToID("_RectHeight");
        }

        void Start()
        {
            //Set Values:
            UpdateMaterial();
        }

        void Update()
        {
            if (updateChanges)
            {
                if (lastSizeDelta != rectTransform.sizeDelta)
                {
                    UpdateMaterial();
                }
            }
        }

        public void UpdateMaterial()
        {
            lastSizeDelta = rectTransform.sizeDelta;
            runtimeMaterial.SetFloat(rectWidthID, lastSizeDelta.x);
            runtimeMaterial.SetFloat(rectHeightID, lastSizeDelta.y);
        }
    }
}
