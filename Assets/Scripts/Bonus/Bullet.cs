using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField ] bool isShield = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player" || other.gameObject.tag=="Bonus" || other.gameObject.tag=="Bounce") return;
        else{
            Destroy(other.gameObject);
            if(!isShield)Destroy(gameObject);
        }
    }
}
