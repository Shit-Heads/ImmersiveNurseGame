using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestGiver : MonoBehaviour
{
    public List<Quest> quests;  // List to hold multiple quests
    private int currentQuestIndex = 0;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI experienceText;
    public TextMeshProUGUI goldText;
    public GameObject questWindow;
    public GameObject touchControlManager;
    // public Goal goal;
    public Player Player;
    public GuidanceSystem guidanceSystem; // Reference to the GuidanceSystem

    public MissionWaypoint missionWaypoint;

    public Quest CurrentQuest => quests[currentQuestIndex];
    [SerializeField] private LeaderboardManager lm;

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
            return;
        }

        questWindow.SetActive(false);
        CurrentQuest.isActive = true;
        // goal.playerInRange = false;
        touchControlManager.gameObject.GetComponent<TouchControlManager>().ToggleTouchUI(true);
        Player.UpdateHealth(-1);

        SceneManager.LoadScene(2);

        /* // Update guidance for the new quest
        guidanceSystem.UpdateGuidance(CurrentQuest);

        // Activate the MissionWaypoint for the current quest's target
        if (missionWaypoint != null && CurrentQuest.target != null)
        {
            missionWaypoint.Activate(CurrentQuest.target);
        }
        */
    }

    public void CompleteQuest()
    {
        if (CurrentQuest.isActive && !CurrentQuest.isCompleted)
        {
            CurrentQuest.isActive = false;
            CurrentQuest.isCompleted = true;
            Player.UpdateExperience(CurrentQuest.experienceReward);
            Player.gold += CurrentQuest.goldReward;
            lm.SendLeaderBoard(CurrentQuest.experienceReward);

            Debug.Log($"Quest '{CurrentQuest.title}' completed!");

            // Move to the next quest
            if (currentQuestIndex < quests.Count - 1)
            {
                currentQuestIndex++;
            }
            else
            {
                Debug.Log("All quests completed!");
            }

            // Deactivate the MissionWaypoint
            if (missionWaypoint != null)
            {
                missionWaypoint.Deactivate();
            }

            // Clear guidance
            guidanceSystem.ClearGuidance();
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}