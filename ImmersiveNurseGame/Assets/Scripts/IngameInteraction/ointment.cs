using UnityEngine;
using System.Collections;

public class Ointment : MonoBehaviour
{
    public Transform patientPosition; 
    public GameObject successMessagePanel; 
    public GameObject new3dObject;
    public GameObject arrowIndicator;
    public GameObject wound;
    public GameObject imagePrefab; // Prefab of the image to display

    public string targetTag; 

    private bool isPlaced = false;
    private bool isTouched = false;
    private bool isDragging = false;
    private float elapsedTime = 0f;
    private float maxTime = 5f; // Time in seconds to allow image display
    private GameObject instantiatedImage;

    private void Start()
    {
        // Ensure the arrow and image are initially hidden
        if (arrowIndicator != null)
        {
            arrowIndicator.SetActive(false);
        }

        if (imagePrefab != null)
        {
            imagePrefab.SetActive(false);
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
        if (other.CompareTag(targetTag) && instantiatedImage != null)
        {
            Destroy(instantiatedImage);
        }
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

        transform.position = patientPosition.position;
        transform.rotation = patientPosition.rotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        Debug.Log("Placed successfully!");

        // Start coroutine to display image after 2 seconds
        StartCoroutine(DisplayImageWithDelay(2f));
    }

    private IEnumerator DisplayImageWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (imagePrefab != null)
        {
            instantiatedImage = Instantiate(imagePrefab, patientPosition.position, Quaternion.identity);
            instantiatedImage.SetActive(true);
            Debug.Log("Image displayed after delay at position: " + patientPosition.position);
        }
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