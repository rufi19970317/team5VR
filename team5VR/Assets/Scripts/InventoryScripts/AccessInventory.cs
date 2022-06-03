using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessInventory : MonoBehaviour
{
    public GameObject[] slots;
    
    public void SlotCheck(GameObject obj)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            Slot temp = slots[i].GetComponent<Slot>();
            if(temp.IsinItem() == true)
            {
                string objID = obj.GetComponent<ItemInfo>().GetId();
                if(objID == temp.GetItemIDinSlot())
                {
                    temp.InsertItem(obj);
                    return;
                }
            }
            else
            {
                temp.InsertItem(obj);
                return;
            }
        }
    }
}

