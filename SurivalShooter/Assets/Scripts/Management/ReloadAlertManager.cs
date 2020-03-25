using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReloadAlertManager : MonoBehaviour
{
    public PlayerShooting playerShooting;       //N.B le variabili public non hanno bisogno di essere inizializzate ,poichè il loro valore verrà settato nell'Inspector
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerShooting.getRoundsInMag() == 0)
        {
            anim.SetTrigger("isOutOfAmmo");
        }
        else if( playerShooting.getRoundsInMag() != 0)
        {
            anim.ResetTrigger("isOutOfAmmo");
        }


    }
}
