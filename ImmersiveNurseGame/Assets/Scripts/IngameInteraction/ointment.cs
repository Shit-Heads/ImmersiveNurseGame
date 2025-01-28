using UnityEngine;

public class Ointment : MonoBehaviour
{
    public Transform patientPosition; 
    public GameObject successMessagePanel; 
    public GameObject new3dObject;
    public GameObject arrowIndicator;
    public GameObject wound;

    public string targetTag; 

    private bool isPlaced = false;
    private bool isTouched = false;
    private bool isDragging = false;
    private float elapsedTime = 0f;
    private float maxTime = 5f; // Time in seconds to allow image display

    private void Start()
    {
        // Ensure the arrow is initially hidden
        if (arrowIndicator != null)
        {
            arrowIndicator.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (!isTouched)
        {
            ShowArrow();
        }
    }

    private void OnMouseDrag()
    {
        if (isPlaced && elapsedTime < maxTime)
        {
            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= maxTime)
            {
                ShowSuccessMessage();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTouched && !isPlaced && other.CompareTag(targetTag)) 
        {
            PlaceObject();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // No need to call DisplayImageWithDelay here
    }

    private void OnTriggerExit(Collider other)
    {
        // No need to destroy instantiated image here
    }

    private void ShowArrow()
    {
        isTouched = true;

        if (arrowIndicator != null)
        {
            arrowIndicator.SetActive(true);
        }

        Debug.Log("Arrow shown!");
    }

    private void PlaceObject()
    {
        isPlaced = true;

        // Hide the ointment object
        gameObject.SetActive(false);

        // Show the new 3D object
        if (new3dObject != null)
        {
            new3dObject.SetActive(true);
        }

        Debug.Log("Ointment placed and new 3D object shown!");
    }

    private void ShowSuccessMessage()
    {
        if (successMessagePanel != null)
        {
            successMessagePanel.SetActive(true);
        }

        Debug.Log("Success message displayed!");

        // Now we can destroy the ointment object
        Destroy(gameObject);
    }

    public void ApplyOintment()
    {
        if (isPlaced)
        {
            // Apply ointment logic here
            Debug.Log("Ointment applied!");

            if (arrowIndicator != null)
            {
                arrowIndicator.SetActive(false);
            }

            ShowSuccessMessage();
        }
    }
}