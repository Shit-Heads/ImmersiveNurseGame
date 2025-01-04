using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public QuestGiver questGiver;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && questGiver.CurrentQuest.isActive)
        {
            if (questGiver.CurrentQuest.completionType == QuestCompletionType.CollectItem)
            {
                questGiver.CompleteQuest();
                Debug.Log("Quest completed by collecting the item!");
                Destroy(gameObject); // Remove the item after collection
            }
        }
    }
}

