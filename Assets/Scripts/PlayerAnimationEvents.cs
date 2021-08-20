using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement = default;


    public void UnlockMovement()
    {
        _playerMovement.SetLockMovement(false);
    }
}
