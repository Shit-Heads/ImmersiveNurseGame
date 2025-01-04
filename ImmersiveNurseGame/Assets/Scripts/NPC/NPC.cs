using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPC : MonoBehaviour
{
    public bool playerInRange;
    public bool questAccepted = false; // Flag to track quest acceptance
    public GameObject playerwindow;
    public GameObject touchController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerwindow.SetActive(true);
            touchController.SetActive(false);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            touchController.SetActive(true); // Show touch controller after accepting the quest
            playerInRange = false;
            playerwindow.SetActive(false);
        }
    }

    public void OnQuestAccepted()
    {
        questAccepted = true;
        if (playerInRange)
        {
            playerwindow.SetActive(false); // Hide player window after accepting the quest
        }
    }
}
