using System.Collections.Generic;
using UnityEngine;

public class BaseTogglesGroup : MonoBehaviour
{
    private List<BaseToggle> _toggles = new List<BaseToggle>();
    public BaseToggle ActiveToggle { get; set; }

    public void CheckToggles()
    {
        for (int i = 0; i < _toggles.Count; i++)
            _toggles[i].ResetToggle();
    }

    public void AddToggle(BaseToggle toggle) => _toggles.Add(toggle);
}
