using System;
using UnityEngine;

[Serializable]
public enum ActionSchemeTypes { Gameplay, UI, Training };

public class ActionScheme : MonoBehaviour
{
    [SerializeField] private ActionSchemeTypes _actionSchemeTypeEnum = default;

    public ActionSchemeTypes ActionSchemeTypes { get { return _actionSchemeTypeEnum; } private set { } }
}