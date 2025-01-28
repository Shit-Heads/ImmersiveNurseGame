using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HandAnimationController : MonoBehaviour
{
    public Animator handAnimator; // Reference to the Animator
    public ParticleSystem RunningWater; // Reference to the Particle System

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.H)) // Detect "H" key press
        {
            //handAnimator.SetBool("PlayHandAnimation", true); // Trigger the animation
            handAnimator.SetTrigger("WashAnimation"); // Trigger the animation
            StartCoroutine(PlayRunningWaterWithDelay(1.5f)); // Play the particle system with a delay of 3 seconds
        }
    }

    IEnumerator PlayRunningWaterWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RunningWater.Play(); // Play the particle system
    }
    void Start()
    {
        RunningWater.Stop(); // Stop the particle system
    }
}
