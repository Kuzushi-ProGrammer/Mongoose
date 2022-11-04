


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

    public Transform playerSpawnPoint;
    public Transform SKSspawnPoint;
    public Transform BigIronSpawnPoint;
    public Transform bulletSpawnPoint;
    public Transform BIGIRONbulletSpawnPoint;

    public GameObject SKSPrefab;
    public GameObject BigIronPrefab;
    public GameObject bulletPrefab;
    public GameObject BigIronBulletPrefab;

    public Sprite[] bullet;
    public Sprite[] BigIronBullet;

    [SerializeField] GameObject uiObject;
    GameObject newGun;

    public List<string> playerInventory = new() {"none", "none"};

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
    }

    void Update()
    {
        PlayerRotation();
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            inventoryUI.ChangeActiveSlot(1);
            Debug.Log(playerInventory[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            inventoryUI.ChangeActiveSlot(2);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            switchGuns(); // change to switch inventory slots
        }

        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space))
        {
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
                            StartCoroutine(FireRate(0.1f));
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

        Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);
        transform.position = transform.position + movement;
    }

    public void AddItemToInventory(string item)
    {
        playerInventory.RemoveAt(0);
        playerInventory.Insert(0, item);
        inventoryUI.UIupdate(item, 0);
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