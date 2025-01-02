using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Video;
using UnityEngine.InputSystem.Composites;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayfabSessionManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text messageText;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    [Header("Misc")]
    public GameObject nameWindow;
    public GameObject loginWindow;
    public TMP_InputField nameInput;

    public void RegisterButton(){
        if(passwordInput.text.Length < 6){
            messageText.text = "Password should be of minimum 6 characters";
            return;
        }

        var request = new RegisterPlayFabUserRequest{
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSucess, OnError);
    }

    public void LoginButton(){
        var request = new LoginWithEmailAddressRequest{
            Email = emailInput.text,
            Password = passwordInput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams{
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSucess, OnError);
    }

    public void SubmitNameButton(){
        var request = new UpdateUserTitleDisplayNameRequest{
            DisplayName = nameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnRegisterSucess(RegisterPlayFabUserResult result){
        messageText.text = "Registered! now login";
    }

    void OnLoginSucess(LoginResult result){
        messageText.text = "Logged In";
        Debug.Log("Sucessfull Account Created");
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null){
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }
        if(name == null){
            nameWindow.SetActive(true);
            loginWindow.SetActive(false);
        }else{
            StartCoroutine(nextScene(1));
        }
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result){
        Debug.Log("Updated display name");
        StartCoroutine(nextScene(1));
    }

    void OnError(PlayFabError error){
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    IEnumerator nextScene(int nextScene){
        SceneManager.LoadScene(nextScene);
        yield return new WaitForSeconds(2);
    }
}
