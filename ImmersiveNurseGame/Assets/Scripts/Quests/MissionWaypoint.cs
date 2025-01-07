using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    public Image img; // The marker icon
    public Text meter; // Distance display
    public Vector3 offset; // Adjust the marker position
    private Transform target; // Dynamic quest target
    private bool isActive = false;

    private void Update()
    {
        if (!isActive || target == null)
        {
            img.gameObject.SetActive(false);
            meter.gameObject.SetActive(false);
            return;
        }

        // Show marker and distance text
        img.gameObject.SetActive(true);
        meter.gameObject.SetActive(true);

        // Calculate marker position and constraints
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        // Check if the target is behind the player
        if (Vector3.Dot((target.position - Camera.main.transform.position), Camera.main.transform.forward) < 0)
        {
            pos.x = pos.x < Screen.width / 2 ? maxX : minX;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
        meter.text = $"{(int)Vector3.Distance(target.position, Camera.main.transform.position)}m";
    }

    public void Activate(Transform questTarget)
    {
        target = questTarget;
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
