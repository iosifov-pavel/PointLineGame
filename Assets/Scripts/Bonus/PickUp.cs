using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] pickups pick;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public pickups GetPickUp(){
        return pick;
    }
}

public enum pickups{
    shield,
    shoot,
    fly,
    lowGravity
}
