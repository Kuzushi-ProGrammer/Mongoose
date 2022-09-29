using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public struct PlayerDataMessage : NetworkMessage
    {
        public string species; //mongoose or racoon
        public int scrap; //money
    }
    public void SendPlayerData(string species, int scrap)
    {
        PlayerDataMessage data = new PlayerDataMessage()
        {
            species = species,
            scrap = scrap
        };

        NetworkServer.SendToAll(data);
    }
    public void SetupClient()
    {
        NetworkClient.RegisterHandler<PlayerDataMessage>(ChangeSpeciesToRacoon); // not sure how to communicate to the server that the variable changed
        NetworkClient.Connect("localhost");
    }

    public void ChangeSpeciesToMongoose(PlayerDataMessage data)
    {
        data.species = "mongoose";
    }
    public void ChangeSpeciesToRacoon(PlayerDataMessage data)
    {
        data.species = "racoon";
    }
}
