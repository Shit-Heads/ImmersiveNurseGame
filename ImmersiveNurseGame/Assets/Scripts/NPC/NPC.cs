using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    public bool playerInRange;
    public GameObject playerwindow;
    public GameObject touchController;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            touchController.SetActive(false);
            playerwindow.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            touchController.SetActive(true);
            playerwindow.SetActive(false);
        }
    }
}
