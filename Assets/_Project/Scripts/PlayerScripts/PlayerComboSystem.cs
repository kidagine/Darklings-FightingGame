using UnityEngine;

public class PlayerComboSystem : MonoBehaviour
{
    public static AttackSO GetComboAttack(PlayerStatsSO playerStats, InputEnum inputEnum, bool isCrouching, bool isAir)
    {
        if (inputEnum == InputEnum.Special)
            return GetArcana(playerStats, isCrouching, isAir);
        if (inputEnum == InputEnum.Throw)
            return playerStats.mThrow;
        if (isCrouching)
            return GetCrouchingAttackType(playerStats, inputEnum);
        else
            if (isAir)
            return GetJumpAttackType(playerStats, inputEnum);
        else
            return GetStandingAttackType(playerStats, inputEnum);
    }

    private static AttackSO GetJumpAttackType(PlayerStatsSO playerStats, InputEnum inputEnum)
    {
        if (inputEnum == InputEnum.Light)
            return playerStats.jL;
        else if (inputEnum == InputEnum.Medium)
            return playerStats.jM;
        else
            return playerStats.jH;
    }

    private static AttackSO GetCrouchingAttackType(PlayerStatsSO playerStats, InputEnum inputEnum)
    {
        if (inputEnum == InputEnum.Light)
            return playerStats.m2L;
        else if (inputEnum == InputEnum.Medium)
            return playerStats.m2M;
        else if (inputEnum == InputEnum.Heavy)
            return playerStats.m2H;
        return null;
    }

    private static AttackSO GetStandingAttackType(PlayerStatsSO playerStats, InputEnum inputEnum)
    {
        if (inputEnum == InputEnum.Light)
            return playerStats.m5L;
        else if (inputEnum == InputEnum.Medium)
            return playerStats.m5M;
        else if (inputEnum == InputEnum.Heavy)
            return playerStats.m5H;
        return null;
    }

    public static AttackSO GetThrow(PlayerStatsSO playerStats) => playerStats.mThrow;

    public static AttackSO GetRedFrenzy(PlayerStatsSO playerStats) => playerStats.mRedFrenzy;

    public static ArcanaSO GetArcana(PlayerStatsSO playerStats, bool isCrouching = false, bool isAir = false)
    {
        if (isAir)
            return playerStats.jArcana;
        if (isCrouching)
            return playerStats.m2Arcana;
        return playerStats.m5Arcana;
    }
}
