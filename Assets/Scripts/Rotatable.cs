using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{
    // Start is called before the first frame update
    float speed;
    Vector3 rotated;
    void Start()
    {
        speed = Random.Range(-3,3);
        rotated = new Vector3(0,0,speed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotated);
    }
}
