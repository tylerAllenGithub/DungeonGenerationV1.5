using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour {


    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }

    public enum HeroGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        DONE
    }

    public PerformAction battleStates;
    public Transform spacer;
    public List<TurnHandler> performList = new List<TurnHandler>();
    public List<GameObject> playersInBattle = new List<GameObject>();
    public List<GameObject> enemiesInBattle = new List<GameObject>();
    
    public HeroGUI heroInput;
    public GameObject enemyButton;

    public List<GameObject> heroesToManage = new List<GameObject>();
    private TurnHandler heroChoice;

    public GameObject attackPanel;
    public GameObject enemySelectPanel;

	// Use this for initialization
	void Start ()
    {
        attackPanel.SetActive(false);
        enemySelectPanel.SetActive(false);

        battleStates = PerformAction.WAIT;
        enemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        playersInBattle.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        heroInput = HeroGUI.ACTIVATE;

        EnemyButtons();
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch (battleStates)
        {
            case (PerformAction.WAIT):
                if(performList.Count > 0)
                {
                    battleStates = PerformAction.TAKEACTION;
                }
                break;

            case (PerformAction.TAKEACTION):
                GameObject performer = GameObject.Find(performList[0].attacker);
                if(performList[0].type == "Enemy")
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    ESM.targetToAttack = performList[0].targetOfAttacker;
                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                }
                if (performList[0].type == "Hero")
                {
                    HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
                    HSM.targetToAttack = performList[0].targetOfAttacker;
                    HSM.currentState = HeroStateMachine.TurnState.ACTION;
                }
                battleStates = PerformAction.PERFORMACTION;
                break;

            case (PerformAction.PERFORMACTION):

                break;
        }

        switch (heroInput)
        {
            case (HeroGUI.ACTIVATE):
                if (heroesToManage.Count > 0)
                {
                    heroesToManage[0].transform.FindChild("Selector").gameObject.SetActive(true);
                    heroChoice = new TurnHandler();
                    attackPanel.SetActive(true);
                    heroInput = HeroGUI.WAITING;
                }
                break;

            case (HeroGUI.WAITING):
                //idle
                break;

            case (HeroGUI.DONE):
                heroInputDone();
                break;
        }
	}

    public void CollectActions(TurnHandler action)
    {
        performList.Add(action);
    }

    void EnemyButtons()
    {
        foreach(GameObject enemy in enemiesInBattle)
        {
            GameObject newButton = Instantiate(enemyButton) as GameObject;
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();
            EnemyStateMachine currentEnemy = enemy.GetComponent<EnemyStateMachine>();

            Text buttonText = newButton.GetComponentInChildren <Text>();
            buttonText.text = currentEnemy.enemy.name;

            button.enemyObject = enemy;
            newButton.transform.SetParent(spacer,false);
        }
    }

    public void Input1()//attack button
    {
        heroChoice.attacker = heroesToManage[0].name;
        heroChoice.attackerGameObject = heroesToManage[0];
        heroChoice.type = "Hero";

        attackPanel.SetActive(false);
        enemySelectPanel.SetActive(true);
    }

    public void Input2(GameObject enemyChosen)//choose enemy
    {
        heroChoice.targetOfAttacker = enemyChosen;
        heroInput = HeroGUI.DONE;
    }

    void heroInputDone()
    {
        performList.Add(heroChoice);
        enemySelectPanel.SetActive(false);
        heroesToManage[0].transform.FindChild("Selector").gameObject.SetActive(false);
        heroesToManage.RemoveAt(0);
        heroInput = HeroGUI.ACTIVATE;
    }
}
