using UnityEngine;

public class PlayerComboSystem : MonoBehaviour
{
    public static AttackSO GetComboAttack(PlayerStatsSO playerStats, InputEnum inputEnum, Vector2 direction, bool isAir)
    {
        if (inputEnum == InputEnum.Special)
            return GetArcana(playerStats, direction, isAir);
        if (inputEnum == InputEnum.Throw)
            return playerStats.mThrow;
        if (direction.y == -1 && !isAir)
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

    public static ArcanaSO GetArcana(PlayerStatsSO playerStats, Vector2 direction = default, bool isAir = false, bool frenzied = false)
    {
        if (isAir)
            if (frenzied)
                return playerStats.jArcanaFrenzy;
            else
                return playerStats.jArcana;
        if (direction.y == -1)
        {
            if (frenzied)
                return playerStats.m2ArcanaFrenzy;
            else
                return playerStats.m2Arcana;
        }
        if (direction == Vector2.zero)
        {
            if (frenzied)
                return playerStats.m5ArcanaFrenzy;
            else
                return playerStats.m5Arcana;
        }
        if (direction == Vector2.right)
        {
            if (frenzied)
                return playerStats.m6ArcanaFrenzy;
            else
                return playerStats.m6Arcana;
        }
        return playerStats.m5Arcana;
    }
}
