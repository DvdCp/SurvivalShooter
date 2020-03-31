using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpGeneric : MonoBehaviour
{
    public int buffDuration;
    public float existTime;

    private float timer;

    public void Update()
    {
        gameObject.transform.Rotate(Vector3.right * Time.deltaTime * 100);
        gameObject.transform.Rotate(Vector3.up * Time.deltaTime * 100);

        StartCoroutine( Exists() );

    }

    public void OnTriggerEnter(Collider other)
    { 
        if(other.CompareTag("Player"))
        {
            PickUp(other);
        }
    }

    public IEnumerator Exists()
    {
       yield return new WaitForSeconds(existTime);

        Destroy(gameObject);
    }

    abstract public void PickUp(Collider player);
}
