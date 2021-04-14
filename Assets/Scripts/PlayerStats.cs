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
    public int baseShot=0, bounceShot=0, deadlyShot=0, stickyShot=0;
    public int baseShield=0, bounceShield=0, deadlyShield=0, stickyShield=0;
    public int totalDestroyed=0;
    public bool isFlying = false;
    bool blockedCount = false;

    private void Awake() {
        stats = this;    
    }
    void Start()
    {

    }

    public void CalculateJump(){
        if(isFlying && !blockedCount){
            blockedCount = true;
            return;
        } 
        if(isFlying && blockedCount) return;
        if(!isFlying && blockedCount){
            blockedCount = false;
            jumps++;
            previousScore = score;
            return;
        }
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
        obstacleByBullet = baseShot + bounceShot + stickyShot + deadlyShot;
        obstacleByShield = baseShield + bounceShield + stickyShield + deadlyShield;
        totalDestroyed = obstacleByBullet+obstacleByShield;
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

    public void GetShield(Transform obstacle){
        if(obstacle.gameObject.GetComponent<DeathArea>()!=null) deadlyShield++;
        else if(obstacle.gameObject.GetComponent<Sticky>()!=null) stickyShield++;
        else if(obstacle.gameObject.GetComponent<Bounce>()!=null) bounceShield++;
        else baseShield++;
    }

    public void CheckBestScore(){
        if(score>SaveLoadManager.game.gameData.bestScore){
            SaveLoadManager.game.gameData.bestScore = score;
        }
    }
}
