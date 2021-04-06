﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSphere : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float bounceRadiusPower = 1f;
    [SerializeField] float bounceConstPower = 1.25f;
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
            playerBody = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDirection = other.transform.position - transform.position;
            forceDirection.Normalize();
            //playerBody.velocity = Vector2.zero;
            playerBody.velocity *= 0.75f;
            playerBody.AddForce(forceDirection*(bounceConstPower+bounceRadiusPower*1.25f),ForceMode2D.Impulse);
            playerBody.velocity = Vector2.ClampMagnitude(playerBody.velocity,6*bounceRadiusPower);
            if(destroyable) Destroy(gameObject);
        }
    }
}
