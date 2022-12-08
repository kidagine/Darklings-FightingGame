using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DemonNameplate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _readyText = default;
    [SerializeField] private TextMeshProUGUI _demonNameText = default;
    [SerializeField] private TextMeshProUGUI _assistText = default;
    [SerializeField] private Image _characterImage = default;
    [SerializeField] private PlayerStatsSO[] _playersStatsSo = default;
    private bool _ready;

    public void SetDemonData(DemonData demonData)
    {
        if (gameObject != null)
        {
            gameObject.SetActive(true);
            _demonNameText.text = demonData.demonName;
            _assistText.text = $"Assist {demonData.assist}";
            _characterImage.sprite = _playersStatsSo[demonData.character].portraits[0];
        }
    }

    public bool ToggleReady()
    {
        _ready = !_ready;
        if (_ready)
        {
            _readyText.text = "Ready";
        }
        else
        {
            _readyText.text = "Unready";
        }
        return _ready;
    }
    public void SetReady(bool ready)
    {
        _ready = ready;
        if (_ready)
        {
            _readyText.text = "Ready";
        }
        else
        {
            _readyText.text = "Unready";
        }
    }
}
