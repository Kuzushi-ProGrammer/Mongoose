using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SelectionMenuScript : NetworkBehaviour
{
    [SerializeField] GameObject mongoose;
    [SerializeField] GameObject racoon;

    [SerializeField]Transform spawnpoint;

    [SerializeField]GameObject player;

    PlayerController playerController;

    string species;

    private void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    public void ChangeToMongoose()
    {
        racoon.SetActive(false);
        mongoose.SetActive(true);
        //something something change team to mongoose 

        playerController.ChangeSpeciesToMongoose();
    }
    
    public void ChangeToRacoon()
    {
        mongoose.SetActive(false);
        racoon.SetActive(true);

        playerController.ChangeSpeciesToRacoon();
    }
}
