using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;  // for stringbuilder
using UnityEngine;
using UnityEngine.Windows.Speech;   // grammar recogniser
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private GrammarRecognizer gr;
    public GameObject player;
    public GameObject projectile;
    public GameObject projectileClone;
    public float timeBetweenShots;
    private float shotTime;
    //Text
    public Text message;
    private bool isDead = false;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
   
   
    private void Start(){

        isDead = false;
        GameManager.isPaused = false;
        gr = new GrammarRecognizer(Application.streamingAssetsPath + "/GameGrammar.xml", ConfidenceLevel.Medium);
        gr.OnPhraseRecognized += GR_OnPhraseRecognized;
        gr.Start();
        Debug.Log("Grammer loaded and recogniser started");
        //change message
        message.text = "Say Left to go left or right to go right" + "\n" + "Say pause to see full list of commands";

    }

    private enum Actions
    {
        Stop,
        Left,
        Right,
        Menu,
        Play,
        Exit,
        Reload,
        Main,
        Flee,
        Restart
    }

    private Actions currentActions;

    private void Update()
    {
        
       if (isDead!= true)
       {
            switch(currentActions)
            {
                case Actions.Left:
                    transform.Translate(new Vector3(-5 * Time.deltaTime, 0, 0));
                    break;

                case Actions.Right:
                    transform.Translate(new Vector3(5 * Time.deltaTime, 0, 0));
                    break;
                
                case Actions.Stop:
                    transform.Translate(new Vector3(0 * Time.deltaTime, 0, 0));
                    break; 

                case Actions.Menu:
                    GameManager.isPaused = true;
                    pausePanel.SetActive(true);
                    break; 

                case Actions.Play:
                    GameManager.isPaused = false;
                    pausePanel.SetActive(false);
                    break; 

                case Actions.Reload:
                    Debug.Log("Reloaded");
                    GameManager.bullets = 6;
                    shoot();
                    break;  

                case Actions.Main:
                    //load main scene
                    SceneManager.LoadScene("Main", LoadSceneMode.Single);
                    break;  

                case Actions.Flee:
                    transform.Translate(new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f)));
                    break;   

                case Actions.Restart:
                    GameManager.lives = 3;
                    GameManager.points = 0;
                    PlayerPrefs.SetInt("PreviousScore", 0);
                    GameManager.bullets = 6;
                    SceneManager.LoadScene("Game", LoadSceneMode.Single);
                    break;
            }

        }
        if(GameManager.bullets == 0)
        {
             //change message
            message.text = "Reload";
        }
        else if(GameManager.lives == 0)
        {
              //change message
            message.text = "You are DEAD"+ "\n" + "Say Exit to return to menu" + "\n" + "Say restart to play again";
            GameManager.isPaused = true;
            PlayerPrefs.SetInt("PreviousScore", GameManager.points);
        }
        else
        {
            //change message
            message.text = " ";
        }
        if(WaveSpawner.waveCount > 10)
        {
            //change message
            message.text = "You Win"+ "\n" + "Say Exit to return to menu" + "\n" + "Say restart to play again";
            GameManager.isPaused = true;
        }

        shoot();
    }

    private void GR_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        var message = new StringBuilder();
        var meanings = args.semanticMeanings;
        
        foreach(var meaning in meanings)
        {
            var keyString = meaning.key.Trim();
            var valueString = meaning.values[0].Trim();

            message.Append($"Key: {keyString}, Value: {valueString}");
            
            switch(valueString) 
            {
                case "go left":
                case "left":
                case "move left":
                    currentActions = Actions.Left;
                    break;
                
                case "go right":
                case "right":
                case "move right":
                    currentActions = Actions.Right;
                    break;

                case "stop":
                    currentActions = Actions.Stop;
                    break;

                case "pause game":
                case "pause":
                    currentActions = Actions.Menu;
                    break;

                case "resume game":
                case "resume":
                case "Start":
                case "Start Game":
                    currentActions = Actions.Play;
                    break;

                case "quit":
                case "quit game":
                case "quit the game":
                    currentActions = Actions.Exit;
                    break;

                case "reload":
                    currentActions = Actions.Reload;
                    break;

                case "main menu":
                case "exit game":
                case "exit":
                case "stop playing":
                    currentActions = Actions.Main;
                    break;
                
                case "run":
                case "sprint":
                case "flee":
                    currentActions = Actions.Flee;
                    break;
                
                case "restart":
                case "new game":
                case "try again":
                    currentActions = Actions.Restart;
                    break;
               
            }
        }

        Debug.Log(message);
    }

   void shoot(){
       
       if(Input.GetKeyDown(KeyCode.Space)){
           
        
        if(Time.time >= shotTime && GameManager.bullets > 0 )
        {
            projectileClone = Instantiate(projectile, new Vector3(player.transform.position.x, player.transform.position.y + 0.8f, 0), player.transform.rotation) as GameObject;
            shotTime = Time.time + timeBetweenShots;
            GameManager.bullets --;

        }

       }
   }

    
    private void OnApplicationQuit()
    {
        if(gr != null && gr.IsRunning)
        {
            gr.OnPhraseRecognized -= GR_OnPhraseRecognized;
            gr.Stop();
        }

       
    }

    

}

