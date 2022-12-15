using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effects Library", menuName = "Scriptable Objects/Effects Library", order = 1)]
public class EffectsLibrarySO : ScriptableObject
{
    public List<ObjectPool> _objectPools;
}
