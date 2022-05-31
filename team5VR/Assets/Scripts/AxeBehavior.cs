using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class AxeBehavior : MonoBehaviour
{
    public GameObject smallWood;    
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wood") && collision.transform.position.y > 1) {
            Instantiate(smallWood, new Vector3(collision.transform.position.x,collision.transform.position.y,collision.transform.position.z), collision.transform.rotation);
            Instantiate(smallWood, new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z+0.3f), collision.transform.rotation);
            Destroy(collision.transform.gameObject);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
