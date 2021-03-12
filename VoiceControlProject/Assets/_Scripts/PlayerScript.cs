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
        Pause,
        Continue
    }

    private Actions currentActions;

    private void Update()
    {
        switch(currentActions)
        {
            case Actions.Left:
                transform.Translate(new Vector3(-2 * Time.deltaTime, 0, 0));
                break;

            case Actions.Right:
                transform.Translate(new Vector3(2 * Time.deltaTime, 0, 0));
                break;
            
            case Actions.Stop:
                transform.Translate(new Vector3(0 * Time.deltaTime, 0, 0));
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
                case "left":
                    currentActions = Actions.Left;
                    break;
                
                case "right":
                    currentActions = Actions.Right;
                    break;

                case "stop":
                    currentActions = Actions.Stop;
                    break;

               
            }
        }

        Debug.Log(message);
    }

   void shoot(){
       
       if(Input.GetKeyDown(KeyCode.Space)){
        
        if(Time.time >= shotTime)
        {
            projectileClone = Instantiate(projectile, new Vector3(player.transform.position.x, player.transform.position.y + 0.8f, 0), player.transform.rotation) as GameObject;
            shotTime = Time.time + timeBetweenShots;

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

