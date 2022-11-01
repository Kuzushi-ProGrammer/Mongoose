


using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    Rigidbody2D PlayerRB;
    float walkVelocity = 10f;
    float health = 3f;
    public Vector2 playerPos;
    // public GameObject playerPrefab;
    public Transform playerSpawnPoint;
    //  GameObject newPlayer;
    //Gun stuff
    public GameObject SKSPrefab;
    public GameObject BigIronPrefab;
    GameObject newGun;
    public Transform SKSspawnPoint;
    public Transform BigIronSpawnPoint;

    bool hasGun = true;
    public GameObject bulletPrefab;
    public GameObject BigIronBulletPrefab;
    public Transform bulletSpawnPoint;
    public Transform BIGIRONbulletSpawnPoint;
    public Sprite[] bullet;

    int bulletSpeed = 30;

    public Sprite[] BigIronBullet;
    

    Rigidbody2D BulletRB;
    float ammo = 30;
    float BIGIRONammo = 6;
    bool gunHasAmmo = true;
    bool BigIronHasAmmo = true;
    float spareAmmo = 69;
    float BIGIRONspareAmmo = 5;
    bool canshoot = true;
    bool fireCoolDown = true;
    bool BigIron = true;
    bool SKS = false;
    bool canshootBIGIRON = true;
    // Start is called before the first frame update
    void Start()
    {
       newGun = Instantiate(BigIronPrefab, BigIronSpawnPoint.transform, false);
        PlayerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // playerPos = rb.position;
       //  mousepos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;


        //Vector2 playerDirection = mousePos - rb.transform.position;
        float lookAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle));
        // print(playerPos + "PLAYER");

        if (Input.GetKey(KeyCode.W))
        {
            PlayerRB.velocity = new Vector2(0f, walkVelocity);

        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerRB.velocity = new Vector2(- walkVelocity,0f);

        }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerRB.velocity = new Vector2(0f,- walkVelocity);

        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerRB.velocity = new Vector2(walkVelocity,0f);

        }
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            walkVelocity = 20f;
        }
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftControl))
        {
            walkVelocity = 5f;
        }
        else
        {
            walkVelocity = 10f;

        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            switchGuns();
        }

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
                        //reloadtimer += Time.deltaTime;
                        /*
                        if (reloaddelay >= reloadtimer)
                        {
                            ammo = ammo + 10;
                            spareAmmo = spareAmmo - 1;
                            Debug.Log("reloading");
                            gunHasAmmo = true;
                            reloadtimer = 0f;
                        } */


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
            SKS = true;
            BigIron = false;
           // Destroy (BigIronPrefab);
            newGun = Instantiate(SKSPrefab, SKSspawnPoint.transform, false);
            Debug.Log("SKS go boom");
        }
        else if (SKS == true) // switches gun to big iron
        {     
            SKS = false;
            BigIron = true;
             Destroy(SKSPrefab);
            newGun = Instantiate(BigIronPrefab, BigIronSpawnPoint.transform, false);
            Debug.Log("I have cleared leather");
        }
    }
}
