using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite dead;
    [SerializeField] Transform shield, shoot, engine, lowGravityTransform;
    [SerializeField] Button shootButton;
    [SerializeField] Text shootCount;
    [SerializeField] Slider flySlider;
    [SerializeField] Transform bullet;
    [SerializeField] float shieldTime = 10f;
    [SerializeField] float lowGravityTime = 10f;
    float actualGravity;
    [SerializeField] float flyTime = 8f;
    [SerializeField] int shoots = 0;
    [SerializeField] GameObject defeatPanel;
    float shieldTimer = 0, gravityTimer = 0, flyTimer=0;
    bool isDead = false, readyPanel = false;
    bool activeShield = false, flying = false, canShoot = false, lowGravity = false;
    bool switchSide = false;
    bool canSwitch = false;
    public bool isStick = false;
    //public bool isDead = false;
    CircleCollider2D circleCollider;
    Rigidbody2D rbbody;
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rbbody = GetComponent<Rigidbody2D>();
        actualGravity = rbbody.gravityScale;
    }

    bool CheckColliders(Vector2 point){
        Collider2D hit = Physics2D.OverlapCircle(point, circleCollider.radius*transform.localScale.x);
        if(hit!=null){
            if( hit.gameObject.tag == "Bonus" ) return false;
            else return true;
        }
        else return false;
    }

    void ReverseSpeed(Vector2 point){
        point.x*=-1;
        Vector2 newSpeed = rbbody.velocity;
        newSpeed.x*=-1;
        rbbody.velocity = newSpeed;
        transform.position = point;
    }

    IEnumerator SwitchDelay(){
        switchSide = true;
        yield return new WaitForSeconds(0.2f);
        switchSide = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x>3.3f && !switchSide){
            Vector2 point = new Vector3(-3.3f,transform.position.y,transform.position.z);
            if(CheckColliders(point)){
                ReverseSpeed(point);
                //StartCoroutine(SwitchDelay());
                return;
            } 
            else{
                transform.position = point;
                StartCoroutine(SwitchDelay());
            } 
        }
        else if(transform.position.x < -3.3f && !switchSide){
            Vector2 point = new Vector3(3.3f,transform.position.y,transform.position.z);
            if(CheckColliders(point)){
                ReverseSpeed(point);
                //StartCoroutine(SwitchDelay());
                return;
            }
            else{
                transform.position = point;
                StartCoroutine(SwitchDelay());
            } 
        }
        //---------
        if(activeShield){
            shieldTimer+=Time.deltaTime;
            if(shieldTimer>=shieldTime){
                activeShield = false;
                shield.gameObject.SetActive(false);
                shieldTimer = 0;
            }
        }
        if(lowGravity){
            if(rbbody.velocity.y<0){
                if(!isStick)rbbody.gravityScale = actualGravity/5f;
                else rbbody.gravityScale=0;
            }
            else{
                if(!isStick)rbbody.gravityScale = actualGravity;
                else rbbody.gravityScale=0;
            }
            gravityTimer+=Time.deltaTime;
            if(gravityTimer>=lowGravityTime){
                rbbody.gravityScale = actualGravity;
                lowGravity = false;
                gravityTimer = 0;
                lowGravityTransform.gameObject.SetActive(false);
            }
        }
        if(flying){
            float sliderValue = flySlider.value;
            rbbody.velocity = new Vector2(sliderValue*5f,8f);
            flyTimer+=Time.deltaTime;
            if(flyTimer>=flyTime){
                flying = false;
                engine.gameObject.SetActive(false);
                flySlider.gameObject.SetActive(false);
                flyTimer = 0;
                rbbody.gravityScale = actualGravity;
                Vector2 newVel = rbbody.velocity;
                newVel.x=0;
                rbbody.velocity = newVel;
                //circleCollider.isTrigger = false;
            }
        }
        shootCount.text = shoots.ToString();
    }

    public void MakeDead()
    {
        GetComponent<SpriteRenderer>().sprite = dead;
        isDead = true;
        SaveLoadManager.game.gameData.death++;
        PlayerStats.stats.CheckStats();
        SaveLoadManager.game.SaveToFile();
        StartCoroutine(Wait());

    }
    IEnumerator Wait(){
        yield return new WaitForSeconds(1.5f);
        defeatPanel.SetActive(true);
    }

    public bool CheckDead(){
        return isDead;
    }


    public bool CheckShield(){
        return activeShield;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bonus"){
            pickups thisPick = other.gameObject.GetComponent<PickUp>().GetPickUp();
            if(thisPick == pickups.shield){
                activeShield = true;
                shield.gameObject.SetActive(true);
                shieldTimer=0;
                PlayerStats.stats.shieldUp++;
            }
            else if(thisPick == pickups.shoot){
                canShoot = true;
                shootButton.gameObject.SetActive(true);
                shoot.gameObject.SetActive(true);
                shoots+=4;
                PlayerStats.stats.AccumulateShots(shoots);
                PlayerStats.stats.shootUp++;
            }
            else if(thisPick == pickups.fly){
                PlayerStats.stats.isFlying = true;
                flying = true;
                engine.gameObject.SetActive(true);
                flySlider.gameObject.SetActive(true);
                rbbody.velocity = Vector2.zero;
                rbbody.gravityScale = 0;
                flyTimer=0;
                PlayerStats.stats.flyUp++;
                //circleCollider.isTrigger = true;
            }
            else if(thisPick == pickups.lowGravity){
                lowGravity = true;
                lowGravityTransform.gameObject.SetActive(true);
                gravityTimer = 0;
                PlayerStats.stats.gravityUp++;
            }
            Destroy(other.gameObject);
            transform.rotation = Quaternion.identity;
        }
    }

    public void ShootBullet(){
        Transform newBullet = Instantiate(bullet,shoot.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0,9f);
        shoots--;
        if(shoots==0){
            canShoot = false;
            shoot.gameObject.SetActive(false);
            shootButton.gameObject.SetActive(false);
        }
    }
}
