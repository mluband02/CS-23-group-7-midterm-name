using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to player’s transform
    public Vector3 offset = new Vector3(0, 5, -10);  // Default offset (modify as needed)

    void LateUpdate()
    {
        if (player != null)
        {
            // Smooth follow logic (optional)
            transform.position = player.position + offset;
        }
    }
}
