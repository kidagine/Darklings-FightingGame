using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _lostLives = default;
    [SerializeField] private Slider _healthSlider = default;
    [SerializeField] private TextMeshProUGUI _comboText = default;
    [SerializeField] private Transform _healthDividerPivot = default;
    [SerializeField] private GameObject _healthDividerPrefab = default;
    private int _currentLifeIndex;
    private int _currentComboCount;
    private bool _hasComboEnded;


    public void SetMaxHealth(float value)
    {
        float healthSliderWidth = _healthSlider.GetComponent<RectTransform>().sizeDelta.x;
        _healthSlider.maxValue = value;
        float increaseValue = healthSliderWidth / value;
        float currentPositionX = 0.0f;
        for (int i = 0; i < value + 1; i++)
		{
            GameObject healthDivider = Instantiate(_healthDividerPrefab, _healthDividerPivot);
            healthDivider.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentPositionX, 0.0f);
            currentPositionX -= increaseValue;
		}
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

    public void ResetLives()
    {
        _lostLives[0].SetActive(false);
        _lostLives[1].SetActive(false);
        _currentLifeIndex = 0;
    }

    public void IncreaseCombo()
    {
        if (_hasComboEnded)
        {
            _hasComboEnded = false;
            _comboText.gameObject.SetActive(false);
            _currentComboCount = 0;
            _comboText.text = "Hits 0";
        }
        _currentComboCount++;
        _comboText.text = "Hits " + _currentComboCount.ToString();
        if (_currentComboCount > 1)
        {
            _comboText.gameObject.SetActive(true);
        }
    }

    public void ResetCombo()
    {
        StartCoroutine(ResetComboCoroutine());
    }

    IEnumerator ResetComboCoroutine()
    {
        _hasComboEnded = true;
        yield return new WaitForSeconds(1.0f);
        _comboText.gameObject.SetActive(false);
        _currentComboCount = 0;
        _comboText.text = "Hits 0";
    }
}
