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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int x = score.scoreInt/20;
        Debug.Log(x);
        if(score.scoreInt/15==multiplier){
            int obstacleCount = Random.Range(5,10);
            for(int i=0;i<=obstacleCount;i++){
                float rx = Random.Range(leftB,rightB);
                float ry = Random.Range(score.scoreInt+8,score.scoreInt+22);
                Vector3 pos = new Vector3(rx,ry,0);
                float xscale = Random.Range(0.4f,2.2f);
                float yscale = Random.Range(0.4f,2.2f);
                float ra = Random.Range(0,360);
                int obstNumber = Random.Range(0,4);
                Transform obs = Instantiate(obstacles[obstNumber],pos,Quaternion.Euler(0,0,ra));
                obs.localScale = new Vector3(xscale,yscale,1);
            }
            multiplier++;
        }
        else{
            
        }
    }
}
