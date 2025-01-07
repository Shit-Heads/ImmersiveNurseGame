using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public bool isCompleted;
    public string title;
    public string description;
    public int experienceReward;
    public int goldReward;
    public QuestCompletionType completionType; // Type of quest completion

    public Transform target; // Target for the quest
}

public enum QuestCompletionType
{
    EnterRange,
    CollectItem,
    DefeatEnemy,
    CustomAction // For special or custom actions
}