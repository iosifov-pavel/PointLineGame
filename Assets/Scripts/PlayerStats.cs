using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerStats stats;
    public int jumps = 0;
    public int shieldUp = 0, flyUp=0, shootUp=0, gravityUp=0;
    int totalUps=0;
    public int score = 0;
    public int maxJump =0;
    int previousScore = 0;
    public int obstacleByBullet = 0;
    public int obstacleByShield = 0;
    public int maxShots = 0;
    public int nonUniqueTouches=0;
    public int baseTouch=0, bounceTouch=0, deadlyTouch=0, stickyTouch=0, totalTouch=0;
    public int baseShot=0, bounceShot=0, deadlyShot=0, stickyShot=0, totalShot=0;

    private void Awake() {
        stats = this;    
    }
    void Start()
    {

    }

    public void CalculateJump(){
        jumps++;
        int mJ = score - previousScore;
        if(mJ >= maxJump) maxJump = mJ;    
        previousScore = score;
    }

    public void AccumulateShots(int shots){
        if(shots>maxShots) maxShots = shots;
    }

    // Update is called once per frame
    void Update()
    {
        totalUps = shieldUp+shootUp+flyUp+gravityUp;
        totalTouch = baseTouch+deadlyTouch+bounceTouch+stickyTouch;
        totalShot = baseShot+deadlyShot+bounceShot+stickyShot;
    }

    public void GetTouch(Transform obstacle){
        if(obstacle.gameObject.GetComponent<DeathArea>()!=null) deadlyTouch++;
        else if(obstacle.gameObject.GetComponent<Sticky>()!=null) stickyTouch++;
        else if(obstacle.gameObject.GetComponent<Bounce>()!=null) bounceTouch++;
        else baseTouch++;
    }

    public void GetShot(Transform obstacle){
        if(obstacle.gameObject.GetComponent<DeathArea>()!=null) deadlyShot++;
        else if(obstacle.gameObject.GetComponent<Sticky>()!=null) stickyShot++;
        else if(obstacle.gameObject.GetComponent<Bounce>()!=null) bounceShot++;
        else baseShot++;
    }
}
