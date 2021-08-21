using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _lostLives = default;
    [SerializeField] private Slider _healthSlider = default;
    private int _currentLifeIndex;


    public void SetMaxHealth(float value)
    {
        _healthSlider.maxValue = value;
    }

    public void SetHealth(float value)
    {
        _healthSlider.value = value;
    }

    public void SetLives(int lives)
    {
        _lostLives[_currentLifeIndex].SetActive(true);
        _currentLifeIndex++;
    }
}
