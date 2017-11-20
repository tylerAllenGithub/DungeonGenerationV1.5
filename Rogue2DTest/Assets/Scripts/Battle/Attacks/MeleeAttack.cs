using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : BaseAttack
{
    public MeleeAttack()
    {
        attackName = "Melee";
        attackDamage = 0f;//base damage
        mpCost = 0f;
    }
}
