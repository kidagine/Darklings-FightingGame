using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class OnlineErrorMenu : BaseMenu
{
    [SerializeField] private TextMeshProUGUI _errorText = default;


    public void Show(string errorText)
    {
        base.Show();
        _errorText.text = Regex.Replace(errorText, "([a-z])([A-Z])", "$1 $2"); ;
    }
}
