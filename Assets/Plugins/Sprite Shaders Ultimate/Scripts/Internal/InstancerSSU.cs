using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteShadersUltimate
{
    [DisallowMultipleComponent()]
    public class InstancerSSU : MonoBehaviour
    {
        //To prevent multiple components of subtypes.
        [HideInInspector]
        public Material runtimeMaterial;
    }
}
