using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public List<Quest> quests;  // List to hold multiple quests
    private int currentQuestIndex = 0;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI experienceText;
    public TextMeshProUGUI goldText;
    public GameObject questWindow;
    public ActivityManager activityManager; // Reference to ActivityManager
    public GameObject touchControlManager;
    public Goal goal;
    public Player Player;

    public Quest CurrentQuest => quests[currentQuestIndex];

    public void OpenQuestWindow()
    {
        if (CurrentQuest.isCompleted)
        {
            return; // Prevent opening the quest window if the current quest is already completed
        }

        questWindow.SetActive(true);
        titleText.text = CurrentQuest.title;
        descriptionText.text = CurrentQuest.description;
        experienceText.text = CurrentQuest.experienceReward.ToString();
        goldText.text = CurrentQuest.goldReward.ToString();
    }

    public void AcceptQuest()
    {
        if (currentQuestIndex > 0 && !quests[currentQuestIndex - 1].isCompleted)
        {
            Debug.Log("Complete the previous quest first.");
            return; // Prevent accepting the next quest before completing the previous one
        }

        questWindow.SetActive(false);
        CurrentQuest.isActive = true;
        goal.playerInRange = false;
        touchControlManager.gameObject.GetComponent<TouchControlManager>().ToggleTouchUI(true);
    }

    public void CompleteQuest()
    {
        if (CurrentQuest.isActive && !CurrentQuest.isCompleted)
        {
            CurrentQuest.isActive = false;
            CurrentQuest.isCompleted = true;
            Player.experience += CurrentQuest.experienceReward;
            Player.gold += CurrentQuest.goldReward;

            Debug.Log($"Quest '{CurrentQuest.title}' completed!");

            // Move to the next quest if available
            if (currentQuestIndex < quests.Count - 1)
            {
                currentQuestIndex++;
            }
            else
            {
                Debug.Log("All quests completed!");
            }
        }
    }

}
