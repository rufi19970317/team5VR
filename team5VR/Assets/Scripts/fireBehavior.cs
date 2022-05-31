using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class fireBehavior : MonoBehaviour
{
    public GameObject fireFX;
    private int woodNum = 0;
    public float time;
    bool fireON = false;

    void Start()
    {
        fireFX.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("SWood"))
        {
            Destroy(collision.transform.gameObject);
            woodNum = woodNum + 1;
            time = 300;
            Debug.Log(woodNum);
        }

        if (woodNum >= 3 && collision.transform.CompareTag("Torch"))
        {
            fireFX.SetActive(true);
            fireON = true;
        }        
    }

    void fireBurn() {
        if (time == 0)
        {
            fireON = false;
            fireFX.SetActive(false);
        }
    }

    void Update()
    {
        if(fireON == true && time >= 0)
        {
            time = time - Time.deltaTime;
            if (time < 0) {
                time = 0;
            }
        }
        fireBurn();        
    }
}