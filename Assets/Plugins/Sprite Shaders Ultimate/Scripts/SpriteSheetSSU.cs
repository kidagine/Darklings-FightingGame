using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpriteShadersUltimate
{
    /// <summary>
    /// Attach this to sprite renderers or images with a sprite shaders ultimate material.
    /// Enable Sprite Sheet Fix and let this component update the Sprite Sheet Rect variable.
    /// </summary>
    public class SpriteSheetSSU : MonoBehaviour
    {
        public bool updateChanges = false;

        SpriteRenderer spriteRenderer;
        Image image;
        Sprite lastSprite;

        void Awake()
        {
            //References:
            spriteRenderer = GetComponent<SpriteRenderer>();
            image = GetComponent<Image>();

            //Instantiate Image:
            if(image != null && GetComponent<InstancerSSU>() == null)
            {
                image.material = Instantiate<Material>(image.material);
            }

            //Update Rect:
            UpdateSpriteRect();

            //Disable if updateChanges is disabled.
            if(updateChanges == false)
            {
                enabled = false;
            }
        }

        void LateUpdate()
        {
            if((spriteRenderer != null && lastSprite != spriteRenderer.sprite) || (image != null && lastSprite != image.sprite))
            {
                UpdateSpriteRect();
            }
        }

        public void UpdateSpriteRect()
        {
            if (spriteRenderer != null)
            {
                lastSprite = spriteRenderer.sprite;
            }
            else if (image != null)
            {
                lastSprite = image.sprite;
            }

            if (lastSprite != null)
            {
                if (spriteRenderer != null)
                {
                    if(spriteRenderer.HasPropertyBlock())
                    {
                        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
                        spriteRenderer.GetPropertyBlock(mpb);
                        mpb.SetVector("_SpriteSheetRect", GetSheetVector(lastSprite));
                        spriteRenderer.SetPropertyBlock(mpb);
                    }
                    else
                    {
                        spriteRenderer.material.SetVector("_SpriteSheetRect", GetSheetVector(lastSprite));
                    }
                }
                else if (image != null)
                {
                    image.materialForRendering.SetVector("_SpriteSheetRect", GetSheetVector(lastSprite));
                }
            }
        }

        public static Vector4 GetSheetVector(Sprite sprite)
        {
            Rect rect = sprite.rect;
            Texture text = sprite.texture;
            float width = text.width;
            float height = text.height;
            Vector2 minVector = rect.min;
            Vector2 maxVector = rect.max;

            return new Vector4(minVector.x / width, minVector.y / height, maxVector.x / width, maxVector.y / height);
        }
    }
}
