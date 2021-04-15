using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GoogleMobileAds.Api;

public class GooglePain : MonoBehaviour
{
    // Start is called before the first frame update
    private const string scoreboard = "CgkIlo304KkXEAIQAQ";
    void Start()
    {
        MobileAds.Initialize(initStaus =>{});
        Autentification();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Autentification(){
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool sucsess) =>{
            if(sucsess) Debug.Log("Wow");
            else Debug.Log("Auch");
        });
    }

    public void SetBoardScore(int score){
        try{
            Social.ReportScore(score,scoreboard,(bool sucsess) =>{});
        }
        catch{
            Debug.Log("wasnt autentificate");
        }
    }

    public void ShowLeaderBoard(){
        Social.ShowLeaderboardUI();
    }
}
