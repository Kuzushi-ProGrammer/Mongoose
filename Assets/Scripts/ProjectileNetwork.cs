using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ProjectileNetwork : NetworkBehaviour
{
    Projectile projectile;
    public GameObject projectileprefab;
    public float projectileSpeed = 5f;

    [Command]
    public void SpawnProjectile()
    {
        GameObject projobj = Instantiate(projectileprefab, new Vector3(projectileSpeed, 0, 0), Quaternion.identity);
        Projectile projectile = projobj.GetComponent<Projectile>();


        NetworkServer.Spawn(projobj);
    }



}
