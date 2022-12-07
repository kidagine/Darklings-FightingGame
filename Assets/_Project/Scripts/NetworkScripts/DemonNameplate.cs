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

    public void SetDemonData(DemonData demonData)
    {
        gameObject.SetActive(true);
        _demonNameText.text = demonData.demonName;
        _assistText.text = $"Assist {demonData.assist}";
        _characterImage.sprite = _playersStatsSo[demonData.character].portraits[0];
    }

    public void SetReady(bool state)
    {
        if (state)
        {
            _readyText.text = "Ready";
        }
        else
        {
            _readyText.text = "Unready";
        }
    }
}
