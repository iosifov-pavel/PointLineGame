using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePoint : MonoBehaviour
{
    // Start is called before the first frame update
    float speed = 0;
    Vector2 point = Vector2.zero;
    void Start()
    {
        speed = Random.Range(-2f,2f);
        point = Random.onUnitSphere*2;
        point +=(Vector2)transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(point,new Vector3(0,0,1),speed*12*Time.deltaTime);
    }
}
