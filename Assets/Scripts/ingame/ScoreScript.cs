using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text myscore;
    public Text highscore;
    // Update is called once per frame
    void Update()
    {
        myscore.text = "Score: " + BallScript.score;
        highscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
    }
}
