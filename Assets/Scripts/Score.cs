using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour	
{
	private Text scoreBoard;

	private int score;

	private int highScore;
    // Start is called before the first frame update
    void Start()
    {
        scoreBoard = GameObject.Find("Score").GetComponent<Text>();
        score = 0;
        highScore = PlayerPrefs.GetInt("high_score", 0);
    }

    // Update is called once per frame
    void Update()
    {
        scoreBoard.text = "Score: " + score.ToString();
    }

    public void AddPoint()
    {
    	score += 1;
    	if (score > highScore)
    	{
    		PlayerPrefs.SetInt("high_score", score);
    	}
    }
}
