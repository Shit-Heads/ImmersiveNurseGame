using System.Collections;
using UnityEngine;

public class MoveObjects : MonoBehaviour {

    public string draggingTag;
    public Camera cam;

    private Vector3 dis;
    private float posX;
    private float posY;

    private bool touched = false;
    private bool dragging = false;

    private Transform toDrag;
    private Rigidbody toDragRigidbody;
    private Vector3 previousPosition;

    void FixedUpdate () {

        if (Input.touchCount != 1) {
            dragging = false;
            touched = false;
            if (toDragRigidbody) {
                SetFreeProperties(toDragRigidbody);
            }
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Began) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(pos);

            if (Physics.Raycast(ray, out hit) && hit.collider.tag == draggingTag) {
                toDrag = hit.transform;
                previousPosition = toDrag.position;
                toDragRigidbody = toDrag.GetComponent<Rigidbody>();

                dis = cam.WorldToScreenPoint(previousPosition);
                posX = Input.GetTouch(0).position.x - dis.x;
                posY = Input.GetTouch(0).position.y - dis.y;

                SetDraggingProperties(toDragRigidbody);

                touched = true;
            }
        }

        if (touched && touch.phase == TouchPhase.Moved) {
            dragging = true;

            float posXNow = Input.GetTouch(0).position.x - posX;
            float posYNow = Input.GetTouch(0).position.y - posY;
            Vector3 curPos = new Vector3(posXNow, posYNow, dis.z);

            Vector3 worldPos = cam.ScreenToWorldPoint(curPos);
            if (toDrag != null && toDragRigidbody != null) {
                Vector3 moveDirection = worldPos - toDrag.position;
                toDragRigidbody.velocity = moveDirection / Time.fixedDeltaTime;
                previousPosition = toDrag.position;
            }
        }

        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)) {
            dragging = false;
            touched = false;
            previousPosition = Vector3.zero;
            if (toDragRigidbody != null) {
                SetFreeProperties(toDragRigidbody);
            }
        }
    }

    private void SetDraggingProperties (Rigidbody rb) {
        if (rb != null) {
            rb.useGravity = false;
            rb.drag = 20;
        }
    }

    private void SetFreeProperties (Rigidbody rb) {
        if (rb != null) {
            rb.drag = 5;
        }
    }
}