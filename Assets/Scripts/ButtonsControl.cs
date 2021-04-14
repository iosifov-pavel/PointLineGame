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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
