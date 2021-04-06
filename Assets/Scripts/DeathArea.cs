using System.Collections;
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
            Debug.Log("game over");
            //Time.timeScale = 0;
            StartCoroutine(MainMenu());
        }
    }

    IEnumerator MainMenu(){
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
}
