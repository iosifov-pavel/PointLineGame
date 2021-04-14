using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager game;
    string saveName;
    string playerDataPath;
    string file;
    public Game gameData;
    // Start is called before the first frame update

    private void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SaveLoadManager");
        if (objs.Length > 1){
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        game = this;    
        saveName = "/save"+".dat";
        playerDataPath = Application.persistentDataPath +"/Saves";
        file = playerDataPath+saveName;
        if(!PlayerPrefs.HasKey("FirstRun") || !File.Exists(file)){
            PlayerPrefs.SetInt("FirstRun",1);
            FirstLoad();
        }
        else LoadFromFile();
    }



    public void FirstLoad(){
        gameData = new Game();
        SaveToFile();
    }

        public void SaveToFile(){
        if(!Directory.Exists(playerDataPath)){
            Debug.Log("Create Directory");
            Directory.CreateDirectory(playerDataPath);
        }
        if(!File.Exists(file)){
            Debug.Log("Create File");
            File.Create(file).Close();
        }
        string json = JsonUtility.ToJson(gameData, true);
        Debug.Log("Saving as JSON: " + json);
        FileStream fs = new FileStream(file,FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamWriter sr = new StreamWriter(fs);
        sr.WriteLine(json);
        sr.Close();
        fs.Close();
    }

    void LoadFromFile(){
        gameData = new Game();
        if(!File.Exists(file)){
            FirstLoad();
            Debug.Log("Sorry, Load from Blank");
            return;
        }
        FileStream fs = new FileStream(file,FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);
        string json = sr.ReadToEnd();
        Debug.Log(playerDataPath);
        sr.Close();
        fs.Close();
        gameData = JsonUtility.FromJson<Game>(json);
    }
}


[System.Serializable]
public class Game{
    public int death;
    public int points;
    public int bestScore;
    public int totalScore;
    public int totalJumps;
    public int maxJump;
    public float jumpPerPlay;
    public float scorePerPlay;
    public int totalUps;
    public int totalDestroyedObstacle;
    public Game(){
        death=0;
        points=0;
        bestScore=0;
        totalScore=0;
        maxJump = 0;
        totalJumps=0;
        jumpPerPlay=0f;
        scorePerPlay=0f;
        totalUps=0;
        totalDestroyedObstacle=0;
    }
}


