using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool playerInRange;
    public GameObject goalWindow;
    public QuestGiver questGiver;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && questGiver != null && questGiver.CurrentQuest != null && questGiver.CurrentQuest.isActive)
        {
            playerInRange = true;

            // Show the goal window if the quest is active and not completed
            if (goalWindow != null)
            {
                goalWindow.SetActive(true);
            }

            // Check if the current quest is completed by entering the range
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

            // Hide the goal window when the player exits the range
            if (goalWindow != null)
            {
                goalWindow.SetActive(false);
            }
        }
    }
}