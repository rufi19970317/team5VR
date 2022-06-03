using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillManager : MonoBehaviour
{    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("RawBBQ"))
        {
            BBQ bbq = other.GetComponent<BBQ>();
            bbq.isGrill = true;
        }
        else if (other.CompareTag("BBQ"))
        {
            BBQ bbq = other.GetComponent<BBQ>();
            bbq.isGrill = true;
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
