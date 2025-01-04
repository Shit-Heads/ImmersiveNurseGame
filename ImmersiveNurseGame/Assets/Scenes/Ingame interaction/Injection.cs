using System.Collections;
using UnityEngine;

public class Injection : MonoBehaviour
{
    public Transform playerCameraTransform; // The player's camera transform
    public Vector3 injectionOffset = new Vector3(0, -0.5f, 1.5f); // Offset from the camera to position the injection
    private GameObject selectedInjection; // The currently selected injection

    void Update()
    {
        // Check for the "I" key press
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I key pressed");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Raycast hit: " + hit.transform.name);
                // Check if the object hit by the raycast has the InteractableObject component
                InteractableObject interactable = hit.transform.GetComponent<InteractableObject>();
                if (interactable != null && interactable.ItemName == "Injection")
                {
                    Debug.Log("Injection clicked: " + hit.transform.name);
                    // Select the injection and move it to the target position
                    selectedInjection = hit.transform.gameObject;
                    Vector3 targetPosition = playerCameraTransform.position + playerCameraTransform.TransformDirection(injectionOffset);
                    StartCoroutine(MoveInjectionToTarget(selectedInjection.transform, targetPosition));
                }
                else
                {
                    Debug.Log("InteractableObject not found or ItemName is not 'Injection'");
                }
            }
            else
            {
                Debug.Log("Raycast did not hit any object");
            }
        }
    }

    private IEnumerator MoveInjectionToTarget(Transform injection, Vector3 targetPosition)
    {
        float duration = 0.5f; // Duration of the move
        float elapsedTime = 0.0f;
        Vector3 startingPosition = injection.position;

        while (elapsedTime < duration)
        {
            injection.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        injection.position = targetPosition;
        Debug.Log("Injection moved to target position: " + targetPosition);
    }
}
