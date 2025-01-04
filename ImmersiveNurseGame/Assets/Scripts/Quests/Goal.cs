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
        if (other.CompareTag("Player") && questGiver.quest.isActive)
        {
            playerInRange = true;
            goalWindow.SetActive(true);
            // Additional logic to mark the goal as completed
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