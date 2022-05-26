using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class ShowInventory : MonoBehaviour
{
    [SerializeField]
    private InputDeviceCharacteristics controllerCharacteristics;
    [SerializeField]
    GameObject Inventory;
    private InputDevice targetDevice;
    private bool invenActive;
    private bool oneClick;
    // Start is called before the first frame update
    void Start()
    {
        Inventory.SetActive(false);
        invenActive = false;
        oneClick = true;
    }

    private void Tryinitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        if(devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(targetDevice == null || !targetDevice.isValid)
        {
            Tryinitialize();
        }
        else
        {   
            
            if(targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool isClick))
            {
                if (isClick && oneClick)
                {
                    oneClick = false;
                    invenActive = !invenActive;
                    Inventory.SetActive(invenActive);
                }
                if (!isClick)
                {
                    oneClick = true;
                }
            }
        }
    }
}
