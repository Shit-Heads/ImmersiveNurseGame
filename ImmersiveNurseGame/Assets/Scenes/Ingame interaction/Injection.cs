using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Injection : MonoBehaviour
{
    public Transform playerCameraTransform;
    public Vector3 injectionOffset = new Vector3(0, -0.5f, 1.5f);
    public RectTransform injectionImage; // Reference to the image's RectTransform
    public Transform patientTransform; // Reference to the patient's transform
    public float injectionDistanceThreshold = 0.5f; // Distance threshold to consider the injection successful
    public GameObject interaction_Info_UI; // Reference to the interaction info UI
    private GameObject selectedInjection;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isInteractable = false;
    private int originalLayer;

    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f; // Time threshold for double-click detection

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button for double-click detection
        {
            float timeSinceLastClick = Time.time - lastClickTime;
            if (timeSinceLastClick <= doubleClickThreshold)
            {
                ToggleInteraction();
            }
            lastClickTime = Time.time;
        }

        if (isInteractable && selectedInjection != null)
        {
            if (Input.GetMouseButton(0) || Input.touchCount > 0)
            {
                MoveInjectionWithInput();
            }
            else
            {
                // Keep the injection in front of the camera
                AlignInjectionToFrontOfCamera();
            }

            // Check if the injection is close enough to the patient
            CheckInjectionToPatient();
        }
    }

    public bool IsInjectionActive()
    {
        return isInteractable;
    }

    private void ToggleInteraction()
    {
        if (isInteractable)
        {
            StartCoroutine(MoveInjectionToTarget(selectedInjection.transform, originalPosition, originalRotation));
            isInteractable = false;
            selectedInjection.layer = originalLayer; // Restore original layer
            injectionImage.gameObject.SetActive(true); // Make the image visible
            interaction_Info_UI.SetActive(true); // Make the interaction info UI visible
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                InteractableObject interactable = hit.transform.GetComponent<InteractableObject>();
                if (interactable != null && interactable.ItemName == "Injection")
                {
                    selectedInjection = hit.transform.gameObject;
                    originalPosition = selectedInjection.transform.position;
                    originalRotation = selectedInjection.transform.rotation;
                    originalLayer = selectedInjection.layer;
                    selectedInjection.layer = LayerMask.NameToLayer("Ignore Raycast");
                    isInteractable = true;

                    // Align the injection to the front of the camera
                    AlignInjectionToFrontOfCamera();
                    injectionImage.gameObject.SetActive(false); // Make the image disappear
                    interaction_Info_UI.SetActive(false); // Make the interaction info UI disappear
                }
            }
        }
    }

    private void AlignInjectionToFrontOfCamera()
    {
        Vector3 targetPosition = playerCameraTransform.position + playerCameraTransform.forward * injectionOffset.z + playerCameraTransform.up * injectionOffset.y + playerCameraTransform.right * injectionOffset.x;
        selectedInjection.transform.position = targetPosition;
    }

    private void MoveInjectionWithInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            ray = Camera.main.ScreenPointToRay(touch.position);
        }

        Plane plane = new Plane(Vector3.up, selectedInjection.transform.position); // A horizontal plane at the injection's position
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            selectedInjection.transform.position = hitPoint;
        }
    }

    private IEnumerator MoveInjectionToTarget(Transform injection, Vector3 targetPosition, Quaternion targetRotation)
    {
        float duration = 1.0f;
        float elapsedTime = 0.0f;
        Vector3 startingPosition = injection.position;
        Quaternion startingRotation = injection.rotation;

        while (elapsedTime < duration)
        {
            injection.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);
            injection.rotation = Quaternion.Lerp(startingRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        injection.position = targetPosition;
        injection.rotation = targetRotation;
    }

    private void CheckInjectionToPatient()
    {
        float distanceToPatient = Vector3.Distance(selectedInjection.transform.position, patientTransform.position);
        if (distanceToPatient <= injectionDistanceThreshold)
        {
            // Injection is successful
            Debug.Log("Injection successful!");
            // You can add additional logic here, such as playing an animation or sound
        }
    }
}