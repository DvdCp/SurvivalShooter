using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // questo è il target della Camera, ovvero il player 
    public float smoothing = 5f;        // questo serve per dare un effetto più "morbido" allo spostamento della Camera
    private Vector3 offset;
                   // allineamento della camera sul personaggio
    private void Start()
    {
        offset = transform.position - target.position;      //transform.position è un metodo statico che in questo caso si riferisce alle coordinate della Camera
        transform.position = target.position;               // allineamento della camera sul personaggio
    }

    private void FixedUpdate()  // questa funziona fa muovere la Camera perchè l'oggetto target è soggetto alle leggi fisiche di unity; Rispettivamente; il player 
    {                           //si muove con la FixedUpdate() del suo script

        Vector3 PointToReach = target.position + offset;
        transform.position = Vector3.Lerp(transform.position,PointToReach, smoothing * Time.deltaTime);

    }


}
