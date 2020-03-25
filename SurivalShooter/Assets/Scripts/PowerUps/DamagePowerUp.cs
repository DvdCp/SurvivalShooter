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

        if (manager.buffsActivated < manager.maxBuffs)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            if (!manager.isBuffed)
            {
                manager.isBuffed = true;
                playerStats.gunShootDamage += damageBuff;
                manager.buffTimer += buffDuration;
                manager.buffsActivated++;

            }
            else
            {
                manager.buffTimer += buffDuration;
                manager.buffsActivated++;
            }
    
            Destroy(gameObject);
        }
    }

}
