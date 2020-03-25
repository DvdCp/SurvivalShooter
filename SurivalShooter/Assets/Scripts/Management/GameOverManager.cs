using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float restartDelay = 5f;

    Animator anim;
    float restartTimer;

    private void Awake()
    {
        //playerHealth = GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
           
           restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                // Metodo Obsoleto
                //Application.LoadLevel(Application.loadedLevel);
                SceneManager.LoadScene("Level-01");

            }

        }
    }
}
