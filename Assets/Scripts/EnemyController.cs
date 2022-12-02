using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 3;
    public bool hasBeenShot = false;
    bool hasShotBack1 = false;
    DIRECTION enemyDirection = DIRECTION.DOWN;
    Rigidbody2D enemyRB;
    
    int enemySpeed = 10;
    bool changeDirectionCoolDown = true;

    //rotate
    private Vector3 v_diff;
    private float atan2;
    public Transform target;

    //Bullet
    Rigidbody2D BulletRB;
    int bulletSpeed = 69;
    GameObject newbullet;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
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
        Rot();
       if(numbers2() <= 1)
        {
            gunGoBoom();
        }
        
    }
    void gunGoBoom()
    {
        hasBeenShot = false;
        newbullet = Instantiate(bulletPrefab,bulletSpawnPoint.position, transform.rotation);
        BulletRB = newbullet.GetComponent<Rigidbody2D>();
        BulletRB.AddForce(bulletSpawnPoint.up * -bulletSpeed, ForceMode2D.Impulse);
       // Debug.Log("bang bang bang");
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
    int numbers2()
    {
        int randomNumber = Random.Range(1, 500);
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
            yield return new WaitForSeconds(5);
            changeDirectionCoolDown = false;
           
        }
    private void Rot()
    {

        v_diff = (target.transform.position - transform.position);
        atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg - 270f);
    }
    
}

