using UnityEngine;

public class HandAnimationController : MonoBehaviour
{
    public Animator handAnimator; // Reference to the Animator

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.H)) // Detect "L" key press
        {
            handAnimator.SetBool("PlayHandAnimation", true); // Trigger the animation
        }
    }
}
