using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    GameObject newPlayer;
    public GameObject playerSpawnPoint;
    int allotedMongeese = 1;
    public GameObject enemyPrefab;
    GameObject newEnemy;
    public GameObject enemySpawnPoint1;
    public GameObject enemySpawnPoint2;
    public GameObject enemySpawnPoint3;

    PlayerController playerController;
    AudioSource audioSource;
    [SerializeField] AudioClip death;
    [SerializeField] GameObject cameraObject;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void mongooseDie()
    {
        audioSource = cameraObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(death);
    }
   
}
enum DIRECTION 
{
    UP = 0,
    RIGHT = 1,
    LEFT = 2,
    DOWN = 3,
    STAY = 4,
}
