using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public QuestGiver questGiver;
    public GameObject goalWindow; // Reference to the goal window
    public int totalCubes = 3; // Total number of cubes to collect
    private static int collectedCubes = 0; // Number of collected cubes (static to share across instances)
    private bool isCollected = false; // Flag to check if the cube is already collected

    private void Start()
    {
        // Add a BoxCollider to define the range
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector3(5f, 5f, 5f); // Set the range size as needed
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger");

            if (questGiver.CurrentQuest.isActive && !isCollected)
            {
                Debug.Log("Quest is active and item not yet collected");

                if (questGiver.CurrentQuest.completionType == QuestCompletionType.CollectItem)
                {
                    collectedCubes++; // Increment the collected cubes count
                    isCollected = true; // Mark the cube as collected

                    Debug.Log($"Collected {collectedCubes}/{totalCubes} cubes");

                    // If all cubes are collected, complete the quest
                    if (collectedCubes >= totalCubes)
                    {
                        questGiver.CompleteQuest();
                        Debug.Log("Quest completed by collecting all cubes!");

                        // Show the goal window
                        if (goalWindow != null)
                        {
                            goalWindow.SetActive(true);
                        }
                    }

                    Destroy(gameObject); // Remove the cube after collection
                }
            }
        }
    }
}