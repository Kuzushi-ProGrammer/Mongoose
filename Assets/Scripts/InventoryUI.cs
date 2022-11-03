using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject slot1;
    [SerializeField] GameObject slot2;

    [SerializeField] Sprite none;
    [SerializeField] Sprite sks;
    [SerializeField] Sprite bigIron;

    Image image1;
    Image image2;

    public void UIupdate(string s)
    {
        PlayerController playerController = playerObject.GetComponent<PlayerController>();

        image1 = slot1.GetComponent<Image>();
        image2 = slot2.GetComponent<Image>();

        switch (s)
        {
            case "None":
                image1.sprite = none;
                break;

            case "SKS":
                image1.sprite = sks; 
                break;

            case "Big_Iron":
                image1.sprite = bigIron;
                break;
        }
 

    }
}
