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
            //playerSpeciesPrefab 
        };

        NetworkClient.Send(characterMessage); // Changes the state of the cube when it connects to the server, only server side

        Debug.Log("Client Connected");
    }

    void OnCreateCharacter(NetworkConnectionToClient connection, CharacterMessage message)
    {

        GameObject playerObject = Instantiate(playerPrefab);
        Player player = playerObject.GetComponent<Player>();
        player.playerColor = message.playerColor;
        NetworkServer.AddPlayerForConnection(connection, playerObject);

        Debug.Log("Character Created");
    }
    
    public void ChangeCharacter(NetworkConnectionToClient connection, GameObject newprefab)
    {
        // Cache a reference to the current player object
        GameObject oldPlayer = connection.identity.gameObject;

        //newprefab =

        NetworkServer.ReplacePlayerForConnection(connection, Instantiate(newprefab), true);
        Destroy(oldPlayer, 0.1f);
    }

}


public struct CharacterMessage : NetworkMessage
{
    public Color playerColor;
    //public GameObject playerSpeciesPrefab;
}
