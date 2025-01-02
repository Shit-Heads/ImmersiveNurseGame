using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Unity.VisualScripting;
using System.Data;
using TMPro;

public class PlayfabManager : MonoBehaviour
{
    void Start()
    {
        Login();
    }

    void Login(){
        var request = new LoginWithCustomIDRequest{
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnSucess, OnError);
    }

    void OnSucess(LoginResult result){
        Debug.Log("Sucessfull Account Created");
    }

    void OnError(PlayFabError error){
        Debug.Log("Error while logging in/creating account");
        Debug.Log(error.GenerateErrorReport());
    }
}
