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
    GooglePain pain;
    string firstBlood = "CgkIlo304KkXEAIQAg";
    string thatWasEasy = "CgkIlo304KkXEAIQAw";
    string wellPlayed = "CgkIlo304KkXEAIQBA";
    string evenMoreFun = "CgkIlo304KkXEAIQBQ";
    string littleAddicted = "CgkIlo304KkXEAIQBg";
    string littleHero = "CgkIlo304KkXEAIQBw";
    string hero = "CgkIlo304KkXEAIQCA";
    string bigHero = "CgkIlo304KkXEAIQCQ";
    string highJump = "CgkIlo304KkXEAIQCg";
    string iLoveJumping = "CgkIlo304KkXEAIQCw";
    string jumpAddicted = "CgkIlo304KkXEAIQDA";
    string oneKmUP = "CgkIlo304KkXEAIQDQ";
    string revive = "CgkIlo304KkXEAIQDg";

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
        if(mJ >= maxJump){
            maxJump = mJ;
            if(maxJump>SaveLoadManager.game.gameData.maxJump){
                SaveLoadManager.game.gameData.maxJump = maxJump; 
            }   
        }  
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

    public void CheckStats(){
        pain = FindObjectOfType<GooglePain>();
        Achivs();
        if(score>SaveLoadManager.game.gameData.bestScore){
            SaveLoadManager.game.gameData.bestScore = score;
            pain.SetBoardScore(score);
        }
        SaveLoadManager.game.gameData.totalScore+=score;
        SaveLoadManager.game.gameData.totalJumps+=jumps;
        SaveLoadManager.game.gameData.jumpPerPlay = (float)SaveLoadManager.game.gameData.totalJumps / (float)SaveLoadManager.game.gameData.death;
        SaveLoadManager.game.gameData.scorePerPlay = (float)SaveLoadManager.game.gameData.totalScore / (float)SaveLoadManager.game.gameData.death;
        SaveLoadManager.game.gameData.totalUps+=totalUps;
        SaveLoadManager.game.gameData.totalDestroyedObstacle+=totalDestroyed;
    }

    void Achivs(){
        pain.CheckAchivs(firstBlood);
        if(jumps>=100) pain.CheckAchivs(iLoveJumping);
        if(jumps>=250) pain.CheckAchivs(jumpAddicted);
        if(SaveLoadManager.game.gameData.totalScore>=1000) pain.CheckAchivs(oneKmUP);
        if(maxJump>=25) pain.CheckAchivs(highJump);
        if(score==0) pain.CheckAchivs(thatWasEasy);
        if(score>=100) pain.CheckAchivs(wellPlayed);
        if(score>=200) pain.CheckAchivs(littleHero);
        if(score>=350) pain.CheckAchivs(hero);
        if(score>=500) pain.CheckAchivs(bigHero);
        if(SaveLoadManager.game.gameData.death>=10) pain.CheckAchivs(evenMoreFun);
        if(SaveLoadManager.game.gameData.death>=100) pain.CheckAchivs(littleAddicted);
    }

    public void ReviveAchiv(){
        pain.CheckAchivs(revive);
    }

    

}
