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
            playerController = collision.GetComponent<PlayerController>();
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            Debug.Log("Collided with Keyhole");

            switch (gameObject.name)
            {
                case ("Red Door"):
                    if (playerController.keyInventory.Contains("RedKey"))
                    {
                        Destroy(gameObject.transform.Find("Keyhole"));
                        spriteRenderer.sprite = closed;

                        Destroy(gameObject.transform.Find("Door Left")); // replace with interpolation like an opening motion if time allows
                        Destroy(gameObject.transform.Find("Door Right"));

                    }
                    break;

                case ("Blue Door"):
                    if (playerController.keyInventory.Contains("BlueKey"))
                    {
                        Destroy(gameObject.transform.Find("Keyhole"));
                        spriteRenderer.sprite = closed;

                        Destroy(gameObject.transform.Find("Door Left")); // replace with interpolation like an opening motion if time allows
                        Destroy(gameObject.transform.Find("Door Right"));

                    }
                    break;

                case ("Green Door"):
                    if (playerController.keyInventory.Contains("GreenKey"))
                    {
                        Destroy(gameObject.transform.Find("Keyhole"));
                        spriteRenderer.sprite = closed;

                        Destroy(gameObject.transform.Find("Door Left")); // replace with interpolation like an opening motion if time allows
                        Destroy(gameObject.transform.Find("Door Right"));

                    }
                    break;

                case ("Yellow Door"):
                    if (playerController.keyInventory.Contains("YellowKey"))
                    {
                        Destroy(gameObject.transform.Find("Keyhole"));
                        spriteRenderer.sprite = closed;

                        Destroy(gameObject.transform.Find("Door Left")); // replace with interpolation like an opening motion if time allows
                        Destroy(gameObject.transform.Find("Door Right"));

                    }
                    break;
            }
        }
    }

}
