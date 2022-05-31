using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBQ : MonoBehaviour
{
    public bool isGrill = false;
    public float grilTime = 0;

    void Update()
    {
        if(isGrill)
        {
            grilTime += Time.deltaTime;
        }
    }
}
