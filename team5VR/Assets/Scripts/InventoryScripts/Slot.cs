using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
public class Slot : MonoBehaviour
{
    [SerializeField]
    private bool inItem = false;
    private GameObject obj;
    [SerializeField]
    private string itemID;
    private int stockNum = 0;
    private Color defaultColor;
    public Image slotImage;
    [SerializeField]
    private Text stockText;
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = slotImage.color;
    }

    public void InsertItem(GameObject item)
    {
        if (item.CompareTag("fruits") || item.CompareTag("crops"))
        {
            if (item.GetComponent<ItemInfo>().inSlot == false)
            {
                if (inItem)
                {
                    if (itemID == item.GetComponent<ItemInfo>().GetId())
                    {
                        item.GetComponent<ItemInfo>().inSlot = true;
                        stockNum += 1;
                        UpdateStockText();
                        Destroy(item.gameObject);
                    }
                }
                else
                {
                    obj = Instantiate(item);
                    obj.name = item.name;
                    ItemInfo info = obj.GetComponent<ItemInfo>();
                    itemID = info.GetId();
                    info.inSlot = true;
                    inItem = true;
                    stockNum += 1;
                    UpdateStockText();
                    stockText.gameObject.SetActive(true);
                    obj.GetComponent<Rigidbody>().isKinematic = true;
                    obj.transform.SetParent(this.transform);
                    obj.transform.localPosition = info.offset;
                    obj.transform.localScale = info.slotSize;
                    obj.transform.localEulerAngles = info.slotRotation;
                    obj.GetComponent<XRGrabInteractable>().enabled = false;
                    obj.GetComponent<Collider>().enabled = false;
                    Destroy(item.gameObject);
                    slotImage.color = Color.gray;
                }
            }
        }
    }

    public string GetItemIDinSlot()
    {
        return itemID;
    }

    public bool IsinItem()
    {
        return inItem;
    }
    void UpdateStockText()
    {
        stockText.text = ""+stockNum;
    }
    public void SpawnItem(Transform handTransform)
    {
        GameObject temp = Instantiate(obj, handTransform.position, Quaternion.Euler(obj.GetComponent<ItemInfo>().slotRotation));
        temp.transform.localScale = obj.GetComponent<ItemInfo>().defauultSize;
        temp.gameObject.GetComponent<ItemInfo>().inSlot = false;
        temp.GetComponent<XRGrabInteractable>().enabled = true;
        temp.GetComponent<Collider>().enabled = true;
        temp.name = obj.name;
        stockNum -= 1;
        UpdateStockText();
        if (stockNum == 0)
        {
            Destroy(obj);
            stockText.gameObject.SetActive(false);
            slotImage.color = defaultColor;
            inItem = false;
            itemID = null;
        }
    }
}
