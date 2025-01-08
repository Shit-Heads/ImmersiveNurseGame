using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuidanceSystem : MonoBehaviour
{
    public TextMeshProUGUI guidanceText; // Text to display guidance

    private Transform playerTransform;
    private Transform currentObjective;

    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;
    }

    public void UpdateGuidance(Quest quest)
    {
        switch (quest.title)
        {
            case "Scenario 1: Before Touching a Patient":
                SetGuidance("Go to the range of the cube.");
                break;
            case "Scenario 2: Before a Clean/Aseptic Procedure":
                SetGuidance("Collect the 3 spheres.");
                break;
            case "Injection":
                SetGuidance("Take the steps to reach the injection room");
                break;
            default:
                ClearGuidance();
                break;
        }
    }

    private void SetGuidance(string text)
    {
        guidanceText.text = text;
    }

    public void ClearGuidance()
    {
        guidanceText.text = "";
    }
}