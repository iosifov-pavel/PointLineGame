using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasTouched : MonoBehaviour
{
    // Start is called before the first frame update
    bool wasTouched = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Player"){
            PlayerStats.stats.nonUniqueTouches++;
            if(wasTouched) return;
            wasTouched = true;
            PlayerStats.stats.GetTouch(transform);
        }
    }
}
