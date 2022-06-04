using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillManager : MonoBehaviour
{
    public AudioSource audio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BBQ"))
        {
            if (other.GetComponent<BBQ>() != null)
            {
                BBQ bbq = other.GetComponent<BBQ>();
                bbq.isGrill = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("BBQ"))
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BBQ"))
        {
            if (other.GetComponent<BBQ>() != null)
            {
                other.GetComponent<BBQ>().isGrill = false;
            }
            if (audio.isPlaying)
            {
                audio.Pause();
            }
        }
    }
}
