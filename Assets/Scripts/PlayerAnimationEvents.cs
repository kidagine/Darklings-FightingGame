using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement = default;


    public void UnlockMovement()
    {
        _playerMovement.SetLockMovement(false);
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1.0f;
    }
}
