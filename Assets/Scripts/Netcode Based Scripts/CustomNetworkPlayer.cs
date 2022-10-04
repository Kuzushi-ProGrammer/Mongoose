using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.XR;

public class CustomNetworkPlayer : NetworkRoomPlayer
{
    CharacterMessage message;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            message.playerSpecies = "mongoose";
            Debug.Log("M was pressed");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            message.playerSpecies = "racoon";
            Debug.Log("R was pressed");
        }
    }

}
