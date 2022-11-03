using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{ 
    public GameObject player;
    Vector3 playerPos;

    void Update()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        transform.position = playerPos;
    }
    
}
