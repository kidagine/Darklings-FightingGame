using UnityEngine;

public class RightClickEditor : MonoBehaviour
{
    [SerializeField] private GameObject _rightClickWindow = default;
    [SerializeField] private GameObject _canvas = default;
    [SerializeField] private GameObject _hitboxPrefab = default;
    [SerializeField] private GameObject _hurtboxPrefab = default;
    [SerializeField] private Transform _hitboxGroup = default;
    [SerializeField] private Transform _hurtboxGroup = default;


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_canvas.activeSelf)
            {
                _canvas.SetActive(false);
            }
            else
            {
                _canvas.SetActive(true);
                _rightClickWindow.transform.position = Input.mousePosition;
            }
        }
    }

    public void CreateHurtbox()
    {
        Instantiate(_hurtboxPrefab, _hurtboxGroup);
    }

    public void CreateHitbox()
    {
        Instantiate(_hitboxPrefab, _hitboxGroup);
    }
}
