using UnityEditor.Animations;
using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private AnimatorController _characterAnimatorController = default;
	[SerializeField] private string _characterName = default;

	public AnimatorController CharacterAnimatorController { get { return _characterAnimatorController; }  set { } }
	public string CharacterName { get { return _characterName; } set { } }
}
