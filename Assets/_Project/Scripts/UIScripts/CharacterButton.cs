using Demonics.Sounds;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private RuntimeAnimatorController _characterAnimatorController = default;
	[SerializeField] private PlayerStatsSO _playerStatsSO = default;
    [SerializeField] private CharacterMenu _characterMenu = default;
    [SerializeField] private GameObject _randomSpriteRenderer = default;
    [SerializeField] private GameObject _firstPlayerSelector = default;
    [SerializeField] private GameObject _secondPlayerSelector = default;
	[SerializeField] private bool _isRandomizer = default;
    private Audio _audio;

	public RuntimeAnimatorController CharacterAnimatorController { get { return _characterAnimatorController; }  set { } }
	public PlayerStatsSO PlayerStatsSO { get { return _playerStatsSO; } set { } }
	public bool IsRandomizer { get { return _isRandomizer; } private set { } }

    void Start()
    {
        _audio = GetComponent<Audio>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        _audio.Sound("Selected").Play();
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
        _audio.Sound("Pressed").Play();
        _randomSpriteRenderer.gameObject.SetActive(false);
        _characterMenu.SelectCharacterImage();
    }

	private void OnDisable()
    {
        _firstPlayerSelector.SetActive(false);
        _secondPlayerSelector.SetActive(false);
    }
} 