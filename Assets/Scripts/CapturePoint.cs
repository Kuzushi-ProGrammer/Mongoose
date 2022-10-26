using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<PlayerMapAreas>() != null)
        {
            Debug.Log("take some food, go on take it, do it you won't.");
        }
    }
}
