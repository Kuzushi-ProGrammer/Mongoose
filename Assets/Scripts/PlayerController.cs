


using UnityEngine;


public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    float walkVelocity = 1f;
    float health = 3f;
    // public Vector2 playerPos;

    //Gun stuff
    bool hasGun = true;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public Sprite[] bullet;
    float bulletSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // playerPos = rb.position;
        // mousepos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;


        //Vector2 playerDirection = mousePos - rb.transform.position;
        float lookAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle));
        // print(playerPos + "PLAYER");

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + walkVelocity);

        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(rb.velocity.x - walkVelocity, rb.velocity.y);

        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - walkVelocity);

        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(rb.velocity.x + walkVelocity, rb.velocity.y);

        }
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            walkVelocity = 2f;
        }
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftShift))
        {
            walkVelocity = 0.5f;
        }
        else
        {
            walkVelocity = 1f;

        }


        if (hasGun)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                GunGoBoom();
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
        GameObject newbullet = Instantiate(bulletPrefab, bulletSpawnPoint.gameObject.transform. position, transform.rotation);
        Rigidbody2D bulletRB = newbullet.GetComponent<Rigidbody2D>();
        //newbullet.GetComponent<bulletSpwanPoint>().SetVelocity(transform.up * bulletSpeed);
        bulletRB.velocity = new Vector2(0f, bulletSpeed);
    }
}
