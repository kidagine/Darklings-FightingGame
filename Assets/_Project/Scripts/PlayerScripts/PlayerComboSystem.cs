using UnityEngine;

public class PlayerComboSystem : MonoBehaviour
{
    private Player _player;

    void Awake()
    {
        _player = GetComponent<Player>();
    }

    public AttackSO GetComboAttack(InputEnum inputEnum, bool isCrouching, bool isAir)
    {
        if (inputEnum == InputEnum.Throw)
        {
            return _player.playerStats.mThrow;
        }
        if (isCrouching)
        {
            return GetCrouchingAttackType(inputEnum);
        }
        else
        {
            if (isAir)
            {
                return GetJumpAttackType(inputEnum);
            }
            else
            {
                return GetStandingAttackType(inputEnum);
            }
        }
    }

    private AttackSO GetJumpAttackType(InputEnum inputEnum)
    {
        if (inputEnum == InputEnum.Light)
        {
            return _player.playerStats.jL;
        }
        else if (inputEnum == InputEnum.Medium)
        {
            return _player.playerStats.jM;
        }
        else
        {
            return _player.playerStats.jH;
        }
    }

    private AttackSO GetCrouchingAttackType(InputEnum inputEnum)
    {
        if (inputEnum == InputEnum.Light)
        {
            return _player.playerStats.m2L;
        }
        else if (inputEnum == InputEnum.Medium)
        {
            return _player.playerStats.m2M;
        }
        else
        {
            return _player.playerStats.m2H;
        }
    }

    private AttackSO GetStandingAttackType(InputEnum inputEnum)
    {
        if (inputEnum == InputEnum.Light)
        {
            return _player.playerStats.m5L;
        }
        else if (inputEnum == InputEnum.Medium)
        {
            return _player.playerStats.m5M;
        }
        else
        {
            return _player.playerStats.m5H;
        }
    }

    public AttackSO GetThrow()
    {
        return _player.playerStats.mThrow;
    }

    public ArcanaSO GetArcana(bool isCrouching = false, bool isAir = false)
    {
        if (isAir)
        {
            return _player.playerStats.jArcana;
        }
        if (isCrouching)
        {
            return _player.playerStats.m2Arcana;
        }
        return _player.playerStats.m5Arcana;
    }
}
