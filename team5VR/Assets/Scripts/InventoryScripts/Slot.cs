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

    // Update is called once per frame
    void Update()
    {
        if(obj != null)
        {
            obj.transform.localPosition = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item") || collision.gameObject.CompareTag("fruits") || collision.gameObject.CompareTag("crops"))
        {
            if (collision.gameObject.GetComponent<ItemInfo>().inSlot == false)
            {
                if (inItem)
                {
                    if (itemID == collision.gameObject.GetComponent<ItemInfo>().GetId())
                    {
                        collision.gameObject.GetComponent<ItemInfo>().inSlot = true;
                        stockNum += 1;
                        UpdateStockText();
                        Destroy(collision.gameObject);
                    }
                }
                else
                {
                    obj = collision.gameObject;
                    itemID = collision.gameObject.GetComponent<ItemInfo>().GetId();
                    obj.gameObject.GetComponent<ItemInfo>().inSlot = true;
                    inItem = true;
                    stockNum += 1;
                    UpdateStockText();
                    stockText.gameObject.SetActive(true);
                    obj.GetComponent<Rigidbody>().isKinematic = true;
                    obj.transform.SetParent(this.transform);
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.localEulerAngles = obj.GetComponent<ItemInfo>().slotRotation;
                    obj.GetComponent<Collider>().enabled = false;
                    slotImage.color = Color.gray;
                }
            }
        }
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
