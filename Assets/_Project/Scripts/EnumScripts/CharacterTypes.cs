using System;
using UnityEngine;

[Serializable]
public enum CharacterTypeEnum { Tasko, Cinemon, Mufin, Behapeno, Wita };

public class CharacterTypes : MonoBehaviour
{
    [SerializeField] private CharacterTypeEnum _characterTypeEnum = default;

    public CharacterTypeEnum CharacterTypeEnum { get { return _characterTypeEnum; } private set { } }
}
