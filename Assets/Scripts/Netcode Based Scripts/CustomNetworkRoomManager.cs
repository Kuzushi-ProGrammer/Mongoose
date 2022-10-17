using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustomNetworkRoomManager : NetworkRoomManager
{

    GameObject mongoosePrefab;
    GameObject racoonPrefab;


    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<CharacterMessage>(OnCreateCharacter);

        Debug.Log("Server Started");
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        CharacterMessage characterMessage = new CharacterMessage
        {
            playerColor = Color.green,
            playerSpecies = ""
        };

        NetworkClient.Send(characterMessage); // Changes the state of the cube when it connects to the server, only server side

        Debug.Log("Client Connected");
    }

    void OnCreateCharacter(NetworkConnectionToClient connection, CharacterMessage message)
    {
        if (message.playerSpecies.Equals("mongoose"))
        {
            GameObject playerObject = Instantiate(mongoosePrefab);
            PlayerController playerController = playerObject.GetComponent<PlayerController>();
            playerController.species = message.playerSpecies;
            NetworkServer.AddPlayerForConnection(connection, playerObject);
        }

        else if (message.playerSpecies.Equals("racoon"))
        {
            GameObject playerObject = Instantiate(racoonPrefab);
            PlayerController playerController = playerObject.GetComponent<PlayerController>();
            playerController.species = message.playerSpecies;
            NetworkServer.AddPlayerForConnection(connection, playerObject);
        }
        else
        {
            Debug.Log(":skull:");
        }
    }

    public struct CharacterMessage : NetworkMessage
    {
        public Color playerColor;
        public string playerSpecies;
    }
}