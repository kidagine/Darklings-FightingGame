using UnityEngine;
using UnityEngine.U2D.Animation;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController _characterAnimatorController = default;
	[SerializeField] private PlayerStatsSO _playerStatsSO = default;
	[SerializeField] private int _characterIndex = default;
	[SerializeField] private bool _isRandomizer = default;

	public RuntimeAnimatorController CharacterAnimatorController { get { return _characterAnimatorController; }  set { } }
	public PlayerStatsSO PlayerStatsSO { get { return _playerStatsSO; } set { } }
	public int CharacterIndex { get { return _characterIndex; } set { } }
	public bool IsRandomizer { get { return _isRandomizer; } private set { } }
}
