using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteShadersUltimate
{
    public class WindParallaxSSU : MonoBehaviour
    {
        float originalXPosition;

        void Awake()
        {
            originalXPosition = transform.position.x;
        }

        void Start()
        {
            GetComponent<Renderer>().material.SetFloat("_WindXPosition", originalXPosition);
        }
    }
}