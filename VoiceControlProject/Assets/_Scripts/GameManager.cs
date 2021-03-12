﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int lives = 3;
    public static int points = 0;
    public static bool isPaused = false;

    public Text livesCount;
    public Text scoreCount;
    // Start is called before the first frame update
    void Start()
    {
        livesCount.text = "Lives: " + lives;
        scoreCount.text = "Points: " + points;
    }

    // Update is called once per frame
    void Update()
    {
        livesCount.text = "Lives: " + lives;
        scoreCount.text = "Points: " + points;
        
        if(isPaused)
        {
            ActivateMenu();
        }
        else
        {
           DeactivateMenu();
        }
    }

    void ActivateMenu(){
        Time.timeScale = 0;
    }

    void DeactivateMenu(){
        Time.timeScale = 1;
    }


}