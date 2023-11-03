using System.Collections.Generic;
using UnityEngine;

public class BaseTogglesGroup : MonoBehaviour
{
    [SerializeField] private BaseToggle _initialActiveToggle = default;
    [SerializeField] private List<BaseToggle> _toggles = default;
    public BaseToggle ActiveToggle { get; set; }
    public bool LockPage { get; set; }

    private void Awake() => ActiveToggle = _initialActiveToggle;

    public void CheckToggles()
    {
        if (_toggles.Count > 0)
            for (int i = 0; i < _toggles.Count; i++)
                _toggles[i].ResetToggle();
    }

    public void CheckHover(BaseToggle baseToggle)
    {
        if (_toggles.Count > 0)
            for (int i = 0; i < _toggles.Count; i++)
                if (_toggles[i] != baseToggle)
                    _toggles[i].ResetHover();
    }

    public void AddToggle(BaseToggle toggle) => _toggles.Add(toggle);

    public void NextPage()
    {
        if (LockPage)
            return;
        int index = _toggles.FindIndex(a => a == ActiveToggle) + 1;
        if (index > _toggles.Count - 1)
            index = 0;
        _toggles[index].Activate();
    }

    public void PreviousPage()
    {
        if (LockPage)
            return;
        int index = _toggles.FindIndex(a => a == ActiveToggle) - 1;
        if (index < 0)
            index = _toggles.Count - 1;
        _toggles[index].Activate();
    }
}
