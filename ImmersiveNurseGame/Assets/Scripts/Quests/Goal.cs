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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && playerInRange)
        {
            playerInRange = false;
            goalWindow.SetActive(false);

            // Mark quest as completed
            questGiver.CompleteQuest();
        }
    }
}
