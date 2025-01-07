using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public GameObject interaction_Info_UI;
    public RectTransform pointerImage; // Reference to the pointer image's RectTransform
    Text interaction_text;
    public Injection injectionScript; // Reference to the Injection script

    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<Text>();
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Check if the mouse position is within the screen bounds
        if (mousePosition.x >= 0 && mousePosition.x <= Screen.width && mousePosition.y >= 0 && mousePosition.y <= Screen.height)
        {
            // Update the position of the pointer image based on the cursor position
            pointerImage.position = mousePosition;

            if (injectionScript.IsInjectionActive())
            {
                interaction_Info_UI.SetActive(false);
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var selectionTransform = hit.transform;

                if (selectionTransform.GetComponent<InteractableObject>())
                {
                    interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                    interaction_Info_UI.SetActive(true);
                    // Update the position of the interaction info UI to be near the pointer image
                    interaction_Info_UI.transform.position = mousePosition + new Vector3(10, -10, 0); // Adjust the offset as needed
                }
                else
                {
                    interaction_Info_UI.SetActive(false);
                }
            }
            else
            {
                interaction_Info_UI.SetActive(false);
            }
        }
        else
        {
            interaction_Info_UI.SetActive(false);
        }
    }
}