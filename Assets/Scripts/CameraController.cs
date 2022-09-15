using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerObject;
    private Vector2 playerPos;
    private Vector2 cameraPos;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GetComponent<Transform>();
        playerPos = playerObject.position;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos = playerPos;
        print(cameraPos + "CAM");




    }
}
