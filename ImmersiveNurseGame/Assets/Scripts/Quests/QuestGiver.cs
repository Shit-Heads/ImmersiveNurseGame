using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public NPC NPC;
    public GameObject playerwindow;
    public Player Player;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI experienceText;
    public TextMeshProUGUI goldText;

    public GameObject questWindow;
    public Goal goal; // Reference to the Goal script

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        experienceText.text = quest.experienceReward.ToString();
        goldText.text = quest.goldReward.ToString();
    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest.isActive = true;
        Player.quest = quest;
        goal.playerInRange = false; // Ensure goal starts inactive
        NPC.OnQuestAccepted(); // Notify NPC that the quest is accepted
    }

}