using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public string buffName;
    public int _maxBuffs;
    public int maxBuffs
    {
        get => _maxBuffs;
        set => _maxBuffs = value;
    }

    private bool _buff;
    public bool isBuffed
    {
        get => _buff;
        set => _buff = value;
    }

    private int _buffsActivated;
    public int buffsActivated
    {
        get => _buffsActivated;
        set => _buffsActivated = value;
    }

    private float _buffTimer;
    public float buffTimer
    {
        get => _buffTimer;
        set => _buffTimer = value;
    }

    private void Awake()
    {
        isBuffed = false;
        buffsActivated = 0;
        buffTimer = 0.0f;
    }

    private int _buffDuration;
    public int buffDuration
    {
        get => _buffDuration;
        set => _buffDuration = value;
    }
    public void UseBuffs(out bool temp)
    {
        if (isBuffed && buffsActivated >= 1)
        {
            Debug.Log("X");
            if (buffTimer > 0.0f)
            {
                Debug.Log("Y");
                buffTimer -= Time.deltaTime;
                Debug.Log(buffTimer);

                if (buffTimer % buffDuration == 0)
                {
                    Debug.Log("Z");
                    buffsActivated--;
                }
            }
            else
            {
                temp = false;
                return;
            }
            Debug.Log("W");
            temp = true;
            return;
        }
        else
        {
            Debug.Log("O");
            buffsActivated = 0;
            buffTimer = 0.0f;
            isBuffed = false;
            temp = false;
            return;
        }

    }

}



