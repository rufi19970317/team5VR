using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeXRController : MonoBehaviour
{
    public GameObject scope;
    public GameObject origin;
    public Camera mainCam;
    public Camera scopeCam;
    public GameObject sub;
    int p = 0;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       p = scope.GetComponent<ScopeViewController>().getPlay();
        if (p == 1 && !origin.activeSelf)
        {
            scope.GetComponent<ScopeViewController>().setPlay(0);
            scopeCam.gameObject.SetActive(false);
            sub.SetActive(false);
            origin.SetActive(true);
            mainCam.gameObject.SetActive(true);
        }
    }
}
