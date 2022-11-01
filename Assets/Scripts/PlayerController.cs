


using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    HeldItem item;

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

    GameObject newGun;

    bool hasGun = true;
    bool canshoot = true;
    bool canshootBIGIRON = true;
    bool fireCoolDown = true;
    bool gunHasAmmo = true;
    bool BigIronHasAmmo = true;
    bool BigIron = true;
    bool SKS = false;

    float walkVelocity = 10f;
    float health = 3f;
    float ammo = 30;
    float BIGIRONammo = 6;
    float spareAmmo = 69;
    float BIGIRONspareAmmo = 5;

    int bulletSpeed = 30;

    void Start()
    {
        newGun = Instantiate(BigIronPrefab, BigIronSpawnPoint.transform, false);
        PlayerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Player Rotation
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float lookAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle));

        //Player Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            switchGuns();
        }

        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space))
        {
            if (gunHasAmmo)
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
                            StartCoroutine(fireRate());
                        }
                        break;

                    case HeldItem.BigIron:
                        if (fireCoolDown == true)
                        {
                            fireCoolDown = false;
                            StartCoroutine(BIGIRONfireRate());
                            BigIronGoBoom();
                        }
                        break;
                    default:
                        Debug.Log("defaulted");
                        break;
                }
            }

        }
        /*
        if (hasGun)
        {
            if (SKS)
            {
                if (gunHasAmmo)
                {

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        GunGoBoom();
                    }
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (fireCoolDown == true)
                        {
                            GunGoBoom();
                            fireCoolDown = false;
                            StartCoroutine(fireRate());
                        }
                    }
                }
            }
            if (BigIronHasAmmo)
            {
                if (BigIron)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        BigIronGoBoom();
                        fireCoolDown = false;
                        StartCoroutine(BIGIRONfireRate());
                    }
                }
            }
        }
        */
    }

    //Health
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            health = health - 1;
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
        if (canshoot)
        {
                GameObject newbullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
                Rigidbody2D BulletRB = newbullet.GetComponent<Rigidbody2D>();
                BulletRB.AddForce(bulletSpawnPoint.right * bulletSpeed, ForceMode2D.Impulse);
                ammo = ammo - 1;
                Debug.Log("you have " + ammo);
                if (ammo == 0)
                {
                    gunHasAmmo = false;
                    Debug.Log("out of ammo");

                    if (spareAmmo > 0)
                    {
                        canshoot = false;
                        StartCoroutine(gunNoGoBoom());
                    }
                }
            
        }
        

    }
    void BigIronGoBoom()
    {
        if (canshootBIGIRON)
        {


            GameObject newbullet = Instantiate(BigIronBulletPrefab, BIGIRONbulletSpawnPoint.position, transform.rotation);
            Rigidbody2D BulletRB = newbullet.GetComponent<Rigidbody2D>();
            BulletRB.AddForce(bulletSpawnPoint.right * bulletSpeed, ForceMode2D.Impulse);
            BIGIRONammo = BIGIRONammo - 1;
            Debug.Log("you have " + BIGIRONammo);
            if (BIGIRONammo == 0)
            {
                BigIronHasAmmo = false;
                Debug.Log("out of ammo");

                if (BIGIRONspareAmmo > 0)
                {
                    canshootBIGIRON = false;
                    StartCoroutine(BIGIRONNoGoBoom());
                }
            }

        }


    }

    IEnumerator gunNoGoBoom()
    {
        Debug.Log("reloading");
        yield return new WaitForSeconds(3);
        canshoot = true;
        ammo = ammo + 30;
        spareAmmo = spareAmmo - 1;
        Debug.Log(ammo);
        Debug.Log("done reload");
        Debug.Log("mags left"+spareAmmo);
        gunHasAmmo = true;
    }
    IEnumerator BIGIRONNoGoBoom()
    {
        Debug.Log("reloading");
        yield return new WaitForSeconds(6);
        canshootBIGIRON = true;
        BIGIRONammo = BIGIRONammo + 6;
        BIGIRONspareAmmo = BIGIRONspareAmmo - 1;
        Debug.Log(ammo);
        Debug.Log("done reload");
        Debug.Log("Reloads left" + BIGIRONspareAmmo);
        BigIronHasAmmo = true;
    }
    IEnumerator fireRate()
    {
        yield return new WaitForSeconds(0.1f);
        fireCoolDown = true;
        Debug.Log("Gun go daka daka");
    }
    IEnumerator BIGIRONfireRate()
    {
        yield return new WaitForSeconds(1f);
        fireCoolDown = true;
       
    }
    private void switchGuns()
    {
        if (BigIron == true) // switches gun to sks
        {
            item = HeldItem.SKS;
            SKS = true;
            BigIron = false;
            newGun = Instantiate(SKSPrefab, SKSspawnPoint.transform, false);
            Debug.Log("SKS go boom");
        }
        else if (SKS == true) // switches gun to big iron
        {
            item = HeldItem.BigIron;
            SKS = false;
            BigIron = true;
            Destroy(SKSPrefab);
            newGun = Instantiate(BigIronPrefab, BigIronSpawnPoint.transform, false);
            Debug.Log("I have cleared leather");
        }
    }
}

public enum HeldItem
{
    None, // 0
    SKS, // 1
    BigIron // 2
}