using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public Transform patientPosition; 
    public GameObject successMessagePanel; 
    public GameObject new3dObject;

    public string targetTag; 

    private bool isPlaced = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlaced && other.CompareTag(targetTag)) 
        {
            PlaceObject();
        }
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

        if (successMessagePanel != null)
        {
            successMessagePanel.SetActive(true);
        }

        Debug.Log("Placed successfully!");

        if (new3dObject != null)
        {
            new3dObject.SetActive(true);
        }

        Destroy(gameObject);
    }
}