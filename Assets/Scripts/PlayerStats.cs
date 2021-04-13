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
    }
}
