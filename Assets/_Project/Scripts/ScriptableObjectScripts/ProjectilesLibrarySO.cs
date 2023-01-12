using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectiles Library", menuName = "Scriptable Objects/Projectiles Library", order = 1)]
public class ProjectilesLibrarySO : ScriptableObject
{
    public List<ObjectPool> _projectilesPools;
}
