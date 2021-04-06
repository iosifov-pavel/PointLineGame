using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUpFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform target;
    [SerializeField] bool followX = false;
    [SerializeField] Transform deathX;
    bool switchSide = false;
    float maxTargetY;
    void Start()
    {
        maxTargetY = (target.position.y);
        if(!followX){
            //deathX.gameObject.SetActive(true);
        }
    }
    
    void Update(){
        if(target.position.x>=3.3f && !switchSide){
            target.position = new Vector3(-3.3f,target.position.y,target.position.z);
            StartCoroutine(SwitchDelay());
        }
        else if(target.position.x <= -3.3f && !switchSide){
            target.position = new Vector3(3.3f, target.position.y, target.position.z);
            StartCoroutine(SwitchDelay());
        }
    }

    IEnumerator SwitchDelay(){
        switchSide = true;
        yield return new WaitForSeconds(0.2f);
        switchSide = false;
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
