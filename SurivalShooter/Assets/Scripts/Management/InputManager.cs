using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Per ottenere i valori delle 2 levette analogiche viene usato Input.GetAxis(string nome_tasto)
    N.B i nomi dei tasti sono stati settati in Edit->Project Settings->Input

Per ottenere i valori dei singoli bottoni vengono usati i KeyCode.JoystickButton#   
*/

public class InputManager : MonoBehaviour
{
    public string leftStickX;                                                   
    public string leftStickY;
    public string rightStickX;
    public string rightStickY;
   
    public Vector3 UseLeftStick()           // metodo per muovere il personaggio
    {
        float h = Input.GetAxis(leftStickX);
        float v = Input.GetAxis(leftStickY);

        return new Vector3(h,0f,v);

    }

    public Vector3 UseRightStick()           // metodo per muovere il personaggio
    {
        float h = Input.GetAxis(rightStickX);
        float v = Input.GetAxis(rightStickY);

        return new Vector3(h,0f,v);
    }

    public bool UseL1Button()
    {
        return Input.GetKey(KeyCode.JoystickButton11);

    }

    public bool UseSquareButton()
    {
        return Input.GetKey(KeyCode.JoystickButton15);

    }

}

