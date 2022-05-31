using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillManager : MonoBehaviour
{
    [SerializeField]
    private GameObject BBQ;

    [SerializeField]
    private GameObject Garbage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("RawBBQ"))
        {
            BBQ bbq = other.GetComponent<BBQ>();
            bbq.isGrill = true;
            if(bbq.grilTime > 15f)
            {
                Instantiate(BBQ, bbq.transform.position, bbq.transform.rotation);
                Destroy(bbq);
            }
        }
        else if (other.CompareTag("BBQ"))
        {
            BBQ bbq = other.GetComponent<BBQ>();
            bbq.isGrill = true;
            bbq.grilTime = 15f;
            if (bbq.grilTime > 30f)
            {
                Instantiate(Garbage, bbq.transform.position, bbq.transform.rotation);
                Destroy(bbq);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RawBBQ") || other.CompareTag("BBQ"))
        {
            other.GetComponent<BBQ>().isGrill = false;
        }
    }
}
