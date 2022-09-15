using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    float walkVelocity = 1f;
    float health = 3f;
    public Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = rb.position;
        // mousepos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;


        //Vector2 playerDirection = mousePos - rb.transform.position;
        float lookAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle));

        //print(mousePos);
        //print(lookAngle);
        print(playerPos + "PLAYER");


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
        else
        {
            walkVelocity = 1f;

        }

        //Health
       void OnTriggerEnter2D(Collider2D collision)
    {
            health = health - 1;
            Debug.Log("Sus -1 Health ");
            if(health== 0)
            {
                Destroy(gameObject);
                Debug.Log("I have fallen and can't get up");
            }
    }
    //if(hasGun)
    //{
    //if(imput.GetKeyDown(KeyCode.Space)
    //
}

}
