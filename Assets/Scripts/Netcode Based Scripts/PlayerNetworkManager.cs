using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNetworkManager : NetworkManager
{
    PlayerData playerData;
    public GameObject mongoosePrefab;
    public GameObject racoonPrefab;

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
            playerSpecies = "mongoose"
        };

        NetworkClient.Send(characterMessage); // Changes the state of the cube when it connects to the server, only server side

        Debug.Log("Client Connected");
    }

    void OnCreateCharacter(NetworkConnectionToClient connection, CharacterMessage message)
    {

        if (message.playerSpecies.Equals("mongoose"))
        {
            Debug.Log("MONGOOSE");
        }
        else if (message.playerSpecies.Equals("racoon"))
        {
            Debug.Log("RACOON");
        }

        GameObject playerObject = Instantiate(playerPrefab);
        Player player = playerObject.GetComponent<Player>();
        player.playerColor = message.playerColor;
        NetworkServer.AddPlayerForConnection(connection, playerObject);

        Debug.Log("Character Created");
    }

}

public struct CharacterMessage : NetworkMessage
{
    public Color playerColor;
    public string playerSpecies;
}