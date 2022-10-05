


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
    bool hasGun = true;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public Sprite[] bullet;
    int bulletSpeed = 30;
    Rigidbody2D BulletRB;
    float ammo = 10;
    bool gunHasAmmo = true;
    float spareAmmo = 10;
    bool canshoot = true;
    float reloadtimer = 0f;
    float reloaddelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
       // newPlayer = Instantiate(playerPrefab, playerspawnpoint.transform, false);
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

        if (hasGun)
        {
            if (gunHasAmmo)
            {
                
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GunGoBoom();
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    GunGoBoom();
                }
            }
        
        }
    }
    //Health
    private void OnTriggerEnter2D(Collider2D collision)
    {
        health = health - 1;
        Debug.Log("Sus -1 Health ");
        if (health == 0)
        {
            Destroy(gameObject);
            Debug.Log("I have fallen and can't get up");
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

    IEnumerator gunNoGoBoom()
    {
        Debug.Log("reloading");
        yield return new WaitForSeconds(3);
        canshoot = true;
        ammo = ammo + 10;
        spareAmmo = spareAmmo - 1;
        Debug.Log(ammo);
        Debug.Log("done reload");
        gunHasAmmo = true;
    }
 
}
