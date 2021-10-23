using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController _characterAnimatorController = default;
	[SerializeField] private string _characterName = default;
	[SerializeField] private int _characterIndex = default;
	[SerializeField] private bool _isRandomizer = default;

	public RuntimeAnimatorController CharacterAnimatorController { get { return _characterAnimatorController; }  set { } }
	public string CharacterName { get { return _characterName; } set { } }
	public int CharacterIndex { get { return _characterIndex; } set { } }
	public bool IsRandomizer { get { return _isRandomizer; } private set { } }
}
