using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Lives variable
    public static int lives = 3;
    //points variable
    public static int points = 0;

    public static float energy;
    //is paused false
    public static bool isPaused = false;
    //6 bullets
    public static int bullets = 6;
    //lives text area
    public Text livesCount;
    //score text area
    public Text scoreCount;
    //bullet text area
    public Text bulletCount;
    //Energy 
    public Text energyCount;

    // Start is called before the first frame update
    void Start()
    {   // display lives, bullets, points
        livesCount.text = "Lives: " + lives;
        scoreCount.text = "Points: " + points;
        bulletCount.text = "Vaccines: " + bullets;
    }

    // Update is called once per frame
    void Update()
    {
        // update lives, bullets, points, energy
        livesCount.text = "Lives: " + lives;
        scoreCount.text = "Points: " + points;
        bulletCount.text = "Vaccines: " + bullets;
        PlayerPrefs.SetInt("PreviousScore", points);
        
       
        //Pause Menu activation
        if(isPaused)
        {
            ActivateMenu();
        }
        //deactivate pause menu
        else
        {
           DeactivateMenu();
        }
    }
    //if paused time freezes
    void ActivateMenu(){
        Time.timeScale = 0;
    }
    // if not paused normal time
    void DeactivateMenu(){
        Time.timeScale = 1;
    }


}
