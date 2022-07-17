using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReplayCard : MonoBehaviour
{
	[SerializeField] private Image _playerOneImage = default;
	[SerializeField] private Image _playerTwoImage = default;
	[SerializeField] private TextMeshProUGUI _versionText = default;
	[SerializeField] private Sprite[] _characterPortraits = default;
	public ReplayCardData ReplayCardData { get; private set; }


	public void SetData(ReplayCardData replayData)
	{
		ReplayCardData = replayData;
		_playerOneImage.sprite = GetCharacterPortrait(replayData.characterOne);
		_playerTwoImage.sprite = GetCharacterPortrait(replayData.characterTwo);
		_versionText.text = $"Ver {replayData.versionNumber}";
	}

	private Sprite GetCharacterPortrait(int index)
	{
		for (int i = 0; i < _characterPortraits.Length; i++)
		{
			return _characterPortraits[index];
		}
		return null;
	}
}

public struct ReplayCardData
{
	public string versionNumber;
	public int characterOne;
	public int colorOne;
	public int assistOne;
	public int characterTwo;
	public int colorTwo;
	public int assistTwo;
	public int stage;
	public string musicName;
	public bool bit1;
	public string[] playerOneInputs;
	public string[] playerTwoInputs;
	public int skip;
}
