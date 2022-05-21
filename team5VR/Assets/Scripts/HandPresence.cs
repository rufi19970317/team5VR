using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{

    private InputDevice targetDevice;

    [SerializeField]
    private List<GameObject> controllerPrefabs;

    [SerializeField]
    private InputDeviceCharacteristics controllerCharacteristics;

    private GameObject spawnedController;

    [SerializeField]
    private bool showController = false;

    [SerializeField]
    private GameObject handModelPrefab;

    private GameObject spawnedHand;

    private Animator handAnimator;

    void Start()
    {
        TryInitialize();
    }

    private void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);


        if (devices.Count > 0)
        {
            targetDevice = devices[0];

            //instantiate controller prefab
            GameObject controller = controllerPrefabs.Find(ctrl => ctrl.name == targetDevice.name);

            if (controller != null)
            {
                spawnedController = Instantiate(controller, transform);
            }
            else
            {
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
        }

        // spawn the hand
        spawnedHand = Instantiate(handModelPrefab, transform);

        //get an animator
        handAnimator = spawnedHand.GetComponent<Animator>();
    }

    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            // set controller / hand active/deactive
            spawnedHand.SetActive(!showController);
            spawnedController.SetActive(showController);

            //update hand animation if not showing controller
            if (!showController)
            {
                UpdateHandAnimation();
            }
        }
    }

    private void UpdateHandAnimation()
    {
        //trigger pressed
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        //grip pressed
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

}
