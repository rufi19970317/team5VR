using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class ScopeViewController : MonoBehaviour
{
    public Camera mainCam;
    public GameObject origin;
    public GameObject sub;
    public Material blackPanel;

    public float aaa = 0;
    public float a = 0;

    private Vector3 pos;
    private Vector3 rot;

    bool isfade = false;
    public int play = 0;


    [SerializeField]
    private InputDeviceCharacteristics controllerCharacteristics;

    private InputDevice targetDevice;
    private bool oneClick;

    // Start is called before the first frame update
    void Start()
    {
        isfade = false;
        a = 1f;
        oneClick = true;
    }

    private void Tryinitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    void deviceInput()
    {
        // Keyboard Test
        if (Input.GetKeyDown(KeyCode.B))
        {
            isfade = true;
        }

        if (targetDevice == null || !targetDevice.isValid)
        {
            Tryinitialize();
        }
        else
        {
            if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool isClick))
            {
                if (isClick && oneClick)
                {
                    isfade = true;
                    oneClick = false;
                }
                if (!isClick)
                {
                    oneClick = true;
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!isfade) StartCoroutine(FadeinCoroutine());
        else StartCoroutine(FadeoutCoroutine());
        deviceInput();
    }

    public int getPlay()
    {
        return play;
    }

    public void setPlay(int i)
    {
        play = i;
    }


    IEnumerator FadeinCoroutine()
    {
        while (a > 0f)
        {
            a -= 0.005f;
            yield return new WaitForSeconds(0.01f);
            blackPanel.color = new Color(0, 0, 0, a);

            if (a <= 0f) { 
                
                sub.SetActive(false);
                sub.SetActive(true);
            }
        }
    }

    IEnumerator FadeoutCoroutine()
    {
        while (a < 1.0f)
        {
            a += 0.005f;
            yield return new WaitForSeconds(0.01f);
            blackPanel.color = new Color(0, 0, 0, a);

            if (a >= 1.0f)
            {
                play = 1;
                isfade = false;
                a = 1f;
                aaa = 0f;
            }
        }       
    }
}
