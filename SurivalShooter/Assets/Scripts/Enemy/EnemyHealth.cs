
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public int powerUpDropChance;
    public GameObject[] powerUps;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    // Start is called before the first frame update
    void Awake()    //iniziallizazione dello script e dei rispettivi collegamenti con i vari componenti
    {
        currentHealth = startingHealth;

        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();


    }

    // Update is called once per frame
    void Update()       // questa funzione verifica solo se l'enemy in questione sta affondando
    {
        if (isSinking)
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);

    }

    public void TakeDamage(int damage, Vector3 hitPoint)
    {
        if (isDead) // verifica se l'enemy è già morto
            return;

        enemyAudio.Play();
        currentHealth -= damage;
                                                                                // fuoriuscita delle particella dal punto in cui l'enemy è stato colpito
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
            Death();
    }

    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;                                       //in questo modo, i proiettili posso trapassare l'enemy morente

        anim.SetTrigger("Death");
        enemyAudio.clip = deathClip;                                            // deathClip è passata dall'Inspector
        enemyAudio.Play();

        if (Random.Range(0, 100) <= powerUpDropChance)
        {
            Instantiate(powerUps[Random.Range(0, 3)], transform.position, transform.rotation);

        }
    }

    public void StartSinking()                                                  // mentre l'enemy muore, viene disattivato il suo Nav Mesh Agent N.B questo funzione viene chiamata dall'Animator
    {
        GetComponent<NavMeshAgent>().enabled = false;

                                                                                // si rende kinematic il rigigbody dell'enemy per evitare che Unity vada a calcolare la fisica dell'enemy morente
        GetComponent<Rigidbody>().isKinematic = true;

        isSinking = true;

        ScoreManager.score += scoreValue;

        Destroy(gameObject, 2f);                                                // distrugge questo enemy dopo 2 secondi



    }


}