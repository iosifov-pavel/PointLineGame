using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    float minSpeed = 0.4f, maxSpeed = 2.2f;
    float speed = 0;
    Vector2 original, destination;
    Vector2 move = Vector2.zero;
    void Start()
    {
        speed = Random.Range(minSpeed,maxSpeed);
        float mX = Random.Range(-4f,4f);
        float mY = Random.Range(-4f, 4f);
        move = new Vector2(mX,mY);
        original = transform.position;
        destination = original+move;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,destination,speed*Time.deltaTime);
        if((Vector2)transform.position==destination)
        {
            Vector2 temp = destination;
            destination = original;
            original = temp;
        }
    }
}
