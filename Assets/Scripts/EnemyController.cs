using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            health--;
            Debug.Log("you've shot me what did I so to you?");
            if (health == 0)
            {
                Destroy(gameObject);
                Debug.Log("I have fallen and I can't get up");
            }
        }
    }
}