using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeantweenTest : MonoBehaviour
{
    public GameObject powerUpAlert;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(powerUpAlert);
        powerUpAlert.SetActive(false);
        LeanTween.moveLocalY(gameObject,100,1.5f).setOnComplete(ActiveGameobject);
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }

    void ActiveGameobject()
    {
        powerUpAlert.SetActive(true);
    }
}
