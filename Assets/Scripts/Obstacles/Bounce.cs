using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    // Start is called before the first frame update
    Collider2D collider2d;
    void Start()
    {
        collider2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        foreach(ContactPoint2D contact in other.contacts){
            if(contact.collider.gameObject.tag=="Player"){
                Rigidbody2D player = other.gameObject.GetComponent<Rigidbody2D>();
                Vector2 direction = -contact.normal;
                float force = (transform.localScale.x + transform.localScale.y)/2;
                player.velocity *=0.55f;
                player.AddForce(direction*force*4, ForceMode2D.Impulse);
            }
        }
    }
}
