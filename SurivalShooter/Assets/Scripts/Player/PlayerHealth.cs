using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth ;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 0.5f;
    public Color flashColor = new Color(1f,0f,0f,0.1f);

    private Animator _anim;
    private AudioSource _playerAudio;
    private PlayerMovement _playerMovement;
    private PlayerShooting _playerShooting;
    private bool _isDead;
    private bool _damaged;

    private void Awake()
    {
        currentHealth = startingHealth;
        _playerMovement = GetComponent<PlayerMovement>();
        _anim = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
        _playerShooting = GetComponentInChildren<PlayerShooting>();
        //damageImage = GetComponent<Image>();
       
    }

    // Update is called once per frame
    private void Update()
    {
      if(_damaged)
        {
            // la funzione TakeDamage() viene utilizzata dello script "EnemyAttack"
            damageImage.color = flashColor;
        }
      else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        
        _damaged = false;
        healthSlider.value = currentHealth;
    }


    public void TakeDamage(int damage)      
    {
        _damaged = true;
        currentHealth -= damage;
        //healthSlider.value = currentHealth;
        _playerAudio.Play();

        // se la vita corrente del player è <= 0 ma il flag "isDead" non è ancora stato settato come "true"...
        if (currentHealth <= 0 && !_isDead)
            Death();
    }

    private void Death()
    {
        _isDead = true;

        //disattivazione del player
        _playerMovement.enabled = false;
        _playerShooting.enabled = false;

        _playerShooting.DisableEffects();

        _anim.SetTrigger("Dead");
        _playerAudio.clip = deathClip;
        _playerAudio.Play();
    }
}
    