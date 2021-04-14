using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateSphere : MonoBehaviour
{
    bool touchOnScreen = false;
    private Camera cam;
    Vector2 pointPosition = Vector2.zero;
    [SerializeField] GameObject spherePrefab;
    [SerializeField] int maxSpheresCount = 4;
    [SerializeField] bool destroyable = false;
    [SerializeField] Text startText;
    [SerializeField] Text pauseText;
    [SerializeField] Button PauseButton;
    [SerializeField] PlayerControler player;
    [SerializeField] float maxScale = 12f;
    int spheresCount = 0;
    bool startGame = false;
    GameObject activeSphere = null;
    public bool pause = false;
    public bool continuePressed = false;
    Queue<GameObject> spheres = new Queue<GameObject>();

    [SerializeField] GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    // Start is called before the first frame update
    void Start()
    {
        PauseButton.gameObject.SetActive(false);
        cam = Camera.main;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.CheckDead()) return;
        if(Input.GetMouseButton(0) && ((!startGame) || (pause && continuePressed && startGame))){
            CheckUI();
            if(touchOnScreen) return;
            startGame = true;
            pause = false;
            continuePressed = false;
            PauseButton.gameObject.SetActive(true);
            startText.gameObject.SetActive(false);
            pauseText.gameObject.SetActive(false);
            Time.timeScale = 1f;
            touchOnScreen = true;
        }
        if(!startGame || pause) return;
        if(Input.GetMouseButton(0) && !touchOnScreen){
            CheckUI();
            if(touchOnScreen) return;
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

    void CheckUI(){
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
            Debug.Log(result.gameObject.tag);
        }
        if(results.Count==1 && results[0].gameObject.tag=="NotUI") touchOnScreen = false;
        else if(results.Count==0) touchOnScreen = false;
        else touchOnScreen = true;
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
