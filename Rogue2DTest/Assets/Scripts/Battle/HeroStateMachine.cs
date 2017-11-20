using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour {

    private BattleStateMachine BSM;

    public BaseHero hero;

    public enum TurnState
    {
        PROCESSING, //waiting for progress bar to fill
        READY, //add to list, progress bar filled
        WAITING, //idle
        ACTION,
        DEAD
    }


    public TurnState currentState;

    private float currentCoolDown = 0f;
    private float maxCoolDown = 5f;

    public Image progressBar;
    public GameObject selector;
    //Ienumerator
    public GameObject targetToAttack;
    private Vector3 startPosition;
    private bool actionStarted = false;
    private float animationSpeed = 10f;

    // Use this for initialization
    void Start ()
    {
        startPosition = transform.position;
        currentCoolDown = Random.Range(0, 2.5f);
        selector.SetActive(false);
        currentState = TurnState.PROCESSING;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		switch(currentState)
        {
            case (TurnState.PROCESSING):
                FillProgressBar();
                break;

            case (TurnState.READY):
                BSM.heroesToManage.Add(this.gameObject);
                currentState = TurnState.WAITING;
                break;

            case (TurnState.WAITING):
                //idle state
                break;
            case (TurnState.ACTION):
                StartCoroutine(timeForAction());
                break;

            case (TurnState.DEAD):

                break;
        }
	}

    void FillProgressBar()
    {
        currentCoolDown = currentCoolDown + Time.deltaTime;
        float calculateCoolDown = currentCoolDown / maxCoolDown;
        progressBar.transform.localScale = new Vector3(Mathf.Clamp(calculateCoolDown,0,1), progressBar.transform.localScale.y, progressBar.transform.localScale.z);
        if(currentCoolDown >= maxCoolDown)
        {
            currentState = TurnState.READY;
        }
    }

    private IEnumerator timeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        //animate enemy to the hero to be attacked
        Vector3 targetPosition = new Vector3(targetToAttack.transform.position.x + 1.5f, targetToAttack.transform.position.y, targetToAttack.transform.position.z);
        while (MoveTowardsPosition(targetPosition))
        {
            yield return null;
        }
        //wait
        yield return new WaitForSeconds(0.5f);
        //animate attack and do damage

        //animate back to start position
        Vector3 originalPosition = startPosition;
        while (MoveTowardsPosition(originalPosition))
        {
            yield return null;
        }
        //remove from perform list
        BSM.performList.RemoveAt(0);
        //reset battle state machine to waiting
        BSM.battleStates = BattleStateMachine.PerformAction.WAIT;

        actionStarted = false;

        currentCoolDown = 0f;
        currentState = TurnState.PROCESSING;
    }

    private bool MoveTowardsPosition(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animationSpeed * Time.deltaTime));
    }
}
