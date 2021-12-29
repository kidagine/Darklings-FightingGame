using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D.Animation;

public class CharacterButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private RuntimeAnimatorController _characterAnimatorController = default;
	[SerializeField] private PlayerStatsSO _playerStatsSO = default;
    [SerializeField] private CharacterMenu _characterMenu = default;
    [SerializeField] private GameObject _firstPlayerSelector = default;
    [SerializeField] private GameObject _secondPlayerSelector = default;
	[SerializeField] private bool _isRandomizer = default;

	public RuntimeAnimatorController CharacterAnimatorController { get { return _characterAnimatorController; }  set { } }
	public PlayerStatsSO PlayerStatsSO { get { return _playerStatsSO; } set { } }
	public bool IsRandomizer { get { return _isRandomizer; } private set { } }

    public void OnSelect(BaseEventData eventData)
    {
        _firstPlayerSelector.SetActive(true);
        _characterMenu.SetCharacterOneImage(true, CharacterAnimatorController, PlayerStatsSO, IsRandomizer);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _firstPlayerSelector.SetActive(false);
    }
}
