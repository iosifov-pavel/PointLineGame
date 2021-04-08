﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite dead;
    bool switchSide = false;
    bool canSwitch = false;
    //public bool isDead = false;
    CircleCollider2D circleCollider;
    Rigidbody2D rbbody;
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rbbody = GetComponent<Rigidbody2D>();
    }

    bool CheckColliders(Vector2 point){
        Collider2D hit = Physics2D.OverlapPoint(point);
        if(hit!=null){
            return true;
        }
        else return false;
    }

    void ReverseSpeed(){
        Vector2 newSpeed = rbbody.velocity;
        newSpeed.x*=-1;
        rbbody.velocity = newSpeed;
    }

    IEnumerator SwitchDelay(){
        switchSide = true;
        yield return new WaitForSeconds(0.2f);
        switchSide = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x>=3.2f && !switchSide){
            Vector2 point = new Vector3(-3.2f,transform.position.y,transform.position.z);
            if(CheckColliders(point)){
                ReverseSpeed();
                return;
            } 
            else{
                transform.position = point;
                StartCoroutine(SwitchDelay());
            } 
        }
        else if(transform.position.x <= -3.2f && !switchSide){
            Vector2 point = new Vector3(3.2f,transform.position.y,transform.position.z);
            if(CheckColliders(point)){
                ReverseSpeed();
                return;
            }
            else{
                transform.position = point;
                StartCoroutine(SwitchDelay());
            } 
        }
    }

    public void MakeDead()
    {
        GetComponent<SpriteRenderer>().sprite = dead;
    }
}
