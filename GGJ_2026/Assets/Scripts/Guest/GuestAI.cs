using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private List<InteractionObject> availableActivities;

    private InteractionObject currentActivity;
    private InteractionObject lastAttemptedActivity;
    private bool movingToActivity;

    private void FixedUpdate()
    {
        if (gc.currentAction == GuestActions.Ready) 
        {
            ChooseNextAction();
        }
    }

    public void GenerateActivityList()
    {
        availableActivities = MapController.instance.GetInteractionObjects();

        gc.gtm.RemoveConflictingInteractions(ref availableActivities);
    }

    public void OnMovementEnd() 
    {
        if (movingToActivity)
        {
            PerformActivity();
            return;
        }
        lastAttemptedActivity = null;
        StartCoroutine(WaitBetweenAmount(minTimeBetweenActions, maxTimeBetweenActions));
    }

    private void ChooseNextAction() 
    {
        float chance = Random.Range(0, 100);

        if (chance < ActivityChance)
        {
            MoveToActivity();
        }
        else 
        {
            gc.gm.MoveToRandomTile();
        }
    }

    private void MoveToActivity() 
    {
        List<InteractionObject> currentAvaibleActivities = new List<InteractionObject>(availableActivities);

        if (lastAttemptedActivity != null)
            currentAvaibleActivities.Remove(lastAttemptedActivity);

        int activityCount = currentAvaibleActivities.Count;

        if (activityCount == 0)
        {
            gc.gm.MoveToRandomTile();
            return;
        }

        int index = Random.Range(0, currentAvaibleActivities.Count);

        movingToActivity = true;
        currentActivity = currentAvaibleActivities[index];

        gc.gm.MoveToTile(currentActivity.interactionTile);

        gc.onNearMovementEnd.AddListener(CheckActivityAvailability);
    }

    public void CheckActivityAvailability()
    {
        lastAttemptedActivity = currentActivity;

        if (currentActivity.IsOccupied())
        {
            movingToActivity = false;
            gc.ChangeCurrentState(GuestActions.Ready);
        }
        else 
        {
            currentActivity.SetOccupied(gc);
        }

        gc.onNearMovementEnd.RemoveListener(CheckActivityAvailability);
    }

    private void PerformActivity() 
    {
        movingToActivity = false;

        if (currentActivity.Activate(gc)) 
        {
            gc.ChangeCurrentState(GuestActions.Acting);
        }
        else 
        {
            gc.ChangeCurrentState(GuestActions.Ready);
        }
    }

    public void OnActivityEnd() 
    {
        gc.ChangeCurrentState(GuestActions.Ready);
    }

    private IEnumerator WaitBetweenAmount(float minTime, float maxTime) 
    {
        gc.ChangeCurrentState(GuestActions.Waiting);
        float waitTime = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(waitTime);
        gc.ChangeCurrentState(GuestActions.Ready);
    }
}
