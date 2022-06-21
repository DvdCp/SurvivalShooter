using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// questo script andrà attaccato a Player/GunEndBarrel

public class PlayerShooting : MonoBehaviour
{
    private const  int NORMAL_GUN_SHOT_DAMAGE = 20;
    private const float NORMAL_FIRE_RATEO = 0.20f;

    public Text ammoCount;
    public int gunShootDamage;
    public float fireRateo;
    public float range;

    private bool _isReloading;

    public Material normalShotMaterial;
    public Material damageBuffedShotMaterial;
    public Material fireRateoBuffedShotMaterial;
    
    private int maxRounds = 30;
    private int _roundsInMag;
    
    /*--------------------*/
    /*Buff Managers*/
    private bool _isDamageBoosted;
    private bool _isFireRateoBoosted;
    private PowerUpManager[] _managers;
    
    private PowerUpManager _fireRateoManager;
    public PowerUpManager fireRateoManager
    {
        get => _fireRateoManager;
        private set => _fireRateoManager = value;
    }

    private PowerUpManager _damageManager;
    public PowerUpManager damageManager
    {
        get => _damageManager;
        private set => _damageManager = value;
    }

    /*--------------------*/

    private Animator _anim;
    private float _timer;
    private Ray _shootRay;                   
    private RaycastHit _shootHit;            
    private int _shootableMask;              
    private ParticleSystem _gunParticles;
    private LineRenderer _gunLine;

    private AudioSource[] _audioSources;
    private AudioSource _source;
    private AudioClip _gunShot;
    private AudioClip _dryGun;
    private AudioClip _reloadSound;

    private Light _gunLight;
    private float _effectsDisplayTime = 0.2f;


    private void Awake()      
    {
        _shootableMask = LayerMask.GetMask("Shootable");

        gunShootDamage = NORMAL_GUN_SHOT_DAMAGE;

        _roundsInMag = maxRounds;
        ammoCount.text = "" + _roundsInMag; 

        _gunParticles = GetComponent<ParticleSystem>();
        _gunLine = GetComponent<LineRenderer>();

        // Ottenimento delle clip audio nell'ordine mostrato nell'Inspector
        _audioSources = GetComponents<AudioSource>();
        _source = _audioSources[0];
        _gunShot = _audioSources[0].clip;
        _dryGun = _audioSources[1].clip;
        _reloadSound = _audioSources[2].clip;

        _gunLight = GetComponent<Light>();
        _anim = GetComponent<Animator>();

        _managers = GetComponents<PowerUpManager>();
        damageManager = _managers[0];    // primo manager in base all'ordine top-down dell'inspector
        fireRateoManager = _managers[1]; //secondo manager in base all'ordine top-down dell'inspector
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        
        damageManager.ManageBuffs(out _isDamageBoosted);
        if (_isDamageBoosted)
            _gunLine.material = damageBuffedShotMaterial;
        else
            gunShootDamage = NORMAL_GUN_SHOT_DAMAGE;

        fireRateoManager.ManageBuffs(out _isFireRateoBoosted);
        if (_isFireRateoBoosted)
            _gunLine.material = fireRateoBuffedShotMaterial;
        else
            fireRateo = NORMAL_FIRE_RATEO;
        
        //se entrambi i buff sull'arma sono esauriti, rispristina il material del colpo normale (DA TENERE SOTTOCONTROLLO !!!)
        if (!_isDamageBoosted && !_isFireRateoBoosted)
            _gunLine.material = normalShotMaterial;
        
        if(Input.GetButton("Fire1") && _timer >= fireRateo && _roundsInMag >= 0 && !_isReloading)
        {
            _timer = 0f;
            Shoot();
        }
        // se il timer è maggiore del fireRateo * effectsDisplayTime (ovvero non si spara), disabilita gli effetti dello sparo precedente
        if (_timer >= fireRateo * _effectsDisplayTime)
        {
            DisableEffects(); 
        }

        if (Input.GetKey(KeyCode.R) && !_isReloading && _roundsInMag != maxRounds)
        {
            StartCoroutine(Reload());
        }
    }

    // metodo richiamato in PlayerHealth
    public void DisableEffects()        
    {
        _gunLine.enabled = false;
        _gunLight.enabled = false;
    }

    private void Shoot()
    {   
        // se il caricatore è vuoto...*click!*
        if( _roundsInMag == 0 )
        {
            _anim.SetBool("isOutOfAmmo", true);
            _source.PlayOneShot(_dryGun);
            return;
        }
        // colpo sparato; timer resettato
        _timer = 0f;
        _roundsInMag--;
        ammoCount.text = "" + _roundsInMag; 
        _source.PlayOneShot(_gunShot);
        _gunLight.enabled = true;

        //reset degli effetti dello sparo precedente ed avvio di quelli dello sparo attuale
        _gunParticles.Stop();
        _gunParticles.Play();

        //riattivazoine del gunLine e posizionamento dello stesso sulla fine del GunBarrelEnd
        _gunLine.enabled = true;
        _gunLine.SetPosition(0,transform.position);    // 0 sta per l'indice della posizione da settare (in questo caso l'inizio); controlla l'Inspector

        //tracciamento del colpo partendo dalla fine del GunBarrelEnd e proseguendo dritto
        _shootRay.origin = transform.position;
        _shootRay.direction = transform.forward;

        // verifica se il Raycast ha colpito qualcosa partendo dal suo punto di origine,proseguendo in una data direzione(shootRay.origin e shootRay.direction); out shootHit memorizza le info delle collisione(avvenuta o non)
        // range è la distanza che può percorrere il colpo; shootableMask è il layer dove si controlla se sono avvenute le collisioni
        if(Physics.Raycast(_shootRay, out _shootHit, range, _shootableMask))
        {
            // acquisizione del oggetto colpito 
            EnemyHealth enemy = _shootHit.collider.GetComponent<EnemyHealth>();
            
            // se l'oggetto colpito è un enemy, ottieni il suo componente EnemyHealth
            if (enemy != null)
                enemy.TakeDamage(gunShootDamage, _shootHit.point);

                                                             // setta la fine del gunLine (colpo sparato) sul punto della collisione con l'enemy
            _gunLine.SetPosition(1, _shootHit.point);       // 1 sta per l'indice della posizione da settare (in questo caso la fine); controlla l'Inspector
        }

        // se il RayCast non ha colpito niente, la linea tracciata prosegue fino alla fine del range
        else 
        {
            _gunLine.SetPosition(1, _shootRay.origin + _shootRay.direction * range);
        }
    
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        _source.PlayOneShot(_reloadSound);

        yield return new WaitForSeconds(1f);
        _roundsInMag = maxRounds;
        ammoCount.text = "" + _roundsInMag;
        _anim.SetBool("isOutOfAmmo", false);

        _isReloading = false;
    }

    public int getRoundsInMag()
    { 
        return _roundsInMag;
    }

    public LineRenderer getGunLine()
    {
        return _gunLine;
    }
}


