using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 5;
    public int experience = 40;
    public int gold = 1000;

    public Quest quest;

    public void UpdateHealth(int amount)
    {
        health += amount;
        // Update the health in the player window
        // This should trigger the UI update logic
    }

    public void UpdateExperience(int amount)
    {
        experience += amount;
        // Update the experience in the player window
        // This should trigger the UI update logic
    }
}