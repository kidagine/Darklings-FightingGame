using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private RuntimeAnimatorController _characterAnimatorController = default;
	[SerializeField] private PlayerStatsSO _playerStatsSO = default;
    [SerializeField] private CharacterMenu _characterMenu = default;
    [SerializeField] private GameObject _randomSpriteRenderer = default;
    [SerializeField] private GameObject _firstPlayerSelector = default;
    [SerializeField] private GameObject _secondPlayerSelector = default;
	[SerializeField] private bool _isRandomizer = default;

	public RuntimeAnimatorController CharacterAnimatorController { get { return _characterAnimatorController; }  set { } }
	public PlayerStatsSO PlayerStatsSO { get { return _playerStatsSO; } set { } }
	public bool IsRandomizer { get { return _isRandomizer; } private set { } }

    public void OnSelect(BaseEventData eventData)
    {
        if (!_characterMenu.FirstCharacterSelected)
        {
            _firstPlayerSelector.SetActive(true);
        }
        else
        {
            _secondPlayerSelector.SetActive(true);
        }
        _characterMenu.SetCharacterImage(CharacterAnimatorController, PlayerStatsSO, IsRandomizer);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (!_characterMenu.FirstCharacterSelected)
        {
            _firstPlayerSelector.SetActive(false);
        }
        else
        {
            _secondPlayerSelector.SetActive(false);
        }
    }

    public void Click()
    {
        _randomSpriteRenderer.gameObject.SetActive(false);
        _characterMenu.SelectCharacterImage(true);
        GetComponent<Button>().Select();
    }
} 