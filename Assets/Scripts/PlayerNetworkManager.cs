using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNetworkManager : NetworkManager
{
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
            playerColor = Color.green
        };

        NetworkClient.Send(characterMessage);

        Debug.Log("Client Connected");
    }

    void OnCreateCharacter(NetworkConnectionToClient connection, CharacterMessage message)
    {
        GameObject playerObject = Instantiate(playerPrefab);

        NetworkedPlayerController player = playerObject.GetComponent<NetworkedPlayerController>();

        player.playerColor = message.playerColor;

        NetworkServer.AddPlayerForConnection(connection, playerObject);

        Debug.Log("Character Created");
    }

}

public struct CharacterMessage : NetworkMessage
{
    public Color playerColor;
}