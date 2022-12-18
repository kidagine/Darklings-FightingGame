using Demonics.Enum;
using System.Collections.Generic;
using UnityEngine;

namespace Demonics.Utility
{
    public class LayerProvider : MonoBehaviour
    {
        private static List<UnityEngine.LayerMask> _layerMasks = new List<UnityEngine.LayerMask>();


        public static UnityEngine.LayerMask GetLayerMask(LayerMaskEnum layerMaskEnum)
        {
            UnityEngine.LayerMask layerMask = UnityEngine.LayerMask.GetMask(layerMaskEnum.ToString());
            if (!_layerMasks.Contains(layerMask))
            {

            }
            return layerMask;
        }

        public static int GetLayerMaskIndex(LayerMaskEnum layerMasksEnum)
        {
            int index = UnityEngine.LayerMask.NameToLayer(layerMasksEnum.ToString());
            return index;
        }
    }
}