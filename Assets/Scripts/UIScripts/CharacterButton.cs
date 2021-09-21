using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController _characterAnimatorController = default;
	[SerializeField] private string _characterName = default;

	public RuntimeAnimatorController CharacterAnimatorController { get { return _characterAnimatorController; }  set { } }
	public string CharacterName { get { return _characterName; } set { } }
}
