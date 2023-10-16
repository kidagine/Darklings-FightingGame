using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [SerializeField] private Image _notification = default;
    [SerializeField] private TextMeshProUGUI _notificationText = default;
    [Header("Notification Colors")]
    [SerializeField] private Color _punishColor = Color.red;
    [SerializeField] private Color _knockdownColor = Color.blue;
    [SerializeField] private Color _softKnockdownColor = Color.blue;
    [SerializeField] private Color _crossUpColor = Color.yellow;
    [SerializeField] private Color _guardBreakColor = Color.yellow;
    [SerializeField] private Color _reversalColor = Color.yellow;
    [SerializeField] private Color _wallSplatColor = Color.yellow;
    [SerializeField] private Color _throwBreakColor = Color.white;
    [SerializeField] private Color _lockColor = Color.white;

    public void SetNotification(NotificationTypeEnum notificationType)
    {
        _notificationText.text = Regex.Replace(notificationType.ToString(), "([a-z])([A-Z])", "$1 $2");
        switch (notificationType)
        {
            case NotificationTypeEnum.Punish:
                _notification.color = _punishColor;
                break;
            case NotificationTypeEnum.Knockdown:
                _notificationText.text = "H.Knockdown";
                _notification.color = _knockdownColor;
                break;
            case NotificationTypeEnum.CrossUp:
                _notification.color = _crossUpColor;
                break;
            case NotificationTypeEnum.GuardBreak:
                _notification.color = _guardBreakColor;
                break;
            case NotificationTypeEnum.Reversal:
                _notification.color = _reversalColor;
                break;
            case NotificationTypeEnum.WallSplat:
                _notification.color = _wallSplatColor;
                break;
            case NotificationTypeEnum.ThrowBreak:
                _notification.color = _throwBreakColor;
                break;
            case NotificationTypeEnum.Lock:
                _notification.color = _lockColor;
                break;
            case NotificationTypeEnum.SoftKnockdown:
                _notificationText.text = "S.Knockdown";
                _notification.color = _softKnockdownColor;
                break;
        }
    }
}
