using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update
    CircleCollider2D circle;
    [SerializeField] pickups pick;
    public bool isOverlaping = false;
    public List<Collider2D> hits = new List<Collider2D>();
    public int num=0;

    void Start()
    {
        circle = GetComponent<CircleCollider2D>();
        OverlapSomethingStart();
    }

    // Update is called once per frame
    void Update()
    {
        OverlapSomething();
    }

    public void OverlapSomething(){
        num  = Physics2D.OverlapCollider(circle,new ContactFilter2D(),hits);
        if(num==0) return;
        foreach(Collider2D hit in hits){
            if(hit.gameObject.tag=="Bounce" || hit.gameObject.tag=="Player") continue;
            if(hit.transform.parent.gameObject.GetComponent<Rotatable>()!=null
            || hit.transform.parent.gameObject.GetComponent<MovementObstacle>()!=null
            || hit.transform.parent.gameObject.GetComponent<RotatePoint>()!=null) return;
            ColliderDistance2D colliderDistance = hit.Distance(circle);
                    if (colliderDistance.isOverlapped){
	                	transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
	                }
        }
    }

    void OverlapSomethingStart(){
        num  = Physics2D.OverlapCollider(circle,new ContactFilter2D(),hits);
        if(num==0) return;
        foreach(Collider2D hit in hits){
            if(hit.gameObject.tag=="Bounce" || hit.gameObject.tag=="Player") continue;
            ColliderDistance2D colliderDistance = hit.Distance(circle);
                    if (colliderDistance.isOverlapped){
	                	transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
	                }
        }
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
