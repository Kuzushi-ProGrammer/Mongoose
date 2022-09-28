using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ProjectileNetwork : NetworkBehaviour
{
    Projectile projectile;
    Rigidbody2D projectileRigidbody;
    public GameObject projectileprefab;
    public float projectileSpeed = 5f;

    [Command]
    public void SpawnProjectile()
    {
        GameObject projobj = Instantiate(projectileprefab, new Vector3(0, 0, 0), Quaternion.identity);
        projectile = projobj.GetComponent<Projectile>();
        projectileRigidbody = projobj.GetComponent<Rigidbody2D>();
        projectileRigidbody.velocity = new Vector3(projectileSpeed, 0, 0);

        NetworkServer.Spawn(projobj);
    }



}
