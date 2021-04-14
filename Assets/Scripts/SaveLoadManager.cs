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
    int first_index=1, second_index=1;
    Game gameData;
    // Start is called before the first frame update

    private void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SaveLoadManager");
        if (objs.Length > 1){
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        game = this;    
    }
    void Start()
    {
        saveName = "/save"+".dat";
        playerDataPath = Application.persistentDataPath +"/Save";
        file = playerDataPath+saveName;
        if(!PlayerPrefs.HasKey("FirstRun") || !File.Exists(file)){
            PlayerPrefs.SetInt("FirstRun",1);
            FirstLoad();
        }
    }

    //public void Loading(){
    //    manager.game_info = new Game();
    //    for(first_index=1;first_index<=11;first_index++){
    //        string sn = string.Format("S{0}",first_index);
    //        manager.game_info.sections.Add(new Section(first_index,sn));
    //        for(second_index=1;second_index<=10;second_index++){
    //            string sl = string.Format("{0}L{1}",first_index,second_index);
    //            manager.game_info.sections[first_index-1].levels.Add(new Level(second_index,sl));
    //            if(second_index==10){
    //                manager.game_info.sections[first_index-1].levels[second_index-1].boss_stage=true;
    //            }
    //        }
    //    }
    //    manager.game_info.sections[0].levels[0].blocked = false;
    //    manager.game_info.sections[0].blocked = false;
    //    manager.SaveToFile();
    //}

    public void FirstLoad(){
        gameData = new Game();
        SaveToFile();
    }

        public void SaveToFile(){
        if(!File.Exists(playerDataPath)){
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
        if(!File.Exists(file)){
            FirstLoad();
            Debug.Log("Sorry, Load from Blank");
            return;
        }
        FileStream fs = new FileStream(file,FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);
        string json = sr.ReadToEnd();
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
    public Game(){
        death=0;
        points=0;
        bestScore=0;
        totalScore=0;
        totalJumps=0;
    }
}


