using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    NavMeshAgent nav;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();

        nav = GetComponent<NavMeshAgent>();

        enemyHealth = GetComponent<EnemyHealth>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // se sia l'enemy che il player sono vivi..
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0 )
        {
            // l'enemy contiua a cercare il player
            nav.SetDestination(player.position);
        }
        else
        {   
            // altrimenti no(viene disabilitato il nav mesh agent)
            nav.enabled = false;
        }


    }
}
