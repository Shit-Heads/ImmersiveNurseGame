using UnityEngine;
using UnityEngine.UI; // Add this line to include the UnityEngine.UI namespace

public class Selection_general : MonoBehaviour
{
    public GameObject interaction_Info_UI;
    public RectTransform pointerImage; // Reference to the pointer image's RectTransform
    private Text interaction_text;

    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<Text>();
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
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
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