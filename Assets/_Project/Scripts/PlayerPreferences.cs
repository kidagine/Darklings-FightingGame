using System;
using TMPro;
using UnityEngine;

public class PlayerPreferences : MonoBehaviour
{
	[SerializeField] private bool _checkOnline = default;
	[SerializeField] private bool _checkOptions = default;
	[SerializeField] private bool _checkTraining = default;
	[Header("ONLINE")]
	[SerializeField] private string _playerNameInputFieldInitial = "Demon";
	[SerializeField] private TMP_InputField _playerNameInputField = default;
	[SerializeField] private BaseSelector _characterSelector = default;
	[SerializeField] private int _characterSelectorInitial = default;
	[SerializeField] private BaseSelector _charactersAssistSelector = default;
	[SerializeField] private int _characterAssistSelectorInitial = default;
	[SerializeField] private BaseSelector _characterColorSelector = default;
	[SerializeField] private int _characterColorSelectorInitial = default;
	[Header("OPTIONS")]
	[Header("Audio")]
	[SerializeField] private BaseSelector _vfxSelector = default;
	[Range(0, 10)]
	[SerializeField] private int _vfxSelectorInitial = default;
	[SerializeField] private BaseSelector _uiSelector = default;
	[Range(0, 10)]
	[SerializeField] private int _uiSelectorInitial = default;
	[SerializeField] private BaseSelector _musicSelector = default;
	[Range(0, 10)]
	[SerializeField] private int _musicSelectorInitial = default;
	[Header("TRAINING")]
	[Header("General")]
	[SerializeField] private BaseSelector _healthSelector = default;
	[SerializeField] private int _healthSelectorInitial = default;
	[SerializeField] private BaseSelector _arcanaSelector = default;
	[SerializeField] private int _arcanaSelectorInitial = default;
	[SerializeField] private BaseSelector _assistSelector = default;
	[SerializeField] private int _assistSelectorInitial = default;
	[SerializeField] private BaseSelector _hitboxesSelector = default;
	[SerializeField] private int _hitboxesSelectorInitial = default;
	[SerializeField] private BaseSelector _framedataSelector = default;
	[SerializeField] private int _framedataSelectorInitial = default;
	[Header("CPU")]
	[SerializeField] private BaseSelector _cpuSelector = default;
	[SerializeField] private int _cpuSelectorInitial = default;
	[SerializeField] private BaseSelector _blockSelector = default;
	[SerializeField] private int _blockSelectorInitial = default;
	[SerializeField] private BaseSelector _onHitSelector = default;
	[SerializeField] private int _onHitSelectorInitial = default;
	[Header("Misc")]
	[SerializeField] private BaseSelector _slowdownSelector = default;
	[SerializeField] private int _slowdownSelectorInitial = default;
	[SerializeField] private BaseSelector _inputSelector = default;
	[SerializeField] private int _inputSelectorInitial = default;


	void Start()
	{
		if (_checkOnline)
		{
			LoadOnlinePreferences();
		}
		if (_checkOptions)
		{
			LoadOptionPreferences();
		}
		if (_checkTraining)
		{
			if (GameManager.Instance.IsTrainingMode)
			{
				LoadTrainingPreferences();
			}
		}
	}

	private void LoadOnlinePreferences()
	{
		_playerNameInputField.text = PlayerPrefs.GetString("playerName", _playerNameInputFieldInitial);
		_characterSelector.SetValue(PlayerPrefs.GetInt("character", _characterSelectorInitial));
		_charactersAssistSelector.SetValue(PlayerPrefs.GetInt("characterAssist", _characterAssistSelectorInitial));
		_characterColorSelector.SetValue(PlayerPrefs.GetInt("characterColor", _characterColorSelectorInitial));
	}

	private void LoadOptionPreferences()
	{
		_vfxSelector.SetValue(PlayerPrefs.GetInt("vfx", _vfxSelectorInitial));
		_uiSelector.SetValue(PlayerPrefs.GetInt("ui", _uiSelectorInitial));
		_musicSelector.SetValue(PlayerPrefs.GetInt("music", _musicSelectorInitial));
	}

	private void LoadTrainingPreferences()
	{
		//General
		_healthSelector.SetValue(PlayerPrefs.GetInt("health", _healthSelectorInitial));
		_arcanaSelector.SetValue(PlayerPrefs.GetInt("arcana", _arcanaSelectorInitial));
		_assistSelector.SetValue(PlayerPrefs.GetInt("assist", _assistSelectorInitial));

		//Cpu
		_cpuSelector.SetValue(PlayerPrefs.GetInt("cpu", _cpuSelectorInitial));
		_blockSelector.SetValue(PlayerPrefs.GetInt("block", _blockSelectorInitial));
		_onHitSelector.SetValue(PlayerPrefs.GetInt("onHit", _onHitSelectorInitial));

		//Misc
		_hitboxesSelector.SetValue(PlayerPrefs.GetInt("hitboxes", _hitboxesSelectorInitial));
		_framedataSelector.SetValue(PlayerPrefs.GetInt("framedata", _framedataSelectorInitial));
		_slowdownSelector.SetValue(PlayerPrefs.GetInt("slowdown", _slowdownSelectorInitial));
		_inputSelector.SetValue(PlayerPrefs.GetInt("input", _inputSelectorInitial));
	}

	public void SavePreference(string key, int value)
	{
		PlayerPrefs.SetInt(char.ToLowerInvariant(key[0]) + key.Substring(1), value);
		PlayerPrefs.Save();
	}

	public void RestoreToDefault()
	{
		PlayerPrefs.DeleteAll();
		LoadTrainingPreferences();
	}
}
