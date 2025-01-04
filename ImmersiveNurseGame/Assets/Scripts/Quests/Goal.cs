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
        if (other.CompareTag("Player") && questGiver.CurrentQuest.isActive)
        {
            playerInRange = true;
            goalWindow.SetActive(true);

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
            goalWindow.SetActive(false);
        }
    }
}
