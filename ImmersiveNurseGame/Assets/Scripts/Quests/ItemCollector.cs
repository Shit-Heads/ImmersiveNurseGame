using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public QuestGiver questGiver;
    public int totalCubes = 3; // Total number of cubes to collect
    private static int collectedCubes = 0; // Number of collected cubes (static to share across instances)
    private bool isCollected = false; // Flag to check if the cube is already collected

    private void Start()
    {
        // Add a SphereCollider to define the range
        SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = 3f; // Set the range radius as needed
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
                    }

                    Destroy(gameObject); // Remove the cube after collection
                }
            }
        }
    }
}