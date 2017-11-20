using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseClass{

    public string theName;

    public float baseHealth;
    public float currentHealth;

    public float baseMP;
    public float currentMP;

    public float attack;
    public float defense;

    public List<BaseAttack> usableAttacks = new List<BaseAttack>();
}
