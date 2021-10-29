using UnityEngine;
using UnityEngine.U2D.Animation;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController _characterAnimatorController = default;
	[SerializeField] private PlayerStatsSO _playerStatsSO = default;
	[SerializeField] private bool _isRandomizer = default;

	public RuntimeAnimatorController CharacterAnimatorController { get { return _characterAnimatorController; }  set { } }
	public PlayerStatsSO PlayerStatsSO { get { return _playerStatsSO; } set { } }
	public bool IsRandomizer { get { return _isRandomizer; } private set { } }
}
