using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtParentState : State
{
    public int CalculateDamage(int damage, float defense)
    {
        int calculatedDamage = (int)((DemonicsFloat)damage / (DemonicsFloat)defense);
        return calculatedDamage;
    }
    public int CalculateRecoverableDamage(int damage, float defense)
    {
        int calculatedDamage = (int)((DemonicsFloat)damage / (DemonicsFloat)defense) - 200;
        return calculatedDamage;
    }
}
