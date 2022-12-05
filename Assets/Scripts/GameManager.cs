using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip death;
    [SerializeField] GameObject cameraObject;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void mongooseDie()
    {
        audioSource = GetComponent<AudioSource>();
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
