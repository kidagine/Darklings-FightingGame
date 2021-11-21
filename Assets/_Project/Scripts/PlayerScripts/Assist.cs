using UnityEngine;

public class Assist : MonoBehaviour
{
    [SerializeField] private Animator _animator = default;
    [SerializeField] private AssistStatsSO _assistStatsSO = default;

	public AssistStatsSO AssistStats { get { return _assistStatsSO; } private set { } }


	public void Attack()
    {
        _animator.SetTrigger("Attack");
    }
}
