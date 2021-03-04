using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;  // for stringbuilder
using UnityEngine;
using UnityEngine.Windows.Speech;   // grammar recogniser

public class PlayerScript : MonoBehaviour
{
   private GrammarRecognizer gr;
   public Rigidbody2D playerRb;

    private void Start(){
        playerRb = this.GetComponent<Rigidbody2D>(); 
        gr = new GrammarRecognizer(Application.streamingAssetsPath + "/SimpleGrammar.xml", ConfidenceLevel.Medium);
        gr.OnPhraseRecognized += GR_OnPhraseRecognized;
        gr.Start();
        Debug.Log("Grammer loaded and recogniser started");

    }

    private enum Direction
    {
        Stop,
        Left,
        Right
    }

    private Direction currentDirection;

    private void Update()
    {
        switch(currentDirection)
        {
            case Direction.Left:
                transform.Translate(Vector3.right * -2 * Time.deltaTime);
                break;

            case Direction.Right:
                transform.Translate(Vector3.right * 2 * Time.deltaTime);
                break;
            
            case Direction.Stop:
                transform.Translate(Vector3.right * 0 * Time.deltaTime);
                break;
        }
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
                    currentDirection = Direction.Left;
                    break;
                
                case "right":
                    currentDirection = Direction.Right;
                    break;

                case "stop":
                    currentDirection = Direction.Stop;
                    break;
            }
        }

        Debug.Log(message);
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

