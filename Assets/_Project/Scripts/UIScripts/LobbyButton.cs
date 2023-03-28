using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyButton : BaseButton
{
    [SerializeField] private TextMeshProUGUI _Idtext = default;


    public void SetData(string id)
    {
        _Idtext.text = id;
    }
}
