using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelect : MonoBehaviour
{

    private Text easy;

    private Text medium;

    private Text hard;

    private int numDiffs = 3;

    private Text[] difficulties;

    // Start is called before the first frame update
    void Start()
    {
        easy = GameObject.Find("EasyText").GetComponent<Text>();
        medium = GameObject.Find("MediumText").GetComponent<Text>();
        hard = GameObject.Find("HardText").GetComponent<Text>();
        difficulties = new Text[numDiffs];
        difficulties[0] = easy;
        difficulties[1] = medium;
        difficulties[2] = hard;
        string curr = PlayerPrefs.GetString("difficulty", "medium");
        SetCurr(curr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Should probably make these difficulty strings enums instead

    public void SetEasy()
    {
        PlayerPrefs.SetString("difficulty", "easy");
        Bold(easy);
    }

    public void SetMedium()
    {
        PlayerPrefs.SetString("difficulty", "medium");
        Bold(medium);
    }

    public void SetHard()
    {
        PlayerPrefs.SetString("difficulty", "hard");
        Bold(hard);
    }

    private void Bold(Text selected)
    {
        foreach (Text t in difficulties)
        {
            if (t.Equals(selected))
            {
                t.text = "<b>" + t.text + "</b>";
            }
            else
            {
                t.text = t.text.Replace("<b>", "");
                t.text = t.text.Replace("</b>", "");
            }
        }
    }

    private void SetCurr(string curr)
    {
        if (curr == "easy")
        {
            Bold(easy);
        }
        else if (curr == "medium")
        {
            Bold(medium);
        }
        else if (curr == "hard")
        {
            Bold(hard);
        }
    }

}
