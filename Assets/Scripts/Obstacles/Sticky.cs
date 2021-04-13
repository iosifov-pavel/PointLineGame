using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerControler playerControler=null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Player"){
            Rigidbody2D player = other.gameObject.GetComponent<Rigidbody2D>();
            playerControler = other.gameObject.GetComponent<PlayerControler>();
            playerControler.isStick=true;
            player.velocity = Vector2.zero;
            player.gravityScale = 0;
            other.transform.SetParent(transform.parent);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag=="Player"){
            Rigidbody2D player = other.gameObject.GetComponent<Rigidbody2D>();
            playerControler.isStick=false;
            player.gravityScale = 0.8f;
            other.transform.parent = null;
            other.transform.rotation = Quaternion.identity;
        }
    }
}
