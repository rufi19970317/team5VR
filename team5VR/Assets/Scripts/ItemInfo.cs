using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    [SerializeField]
    private string itemID;
    public bool inSlot = false;
    public Vector3 slotRotation = Vector3.zero;
    public Vector3 defauultSize;
    public Vector3 offset;
    public Vector3 slotSize;
    // Start is called before the first frame update
    void Start()
    {
        if (!inSlot)
        {
            defauultSize = transform.localScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string GetId()
    {
        return itemID;
    }
}
