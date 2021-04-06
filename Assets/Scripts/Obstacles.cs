using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform[] obstacles;
    [SerializeField] ScoreCount score;
    bool doneGenerate = true;
    int multiplier = 1;
    float leftB = -3.1f, rightB = 3.1f;
    int deadlyObstaclePercent = 10;
    bool deadlyOn =false;
    bool movingOn = false;
    int minimumDeadlyObstacleCount = 1;
    int currentDeadlyCount =0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int x = score.scoreInt/12;
        if(x==multiplier){
            Debug.Log("multiplier="+x);
            int obstacleCount = Random.Range(5,10+multiplier/10);
            for(int i=0;i<=obstacleCount;i++){
                float rx = Random.Range(leftB,rightB);
                float ry = Random.Range(score.scoreInt+9,score.scoreInt+21);
                Vector3 pos = new Vector3(rx,ry,0);
                float xscale = Random.Range(0.4f,2.2f + multiplier / 20f);
                float yscale = Random.Range(0.4f,2.2f + multiplier / 20f);
                float ra = Random.Range(0,360);
                int obstNumber = Random.Range(0,3);
                Transform obs = Instantiate(obstacles[obstNumber],pos,Quaternion.Euler(0,0,ra));
                obs.GetComponent<SpriteRenderer>().color = Color.black;
                obs.localScale = new Vector3(xscale,yscale,1);
                if (deadlyOn && currentDeadlyCount < minimumDeadlyObstacleCount)
                {
                    currentDeadlyCount++;
                    MakeDeadly(obs);
                }
                else if(deadlyOn && currentDeadlyCount >= minimumDeadlyObstacleCount){
                    int percent = Random.Range(1,101);
                    if(percent<=deadlyObstaclePercent){
                        MakeDeadly(obs);
                    }
                }
            }
            multiplier++;
            if(multiplier>=4) deadlyOn = true;
            currentDeadlyCount = 0;
            deadlyObstaclePercent++;
            if(deadlyObstaclePercent>50) deadlyObstaclePercent = 50;
            Debug.Log("percent: "+deadlyObstaclePercent);
        }

    }

    void MakeDeadly(Transform obstacle){
        obstacle.GetComponent<SpriteRenderer>().color = Color.red;
        obstacle.gameObject.AddComponent<DeathArea>();
    }
}
