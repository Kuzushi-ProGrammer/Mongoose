using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] GameObject bossExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            health--;
            if (health == 0)
            {
                Destroy(bossExit);
                Destroy(gameObject);
            }
        }
    }

}
