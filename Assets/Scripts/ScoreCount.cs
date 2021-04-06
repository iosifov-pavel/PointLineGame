﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text score;
    public int scoreInt=0;
    int startPosition;
    int currentPosition;
    void Start()
    {
        score.text = scoreInt.ToString();
        startPosition = (int)transform.position.y;
        if(startPosition<0) startPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = (int)transform.position.y;
        if(currentPosition<0) currentPosition = 0;
        if(currentPosition>startPosition){
            scoreInt = (currentPosition);
            startPosition = currentPosition;
            score.text = scoreInt.ToString();
        }
    }
}
