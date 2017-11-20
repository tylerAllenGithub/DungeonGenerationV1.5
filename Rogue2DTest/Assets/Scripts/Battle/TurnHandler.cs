using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurnHandler{

    public string attacker; //who is attacking
    public string type; //enemy or player
    public GameObject attackerGameObject;
    public GameObject targetOfAttacker;


    public BaseAttack chosenAttack;
}
