


using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Mirror;
using System.Xml.Serialization;
using Mirror.Examples.Tanks;

public class PlayerController : NetworkBehaviour
{
    Rigidbody2D PlayerRB;
    Rigidbody2D BulletRB;
    SpriteRenderer spriteRenderer;

    public Vector2 playerPos;

    public Transform playerSpawnPoint;
    public Transform bulletSpawnPoint;

    [SerializeField] Sprite bullet;
    [SerializeField] Sprite mongooseSprite;
    [SerializeField] Sprite racoonSprite;
    [SerializeField] Sprite debugSprite;

    [SyncVar] Sprite currentPlayerSprite;

    public GameObject bulletPrefab;

    public string species;

    float walkVelocity = 10f;
    float health = 3f;

    float ammo = 10;
    float spareAmmo = 10;
    float reloadtimer = 0f;
    float reloaddelay = 3f;

    bool hasGun = true;
    bool gunHasAmmo = true;
    bool canshoot = true;
    bool fireCoolDown = true;

    bool canHide;
    bool isDisguised;
    bool canDisguise;

    int bulletSpeed = 30;

    void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayer();

        PlayerRotation();

        SpeciesCheck();
    }
    //Player Rotation
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

    //Player M
    void HandlePlayer()
    {
        if (isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);
            transform.position = transform.position + movement;

            if (hasGun && gunHasAmmo)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space))
                {
                    if (fireCoolDown == true)
                    {
                        GunGoBoom();
                        fireCoolDown = false;
                        StartCoroutine(fireRate());
                    }
                }
            }

            if (canDisguise)
            {

            }
        }

    }

    //Species Changing
    public void ChangeSpeciesToMongoose()
    {
        species = "mongoose";
        canHide = false;
        canDisguise = true;
    }
    public void ChangeSpeciesToRacoon()
    {
        species = "racoon";
        canHide = true;
        canDisguise = false;
    }

    public void SpeciesCheck()
    {
        if (isLocalPlayer)
        {
            switch (species)
            {
                case "mongoose":
                    spriteRenderer.sprite = mongooseSprite;
                    break;

                case "disguised_mongoose":
                    spriteRenderer.sprite = debugSprite;
                    break;

                case "racoon":
                    spriteRenderer.sprite = racoonSprite;
                    break;

                default:
                    spriteRenderer.sprite = debugSprite;
                    break;
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

        if (collision.tag == "TrashCan" && canHide && Input.GetKeyDown(KeyCode.E))
        {
            // something something hide in trashcan
        }

    }

 // Bullet will only spawn properly on server with [Command] and only spawn properly on client with no command 
    [Command]
    public void GunGoBoom()
    {
        if (canshoot)
        {
            GameObject newbullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
            Rigidbody2D BulletRB = newbullet.GetComponent<Rigidbody2D>();
            BulletRB.AddForce(bulletSpawnPoint.right * bulletSpeed, ForceMode2D.Impulse);
            NetworkServer.Spawn(newbullet);

            ammoDecrease();

        }
    }

    void ammoDecrease()
    {
        if (isLocalPlayer)
        {
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

    IEnumerator fireRate()
    {
        yield return new WaitForSeconds(0.1f);
        fireCoolDown = true;
        Debug.Log("Gun go daka daka");
    }


}
