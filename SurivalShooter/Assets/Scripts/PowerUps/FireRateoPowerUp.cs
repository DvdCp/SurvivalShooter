using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateoPowerUp : PowerUpGeneric
{
    public float fireRateoBuff;

    private void Awake()
    {
        base.buffDuration = 5;                       // variabile della superclasse
    }

    public override void PickUp(Collider player)
    {
        PlayerShooting playerStats = player.GetComponentInChildren<PlayerShooting>();
        var manager = playerStats.fireRateoManager;

        manager.collectPowerUp(this);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        playerStats.fireRateo = fireRateoBuff;

        Destroy(gameObject);

        //if (manager.canCollectPowerUp())
        //{
        //
        //}
    }
}


