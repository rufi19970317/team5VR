using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class telescopeCamController : MonoBehaviour
{
    public GameObject origin;
    public GameObject sub;
    public Camera mainCam;
    public Camera subCam;
    public Camera scopeCam;

    public Material blackPanel;

    public float a = 0;

    public float speed = 3.0f;
    public GameObject target;
    private Transform targetP;

    public int play = 0;

    private void Start()
    {
        targetP = target.GetComponent<Transform>();
    }

    private void Update()
    {
        if (play == 1) moveToTelescope();
        if (play == 2) ScopeView();
    }
    public void MoveCam(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.gameObject.CompareTag("Telescope"))
        {
            subCam.transform.position = mainCam.transform.position;
            subCam.transform.rotation = Quaternion.Euler(mainCam.transform.eulerAngles);

            mainCam.gameObject.SetActive(false);
            origin.SetActive(false);
            sub.SetActive(true);
            subCam.gameObject.SetActive(true);

            play = 1;
        }
    }

    void moveToTelescope()
    {
        float dist = Vector3.Distance(subCam.transform.position, targetP.position);


        if (dist < 1.5)
            StartCoroutine(FadeCoroutine());

        subCam.transform.LookAt(targetP);

        Vector3 movement = new Vector3(0, 0, speed * 0.1f * Time.deltaTime);
        subCam.transform.Translate(movement);

        float step = speed * 0.1f * Time.deltaTime;
        subCam.transform.position = Vector3.MoveTowards(subCam.transform.position, targetP.position, step);

    }

    IEnumerator FadeCoroutine()
    {
        
        while (a < 1.0f)
        {
            a += 0.005f;
            yield return new WaitForSeconds(0.01f);
            blackPanel.color = new Color(0, 0, 0, a);

            if (a >= 1.0f) play = 2;
        }
    }

    void ScopeView()
    {
        a = 0;
        Debug.Log("mode 2");
        subCam.gameObject.SetActive(false);
        scopeCam.gameObject.SetActive(true);

        blackPanel.color = new Color(0, 0, 0, 0);
        play = 0;
    }
}
