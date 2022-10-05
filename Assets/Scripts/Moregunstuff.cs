using UnityEngine;

public class Moregunstuff : MonoBehaviour
{
    public GameObject floorGun;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Destroy(gameObject);
    }
}
