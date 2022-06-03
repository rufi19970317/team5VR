using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EatFood : MonoBehaviour
{
    private void OnActivated(ActivateEventArgs args) => eatFood();

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<XRGrabInteractable>().activated.AddListener(OnActivated);
    }

    private void eatFood()
    {
        Destroy(gameObject);
    }
}
