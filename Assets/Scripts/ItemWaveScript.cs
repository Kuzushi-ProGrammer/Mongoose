using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWaveScript : MonoBehaviour
{
    [SerializeField] GameObject targetObject;

    float speed = 4f;
    float height = 0.005f;
    

    void Update()
    {
        Vector3 pos = transform.position;
        float newY = Mathf.Sin(Time.time * speed) * height + pos.y; ;
        transform.position = new Vector3(pos.x, newY, 0);
    }
}
