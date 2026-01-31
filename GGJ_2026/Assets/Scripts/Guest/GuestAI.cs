using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuestAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GuestController gc;

    [Header("Activity variables")]
    [SerializeField] private float ActivityChance;

    [Header("Wait variables")]
    [SerializeField] private float minTimeBetweenActions;
    [SerializeField] private float maxTimeBetweenActions;

    private InteractionObject[] availableActivities;

    private void Start()
    {
        availableActivities = MapController.instance.GetInteractionObjects();
    }

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
        float chance = Random.Range(0, 100);

        if (chance < ActivityChance)
        {
            PerformActivity();
        }
        else 
        {
            gc.gm.MoveToRandomTile();
        }
    }

    private void PerformActivity() 
    {
        int index = Random.Range(0, availableActivities.Count());

        print("Alo " + availableActivities[index].interactionTile.ToString());
        gc.gm.MoveToTile(availableActivities[index].interactionTile);
    }

    private IEnumerator WaitBetweenAmount(float minTime, float maxTime) 
    {
        gc.ChangeCurrentState(GuestActions.Waiting);
        float waitTime = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(waitTime);
        gc.ChangeCurrentState(GuestActions.Ready);
    }
}
