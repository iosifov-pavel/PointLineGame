using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUpFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform target;
    [SerializeField] bool followX = false;
    [SerializeField] Transform deathX;
    float maxTargetY;
    void Start()
    {
        //Screen.SetResolution(1080,1920,true);
        maxTargetY = (target.position.y);
        if(!followX){
            //deathX.gameObject.SetActive(true);
        }
    }
    
    void Update(){

    }



    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 position = transform.position;
        if(maxTargetY<target.position.y){
            position.y = target.position.y+2;
            maxTargetY = target.position.y;
        }
        if(followX)position.x = target.position.x;
        transform.position = Vector3.Lerp (transform.position, position, 6*Time.deltaTime);
    }
}
