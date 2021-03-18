using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;  // for stringbuilder
using UnityEngine;
using UnityEngine.Windows.Speech;   // grammar recogniser

public class PlayerScript : MonoBehaviour
{
   private GrammarRecognizer gr;
   public GameObject player;
   public GameObject projectile;
   public GameObject projectileClone;
   public float timeBetweenShots;
   private float shotTime;
  
   
    private void Start(){
       
        gr = new GrammarRecognizer(Application.streamingAssetsPath + "/SimpleGrammar.xml", ConfidenceLevel.Medium);
        gr.OnPhraseRecognized += GR_OnPhraseRecognized;
        gr.Start();
        Debug.Log("Grammer loaded and recogniser started");

    }

    private enum Actions
    {
        Stop,
        Left,
        Right,
        Menu,
        Play,
        Exit,
        Reload
    }

    private Actions currentActions;

    private void Update()
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
                break; 

            case Actions.Play:
                GameManager.isPaused = false;
                break; 

            case Actions.Reload:
                Debug.Log("Reloaded");
                GameManager.bullets = 6;
                shoot();
                break;                                 
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
                    currentActions = Actions.Play;
                    break;

                case "quit":
                case "quit game":
                case "quit the game":
                case "exit":
                case "exit game":
                case "exit the game":
                    currentActions = Actions.Exit;
                    break;

                case "reload":
                    currentActions = Actions.Reload;
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

