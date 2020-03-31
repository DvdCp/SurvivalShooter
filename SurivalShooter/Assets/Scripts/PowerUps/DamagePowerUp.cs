using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerUp : PowerUpGeneric
{
    public int damageBuff;

    private void Awake()
    {
        base.buffDuration = 5;                       // variabile della superclasse
    }

    public override void PickUp(Collider player)
    {
        PlayerShooting playerStats = player.GetComponentInChildren<PlayerShooting>();
        var manager = playerStats.damageManager;

        if (manager.canCollectPowerUp())
        {
            manager.collectPowerUp(this);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            playerStats.gunShootDamage += damageBuff; 
            
            Destroy(gameObject);
        }
        /*
         *else
         * nothing
         */
    }
}
