using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    float walkVelocity = 1f;
    Vector2 mousepos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousepos = Input.mousePosition;
        // transform.LookAt(mousepos);
        // gameObject.transform.LookAt(mousepos);
        print(mousepos);

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
        if (Input.GetKey(KeyCode.RightShift))
        {
            walkVelocity = 2f;
        }
    }
}
