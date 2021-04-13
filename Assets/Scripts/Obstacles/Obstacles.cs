using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform obstacleHolder;
    [SerializeField] Transform[] obstacles;
    [SerializeField] int basePercent=80, LGpercent = 88, Shdpercent = 98, Shtpercent = 93, Fpercent = 100;
    [SerializeField] Transform[] bonusList;
    [SerializeField] ScoreCount score;
    //bool doneGenerate = true;
    public int multiplier = 1;
    float leftB = -3.1f, rightB = 3.1f;
    [SerializeField] float deadlyObstaclePercent = 10;
    [SerializeField] float movingObstaclePercent = 10;
    [SerializeField] float rotateObstaclePercent = 10;
    [SerializeField] float stickyObstaclePercent = 10;
    [SerializeField] float bounceObstaclePercent = 10;
    [SerializeField] int moveMultiplier = 8, deadMultiplier = 6, rotateMultiplier = 4, stickyM = 4, bounceM = 4;
    [SerializeField] float moveAdd = 0.1f, deadAdd = 0.1f, rotateAdd = 0.1f, bounceAdd = 0.1f, stickyAdd = 0.1f;
    bool deadlyOn =false;
    bool movingOn = false;
    bool rotateOn = false;
    bool stickyOn = false;
    bool bounceOn = false;
    bool isBounce=false,isDeadly=false,isSticky=false;
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
            GenerateBonuses(obsH.transform);
            float randRange=0f;
            float percent = 50;
            randRange = Random.Range(0f,1f);
            int maxC = 9 + (int)(multiplier/8 * randRange);
            int minC = 6 + (int)(multiplier/12 * randRange);
            int obstacleCount = Random.Range(minC,maxC);
            for(int i=0;i<=obstacleCount;i++){
                isSticky = false;
                isDeadly = false;
                isBounce = false;
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
                GameObject newObj = new GameObject();
                newObj.name = obs.gameObject.name;
                newObj.transform.position = obs.position;
                obs.parent = newObj.transform;
                newObj.transform.parent = obsH.transform;
                obs.GetComponent<SpriteRenderer>().color = Color.black;
                if(obs.gameObject.tag=="Circle"){
                    if(xscale>yscale) obs.localScale = new Vector3(xscale, xscale, 1);
                    else obs.localScale = new Vector3(yscale, yscale, 1);
                } 
                else obs.localScale = new Vector3(xscale, yscale, 1);
                if (deadlyOn && currentDeadlyCount < minimumDeadlyObstacleCount)
                {
                    currentDeadlyCount++;
                    MakeDeadly(obs);
                    isDeadly = true;
                }
                if(deadlyOn && currentDeadlyCount >= minimumDeadlyObstacleCount){
                    percent = Random.Range(1,101);
                    //randRange = Random.Range(-100,1);
                    if(percent+randRange<=deadlyObstaclePercent){
                        MakeDeadly(obs);
                        isDeadly = true;
                    }
                }
                if(bounceOn && !isDeadly){
                    percent = Random.Range(1, 101);
                    //randRange = Random.Range(-100, 1);
                    if (percent+  randRange <= bounceObstaclePercent)
                    {
                        MakeBounce(obs);
                        isBounce = true;
                    }
                }
                if(stickyOn && !isBounce && !isDeadly){
                    percent = Random.Range(0, 101);
                    //randRange = Random.Range(-100, 1);
                    if (percent + randRange <= stickyObstaclePercent)
                    {
                        MakeSticky(obs);
                        isSticky = true;
                    }
                }
                if(movingOn)
                {
                    percent = Random.Range(1, 101);
                    //randRange = Random.Range(-100, 1);
                    if (percent+ randRange <= movingObstaclePercent)
                    {
                        MakeMove(obs);
                    }
                }
                if(rotateOn)
                {
                    percent = Random.Range(1, 101);
                    //randRange = Random.Range(-100, 1);
                    if (percent+ randRange <= rotateObstaclePercent)
                    {
                        MakeRotate(obs);
                    }
                }
            }
            multiplier++;
            if(multiplier>=deadMultiplier) deadlyOn = true;
            if(multiplier>=moveMultiplier) movingOn = true;
            if(multiplier>=rotateMultiplier) rotateOn = true;
            if (multiplier >= bounceM) bounceOn = true;
            if (multiplier >= stickyM) stickyOn = true;
            if(deadlyOn)
            {
                if (multiplier % 22 == 0) minimumDeadlyObstacleCount++;
                currentDeadlyCount = 0;
                deadlyObstaclePercent += deadAdd;
                if (deadlyObstaclePercent > 30) deadlyObstaclePercent = 30;
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
            if (bounceOn)
            {
                bounceObstaclePercent += bounceAdd;
                if (bounceObstaclePercent > 20) bounceObstaclePercent = 20;
                Debug.Log("Bounce percent: " + bounceObstaclePercent);
            }
            if (stickyOn)
            {
                stickyObstaclePercent += stickyAdd;
                if (stickyObstaclePercent > 20) stickyObstaclePercent = 20;
                Debug.Log("Sticky percent: " + stickyObstaclePercent);
            }
        }

    }

    void GenerateBonuses(Transform parent){
        int percent = Random.Range(0,101);
        float rx = Random.Range(leftB, rightB);
        float ry = Random.Range(score.scoreInt + 9, score.scoreInt + 21);
        Vector3 pos = new Vector3(rx, ry, 0);
        if(percent<=basePercent) return;
        if(percent>basePercent && percent<=LGpercent){
            Transform newBonus = Instantiate(bonusList[0],pos,Quaternion.identity);
            newBonus.parent = parent;
            OverlapBonus(newBonus);
        }
        else if(percent>LGpercent && percent<=Shtpercent){
            Transform newBonus = Instantiate(bonusList[2], pos, Quaternion.identity);
            newBonus.parent = parent;
            OverlapBonus(newBonus);
        }
        else if (percent > Shtpercent && percent <= Shdpercent)
        {
            Transform newBonus = Instantiate(bonusList[1], pos, Quaternion.identity);
            newBonus.parent = parent;
            OverlapBonus(newBonus);
        }
        else if (percent > Shdpercent && percent <= Fpercent)
        {
            Transform newBonus = Instantiate(bonusList[3], pos, Quaternion.identity);
            newBonus.parent = parent;
            OverlapBonus(newBonus);
        }
    
    }

    void OverlapBonus(Transform bonus){
        //----
    }

    void MakeDeadly(Transform obstacle){
        obstacle.GetComponent<SpriteRenderer>().color = Color.red;
        obstacle.gameObject.layer = 9;
        obstacle.gameObject.AddComponent<DeathArea>();
        obstacle.GetComponent<DeathArea>().ISObstacle();
    }

    void MakeMove(Transform obstacle)
    {
        obstacle.parent.gameObject.AddComponent<MovementObstacle>();
        //obstacle.gameObject.AddComponent<MovementObstacle>();
    }

    void MakeRotate(Transform obstacle){
        obstacle.parent.gameObject.AddComponent<Rotatable>();
        //obstacle.gameObject.AddComponent<Rotatable>();
    }

    void MakeBounce(Transform obstacle){
        obstacle.GetComponent<SpriteRenderer>().color = Color.green;
        obstacle.gameObject.AddComponent<Bounce>();
    }
    void MakeSticky(Transform obstacle)
    {
        obstacle.GetComponent<SpriteRenderer>().color = Color.blue;
        obstacle.gameObject.AddComponent<Sticky>();
    }
}
