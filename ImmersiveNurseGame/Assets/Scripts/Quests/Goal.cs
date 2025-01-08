using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool playerInRange;
    public GameObject goalWindow;
    public QuestGiver questGiver;
    public MissionWaypoint missionWaypoint; // Reference to MissionWaypoint

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && questGiver != null && questGiver.CurrentQuest != null && questGiver.CurrentQuest.isActive)
        {
            playerInRange = true;

            if (goalWindow != null)
            {
                goalWindow.SetActive(true);
            }

            // Deactivate the marker when the quest is completed
            if (missionWaypoint != null)
            {
                missionWaypoint.Deactivate();
            }

            if (questGiver.CurrentQuest.completionType == QuestCompletionType.EnterRange)
            {
                questGiver.CompleteQuest();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (goalWindow != null)
            {
                goalWindow.SetActive(false);
            }
        }
    }
}
