using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeViewController : MonoBehaviour
{
    public Camera mainCam;
    public GameObject origin;
    public GameObject sub;
    public Material blackPanel;

    public float aaa = 0;
    public float a = 0;

    private Vector3 pos;
    private Vector3 rot;

    bool isfade = false;
    public int play = 0;
    // Start is called before the first frame update
    void Start()
    {
        isfade = false;
        a = 1f;
    }

    // Update is called once per frame
    void Update()
    {
       if(!isfade) StartCoroutine(FadeinCoroutine());
       else StartCoroutine(FadeoutCoroutine());

    }

    public int getPlay()
    {
        return play;
    }

    public void setPlay(int i)
    {
        play = i;
    }


    IEnumerator FadeinCoroutine()
    {
        while (a > 0f)
        {
            a -= 0.005f;
            yield return new WaitForSeconds(0.01f);
            blackPanel.color = new Color(0, 0, 0, a);

            if (a <= 0f) { 
                isfade = true;
                sub.SetActive(false);
                sub.SetActive(true);
            }
        }
    }

    IEnumerator FadeoutCoroutine()
    {
        while (aaa < 1.0f)
        {
            aaa += 0.0005f;
            yield return new WaitForSeconds(0.01f);
        }

        while (a < 1.0f)
        {
            a += 0.0005f;
            yield return new WaitForSeconds(0.01f);
            blackPanel.color = new Color(0, 0, 0, a);

            if (a >= 1.0f)
            {
                play = 1;
                isfade = false;
                a = 1f;
                aaa = 0f;
            }
        }       
    }
}
