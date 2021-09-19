using UnityEditor.Animations;
using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private AnimatorController _characterAnimatorController = default;

	public AnimatorController CharacterAnimatorController { get { return _characterAnimatorController; }  set { } }
}
