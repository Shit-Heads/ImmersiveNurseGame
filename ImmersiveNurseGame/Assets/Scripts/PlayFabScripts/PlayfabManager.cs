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

    public GameObject rowPrefab;
    public Transform rowsParent;

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

    public void SendLeaderBoard(int score){
        var request = new UpdatePlayerStatisticsRequest{
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate{
                    StatisticName = "NurseScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnError);
    }

    public void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result){
        Debug.Log("Sucessfully leaderboard sent");
    }

    public void GetLeaderBoard(){
        var request = new GetLeaderboardRequest{
            StatisticName = "NurseScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGet, OnError);
    }

    public void OnLeaderBoardGet(GetLeaderboardResult result){
        foreach(Transform item in rowsParent){
            Destroy(item.gameObject);
        }

        foreach(var item in result.Leaderboard){
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.PlayFabId;
            texts[2].text = item.StatValue.ToString();
            Debug.Log(item.Position + "" + item.PlayFabId + "" + item.StatValue);
        }
    }
}
