using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public Color playerColor;
    SpriteRenderer spriteRenderer;

    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);
            transform.position = transform.position + movement;
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        spriteRenderer.color = playerColor;

        HandleMovement();

        if (isLocalPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            CommandTest();
        }


    }

    [Command]
    void CommandTest()
    {
        Debug.Log("Command recieved");
        playerColor = Color.yellow;
    }
}
