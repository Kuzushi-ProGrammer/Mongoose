using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    GameObject newPlayer;
    public GameObject playerSpawnPoint;
    int allotedMongeese = 1;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (allotedMongeese > 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                allotedMongeese = allotedMongeese - 1;
                newPlayer = Instantiate(playerPrefab, playerSpawnPoint.transform, false);
                Debug.Log("player spawned");

            }
        }
    }
}
