using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMenuScript : MonoBehaviour
{
    [SerializeField] GameObject mongoose;
    [SerializeField] GameObject racoon;

    [SerializeField]Transform spawnpoint;

    public void ChangeToMongoose()
    {
        racoon.SetActive(false);
        mongoose.SetActive(true);
        //something something change team to mongoose
    }

    public void ChangeToRacoon()
    {
        mongoose.SetActive(false);
        racoon.SetActive(true);
    }
}
