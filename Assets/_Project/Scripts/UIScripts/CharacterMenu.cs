using Demonics.UI;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class CharacterMenu : BaseMenu
{
	[SerializeField] private BaseMenu _baseMenu = default;
	[SerializeField] private SpriteRenderer _characterOneImage = default;
	[SerializeField] private SpriteRenderer _characterTwoImage = default;
	[SerializeField] private GameObject _colorsOne = default;
	[SerializeField] private GameObject _colorsTwo = default;
	[SerializeField] private Animator _characterOneAnimator = default;
	[SerializeField] private Animator _characterTwoAnimator = default;
	[SerializeField] private PlayerAnimator _playerAnimatorOne = default;
	[SerializeField] private PlayerAnimator _playerAnimatorTwo = default;
	[SerializeField] private SpriteLibrary _spriteLibraryOne = default;
	[SerializeField] private SpriteLibrary _spriteLibraryTwo = default;
	[SerializeField] private TextMeshProUGUI _playerOneName = default;
	[SerializeField] private TextMeshProUGUI _playerTwoName = default;
	[SerializeField] private PlayerCharacterSelector _playerOneSelector = default;
	[SerializeField] private PlayerCharacterSelector _playerTwoSelector = default;
	[SerializeField] private TextMeshProUGUI _hpTextOne = default;
	[SerializeField] private TextMeshProUGUI _arcanaTextOne = default;
	[SerializeField] private TextMeshProUGUI _speedTextOne = default;
	[SerializeField] private TextMeshProUGUI _hpTextTwo = default;
	[SerializeField] private TextMeshProUGUI _arcanaTextTwo = default;
	[SerializeField] private TextMeshProUGUI _speedTextTwo = default;
	[SerializeField] private PlayerStatsSO[] _playerStatsArray = default;
	private PlayerStatsSO _playerStats;
	private bool _isPlayerTwoEnabled;


	public void EnablePlayerTwoSelector()
	{
		_isPlayerTwoEnabled = true;
		_playerTwoSelector.gameObject.SetActive(true);
	}

	public void SetCharacterOneImage(bool isPlayerOne, RuntimeAnimatorController animatorController, PlayerStatsSO playerStats, bool isRandomizer)
	{
		_playerStats = playerStats;
		if (isPlayerOne)
		{
			_playerOneName.enabled = true;
			if (animatorController.name == "RandomSelectAnimator")
			{
				_characterTwoImage.flipX = false;
				_playerOneName.enabled = false;
			}
			_characterOneImage.enabled = true;
			if (!isRandomizer)
			{
				_playerOneName.text = playerStats.characterName;
				_spriteLibraryOne.spriteLibraryAsset = playerStats.spriteLibraryAssets[0];
				_playerAnimatorOne.PlayerStats.PlayerStatsSO = playerStats;
			}
			else
			{
				_playerOneName.text = "Random";
			}
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
			if (!isRandomizer)
			{
				_playerTwoName.text = playerStats.characterName;
				_spriteLibraryTwo.spriteLibraryAsset = playerStats.spriteLibraryAssets[0];
				_playerAnimatorTwo.PlayerStats.PlayerStatsSO = playerStats;
			}
			else
			{
				_playerTwoName.text = "Random";
			}
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
			if (_playerStats == null)
			{
				int randomPlayer = Random.Range(0, _playerStatsArray.Length);
				_playerStats = _playerStatsArray[randomPlayer];
				_playerOneName.text = _playerStats.characterName;
				_spriteLibraryOne.spriteLibraryAsset = _playerStats.spriteLibraryAssets[0];
				_playerAnimatorOne.PlayerStats.PlayerStatsSO = _playerStats;
				_characterOneAnimator.runtimeAnimatorController = _playerStats.runtimeAnimatorController;
			}
			_hpTextOne.text = $"HP {_playerStats.maxHealth}";
			_arcanaTextOne.text = $"ARCANA {_playerStats.maxArcana}";
			_speedTextOne.text = $"SPEED {_playerStats.runSpeed}";
			SceneSettings.PlayerOne = _playerStats.characterIndex;
		}
		else
		{
			_colorsTwo.SetActive(true);
			_playerTwoSelector.HasSelected = true;
			if (_playerStats == null)
			{
				int randomPlayer = Random.Range(0, _playerStatsArray.Length);
				_playerStats = _playerStatsArray[randomPlayer];
				_playerTwoName.text = _playerStats.characterName;
				_spriteLibraryTwo.spriteLibraryAsset = _playerStats.spriteLibraryAssets[0];
				_playerAnimatorTwo.PlayerStats.PlayerStatsSO = _playerStats;
				_characterTwoAnimator.runtimeAnimatorController = _playerStats.runtimeAnimatorController;
			}
			_hpTextTwo.text = $"HP {_playerStats.maxHealth}";
			_arcanaTextTwo.text = $"ARCANA {_playerStats.maxArcana}";
			_speedTextTwo.text = $"SPEED {_playerStats.runSpeed}";
			SceneSettings.PlayerTwo = _playerStats.characterIndex;
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
		SceneSettings.ControllerOne = "Cpu";
		SceneSettings.ControllerTwo = "Cpu";
		_isPlayerTwoEnabled = false;
		_playerTwoSelector.gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		_hpTextOne.text = "";
		_arcanaTextOne.text = "";
		_speedTextOne.text = "";
		_hpTextTwo.text = "";
		_arcanaTextTwo.text = "";
		_speedTextTwo.text = "";
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
