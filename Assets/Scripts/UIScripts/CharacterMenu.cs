using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : BaseMenu
{
	[SerializeField] private BaseMenu _baseMenu = default;
	[SerializeField] private Image _characterOneImage = default;
	[SerializeField] private Image _characterTwoImage = default;
	[SerializeField] private Animator _characterOneAnimator = default;
	[SerializeField] private Animator _characterTwoAnimator = default;
	[SerializeField] private TextMeshProUGUI _playerOneName = default;
	[SerializeField] private TextMeshProUGUI _playerTwoName = default;
	[SerializeField] private PlayerCharacterSelector _playerOneSelector = default;
	[SerializeField] private PlayerCharacterSelector _playerTwoSelector = default;


	public void SetCharacterOneImage(PlayerStatsSO playerStatsSO)
	{
		if (!_playerTwoSelector.gameObject.activeSelf)
		{
			_characterOneImage.enabled = true;
			_playerOneName.text = playerStatsSO.name;
		}
		else
		{
			_characterTwoImage.enabled = true;
			_playerTwoName.text = playerStatsSO.name;
		}
	}

	public void SetCharacterTwoImage(PlayerStatsSO playerStatsSO)
	{
		_characterTwoImage.enabled = true;
		_playerTwoName.text = playerStatsSO.name;
	}

	public void SelectCharacterOneImage(PlayerStatsSO playerStatsSO)
	{
		if (!_playerTwoSelector.gameObject.activeSelf)
		{
			_characterOneAnimator.SetBool("IsTaunting", true);
			StartCoroutine(TauntEndCoroutine());
		}
		else
		{
			SelectCharacterTwoImage(null);
		}
	}

	public void SelectCharacterTwoImage(PlayerStatsSO playerStatsSO)
	{
		_characterTwoAnimator.SetBool("IsTaunting", true);
		StartCoroutine(TauntEndCoroutine());
	}

	IEnumerator TauntEndCoroutine()
	{
		yield return new WaitForSeconds(2.0f);
		if (_playerTwoSelector.gameObject.activeSelf)
		{
			gameObject.SetActive(false);
		}
		else
		{
			_playerTwoSelector.gameObject.SetActive(true);
		}
		if (_playerOneSelector.HasSelected && _playerTwoSelector.HasSelected)
		{
			_baseMenu.Show();
		}
	}
}
