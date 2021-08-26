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


	public void SetCharacterOneImage(PlayerStatsSO playerStatsSO)
	{
		_characterOneImage.enabled = true;
		_playerOneName.text = playerStatsSO.name;
	}

	public void SetCharacterTwoImage(PlayerStatsSO playerStatsSO)
	{
		_characterTwoImage.enabled = true;
		_playerTwoName.text = playerStatsSO.name;
	}

	public void SelectCharacterOneImage(PlayerStatsSO playerStatsSO)
	{
		_characterOneAnimator.SetBool("IsTaunting", true);
		StartCoroutine(TauntEndCoroutine());
	}

	public void SelectCharacterTwoImage(PlayerStatsSO playerStatsSO)
	{
		_characterTwoAnimator.SetBool("IsTaunting", true);
	}

	IEnumerator TauntEndCoroutine()
	{
		yield return new WaitForSeconds(2.0f);
		_baseMenu.Show();
		gameObject.SetActive(false);
	}
}
