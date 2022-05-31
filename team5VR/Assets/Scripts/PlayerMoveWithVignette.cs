using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PlayerMoveWithVignette : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private InputDeviceCharacteristics leftController;
    [SerializeField]
    private InputDeviceCharacteristics rightController;
    [SerializeField]
    private float angleSpeed = 60f;
    [SerializeField]
    private Volume vignetteVolume;
    private Vignette vignette;

    private InputDevice leftTarget;
    private InputDevice rightTarget;

    private Vector2 lInputAxis;
    private Vector2 rInputAxis;
   
    private CharacterController charController;
    private XROrigin xrOrigin;

    private float gravity = -9.8f;
    private float fallingSpeed;
    public float vigIntensity = 0.9f;
    public float additionalHeight = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        xrOrigin = GetComponent<XROrigin>();
        if (vignetteVolume.profile.TryGet(out Vignette temp)) 
        {
            vignette = temp;
        }
        TryInitialize();
    }

    private void TryInitialize()
    {
        List<InputDevice> lDevices = new List<InputDevice>();
        List<InputDevice> rDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(leftController, lDevices);
        InputDevices.GetDevicesWithCharacteristics(rightController, rDevices);
        
        if(lDevices.Count > 0)
        {
            leftTarget = lDevices[0];
        }

        if (rDevices.Count > 0)
        {
            rightTarget = rDevices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((leftTarget == null || !leftTarget.isValid) || (rightTarget == null || !rightTarget.isValid))
        {
            TryInitialize();
            return;
        }

        bool isGround = charController.isGrounded;

        if(isGround && fallingSpeed < 0)
        {
            fallingSpeed = 0;
        }

        if (!isGround)
        {
            fallingSpeed += gravity * Time.deltaTime;
        }
        charController.Move(Vector3.up * fallingSpeed * Time.deltaTime);

        leftTarget.TryGetFeatureValue(CommonUsages.primary2DAxis, out lInputAxis);
        rightTarget.TryGetFeatureValue(CommonUsages.primary2DAxis, out rInputAxis);
        
        if (lInputAxis == Vector2.zero && rInputAxis == Vector2.zero)
        {
            SetVignette(0f);        }
        else
        {
            SetVignette(vigIntensity);
            //Continuous move
            Quaternion headyaw = Quaternion.Euler(0, xrOrigin.Camera.transform.eulerAngles.y, 0);
            Vector3 dir = headyaw * new Vector3(lInputAxis.x, 0, lInputAxis.y);
            charController.Move(dir * Time.deltaTime * speed);

            //continuous turn
            transform.Rotate(Vector3.up * rInputAxis.x * angleSpeed * Time.deltaTime);
        }
    }

    private void CapsuleFollowHeadset()
    {
        charController.height = xrOrigin.CameraInOriginSpaceHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(xrOrigin.Camera.transform.position);

        charController.center = new Vector3(capsuleCenter.x, charController.height / 2 + charController.skinWidth, capsuleCenter.z);
    }

    private void SetVignette(float x)
    {
        vignette.intensity.Override(x);
        vignette.smoothness.Override(x);
    }
}
