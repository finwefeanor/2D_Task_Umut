using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset;   // Offset from the player

    void LateUpdate()
    {
        if (player != null)
        {
           
            transform.position = player.position + offset;
        }
    }
}
