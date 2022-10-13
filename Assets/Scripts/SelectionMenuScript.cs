using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMenuScript : MonoBehaviour
{
    [SerializeField] GameObject mongoose;
    [SerializeField] GameObject racoon;

    Transform spawnpoint;

    public void ChangeToMongoose()
    {
        Instantiate(mongoose, spawnpoint);
    }

    public void ChangeToRacoon()
    {
        Instantiate(racoon, spawnpoint);
    }
}
