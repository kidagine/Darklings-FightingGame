using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
	[SerializeField] private Image _notificationSlide = default;
	[SerializeField] private TextMeshProUGUI _notificationText = default;
	[Header("Notification Colors")]
	[SerializeField] private Color _punishColor = Color.red;
	[SerializeField] private Color _knockdownColor = Color.blue;
	[SerializeField] private Color _crossUpColor = Color.yellow;


	public void SetNotification(NotificationTypeEnum notificationType)
	{
		_notificationText.text = notificationType.ToString();
		switch (notificationType)
		{
			case NotificationTypeEnum.Punish:
				_notificationSlide.color = _punishColor;
				_notificationText.color = _punishColor;
				break;
			case NotificationTypeEnum.Knockdown:
				_notificationSlide.color = _knockdownColor;
				_notificationText.color = _knockdownColor;
				break;
			case NotificationTypeEnum.CrossUp:
				_notificationSlide.color = _crossUpColor;
				_notificationText.color = _crossUpColor;
				break;
		}
	}
}
