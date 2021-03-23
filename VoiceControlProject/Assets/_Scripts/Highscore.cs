using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    //Variable to handle the Highscore value
    private int highscoreValue;
    //Reference to Text area on canvas
    public Text highScoreText;
    // Start is called before the first frame update

    private int previousScore;
    void Start()
    {
        //Assigning highScoreText to the component called Text
        highScoreText = GetComponent<Text>();
        //Output highsocre to string. PlayerPrefs allows the score to hold on the machine 
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("Highscore", 0).ToString() + "\n" + 
        "Previous Score: " + PlayerPrefs.GetInt("PreviousScore", GameManager.points).ToString();
        
    }//Start

    // Update is called once per frame
    void Update()
    {
        //if the score is greater than the current highscore
        if (GameManager.points > PlayerPrefs.GetInt("Highscore", 0))
        {    //Create a new highscore with this highscore value
             highscoreValue = GameManager.points;
             PlayerPrefs.SetInt("Highscore", highscoreValue);
             highScoreText.text = "High Score: " + highscoreValue.ToString();
        }//if
       
    }//Update
}
