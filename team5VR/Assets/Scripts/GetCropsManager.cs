using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GetCropsManager : MonoBehaviour
{
    int[] crops = { 0, 0, 0, 0, 0 };
    int[] fruits = { 0, 0, 0 };
    Rigidbody rigidBody;
    GameObject respawnObject;
    Vector3 respawnPos;
    Quaternion respawnRot;
    public GameObject[] fruitsArray;
    public GameObject[] cropsArray;
    public void SaveFruitsAndCropsPosAndRot(HoverEnterEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("fruits") || args.interactableObject.transform.CompareTag("crops"))
        {
            respawnPos = args.interactableObject.transform.position;
            respawnRot = args.interactableObject.transform.rotation;

            Debug.Log("Hover in " + args.interactableObject.transform.name + "pos " + args.interactableObject.transform.position);
        }
    }

    //if player grab crops, crop's useGravity On, Freeze Pos, rot is OFF.
    public void TurnOnRgdBody(SelectExitEventArgs args) 
    {
        if (args.interactableObject.transform.CompareTag("crops"))
        {
            rigidBody =  args.interactableObject.transform.GetComponent<Rigidbody>();
            if (rigidBody.useGravity == false)
            {
                rigidBody.useGravity = true;
                rigidBody.constraints = RigidbodyConstraints.None;
            }
        }
    }

    //if player grab fruits, fruit's is destroyed directly and fruit count + 1.
    public void OnGrabFruits(SelectExitEventArgs args)
    {
        if (args.interactableObject.transform.CompareTag("fruits"))
        {
            if(args.interactableObject.transform.name == "apple")
            {
                fruits[0] += 1;
                Debug.Log("I GOT APPLE " + fruits[0]);
                args.interactableObject.transform.gameObject.SetActive(false);
                StartCoroutine(RespawnCropsFruits(fruitsArray[4], respawnPos, respawnRot));
            }
            if (args.interactableObject.transform.name == "cherry1" || args.interactableObject.transform.name == "cherry2")
            {
                fruits[1] += 1;
                Debug.Log("I GOT cherry " + fruits[1]);
                args.interactableObject.transform.gameObject.SetActive(false);
                if (args.interactableObject.transform.name == "cherry1")
                {
                    StartCoroutine(RespawnCropsFruits(fruitsArray[0], respawnPos, respawnRot));
                }
                else
                {
                    StartCoroutine(RespawnCropsFruits(fruitsArray[1], respawnPos, respawnRot));
                }

            }
            if (args.interactableObject.transform.name == "acorn1" || args.interactableObject.transform.name == "acorn2")
            {
                Debug.Log("Pos " + respawnPos);
                fruits[2] += 1;
                Debug.Log("I GOT acorn " + fruits[2]);
                args.interactableObject.transform.gameObject.SetActive(false);
                if (args.interactableObject.transform.name == "acorn1")
                {
                    StartCoroutine(RespawnCropsFruits(fruitsArray[2], respawnPos, respawnRot));
                }
                else
                {
                    StartCoroutine(RespawnCropsFruits(fruitsArray[3], respawnPos, respawnRot));
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("crops"))
        {
            
            if (collision.gameObject.name == "Turnip_Fruit")
            {
                crops[0] += 1;
                collision.gameObject.SetActive(false);
                Debug.Log(collision.gameObject.name + " TURNIP Count: " + crops[0]);
                StartCoroutine(RespawnCropsFruits(cropsArray[0], respawnPos, respawnRot));
            }
            if (collision.gameObject.name == "Carrot_Fruit")
            {
                crops[1] += 1;
                collision.gameObject.SetActive(false);
                Debug.Log(collision.gameObject.name + " CARROT Count: " + crops[1]);
                StartCoroutine(RespawnCropsFruits(cropsArray[1], respawnPos, respawnRot));
            }
            if (collision.gameObject.name == "Tomato_Fruit")
            {
                crops[2] += 1;
                collision.gameObject.SetActive(false);
                Debug.Log( collision.gameObject.name + " TOMATO Count: " + crops[2]);
                StartCoroutine(RespawnCropsFruits(cropsArray[2], respawnPos, respawnRot));
            }
            if (collision.gameObject.name == "Corn_Fruit")
            {
                crops[3] += 1;
                collision.gameObject.SetActive(false);
                Debug.Log(collision.gameObject.name + " CORN Count: " + crops[3]);
                StartCoroutine(RespawnCropsFruits(cropsArray[3], respawnPos, respawnRot));
            }
            if (collision.gameObject.name == "Eggplant_Fruit")
            {
                crops[4] += 1;
                collision.gameObject.SetActive(false);
                Debug.Log(collision.gameObject.name + " EGG Count: " + crops[4]);
                StartCoroutine(RespawnCropsFruits(cropsArray[4], respawnPos, respawnRot));
            }
        }
    }
    IEnumerator RespawnCropsFruits(GameObject obj, Vector3 pos, Quaternion rot)
    {
        
        Debug.Log("Respawning..." + pos);
        yield return new WaitForSeconds(5);
        respawnObject = Instantiate(obj, pos, rot);
        respawnObject.transform.parent = obj.transform.parent;
        respawnObject.transform.name = obj.name;
        if (obj.gameObject.CompareTag("crops"))
        {
            rigidBody = respawnObject.transform.GetComponent<Rigidbody>();
            rigidBody.useGravity = false;
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }
        respawnObject.SetActive(true);
        Debug.Log("Respawn complete " + respawnObject + respawnObject.transform.position);
    }

}
