using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    [SerializeField]
    private string itemID;
    public Vector3 slotRotation = Vector3.zero;
    public Vector3 defauultSize;
    // Start is called before the first frame update
    void Start()
    {
        defauultSize = transform.localScale;
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
