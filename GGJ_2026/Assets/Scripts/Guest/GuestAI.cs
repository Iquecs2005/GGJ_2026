using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GuestController gc;

    [Header("Wait variables")]
    [SerializeField] private float minTimeBetweenActions;
    [SerializeField] private float maxTimeBetweenActions;

    private void FixedUpdate()
    {
        if (gc.currentAction == GuestActions.Ready) 
        {
            ChooseNextAction();
        }
    }

    public void OnMovementEnd() 
    {
        StartCoroutine(WaitBetweenAmount(minTimeBetweenActions, maxTimeBetweenActions));
    }

    private void ChooseNextAction() 
    {
        gc.gm.MoveToRandomTile();
    }

    private IEnumerator WaitBetweenAmount(float minTime, float maxTime) 
    {
        gc.ChangeCurrentState(GuestActions.Waiting);
        float waitTime = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(waitTime);
        gc.ChangeCurrentState(GuestActions.Ready);
    }
}
