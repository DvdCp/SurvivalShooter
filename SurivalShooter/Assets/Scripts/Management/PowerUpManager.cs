using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public string managerName;
    
    public int _maxBuffs;
    public int maxBuffs
    {
        get => _maxBuffs;
        set => _maxBuffs = value;
    }

    private bool _isBuffed;
    public bool isBuffed
    {
        get => _isBuffed;
        set => _isBuffed = value;
    }

    //private int _buffsActivated;
    //public int buffsActivated
    //{
    //    get => _buffsActivated;
    //    set => _buffsActivated = value;
    //}

    private float _buffTimer;
    public float buffTimer
    {
        get => _buffTimer;
        set => _buffTimer = value;
    }

    private void Awake()
    {
        isBuffed = false;
        //buffsActivated = 0;
        buffTimer = 0.0f;
    }

    private int _buffDuration;
    public int buffDuration
    {
        get => _buffDuration;
        set => _buffDuration = value;
    }

    //public bool canCollectPowerUp()
    //{
    //    if (buffsActivated < maxBuffs)
    //        return true;
    //    else
    //        return false;
    //}
    
    public void collectPowerUp(PowerUpGeneric powerUpGeneric)
    {
        //buffsActivated++;
        buffDuration = powerUpGeneric.buffDuration;
        buffTimer = powerUpGeneric.buffDuration; 
        isBuffed = true;
    }

    public void ManageBuffs(out bool isUsing)
    {
        if (isBuffed && buffTimer > 0.0f)
        {
            buffTimer -= Time.deltaTime; print(buffTimer);
            isUsing = true;

            return;

            //if (buffTimer > 0.0f)
            //{
            //    
            //
            //    var remainTime = buffTimer % buffDuration;
            //    //print($"remain {remainTime}");
            //    if (remainTime <= 0.0f ) // scansione del tempo per un fattore buffDuration.
            //    {
            //        print($"remain {remainTime} and then debuf");
            //        buffsActivated--;
            //    }    
            //    
            //    isUsing = true;
            //    return;
            //}
            //else
            //{
            //    isUsing = false;
            //    return;
            //}
        }
        else
        {
            // buffs esauriti. Ripristino valori...
            //buffsActivated = 0;
            buffTimer = 0.0f;
            isBuffed = false;
            isUsing = false;
            return;
        }
    }
}



