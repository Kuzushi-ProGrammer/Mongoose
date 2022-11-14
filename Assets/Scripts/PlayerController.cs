


using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    HeldItem item;
    InventoryUI inventoryUI;

    Rigidbody2D PlayerRB;
    Rigidbody2D BulletRB;

    public Vector2 playerPos;

    [SerializeField] Transform playerSpawnPoint;
    [SerializeField] Transform SKSspawnPoint;
    [SerializeField] Transform BigIronSpawnPoint;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Transform BIGIRONbulletSpawnPoint;
    [SerializeField] Transform droppedItemSpawnPoint;

    [SerializeField] GameObject SKSPrefab;
    [SerializeField] GameObject BigIronPrefab;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject BigIronBulletPrefab;
    [SerializeField] GameObject uiObject;

    [SerializeField] GameObject bigIronDropable;

    GameObject newGun;

    public List<string> playerInventory = new();

    public bool canPickupItems = true;
    bool canshoot = true;
    bool fireCoolDown = true;
    bool gunHasAmmo = true;

    float health = 3f;
    float SKSammo = 30;
    float BIGIRONammo = 6;
    float SKSspareAmmo = 69;
    float BIGIRONspareAmmo = 5;

    int bulletSpeed = 30;
    int currentActiveSlot;

    void Start()
    {
        newGun = Instantiate(BigIronPrefab, BigIronSpawnPoint.transform, false);
        PlayerRB = GetComponent<Rigidbody2D>();
        item = HeldItem.BigIron;
        inventoryUI = uiObject.GetComponent<InventoryUI>();

        playerInventory.Add("none");
        playerInventory.Add("none");
    }

    private void FixedUpdate()
    {
        PlayerRotation();
        PlayerMovement();
    }

    // make gun swap execute on keypress instead of on fire

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (currentActiveSlot == 1)
            {
                currentActiveSlot = 0;
                inventoryUI.ChangeActiveSlot(0);
            }
            else
            {
                inventoryUI.ChangeActiveSlot(1);
                currentActiveSlot = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (currentActiveSlot == 2)
            {
                currentActiveSlot = 0;
                inventoryUI.ChangeActiveSlot(0);
            }
            else
            {
                inventoryUI.ChangeActiveSlot(2);
                currentActiveSlot = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RemoveItemFromInventory(currentActiveSlot);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            switchGuns(); // change to switch inventory slots
        }

        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space)) // shoot gun
        {

            switch (currentActiveSlot)
            {
                case 0: // none selected
                    Debug.Log("No gun");
                    break;

                case 1: // slot 1
                    ActivateHeldItem(0);
                    break;

                case 2: // slot 2
                    ActivateHeldItem(1);
                    break;
            }

            if (gunHasAmmo && canshoot)
            {
                switch (item)
                {
                    case HeldItem.None:
                        Debug.Log("no weapon");
                        break;

                    case HeldItem.SKS:
                        if (fireCoolDown == true)
                        {
                            GunGoBoom();
                            fireCoolDown = false;
                            StartCoroutine(FireRate(0.5f));
                        }
                        break;

                    case HeldItem.BigIron:
                        if (fireCoolDown == true)
                        {
                            GunGoBoom();
                            fireCoolDown = false;
                            StartCoroutine(FireRate(1f));
                        }
                        break;
                    default:
                        Debug.Log("defaulted");
                        break;
                }
            }

        }
    }

    /*
    pass through number in function
    testfunc(n);
    */

    void ActivateHeldItem(int n)
    {
        switch (playerInventory[n])
        {
            case "SKS":
                Debug.Log("Shooting SKS");
                break;

            case "Big_Iron":
                Debug.Log("Shooting Big Iron");
                break;
        }
    }

    void PlayerRotation()
    {
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float lookAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle));
    }

    void PlayerMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);
        transform.position = transform.position + movement;
    }

    public void AddItemToInventory(string item)
    {
        ListCheck("1st");

        if (playerInventory[0] == "none")
        {
            Debug.Log("player inv 1 = none, adding item to slot 1");
            playerInventory.RemoveAt(0);
            playerInventory.Insert(0, item);
            inventoryUI.UIupdate(item, 0);
        }
        else if (playerInventory[1] == "none")
        {
            Debug.Log("player inv 2 = none, adding item to slot 2");
            playerInventory.RemoveAt(1);
            playerInventory.Insert(1, item);
            inventoryUI.UIupdate(item, 1);
        }

        ListCheck("2nd");
    }

    void RemoveItemFromInventory(int slot)
    {
        switch (slot)
        {
            case 0:
                Debug.Log("no slot selected");
                break;

            case (1):

                Debug.Log("dropping item in slot 1: " + playerInventory[0].ToString());

                switch (playerInventory[0].ToString())
                {
                    case "SKS":
                        Instantiate(SKSPrefab, gameObject.transform.position, Quaternion.identity);
                        StartCoroutine(dropDelay());
                        break;

                    case "Big_Iron":
                        Instantiate(bigIronDropable, gameObject.transform.position, Quaternion.identity);
                        StartCoroutine(dropDelay());
                        break;
                }

                playerInventory.RemoveAt(0);
                playerInventory.Insert(0, "none");
                inventoryUI.UIupdate("none", 0);
                break;

            case (2):

                Debug.Log("dropping item in slot 2: " + playerInventory[1].ToString());

                switch (playerInventory[1].ToString())
                {
                    case "SKS":
                        Instantiate(SKSPrefab, gameObject.transform.position, Quaternion.identity);
                        StartCoroutine(dropDelay());
                        break;

                    case "Big_Iron":
                        Instantiate(bigIronDropable, gameObject.transform.position, Quaternion.identity);
                        StartCoroutine(dropDelay());
                        break;
                }

                playerInventory.RemoveAt(1);
                playerInventory.Insert(1, "none");
                inventoryUI.UIupdate("none", 1);
                break;
        }
    }

    IEnumerator dropDelay()
    {
        canPickupItems = false;
        yield return new WaitForSeconds(0.1f);
        canPickupItems = true;
    }

    void ListCheck(string n) // debug
    {
        // Log each element one at a time
        foreach (var item in playerInventory)
        {
            Debug.Log(item.ToString());
        }

        // Concatenate all items into a single string
        // NOTE:  If the List is long, this would be more efficient with a
        // StringBuilder
        string result = "List contents: ";
        foreach (var item in playerInventory)
        {
            result += item.ToString() + ", ";
        }
        Debug.Log(n + " " + result);
    }

    //Health
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            health--;
            Debug.Log("Sus -1 Health ");
            if (health == 0)
            {
                Destroy(gameObject);
                Debug.Log("I have fallen and can't get up");
            }
        }
    }

    void GunGoBoom()
    {
        GameObject newbullet;

        switch (item)
        {
            case HeldItem.SKS:
                newbullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
                BulletRB = newbullet.GetComponent<Rigidbody2D>();
                BulletRB.AddForce(bulletSpawnPoint.right * bulletSpeed, ForceMode2D.Impulse);
                SKSammo--;
                Debug.Log("you have " + SKSammo);
                if (SKSammo <= 0)
                {
                    gunHasAmmo = false;
                    Debug.Log("out of ammo");

                    if (SKSspareAmmo > 0)
                    {
                        canshoot = false;
                        StartCoroutine(GunNoGoBoom(3f));
                    }
                }
                break;

            case HeldItem.BigIron:
                newbullet = Instantiate(BigIronBulletPrefab, BIGIRONbulletSpawnPoint.position, transform.rotation);
                BulletRB = newbullet.GetComponent<Rigidbody2D>();
                BulletRB.AddForce(bulletSpawnPoint.right * bulletSpeed, ForceMode2D.Impulse);
                BIGIRONammo --;
                Debug.Log("you have " + BIGIRONammo);
                if (BIGIRONammo <= 0)
                {
                    gunHasAmmo = false;
                    Debug.Log("out of ammo");

                    if (BIGIRONspareAmmo > 0)
                    {
                        canshoot = false;
                        StartCoroutine(GunNoGoBoom(6f));
                    }
                }
                break;
        }
    }
    IEnumerator GunNoGoBoom(float t)
    {
        yield return new WaitForSeconds(t);

        switch (item)
        {
            case HeldItem.SKS:
                SKSammo += 30;
                SKSspareAmmo--;
                gunHasAmmo = true;
                canshoot = true;

                Debug.Log(SKSammo);
                Debug.Log("done reload");
                Debug.Log("mags left" + SKSspareAmmo);
                break;

            case HeldItem.BigIron:
                BIGIRONammo += 6;
                BIGIRONspareAmmo--;
                gunHasAmmo = true;
                canshoot = true;

                Debug.Log(BIGIRONammo);
                Debug.Log("done reload");
                Debug.Log("Reloads left" + BIGIRONspareAmmo);
                break;
        }
    }
    IEnumerator FireRate(float t)
    {
        yield return new WaitForSeconds(t);
        fireCoolDown = true;
        Debug.Log("Gun go daka daka");
    }
  
    private void switchGuns()
    {
        switch (item)
        {
            case HeldItem.SKS:
                item = HeldItem.BigIron;
                newGun = Instantiate(BigIronPrefab, BigIronSpawnPoint.transform, false);
                Debug.Log("I have cleared leather");
                break;

            case HeldItem.BigIron:
                item = HeldItem.SKS;
                newGun = Instantiate(SKSPrefab, SKSspawnPoint.transform, false);
                Debug.Log("SKS go boom");
                break;
        }
    }
}

public enum HeldItem
{
    None, // 0
    SKS, // 1
    BigIron // 2
}