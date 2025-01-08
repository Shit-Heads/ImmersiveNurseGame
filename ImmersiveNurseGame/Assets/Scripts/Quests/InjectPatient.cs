using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectPatient : MonoBehaviour
{
    public bool playerInRange;
    public GameObject goalWindow;
    public QuestGiver questGiver;
    public MissionWaypoint missionWaypoint; // Reference to MissionWaypoint
    [SerializeField] private LeaderboardManager lm;

    private bool isInjected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && questGiver != null && questGiver.CurrentQuest != null && questGiver.CurrentQuest.isActive)
        {
            playerInRange = true;
            // Deactivate the marker when the quest is completed
            if (missionWaypoint != null)
            {
                missionWaypoint.Deactivate();
            }
        }
    }

    void Update(){
        if (questGiver.CurrentQuest.completionType == QuestCompletionType.EnterRange && isInjected)
        {
            questGiver.CompleteQuest();
        }

        if(isInjected){
            lm.showLeaderboard();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            /*
            playerInRange = false;

            if (goalWindow != null)
            {
                goalWindow.SetActive(false);
            }
            */
        }
    }

    public void InjectPlayer(){
        if(!isInjected){
            isInjected = true;
        }
    }
}
