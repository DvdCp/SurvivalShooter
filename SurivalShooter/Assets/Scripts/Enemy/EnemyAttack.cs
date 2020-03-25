using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public float timeBetweenAttacks = 0.5f;

    float timer; //mantiene il tempo che intercorre tra un attacco ed un altro. Si aggiorna con ogni Update()
    bool isPlayerInRange;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    
    }

    private void OnTriggerEnter(Collider other) // rilevazione del player tra tutti i gameObjects in collisione con il trigger
    {
        if (other.gameObject == player)
            isPlayerInRange = true;
    }

    private void OnTriggerExit(Collider other) // verifica dell'uscita del player dal range 
    {
        if (other.gameObject == player)
            isPlayerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && isPlayerInRange && enemyHealth.currentHealth > 0 )
            Attack();

        if (playerHealth.currentHealth <= 0)
            anim.SetTrigger("PlayerDeath");
    }

    void Attack()
    {
        timer = 0;  //attacco effettuato; timer resettato

        if (playerHealth.currentHealth > 0)
            playerHealth.TakeDamage(damage);     // invocazione del metodo TakeDamge() di playerHealth
    }
}
