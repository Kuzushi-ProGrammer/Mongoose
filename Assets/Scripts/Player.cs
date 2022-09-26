using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public Color playerColor;

    [SyncVar(hook = nameof(OnChange))] string mongoose = "impostor"; // Runs the function SetString on variable change, only on server

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

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

        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CommandTest();
            }
        }

        if (isServer && Input.GetKeyDown(KeyCode.M))
        {
            RpcTest();
        }

    }

    //Rpc stands for Remote Procedure Call

    // Run CommandTest -> Code executed on server -> Runs RpcReceiver -> RpcReceiver is run on client

    // Command to Server -> Server Responds -> Client Receives

    // Command to commune with server, TargetRpc to receive command from server

    // [Command] Called by client, run by server
    // [ClientRpc] Called by server, run on all clients
    // [TargetRpc] Called by the server, run on a specific client
    // [SyncVar] Variables Syncronized from server to client, only works on scripts inherriting from NetworkBehaviour


    [Command] 
    void CommandTest()
    {
        Debug.Log("Command recieved");
        playerColor = Color.yellow;
        mongoose = "sus"; // Changes string on server
        RpcReciever();
    }

    void OnChange(string oldstring, string newstring)
    {
        newstring = "bitches";
        Debug.Log(newstring);
    }

    [TargetRpc]
    void RpcReciever() 
    {
        Debug.Log("Received from server");
        playerColor = Color.cyan;
    }

    [ClientRpc]
    void RpcTest()
    {
        Debug.Log("Rpc Activated");
        playerColor = Color.red;
    }


}
