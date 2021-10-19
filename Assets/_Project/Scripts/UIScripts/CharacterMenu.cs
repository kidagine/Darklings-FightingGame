using Demonics.UI;
using System.Collections;
using TMPro;
using UnityEngine;

public class CharacterMenu : BaseMenu
{
	[SerializeField] private BaseMenu _baseMenu = default;
	[SerializeField] private SpriteRenderer _characterOneImage = default;
	[SerializeField] private SpriteRenderer _characterTwoImage = default;
	[SerializeField] private GameObject _colorsOne = default;
	[SerializeField] private GameObject _colorsTwo = default;
	[SerializeField] private Animator _characterOneAnimator = default;
	[SerializeField] private Animator _characterTwoAnimator = default;
	[SerializeField] private TextMeshProUGUI _playerOneName = default;
	[SerializeField] private TextMeshProUGUI _playerTwoName = default;
	[SerializeField] private PlayerCharacterSelector _playerOneSelector = default;
	[SerializeField] private PlayerCharacterSelector _playerTwoSelector = default;
	private bool _isPlayerTwoEnabled;


	public void EnablePlayerTwoSelector()
	{
		_isPlayerTwoEnabled = true;
		_playerTwoSelector.gameObject.SetActive(true);
	}

	public void SetCharacterOneImage(bool isPlayerOne, RuntimeAnimatorController animatorController, string characterName)
	{
		if (isPlayerOne)
		{
			_playerOneName.enabled = true;
			if (animatorController.name == "RandomSelectAnimator")
			{
				_characterTwoImage.flipX = false;
				_playerOneName.enabled = false;
			}
			_characterOneImage.enabled = true;
			_playerOneName.text = characterName;
			_characterOneAnimator.runtimeAnimatorController = animatorController;
		}
		else
		{
			_playerTwoName.enabled = true;
			if (animatorController.name == "RandomSelectAnimator")
			{
				_characterTwoImage.flipX = false;
				_playerTwoName.enabled = false;
			}
			else
			{
				_characterTwoImage.flipX = true;
			}
			_characterTwoImage.enabled = true;
			_playerTwoName.text = characterName;
			_characterTwoAnimator.runtimeAnimatorController = animatorController;
		}
	}

	public void SelectCharacterOneImage(bool isPlayerOne)
	{
		_playerOneName.enabled = true;
		_playerTwoName.enabled = true;
		if (isPlayerOne)
		{
			_colorsOne.SetActive(true);
			_playerOneSelector.HasSelected = true;
			_playerOneSelector.gameObject.SetActive(false);
		}
		else
		{
			_colorsTwo.SetActive(true);
			_playerTwoSelector.HasSelected = true;
			_playerTwoSelector.gameObject.SetActive(false);
		}
	}

	public void SetCharacter(bool isPlayerOne)
	{
		if (isPlayerOne)
		{
			_characterOneAnimator.SetTrigger("Taunt");
		}
		else
		{
			_characterTwoAnimator.SetTrigger("Taunt");
		}
		StartCoroutine(TauntEndCoroutine(isPlayerOne));
	}

	IEnumerator TauntEndCoroutine(bool isPlayerOne)
	{
		yield return new WaitForSeconds(1.25f);
		if (isPlayerOne)
		{
			_playerOneSelector.HasSelected = true;
			_playerTwoSelector.gameObject.SetActive(true);
		}
		else
		{
			_playerTwoSelector.HasSelected = true;
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
		_isPlayerTwoEnabled = false;
		_playerTwoSelector.gameObject.SetActive(false);
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
		_playerOneSelector.HasSelected = false;
		_playerTwoSelector.HasSelected = false;
		_playerOneSelector.ResetCanGoPositions();
		_playerTwoSelector.ResetCanGoPositions();
		if (!_isPlayerTwoEnabled)
		{
			_playerTwoSelector.gameObject.SetActive(false);
		}
	}
}
