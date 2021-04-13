using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerStats stats;
    public int jumps = 0;
    public int score = 0;
    public int maxJump =0;
    int previousScore = 0;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
