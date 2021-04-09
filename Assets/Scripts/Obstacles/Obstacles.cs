using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform obstacleHolder;
    [SerializeField] Transform[] obstacles;
    [SerializeField] ScoreCount score;
    //bool doneGenerate = true;
    public int multiplier = 1;
    float leftB = -3.1f, rightB = 3.1f;
    [SerializeField] float deadlyObstaclePercent = 10;
    [SerializeField] float movingObstaclePercent = 10;
    [SerializeField] float rotateObstaclePercent = 10;
    [SerializeField] int moveMultiplier = 8, deadMultiplier = 6, rotateMultiplier = 4;
    [SerializeField] float moveAdd = 0.1f, deadAdd = 0.1f, rotateAdd = 0.1f;
    bool deadlyOn =false;
    bool movingOn = false;
    bool rotateOn = false;
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
            if(multiplier>=3){
               Destroy(obstacleHolder.GetChild(0).gameObject);
            }
            GameObject obsH = new GameObject();
            obsH.name = multiplier.ToString();
            obsH.transform.parent = obstacleHolder;
            int maxC = 8 + multiplier/8;
            int minC = 5 + multiplier/10;
            int obstacleCount = Random.Range(minC,maxC);
            for(int i=0;i<=obstacleCount;i++){
                float rx = Random.Range(leftB,rightB);
                float ry = Random.Range(score.scoreInt+9,score.scoreInt+21);
                Vector3 pos = new Vector3(rx,ry,0);
                float sizeRandom = Random.Range(0f,1f);
                float maxS = 2.2f + multiplier / 24f * sizeRandom;
                float minS = 0.4f;
                float xscale = Random.Range(minS,maxS);
                float yscale = Random.Range(minS,maxS);
                float ra = Random.Range(0,360);
                int obstNumber = Random.Range(0,4);
                Transform obs = Instantiate(obstacles[obstNumber],pos,Quaternion.Euler(0,0,ra));
                obs.parent = obsH.transform;
                obs.GetComponent<SpriteRenderer>().color = Color.black;
                if(obs.gameObject.tag=="Circle"){
                    obs.localScale = new Vector3(xscale, xscale, 1);
                } 
                else obs.localScale = new Vector3(xscale, yscale, 1);
                float randRange=1f;
                if (deadlyOn && currentDeadlyCount < minimumDeadlyObstacleCount)
                {
                    currentDeadlyCount++;
                    MakeDeadly(obs);
                }
                else if(deadlyOn && currentDeadlyCount >= minimumDeadlyObstacleCount){
                    float percent = Random.Range(1,101);
                    randRange = Random.Range(0f,1f);
                    if(percent*randRange<=deadlyObstaclePercent){
                        MakeDeadly(obs);
                    }
                }
                if(movingOn)
                {
                    float percent = Random.Range(1, 101);
                    randRange = Random.Range(0f,1f);
                    if (percent*randRange <= movingObstaclePercent)
                    {
                        MakeMove(obs);
                    }
                }
                if(rotateOn)
                {
                    float percent = Random.Range(1, 101);
                    randRange = Random.Range(0f,1f);
                    if (percent*randRange <= rotateObstaclePercent)
                    {
                        MakeRotate(obs);
                    }
                }
            }
            multiplier++;
            if(multiplier>=deadMultiplier) deadlyOn = true;
            if(multiplier>=moveMultiplier) movingOn = true;
            if(multiplier>=rotateMultiplier) rotateOn = true;
            if(deadlyOn)
            {
                if (multiplier % 22 == 0) minimumDeadlyObstacleCount++;
                currentDeadlyCount = 0;
                deadlyObstaclePercent += deadAdd;
                if (deadlyObstaclePercent > 45) deadlyObstaclePercent = 45;
                Debug.Log("percent: " + deadlyObstaclePercent);
            }
            if(movingOn)
            {
                movingObstaclePercent += moveAdd;
                if (movingObstaclePercent > 35) movingObstaclePercent = 35;
                Debug.Log("Move percent: " + movingObstaclePercent);
            }
            if(rotateOn)
            {
                rotateObstaclePercent += rotateAdd;
                if (deadlyObstaclePercent > 25) deadlyObstaclePercent = 25;
                Debug.Log("Rotate percent: " + rotateObstaclePercent);
            }
        }

    }

    void MakeDeadly(Transform obstacle){
        obstacle.GetComponent<SpriteRenderer>().color = Color.red;
        obstacle.gameObject.layer = 9;
        obstacle.gameObject.AddComponent<DeathArea>();
    }

    void MakeMove(Transform obstacle)
    {
        obstacle.gameObject.AddComponent<MovementObstacle>();
    }

    void MakeRotate(Transform obstacle){
        obstacle.gameObject.AddComponent<Rotatable>();
    }
}
