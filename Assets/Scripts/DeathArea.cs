﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            if(other.gameObject.GetComponent<PlayerControler>().CheckDead()) return;
            Debug.Log("game over");
            other.gameObject.GetComponent<PlayerControler>().MakeDead();
            //Time.timeScale = 0;
            StartCoroutine(MainMenu());
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player"){
            if(other.gameObject.GetComponent<PlayerControler>().CheckDead()) return;
            Debug.Log("game over");
            //Time.timeScale = 0;
            other.gameObject.GetComponent<PlayerControler>().MakeDead();
            StartCoroutine(MainMenu());
        } 
    }

    IEnumerator MainMenu(){
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
}
