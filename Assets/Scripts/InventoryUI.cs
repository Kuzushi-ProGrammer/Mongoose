using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject slot1;
    [SerializeField] GameObject slot2;
    [SerializeField] GameObject slot1BG;
    [SerializeField] GameObject slot2BG;

    [SerializeField] Sprite none;
    [SerializeField] Sprite sks;
    [SerializeField] Sprite bigIron;

    Image image1;
    Image image2;

    private void Awake()
    {
        image1 = slot1.GetComponent<Image>();
        image2 = slot2.GetComponent<Image>();
    }
    public void UIupdate(string item, int slot)
    {
        switch (slot)
        {
            case 0:
                switch (item)
                {
                    case "none":
                        image1.sprite = none;
                        break;

                    case "SKS":
                        image1.sprite = sks;
                        break;

                    case "Big_Iron":
                        image1.sprite = bigIron;
                        break;
                }
                break;

            case 1:
                switch (item)
                {
                    case "none":
                        image2.sprite = none;
                        break;

                    case "SKS":
                        image2.sprite = sks;
                        break;

                    case "Big_Iron":
                        image2.sprite = bigIron;
                        break;
                }
                break;
        }
    }

    public void ChangeActiveSlot(int slot)
    {
        switch (slot)
        {
            case (0):
                slot1BG.SetActive(false);
                slot2BG.SetActive(false);
                break;

            case (1):
                slot1BG.SetActive(true);
                slot2BG.SetActive(false);
                break;

            case (2):
                slot1BG.SetActive(false);
                slot2BG.SetActive(true);
                break;
        }
    }
}
