using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyholeScript : MonoBehaviour
{
    PlayerController playerController;
    SpriteRenderer spriteRenderer;

    [SerializeField] Sprite closed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            Debug.Log("Collided with Keyhole");
            Debug.Log(transform.parent.gameObject.name);

            switch (transform.parent.gameObject.name)
            {
                case ("Red Door"):
                    playerController = collision.GetComponent<PlayerController>();
                    if (playerController.keyInventory.Contains("RedKey"))
                    {
                        Debug.Log("has red key");
                        Destroy(transform.parent.gameObject);
                    }
                    break;

                case ("Blue Door"):
                    playerController = collision.GetComponent<PlayerController>();
                    if (playerController.keyInventory.Contains("BlueKey"))
                    {
                        Debug.Log("has blue key");
                        Destroy(transform.parent.gameObject);
                    }
                    break;

                case ("Green Door"):
                    playerController = collision.GetComponent<PlayerController>();
                    if (playerController.keyInventory.Contains("GreenKey"))
                    {
                        Debug.Log("has green key");
                        Destroy(transform.parent.gameObject);
                    }
                    break;

                case ("Yellow Door"):
                    playerController = collision.GetComponent<PlayerController>();
                    if (playerController.keyInventory.Contains("YellowKey"))
                    {
                        Debug.Log("has yellow key");
                        Destroy(transform.parent.gameObject);
                    }
                    break;
            }
        }
    }

}
