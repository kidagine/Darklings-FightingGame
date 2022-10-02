using UnityEngine;

public class RightClickEditor : MonoBehaviour
{
    [SerializeField] private CharacterEditor _characterEditor = default;
    [SerializeField] private GameObject _rightClickWindow = default;
    [SerializeField] private GameObject _canvas = default;
    private AnimationSO[] _animations;


    void Update()
    {
        if (Input.GetMouseButtonUp(1))
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
        _characterEditor.CreateHurtbox();
        _canvas.SetActive(false);
    }

    public void CreateHitbox()
    {
        _characterEditor.CreateHitbox();
        _canvas.SetActive(false);
    }
}
