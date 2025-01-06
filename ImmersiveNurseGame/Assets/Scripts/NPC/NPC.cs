using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public bool playerInRange;
    public bool questAccepted = false;
    public GameObject playerWindow;
    public GameObject touchControlManager;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI experienceText;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerWindow.SetActive(true);
            touchControlManager.gameObject.GetComponent<TouchControlManager>().ToggleTouchUI(false);
            UpdatePlayerWindow();
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
        player.UpdateHealth(-1); // Deduct one life upon accepting the quest
        playerWindow.SetActive(false);
        UpdatePlayerWindow();
    }

    private void UpdatePlayerWindow()
    {
        healthText.text = $"Health: {player.health}";
        experienceText.text = $"Experience: {player.experience}";
    }
}