using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraController : MonoBehaviour
{ 
    public Transform playerTransform;
    public GameObject playerObj;

    private void Awake()
    {
        playerTransform = playerObj.GetComponent<Transform>();
    }

    void Update()
    {
        transform.position =  new Vector3(playerTransform.position.x, playerTransform.position.y, -10);
    }
    
}
