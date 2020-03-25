using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score;

    Text scoreLabel;

    // Start is called before the first frame update
    void Awake()
    {
        scoreLabel = GetComponent<Text>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = "Score:" + score;

    }
}
