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

                        Destroy(GameObject.Find("Keyhole"));
                        spriteRenderer.sprite = closed;

                        Destroy(GameObject.Find("Left Door")); // replace with interpolation like an opening motion if time allows
                        Destroy(GameObject.Find("Right Door"));
                    }
                    break;

                case ("Blue Door"):
                    playerController = collision.GetComponent<PlayerController>();
                    if (playerController.keyInventory.Contains("BlueKey"))
                    {
                        Debug.Log("has blue key");

                        Destroy(GameObject.Find("Keyhole"));
                        spriteRenderer.sprite = closed;

                        Destroy(GameObject.Find("Left Door")); // replace with interpolation like an opening motion if time allows
                        Destroy(GameObject.Find("Right Door"));
                    }
                    else
                    {
                        Debug.Log("no key :(");
                    }
                    break;

                case ("Green Door"):
                    playerController = collision.GetComponent<PlayerController>();
                    if (playerController.keyInventory.Contains("GreenKey"))
                    {
                        Destroy(GameObject.Find("Keyhole"));
                        spriteRenderer.sprite = closed;

                        Destroy(GameObject.Find("Left Door")); // replace with interpolation like an opening motion if time allows
                        Destroy(GameObject.Find("Right Door"));
                    }
                    break;

                case ("Yellow Door"):
                    playerController = collision.GetComponent<PlayerController>();
                    if (playerController.keyInventory.Contains("YellowKey"))
                    {
                        Destroy(GameObject.Find("Keyhole"));
                        spriteRenderer.sprite = closed;

                        Destroy(GameObject.Find("Left Door")); // replace with interpolation like an opening motion if time allows
                        Destroy(GameObject.Find("Right Door"));
                    }
                    break;
            }
        }
    }

}
