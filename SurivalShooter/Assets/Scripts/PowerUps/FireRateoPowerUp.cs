using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateoPowerUp : PowerUpGeneric
{
    public int fireRateoBuff;

    private void Awake()
    {
        base.buffDuration = 5;                       // variabile della superclasse
    }

    public override void PickUp(Collider player)
    {
        PlayerShooting playerStats = player.GetComponentInChildren<PlayerShooting>();
        var manager = playerStats.fireRateoManager;

        if (manager.buffsActivated < manager.maxBuffs)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            manager.buffDuration = this.buffDuration;

            if (!manager.isBuffed)
            {
                manager.isBuffed = true;
                playerStats.fireRateo -= fireRateoBuff;
                manager.buffTimer += buffDuration;
                manager.buffsActivated++;

            }
            else
            {
                manager.buffTimer += buffDuration;
                manager.buffsActivated++;
            }

            manager.buffsActivated--;
            Destroy(gameObject);
        }
    }
}
