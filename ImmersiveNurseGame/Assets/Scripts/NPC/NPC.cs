using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool playerInRange;
    public bool questAccepted = false;
    public GameObject playerWindow;
    public ActivityManager activityManager; // Reference to ActivityManager
    public GameObject touchControlManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerWindow.SetActive(true);
            touchControlManager.gameObject.GetComponent<TouchControlManager>().ToggleTouchUI(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerWindow.SetActive(false);
        }
    }

    public void OnQuestAccepted()
    {
        questAccepted = true;
        playerWindow.SetActive(false);
    }
}
