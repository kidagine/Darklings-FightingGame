using UnityEngine;

public class PlayerPreferences : MonoBehaviour
{
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
	[Header("Misc")]
	[SerializeField] private BaseSelector _slowdownSelector = default;
	[SerializeField] private int _slowdownSelectorInitial = default;
	[SerializeField] private BaseSelector _inputSelector = default;
	[SerializeField] private int _inputSelectorInitial = default;


	void Start()
	{
		_healthSelector.SetValue(PlayerPrefs.GetInt("health", _healthSelectorInitial));
		_arcanaSelector.SetValue(PlayerPrefs.GetInt("arcana", _arcanaSelectorInitial));
		_assistSelector.SetValue(PlayerPrefs.GetInt("assist", _assistSelectorInitial));
		_hitboxesSelector.SetValue(PlayerPrefs.GetInt("hitboxes", _hitboxesSelectorInitial));
		_framedataSelector.SetValue(PlayerPrefs.GetInt("framedata", _framedataSelectorInitial));
		_cpuSelector.SetValue(PlayerPrefs.GetInt("cpu", _cpuSelectorInitial));
		_blockSelector.SetValue(PlayerPrefs.GetInt("block", _blockSelectorInitial));
		_slowdownSelector.SetValue(PlayerPrefs.GetInt("slowdown", _slowdownSelectorInitial));
		_inputSelector.SetValue(PlayerPrefs.GetInt("input", _inputSelectorInitial));
	}

	public void SavePreference(string key, int value)
	{
		Debug.Log(key);
		PlayerPrefs.SetInt(key.ToLower(), value);
		PlayerPrefs.Save();
	}
}
