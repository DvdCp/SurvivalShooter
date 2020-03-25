using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float SPEED = 6f;

    public float speed;

    private Vector3 _movement;
    private Vector3 _lockedRotation;                                                          
    private Animator _anim;                                                              
    private Rigidbody _playerRigidbody;                                                  
    private int _floorMask;                                                              
    private float _camRayLength = 150f;  // questa variabile serve per usare il mouse
    private bool _flag;

    private PowerUpManager _speedManager;
    public PowerUpManager speedManager
    {
        get => _speedManager;
        private set => _speedManager = value;
    }

    private void Awake()                                                                
    {
        speed = SPEED;

        _floorMask = LayerMask.GetMask("Floor");                                 
        _anim = GetComponent<Animator>();                                        
        _playerRigidbody = GetComponent<Rigidbody>();
        speedManager = GetComponent<PowerUpManager>();
    }

    private void FixedUpdate()                                                           
    {
        float h = Input.GetAxisRaw ("Horizontal");
        float v = Input.GetAxisRaw ("Vertical");

        speedManager.UseBuffs(out _flag);
        if(_flag)
        {
            //
        }
        else 
        {
            speed = SPEED;
        }

        Move(h,v); 

        Turning();

        Animating(h,v);
    }

    private void Move(float h, float v)
    {
        _movement.Set(h, 0f, v);

        _movement = _movement.normalized * (speed * Time.deltaTime);

        _playerRigidbody.MovePosition(transform.position + _movement);
    }

    private void Turning()
    {
        // crea un raggio dalla pozione del mouse in direzione della camera
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                                                                                //crea una variabile che conserva il punto dove il Ray colpisce
        RaycastHit floorHit;

                                                                                //si verifica se il Ray ha colpito qualcosa sul Floor Layer
        if (Physics.Raycast(camRay, out floorHit, _camRayLength, _floorMask))
        {
                                                                                // crea un vector3 tra il player ed il punto dove il punta il mouse
            Vector3 playerToMouse = floorHit.point - transform.position;

                                                                                //si assicura che il vector3 si aderente al Floor
            playerToMouse.y = 0f;

                                                                                //creazione di una quaternion(rotazione) basata sul vettore "playerToMouse"
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

                                                                                //impostazione della rotazione del player secondo il mouse
            _playerRigidbody.MoveRotation(newRotation);
        }
    }

    private void Animating(float h, float v)
    {
                                                                                // rilevamento dell'input; se una delle 2 variabili risultasse diversa da 0, vuol dire che il player si sta muovendo
        bool Walking = h != 0f || v != 0f;
                                                                                // settaggio della variabile "IsWalking" dell'AnimatorAC del player
        _anim.SetBool("IsWalking", Walking);
    }
}

