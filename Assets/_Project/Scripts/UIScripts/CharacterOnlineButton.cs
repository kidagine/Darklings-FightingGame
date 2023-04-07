using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterOnlineButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private PlayerStatsSO _playerStatsSO = default;
    [SerializeField] private OnlineSetupMenu _onlineSetupMenu = default;
    [SerializeField] private GameObject _playerSelector = default;
    [SerializeField] private GameObject _backgroundImage = default;
    private Audio _audio;

    public PlayerStatsSO PlayerStatsSO { get { return _playerStatsSO; } set { } }

    void Awake()
    {
        _audio = GetComponent<Audio>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        _audio.Sound("Selected").Play();
        _playerSelector.SetActive(true);
        _onlineSetupMenu.SetCharacterImage(PlayerStatsSO);
    }

    public void Activate()
    {
        _onlineSetupMenu.SetCharacterImage(PlayerStatsSO);
        _backgroundImage.SetActive(true);
    }

    public void Deactivate()
    {
        _backgroundImage.SetActive(false);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _playerSelector.SetActive(false);
    }

    public void Click()
    {
        _audio.Sound("Pressed").Play();
        _onlineSetupMenu.SelectCharacterImage(PlayerStatsSO);
        _backgroundImage.SetActive(true);
    }

    private void OnDisable()
    {
        _playerSelector.SetActive(false);
    }
}
