using System.Collections;
using TMPro;
using UnityEditor.Animations;
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

	public void SetCharacterOneImage(bool isPlayerOne, AnimatorController animatorController, string characterName)
	{
		if (isPlayerOne)
		{
			_characterOneImage.enabled = true;
			_playerOneName.text = characterName;
			_characterOneAnimator.runtimeAnimatorController = animatorController;
		}
		else
		{
			if (animatorController.name == "RandomSelectAnimator")
			{
				_characterTwoImage.transform.localScale = Vector2.one;
				_playerTwoName.transform.localScale = Vector2.one;
			}
			else
			{
				_characterTwoImage.transform.localScale = new Vector2(-1.0f, 1.0f);
				_playerTwoName.transform.localScale = new Vector2(-1.0f, 1.0f);
			}
			_characterTwoImage.enabled = true;
			_playerTwoName.text = characterName;
			_characterTwoAnimator.runtimeAnimatorController = animatorController;
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
		_playerOneName.text = "";
		_characterOneImage.enabled = false;
		_characterOneAnimator.runtimeAnimatorController = null;
		_playerTwoName.text = "";
		_characterTwoImage.enabled = false;
		_characterTwoAnimator.runtimeAnimatorController = null;
		_playerOneSelector.transform.localPosition = new Vector2(-180.0f, -190.0f);
		_playerTwoSelector.transform.localPosition = new Vector2(-180.0f, -190.0f);
		_playerTwoSelector.gameObject.SetActive(false);
		_playerOneSelector.HasSelected = false;
		_playerTwoSelector.HasSelected = false;
	}
}
