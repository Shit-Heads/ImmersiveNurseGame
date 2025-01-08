using UnityEngine;
using System.Collections;

public class PulseOximeter : MonoBehaviour
{
    public Transform patientFinger; // Assign the patient's finger transform in the inspector
    public float snapDistance = 0.5f; // Distance to snap to the finger
    public float moveSpeed = 5f;
    public float liftHeight = 0.2f; // Height to lift the pulse oximeter when dragging
    public KeyCode resetKey = KeyCode.R; // Key to reset the position

    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 originalPosition;

    void Start()
    {
        // Store the original position
        originalPosition = transform.position;
    }

    void Update()
    {
        // Reset the position when the reset key is pressed
        if (Input.GetKeyDown(resetKey))
        {
            ResetPosition();
        }
    }

    void OnMouseDown()
    {
        StartDragging();
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            StopDragging();
            if (Vector3.Distance(transform.position, patientFinger.position) <= snapDistance)
            {
                StartCoroutine(SnapToFinger());
            }
        }
    }

    private void StartDragging()
    {
        isDragging = true;

        // Calculate offset between the object's position and the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position); // A horizontal plane at the oximeter's position
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            offset = transform.position - hitPoint;
        }

        // Lift the pulse oximeter slightly
        transform.position += new Vector3(0, liftHeight, 0);
    }

    private void StopDragging()
    {
        isDragging = false;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            MoveOximeterWithInput();
        }
    }

    private void MoveOximeterWithInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position); // A horizontal plane at the oximeter's position
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            transform.position = hitPoint + offset;
        }
    }

    private IEnumerator SnapToFinger()
    {
        // Ensure the object is not parented to any unintended transform
        transform.SetParent(null);

        while (Vector3.Distance(transform.position, patientFinger.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, patientFinger.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Parent the oximeter to the patient's finger
        transform.SetParent(patientFinger);
        transform.localPosition = Vector3.zero; // Adjust as needed
    }

    private void ResetPosition()
    {
        StopAllCoroutines(); // Stop snapping if it's happening
        transform.SetParent(null); // Detach from any parent
        transform.position = originalPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isDragging)
        {
            // Prevent disappearing by stopping any unintended interactions
            StopDragging();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDragging)
        {
            // Prevent disappearing by stopping any unintended interactions
            StopDragging();
        }
    }
}
