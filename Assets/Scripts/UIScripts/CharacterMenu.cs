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


	public void EnablePlayerTwoSelector()
	{
		_playerTwoSelector.gameObject.SetActive(true);
	}

	public void SetCharacterOneImage(bool isPlayerOne)
	{
		if (isPlayerOne)
		{
			_characterOneImage.enabled = true;
			_playerOneName.enabled = true;
			//_playerOneName.text = playerStatsSO.name;
		}
		else
		{
			_characterTwoImage.enabled = true;
			_playerTwoName.enabled = true;
			//_playerTwoName.text = playerStatsSO.name;
		}
	}

	public void SelectCharacterOneImage(bool isPlayerOne)
	{
		if (isPlayerOne)
		{
			_characterOneAnimator.SetBool("IsTaunting", true);
		}
		else
		{
			_characterTwoAnimator.SetBool("IsTaunting", true);
		}
		StartCoroutine(TauntEndCoroutine(isPlayerOne));
	}

	IEnumerator TauntEndCoroutine(bool isPlayerOne)
	{
		yield return new WaitForSeconds(1.25f);
		if (isPlayerOne)
		{
			_playerTwoSelector.gameObject.SetActive(true);
		}
		if (_playerOneSelector.HasSelected && _playerTwoSelector.HasSelected)
		{
			_baseMenu.Show();
			gameObject.SetActive(false);
		}
	}

	public void ResetControllerInput()
	{
		SceneSettings.ControllerOne = "";
		SceneSettings.ControllerTwo = "";
	}

	private void OnDisable()
	{
		_characterTwoImage.enabled = false;
		_playerTwoName.enabled = false;
		_playerOneSelector.transform.localPosition = new Vector2(-180.0f, -190.0f);
		_playerTwoSelector.transform.localPosition = new Vector2(-180.0f, -190.0f);
		_playerTwoSelector.gameObject.SetActive(false);
		_playerOneSelector.HasSelected = false;
		_playerTwoSelector.HasSelected = false;
	}
}
