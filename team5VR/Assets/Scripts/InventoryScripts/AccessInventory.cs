using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessInventory : MonoBehaviour
{
    public GameObject[] slots;
    
    public void SlotCheck(GameObject obj)
    {
        Debug.Log("�κ��丮 ���� ����" + obj.GetComponent<ItemInfo>().GetId());
        for (int i = 0; i < slots.Length; i++)
        {
            Debug.Log("�κ��丮 ���� ����1 " + obj.GetComponent<ItemInfo>().GetId());
            Slot temp = slots[i].GetComponent<Slot>();
            if(temp.IsinItem() == true)
            {
                Debug.Log("�κ��丮 ���� ���� " + obj.GetComponent<ItemInfo>().GetId());
                string objID = obj.GetComponent<ItemInfo>().GetId();
                if (objID == temp.GetItemIDinSlot())
                {
                    temp.InsertItem(obj);
                    return;
                }
            }
            else
            {
                Debug.Log("�κ��丮 ���� ���� " + obj.GetComponent<ItemInfo>().GetId());
                temp.InsertItem(obj);
                return;
            }
        }
    }
}

