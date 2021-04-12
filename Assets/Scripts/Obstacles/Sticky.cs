using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 scaling;
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
            player.velocity = Vector2.zero;
            player.gravityScale = 0;
            other.transform.SetParent(transform.parent);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag=="Player"){
            Rigidbody2D player = other.gameObject.GetComponent<Rigidbody2D>();
            player.gravityScale = 0.8f;
            other.transform.parent = null;
            other.transform.rotation = Quaternion.identity;
        }
    }
}
