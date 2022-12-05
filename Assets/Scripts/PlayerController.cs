using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    InventoryUI inventoryUI;
    Rigidbody2D BulletRB;
    AudioSource audioSource;

    public TMPro.TMP_Text healthText;
    public TMPro.TMP_Text bulletText;

    [SerializeField] AudioClip pew;
    [SerializeField] AudioClip pewButLower;
    [SerializeField] AudioClip hurt;

    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Transform BIGIRONbulletSpawnPoint;

    [SerializeField] GameObject SKSPrefab;
    [SerializeField] GameObject BigIronPrefab;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject BigIronBulletPrefab;
    [SerializeField] GameObject uiObject;
    [SerializeField] GameObject bigIronDropable;
    [SerializeField] GameObject SKSGameObject;
    [SerializeField] GameObject BigIronGameObject;
    [SerializeField] GameObject gameManagerObject;

    public List<string> playerInventory = new();
    public List<string> keyInventory = new();

    public bool canPickupItems = true;
    bool fireCoolDown = true;
    bool canshoot = true;
    bool canShootBigIron = true;
    bool gunHasAmmo = true;
    bool bigIronHasAmmo = true;

    float SKSammo = 30;
    float BIGIRONammo = 6;
    float SKSspareAmmo = 420;
    float BIGIRONspareAmmo = 5;

    public int health = 3;
    int bulletSpeed = 69;
    int currentActiveSlot;

    void Start()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        inventoryUI = uiObject.GetComponent<InventoryUI>();
        audioSource = gameObject.GetComponent<AudioSource>();

        playerInventory.Add("none");
        playerInventory.Add("none");
    }

    private void FixedUpdate()
    {
        PlayerRotation();
        PlayerMovement();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (currentActiveSlot == 1)
            {
                currentActiveSlot = 0;
                inventoryUI.ChangeActiveSlot(0);
                ActivateHeldItem(3);
            }
            else
            {
                currentActiveSlot = 1;
                inventoryUI.ChangeActiveSlot(1);
                ActivateHeldItem(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (currentActiveSlot == 2)
            {
                currentActiveSlot = 0;
                inventoryUI.ChangeActiveSlot(0);
                ActivateHeldItem(3);
            }
            else
            {
                currentActiveSlot = 2;
                inventoryUI.ChangeActiveSlot(2);
                ActivateHeldItem(1);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RemoveItemFromInventory(currentActiveSlot); //RemoveItemFromInventory(2);
        }

        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space)) // shoot gun
        {
            if (fireCoolDown)
            {
                if (SKSGameObject.activeSelf && gunHasAmmo && canshoot)
                {
                    GunGoBoom("SKS");
                    fireCoolDown = false;
                    StartCoroutine(FireRate(0.1f));
                }
                else if (BigIronGameObject.activeSelf && bigIronHasAmmo && canShootBigIron)
                {
                    GunGoBoom("Big_Iron");
                    fireCoolDown = false;
                    StartCoroutine(FireRate(1f));
                }
            }
        }
    }

    #region [Player Inventory]
    void ActivateHeldItem(int n) // n is the index in the list
    {   
        if (n != 3)
        {
            switch (playerInventory[n])
            {
                case "SKS":
                    BigIronGameObject.SetActive(false);
                    SKSGameObject.SetActive(true);
                    break;

                case "Big_Iron":
                    BigIronGameObject.SetActive(true);
                    SKSGameObject.SetActive(false);
                    break;
            }
        }
        else // 3 is null slot
        {
            BigIronGameObject.SetActive(false);
            SKSGameObject.SetActive(false);
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
        if (playerInventory[0] == "none")
        {
            playerInventory.RemoveAt(0);
            playerInventory.Insert(0, item);
            inventoryUI.UIupdate(item, 0);

            currentActiveSlot = 1;
            inventoryUI.ChangeActiveSlot(1);
            ActivateHeldItem(0);
        }
        else if (playerInventory[1] == "none")
        {
            playerInventory.RemoveAt(1);
            playerInventory.Insert(1, item);
            inventoryUI.UIupdate(item, 1);

            currentActiveSlot = 2;
            inventoryUI.ChangeActiveSlot(2);
            ActivateHeldItem(1);
        }
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
                        SKSGameObject.SetActive(false);
                        StartCoroutine(dropDelay());
                        break;

                    case "Big_Iron":
                        Instantiate(bigIronDropable, gameObject.transform.position, Quaternion.identity);
                        BigIronGameObject.SetActive(false);
                        StartCoroutine(dropDelay());
                        break;
                }

                playerInventory.RemoveAt(0);
                playerInventory.Insert(0, "none");
                inventoryUI.UIupdate("none", 0);
                ActivateHeldItem(3);
                break;

            case (2):

                Debug.Log("dropping item in slot 2: " + playerInventory[1].ToString());

                switch (playerInventory[1].ToString())
                {
                    case "SKS":
                        Instantiate(SKSPrefab, gameObject.transform.position, Quaternion.identity);
                        SKSGameObject.SetActive(false);
                        StartCoroutine(dropDelay());
                        break;

                    case "Big_Iron":
                        Instantiate(bigIronDropable, gameObject.transform.position, Quaternion.identity);
                        BigIronGameObject.SetActive(false);
                        StartCoroutine(dropDelay());
                        break;
                }

                playerInventory.RemoveAt(1);
                playerInventory.Insert(1, "none");
                inventoryUI.UIupdate("none", 1);
                ActivateHeldItem(3);
                break;
        }
    }

    IEnumerator dropDelay()
    {
        canPickupItems = false;
        yield return new WaitForSeconds(0.1f);
        canPickupItems = true;
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            health--;
            healthText.SetText(health.ToString());
            if (health <= 0)
            {
                Destroy(gameObject);
                gameManager.mongooseDie();
            }
            else
            {
                audioSource.PlayOneShot(hurt);
            }
        }
    }

    #region [Shooting and Weaponry]

    void GunGoBoom(string gun)
    {
        GameObject newbullet;

        switch (gun)
        {
            case "SKS":
                newbullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
                BulletRB = newbullet.GetComponent<Rigidbody2D>();
                BulletRB.AddForce(bulletSpawnPoint.right * bulletSpeed, ForceMode2D.Impulse);
                SKSammo--;
                if (SKSammo <= 0)
                {
                    gunHasAmmo = false;
                    if (SKSspareAmmo > 0)
                    {
                        canshoot = false;
                        StartCoroutine(GunNoGoBoom(3f, "SKS"));
                    }
                }
                else
                {
                    audioSource.PlayOneShot(pew);
                }
                break;

            case "Big_Iron":
                newbullet = Instantiate(BigIronBulletPrefab, BIGIRONbulletSpawnPoint.position, transform.rotation);
                BulletRB = newbullet.GetComponent<Rigidbody2D>();
                BulletRB.AddForce(bulletSpawnPoint.right * bulletSpeed, ForceMode2D.Impulse);
                BIGIRONammo --;
                if (BIGIRONammo <= 0)
                {
                    bigIronHasAmmo = false;
                    if (BIGIRONspareAmmo > 0)
                    {
                        canShootBigIron = false;
                        StartCoroutine(GunNoGoBoom(6f, "Big_Iron"));
                    }
                }
                else
                {
                    audioSource.PlayOneShot(pewButLower);
                }
                break;
        }
    }

    IEnumerator GunNoGoBoom(float t, string gun)
    {
        yield return new WaitForSeconds(t);

        switch (gun)
        {
            case "SKS":
                SKSammo += 30;
                SKSspareAmmo--;
                gunHasAmmo = true;
                canshoot = true;
                break;

            case "Big_Iron":
                BIGIRONammo += 6;
                BIGIRONspareAmmo--;
                bigIronHasAmmo = true;
                canShootBigIron = true;
                break;
        }
    }

    IEnumerator FireRate(float t)
    {
        yield return new WaitForSeconds(t);
        fireCoolDown = true;
    }

    #endregion
}