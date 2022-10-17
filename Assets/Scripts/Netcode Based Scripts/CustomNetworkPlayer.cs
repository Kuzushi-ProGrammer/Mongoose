using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.XR;

public class CustomNetworkPlayer : NetworkRoomPlayer
{
    CharacterMessage message;
    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            message.playerSpecies = "mongoose";
            player.SpeciesChange("racoon", "mongoose");
            Debug.Log("M was pressed");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            message.playerSpecies = "racoon";
            player.SpeciesChange("mongoose", "racoon");
            Debug.Log("R was pressed");
        }
    }

}
