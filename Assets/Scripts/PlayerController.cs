using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    float walkVelocity = 1f;
    Vector3 mousepos;
    float health = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //mousepos = Input.mousePosition;
        //rb.transform.LookAt(mousepos.x);
       // print(mousepos);


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
            walkVelocity = 20f;
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
