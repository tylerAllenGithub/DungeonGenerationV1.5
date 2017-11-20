using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour {

    public GameObject enemyObject;

    public void SelectEnemy()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input2(enemyObject);
        enemyObject.transform.Find("Selector").gameObject.SetActive(false);
    }

    public void HideSelector()
    {
        enemyObject.transform.Find("Selector").gameObject.SetActive(false);

    }

    public void ShowSelector()
    {
        enemyObject.transform.Find("Selector").gameObject.SetActive(true);
    }

}
