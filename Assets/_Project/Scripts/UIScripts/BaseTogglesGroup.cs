using System.Collections.Generic;
using UnityEngine;

public class BaseTogglesGroup : MonoBehaviour
{
    private List<BaseToggle> _toggles = new();
    public BaseToggle ActiveToggle { get; set; }

    public void CheckToggles()
    {
        if (_toggles.Count > 0)
            for (int i = 0; i < _toggles.Count; i++)
                _toggles[i].ResetToggle();
    }

    public void AddToggle(BaseToggle toggle) => _toggles.Add(toggle);
}
