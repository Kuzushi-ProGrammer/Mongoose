using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 3;
    public bool hasBeenShot = false;
    GameObject newbullet;

    public GameObject bulletPrefab;
    public GameObject bulletSpawnPoint;
    int bulletSpeed = 100;
    bool hasShotBack1 = false;
    DIRECTION enemyDirection = DIRECTION.DOWN;
    Rigidbody2D enemyRB;
    int enemySpeed = 10;
    bool changeDirectionCoolDown = true;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenShot == true)
        {
            gunGoBoom();
        }
        handleMovement();
        changeDirection();
       

    }
    void gunGoBoom()
    {
        hasBeenShot = false;
        newbullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Debug.Log("bang bang bang");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            health--;
            hasBeenShot = true;
            Debug.Log("you've shot me what did I do to you?");
            if (health == 0)
            {
                Destroy(gameObject);
                Debug.Log("I have fallen and I can't get up");
            }
        }
    }
    void changeDirection()
    {
        
        StartCoroutine(changeDirectionCoolDownTimer());
        if (changeDirectionCoolDown == false)
        {
            if (numbers() <= 6)
            {
                enemyDirection = (DIRECTION)Random.Range(0, 5);
                changeDirectionCoolDown = true;
            }
        }
    }
        int numbers()
        {
            int randomNumber = Random.Range(1,500);
            return randomNumber;
        }
        void handleMovement()
        {
            switch (enemyDirection)
            {
                case DIRECTION.DOWN:
                    enemyRB.velocity = new Vector2(0f, -enemySpeed);
                    break;
                case DIRECTION.UP:
                    enemyRB.velocity = new Vector2(0f, enemySpeed);
                    break;
                case DIRECTION.RIGHT:
                    enemyRB.velocity = new Vector2(enemySpeed, 0f);
                    break;
                case DIRECTION.LEFT:
                    enemyRB.velocity = new Vector2(-enemySpeed, 0f);
                    break;
                case DIRECTION.STAY:
                    enemyRB.velocity = new Vector2(0f, 0f);
                    break;

            }
        }
        IEnumerator changeDirectionCoolDownTimer()
        {
            yield return new WaitForSeconds(10);
            changeDirectionCoolDown = false;
            Debug.Log("I could turn but I don't feel like it");
        }
    
}

