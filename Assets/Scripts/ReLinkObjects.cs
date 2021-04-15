using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReLinkObjects : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button b1;
    void Start()
    {
        GooglePain pain = FindObjectOfType<GooglePain>();
        b1.onClick.AddListener(pain.ShowLeaderBoard); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
