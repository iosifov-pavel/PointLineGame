using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSphere : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float bounceRadiusPower = 1f;
    [SerializeField] float bounceConstPower = 1.75f;
    CircleCollider2D circleCollider;
    public bool destroyable = false;
    Rigidbody2D playerBody;
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bounceRadiusPower = transform.localScale.x;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Player"){
            if(other.gameObject.GetComponent<PlayerControler>().CheckDead()) return;
            playerBody = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDirection = other.transform.position - transform.position;
            forceDirection.Normalize();
            //playerBody.velocity = Vector2.zero;
            playerBody.velocity *= 0.55f;
            playerBody.AddForce(forceDirection*(bounceConstPower+bounceRadiusPower*1.75f),ForceMode2D.Impulse);
            playerBody.velocity = Vector2.ClampMagnitude(playerBody.velocity,7.5f*bounceRadiusPower);
            if(destroyable) Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag=="Player"){
            if(other.gameObject.GetComponent<PlayerControler>().CheckDead()) return;
            playerBody = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDirection = other.transform.position - transform.position;
            forceDirection.Normalize();
            //playerBody.velocity = Vector2.zero;
            playerBody.velocity *= 0.55f;
            playerBody.AddForce(forceDirection*(bounceConstPower+bounceRadiusPower*1.75f),ForceMode2D.Impulse);
            playerBody.velocity = Vector2.ClampMagnitude(playerBody.velocity,7.5f*bounceRadiusPower);
            if(destroyable) Destroy(gameObject);
        }
    }
}
