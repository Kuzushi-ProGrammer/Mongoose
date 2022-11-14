using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player collided with collectible");
            playerController = collision.GetComponent<PlayerController>();

            Debug.Log(gameObject.name);

            if (playerController.canPickupItems)
            {
                if (playerController.playerInventory[0] == "none" || playerController.playerInventory[1] == "none")
                {
                    Debug.Log("inventory not full (collectiblescript)");

                    if (gameObject.name == "SKS" || gameObject.name == "SKS(Clone)")
                    {
                        playerController.AddItemToInventory("SKS");
                        Destroy(gameObject);
                    }
                    else if (gameObject.name == "BigIronScene" || gameObject.name == "BigIronScene(Clone)")
                    {
                        playerController.AddItemToInventory("Big_Iron");
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

}
