using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingUIRotate : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);
    }
}
