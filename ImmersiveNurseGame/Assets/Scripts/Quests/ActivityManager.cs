using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    public GameObject touchControl;

    public void DisableTouchController()
    {
        touchControl.SetActive(false);
    }

    public void EnableTouchController()
    {
        touchControl.SetActive(true);
    }
}

