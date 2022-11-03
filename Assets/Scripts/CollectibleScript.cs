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
            Debug.Log("Player collided with collectible");
            playerController = collision.GetComponent<PlayerController>();
            playerController.AddItemToInventory("SKS");
            Destroy(gameObject);
        }
    }

}
