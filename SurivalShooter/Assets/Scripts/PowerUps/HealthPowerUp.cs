
using System.Collections;
using UnityEngine;

public class HealthPowerUp : PowerUpGeneric
{
    public int healthBuff;
   
    private void Awake()
    {
        base.buffDuration = 0;
    }

    public override void PickUp(Collider player)
    {
        PlayerHealth playerStats = player.GetComponentInChildren<PlayerHealth>();

        if(playerStats.currentHealth == 100)
        {
            return;
        }

                                                                                // dopo aver preso il power up, questo si smaterializza
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        if (playerStats.currentHealth + healthBuff > playerStats.startingHealth)
        {
            playerStats.currentHealth = playerStats.startingHealth;
        }
        else
        {
            playerStats.currentHealth += healthBuff;
        }


        Destroy(gameObject);


    }


}

