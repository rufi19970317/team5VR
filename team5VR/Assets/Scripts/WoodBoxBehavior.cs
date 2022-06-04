using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class WoodBoxBehavior : MonoBehaviour
{
    private int saveWood = 0;
    public GameObject swood;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("SWood"))
        {
            Destroy(collision.transform.gameObject);
            saveWood = saveWood + 1;
            Debug.Log("Wood Left: " + saveWood);
        }        
    }

    public void TriggerEnter(SelectEnterEventArgs args)
    {
        if (saveWood >= 1)
        {
            Instantiate(swood, new Vector3(args.interactableObject.transform.position.x + 2, args.interactableObject.transform.position.y, args.interactableObject.transform.position.z), args.interactableObject.transform.rotation);
            saveWood = saveWood - 1;
        }
        Debug.Log("Wood Left: " + saveWood);
    }
}
