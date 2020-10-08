using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{

	private Text display;
	private int highScore;

    // Start is called before the first frame update
    void Start()
    {
    	display = GetComponent<Text>();
        highScore = PlayerPrefs.GetInt("high_score", 0);
        display.text = "High Score: " + highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
