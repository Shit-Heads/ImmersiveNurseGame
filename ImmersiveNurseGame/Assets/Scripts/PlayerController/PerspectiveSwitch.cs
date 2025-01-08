using System.Collections;
using System.Collections.Generic;
using PlayFab.MultiplayerModels;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class PerspectiveSwitch : MonoBehaviour
{
    public ThirdPersonController tc;
    public LeaderboardManager lm;

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            tc.SwitchPerspective();
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player")){
            tc.SwitchPerspective();
            lm.hideLeaderboard();
        }
    }
}
