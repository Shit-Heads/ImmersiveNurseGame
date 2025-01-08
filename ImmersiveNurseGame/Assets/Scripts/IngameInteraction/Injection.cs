using System.Collections;
using UnityEngine;

public class Injection : MonoBehaviour
{
    public Transform patientTransform; // Reference to the patient's transform
    public float snapDistance = 0.5f; // Distance threshold to consider the injection successful
    public float moveSpeed = 5f;

    private bool isSnapped = false;

    void Update()
    {
        if (!isSnapped && Vector3.Distance(transform.position, patientTransform.position) <= snapDistance)
        {
            StartCoroutine(SnapToPatient());
        }
    }

    private IEnumerator SnapToPatient()
    {
        isSnapped = true;

        // Ensure the object is not parented to any unintended transform
        transform.SetParent(null);

        while (Vector3.Distance(transform.position, patientTransform.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, patientTransform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Parent the injection to the patient's transform
        transform.SetParent(patientTransform);
        transform.localPosition = Vector3.zero; // Adjust as needed
    }
}