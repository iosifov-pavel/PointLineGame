using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform pausePanel;
    [SerializeField] Text pauseText;
    [SerializeField] CreateSphere pauseControl;
    [SerializeField] Button ReviveButton;
    [SerializeField] Button AdReviveButton;
    [SerializeField] GameObject adsObjPrefab;
    bool wasRevived = false;
    GameObject ads;
    public bool adWasWatched = false;
    void Start()
    {
        try{
            ReviveButton.gameObject.SetActive(false);
            ads = Instantiate(adsObjPrefab);
        }
        catch{
            Debug.Log("Main menu error");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(adWasWatched){
            ReviveButton.gameObject.SetActive(true);
            wasRevived = true;
        }
        if(wasRevived){
            AdReviveButton.gameObject.SetActive(false);
            Time.timeScale = 0;
            pauseControl.pause = true;
            pauseControl.continuePressed = true;
            pauseText.gameObject.SetActive(true);
            wasRevived = false;
            adWasWatched = false;
        }
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void StartPlaying(){
        SceneManager.LoadScene(1);
    }

    public void PauseMenu(){
        Time.timeScale = 0;
        pauseControl.pause = true;
        pauseControl.continuePressed = false;
        pausePanel.gameObject.SetActive(true);
        pauseText.gameObject.SetActive(true);
    }

    public void ContinueGame(){
        pauseControl.continuePressed = true;
        pausePanel.gameObject.SetActive(false);
    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
    }

    public void WathcADToRevive(){
        ShowAds sads = ads.GetComponent<ShowAds>();
        sads.UserChoseToWatchAd();
    }
}
