using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    // Start is called before the first frame update
    void Start()
    {
        // invocazione costante del metodo Spawn
        InvokeRepeating("Spawn",spawnTime,spawnTime);
    }

    // Questo metodo gestisce lo spawn di un solo tipo di nemico
    void Spawn()
    {
        if (playerHealth.currentHealth <= 0)
            return;

        //scelta casuale di uno spawnPoint (creati in Unity)
        int spawnPointIndex = Random.Range(0,spawnPoints.Length);

        //instanziazione del nemeci in una data posizione e rotazione
        Instantiate(enemy,spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
