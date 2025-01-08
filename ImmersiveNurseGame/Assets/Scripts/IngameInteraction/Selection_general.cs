using UnityEngine;
using UnityEngine.UI;

public class Selection_general : MonoBehaviour
{
    public GameObject interaction_Info_UI;
    public RectTransform pointerImage; // Reference to the pointer image's RectTransform
    private Text interaction_text;

    private void Start()
    {
        // Ensure interaction_Info_UI has a Text component or find it in children
        interaction_text = interaction_Info_UI.GetComponentInChildren<Text>();
        if (interaction_text == null)
        {
            Debug.LogError("No Text component found in interaction_Info_UI or its children.");
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(pointerImage.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            if (selectionTransform.GetComponent<InteractableObject>())
            {
                string itemName = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interaction_text.text = itemName;
                interaction_Info_UI.SetActive(true);
                // Update the position of the interaction info UI to be near the pointer image
                interaction_Info_UI.transform.position = pointerImage.position + new Vector3(10, -10, 0); // Adjust the offset as needed
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