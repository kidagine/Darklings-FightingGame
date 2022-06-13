using System;
using UnityEngine;

[Serializable]
public enum NotificationTypeEnum { Punish, Knockdown, CrossUp };

public class NotificationTypes : MonoBehaviour
{
    [SerializeField] private NotificationTypeEnum _notificationTypeEnum = default;

    public NotificationTypeEnum NotificationTypeEnum { get { return _notificationTypeEnum; } private set { } }
}
