using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathArea : MonoBehaviour
{
    // Start is called before the first frame update
    bool isObstacle = false;
    void Start()
    {
        if(gameObject.layer == 8) isObstacle = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            MakeContact(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player"){
            MakeContact(other.gameObject);
        } 
    }

    public void ISObstacle(){
        isObstacle = true;
    }

    IEnumerator MainMenu(){
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }

    void MakeContact(GameObject player){
            if(player.GetComponent<PlayerControler>().CheckDead()) return;
            if(player.GetComponent<PlayerControler>().CheckShield() && isObstacle){
                return;
            }
            Debug.Log("game over");
            player.GetComponent<PlayerControler>().MakeDead();
            StartCoroutine(MainMenu());
    }
}
