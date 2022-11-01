using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint2 : MonoBehaviour
{
    public bool playerOnCap = false;
    public bool isCaptured = false;
    public int progress = 0;
    public bool delayHasPassed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (delayHasPassed == false)
        {
            StartCoroutine(BaseCaptureDelay());
            Debug.Log("Delay has pased =" + delayHasPassed);
         
           
        }
        if (playerOnCap == true)
        {
            if (delayHasPassed == true)
            {
                delayHasPassed = false;
                progress = progress + 1;
                Debug.Log("progress has been made" + progress);
                if (progress == 10)
                {
                    isCaptured = true;
                    Debug.Log("the Mongeese have captured the zone");
                }
            }

        }
        if (playerOnCap == false)
        {
            progress = 0;
            Debug.Log("progress has been reset" + progress);
        }

        if (isCaptured == true)
        {
            progress = 10;
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerMapAreas>() == null)
        {
            Debug.Log("the zone is contested");
            playerOnCap = false;
        }
        else if (collider.GetComponent<PlayerMapAreas>() != null)
        {
            Debug.Log("take some food, go on take it, do it you won't.");
            playerOnCap = true;
        }


       
    }
    IEnumerator BaseCaptureDelay()
    {
        yield return new WaitForSeconds(20.0f);
        delayHasPassed = true;
    }
}


