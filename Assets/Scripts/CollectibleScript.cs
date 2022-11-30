using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();

            if (gameObject.tag == "Key")
            {
                playerController.keyInventory.Add(gameObject.name);
                Debug.Log("added: " + gameObject.name);
                Destroy(gameObject);
            }

            else if (gameObject.tag == "Weapon")
            {
                if (playerController.canPickupItems)
                {
                    if (playerController.playerInventory[0] == "none" || playerController.playerInventory[1] == "none")
                    {
                        if (gameObject.name == "SKS" || gameObject.name == "SKS(Clone)" || gameObject.name == "SKS Spawn Pedestal")
                        {
                            playerController.AddItemToInventory("SKS");
                            Destroy(gameObject);
                        }
                        else if (gameObject.name == "BigIronScene" || gameObject.name == "BigIronScene(Clone)" || gameObject.name == "Big Iron Spawn Pedestal")
                        {
                            playerController.AddItemToInventory("Big_Iron");
                            Destroy(gameObject);
                        }
                    }

                }
            }

        }
    }

}
