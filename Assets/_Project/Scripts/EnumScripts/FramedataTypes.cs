using System;
using UnityEngine;

namespace Demonics
{
    [Serializable]
    public enum FramedataTypesEnum { None, StartUp, Active, Recovery, Hurt, Knockdown, Block, Parry, Empty };

    public class FramedataTypes : MonoBehaviour
    {
        [SerializeField] private FramedataTypesEnum _framedataType = default;

        public FramedataTypesEnum FramedataTypesEnum { get { return _framedataType; } private set { } }
    }
}