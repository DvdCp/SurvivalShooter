
using System.Collections;
using UnityEngine;

public class SpeedPowerUp : PowerUpGeneric
{
    public int speedBuff;

    private void Awake()
    {
        base.buffDuration = 5;
    }

    public override void PickUp(Collider player)
    {
        PlayerMovement playerStats = player.GetComponentInChildren<PlayerMovement>();
        var manager = playerStats.speedManager;
       
        if (manager.buffsActivated < manager.maxBuffs)
        {      
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            if (!manager.isBuffed)
            {
                manager.isBuffed = true;
                playerStats.speed += speedBuff;
                manager.buffTimer += this.buffDuration;
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
