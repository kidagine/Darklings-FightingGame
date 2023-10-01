using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteShadersUltimate
{
    public class InteractiveWindSSU : InstancerSSU
    {
        [Tooltip("How much physical interaction bends the sprite.")]
        public float rotationFactor = 1.5f;
        [Tooltip("How fast physical interaction bending fades in.")]
        public float bendInSpeed = 8f;
        [Tooltip("How fast physical interaction bending fades out.")]
        public float bendOutSpeed = 8f;

        [Tooltip("If disabled the sprite will only bend during active movement.")]
        public bool stayBent = true;
        [Tooltip("The minimum speed of the interacting object to trigger bending.")]
        public float minBendSpeed = 1f;

        [Tooltip("Swaps the material with the default sprite material while inactive.")]
        public bool hyperPerformanceMode = false;
        [Tooltip("Adds a tiny little offset to the Z position on start.\nTo prevent random resorting of render order.")]
        public bool randomOffsetZ = true;
        [Tooltip("Define a material to switch to while inactive.")]
        public bool customMaterial = false;
        [Tooltip("The shader used for the default sprite material.")]
        public string inactiveShader = "Sprites/Default";
        [Tooltip("The material used when inactive.")]
        public Material inactiveMaterial;

        [Tooltip("Slightly changes 'Wiggle Frequency', to desync the wiggle shaders of multiple sprites.")]
        public bool randomizeWiggle = false;
        [Tooltip("The editor-side script set's the layer to 'Ignore Raycast' to fix potential issues. Enable this to disable that and set the layer to a different one.")]
        public bool allowCustomLayer = false;

        //Variables:
        HashSet<Collider2D> collidersInside;
        BoxCollider2D boxCollider;

        //Runtime:
        float currentBending;
        float currentRotationDirection;
        bool isActive;
        bool newDirection;

        float lastPosition;
        float lastBend;
        float currentBendTarget;
        bool bentInLastFrame;

        SpriteRenderer sr;
        static Material defaultMaterial;

        int rotationId;

        void Start()
        {
            //Initialize Variables:
            collidersInside = new HashSet<Collider2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            sr = GetComponent<SpriteRenderer>();
            runtimeMaterial = sr.material;

            if(defaultMaterial == null)
            {
                if (customMaterial)
                {
                    defaultMaterial = inactiveMaterial;
                }
                else
                {
                    defaultMaterial = new Material(Shader.Find(inactiveShader));
                }
            }

            if(hyperPerformanceMode)
            {
                sr.material = defaultMaterial;

                if(randomOffsetZ)
                {
                    //Prevent Resorting:
                    Vector3 position = transform.position;
                    position.z += Random.value * 0.1f;
                    transform.position = position;
                }
            }

            if(randomizeWiggle && runtimeMaterial != null)
            {
                float wiggleFrequency = runtimeMaterial.GetFloat("_WiggleFrequency") * (0.9f + 0.2f * Random.value);
                runtimeMaterial.SetFloat("_WiggleFrequency", wiggleFrequency);
            }

            rotationId = Shader.PropertyToID("_WindRotation");
        }

        void FixedUpdate()
        {
            if (isActive == false) return;

            Vector2 localPosition = new Vector2(0, -1000000);

            foreach(Collider2D collider in collidersInside)
            {
                if(collider != null)
                {
                    if(localPosition.y < -99999)
                    {
                        localPosition = collider.bounds.center - transform.position; //Collider Position
                    }
                    else
                    {
                        if (!newDirection) {
                            Vector2 newLocalPosition = (collider.bounds.center - transform.position);
                            if((currentRotationDirection < 0 && newLocalPosition.x > localPosition.x) || (currentRotationDirection > 0 && newLocalPosition.x < localPosition.x))
                            {
                                localPosition = newLocalPosition;  //Take most heavy position
                            }
                        }
                        else
                        {
                            localPosition = ((Vector2)(collider.bounds.center - transform.position) + localPosition) * 0.5f; //Position Deviation (multiple colliders)
                        }
                    }
                }
            }

            if (localPosition.y > -99999) //Colliders are interacting with the wind sprite.
            {
                //Bend Direction:
                if (newDirection)
                {
                    if(localPosition.x < 0)
                    {
                        currentRotationDirection = -1;
                    }else
                    {
                        currentRotationDirection = 1;
                    }

                    newDirection = false;
                }

                //Bend Target:
                float targetBending = 0;
                if(currentRotationDirection < 0)
                {
                    targetBending = Mathf.Clamp01((localPosition.x + boxCollider.size.x * 0.5f) / boxCollider.size.x);
                }
                else
                {
                    targetBending = Mathf.Clamp01((boxCollider.size.x * 0.5f - localPosition.x) / boxCollider.size.x);
                }

                if(stayBent)
                {
                    //Staying Bend:
                    currentBendTarget = targetBending;
                }
                else
                {
                    //Temporary Bend:
                    bool moved = Mathf.Abs(lastPosition - localPosition.x) > Time.fixedDeltaTime * minBendSpeed;

                    if(moved && lastBend != 0 && currentRotationDirection > 0 == (localPosition.x - lastPosition) > 0)
                    {
                        moved = false;
                    }

                    if (moved || bentInLastFrame)
                    {
                        currentBendTarget = targetBending;
                        lastBend = targetBending;
                        bentInLastFrame = true;

                        if (!moved)
                        {
                            bentInLastFrame = false;
                        }
                    }
                    else
                    {
                        currentBendTarget = Mathf.Lerp(currentBendTarget, 0, Time.fixedDeltaTime * bendInSpeed);

                        if (Mathf.Abs(currentBending) < 0.01f)
                        {
                            newDirection = true;
                        }
                    }
                    lastPosition = localPosition.x;
                }

                //Fade In Bending:
                currentBending += (currentBendTarget * currentRotationDirection - currentBending) * Mathf.Min(bendInSpeed * Time.fixedDeltaTime, 1);
                UpdateShader();
            }
            else
            {
                //Fade Out Bending:
                currentBending -= currentBending * Mathf.Min(bendOutSpeed * Time.fixedDeltaTime,1);
                UpdateShader();

                if (Mathf.Abs(currentBending) < 0.005f)
                {
                    isActive = false;
                    lastBend = 0;

                    if (hyperPerformanceMode)
                    {
                        sr.material = defaultMaterial;
                    }
                }
            }
        }

        public void UpdateShader()
        {
            runtimeMaterial.SetFloat(rotationId, -1f * currentBending * rotationFactor);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if(collidersInside.Count == 0 || Mathf.Abs(currentBending) < 0.2f)
            {
                newDirection = true;
            }

            collidersInside.Add(collision);

            if (hyperPerformanceMode && isActive == false)
            {
                sr.material = runtimeMaterial;
            }
            isActive = true;
        }
        void OnTriggerExit2D(Collider2D collision)
        {
            if (collidersInside.Contains(collision))
            {
                collidersInside.Remove(collision);
            }
        }

        //Other:
        public static void DefaultCollider(BoxCollider2D box)
        {
            box.isTrigger = true;
            box.size = new Vector2(2, 1);
            box.offset = new Vector2(0, -0.5f);
        }
    }
}