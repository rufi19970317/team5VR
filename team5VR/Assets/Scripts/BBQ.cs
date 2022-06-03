using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBQ : MonoBehaviour
{
    public bool isGrill = false;
    private float grilTime = 0;

    [SerializeField]
    private GameObject bbq;

    void Update()
    {
        if(isGrill)
        {
            grilTime += Time.deltaTime;
        }


        if (grilTime > 15f)
        {
            Instantiate(bbq, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
