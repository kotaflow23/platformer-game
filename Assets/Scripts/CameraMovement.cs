using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void LateUpdate()
    {
        if (player != null){
        // Camera follows the player on x and y axis (offset by 6 on x)
        transform.position = new Vector3(player.position.x + 6, 0, -10); 
        }
    }
}
