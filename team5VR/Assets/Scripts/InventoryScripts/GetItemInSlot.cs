using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GetItemInSlot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetItem(SelectEnterEventArgs args)
    {
        GameObject temp = args.interactableObject.transform.gameObject;
        if (temp.CompareTag("Slot"))
        {
            Slot slot = temp.GetComponent<Slot>();
            if (slot.IsinItem())
            {
                slot.SpawnItem(transform);
            }
        }
    }
}
