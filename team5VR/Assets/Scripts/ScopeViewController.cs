using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeViewController : MonoBehaviour
{
    public Camera mainCam;
    public GameObject origin;
    public GameObject sub;

    public float xmove = 0;
    public float ymove = 0;
    public float aaa = 0;
    private Vector3 pos;
    private Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        rot = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FadeCoroutine());
        
    }

    //일단 마우스로
    void Zoom()
    {
        
    }

    IEnumerator FadeCoroutine()
    {
        while (aaa < 1.0f)
        {
            aaa += 0.00005f;
            yield return new WaitForSeconds(0.01f);

            if (aaa >= 1.0f) {
                aaa = 0;
                origin.SetActive(true);
                mainCam.gameObject.SetActive(true);
                sub.SetActive(false);
                this.gameObject.SetActive(false);
                
            }
        }
    }
}
