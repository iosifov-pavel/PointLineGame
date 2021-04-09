using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateSphere : MonoBehaviour
{
    bool touchOnScreen = false;
    private Camera cam;
    Vector2 pointPosition = Vector2.zero;
    [SerializeField] GameObject spherePrefab;
    [SerializeField] int maxSpheresCount = 4;
    [SerializeField] bool destroyable = false;
    [SerializeField] Text startText;
    [SerializeField] PlayerControler player;
    [SerializeField] float maxScale = 12f;
    int spheresCount = 0;
    bool startGame = false;
    GameObject activeSphere = null;
    Queue<GameObject> spheres = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.CheckDead()) return;
        if(Input.GetMouseButton(0) && !startGame){
            startGame = true;
            startText.gameObject.SetActive(false);
            Time.timeScale = 1f;
            touchOnScreen = true;
        }
        if(Input.GetMouseButton(0) && !touchOnScreen){
            touchOnScreen = true;
            Vector2 pos = Input.mousePosition;
            pointPosition = cam.ScreenToWorldPoint(pos);
            activeSphere = CreateSphereOnScreen(pointPosition);
            if(!destroyable){
                spheres.Enqueue(activeSphere);
                spheresCount++;
                if(spheresCount>maxSpheresCount){
                    GameObject sphereDestroyed = spheres.Dequeue();
                    Destroy(sphereDestroyed);
                    spheresCount--;
                }
            }
        }
        if(Input.GetMouseButton(0) && touchOnScreen && activeSphere!=null){
            Vector2 scale = activeSphere.transform.localScale;
            scale.x +=Time.deltaTime*8;
            if(scale.x>maxScale) scale.x = maxScale;
            scale.y = scale.x;
            activeSphere.transform.localScale = scale;
        }

        if(Input.GetMouseButtonUp(0)){
            touchOnScreen = false;
            activeSphere = null;
        }
    }

    GameObject CreateSphereOnScreen(Vector2 spherePosition){
        GameObject newSphere = Instantiate(spherePrefab,spherePosition,Quaternion.identity);
        if(destroyable){
            newSphere.GetComponent<BounceSphere>().destroyable = true;
        }
        return newSphere;
    }

    IEnumerator KillSphereAfterTime(GameObject sphere, float time){
        yield return new WaitForSeconds(time);
        Destroy(sphere);
    }
}
