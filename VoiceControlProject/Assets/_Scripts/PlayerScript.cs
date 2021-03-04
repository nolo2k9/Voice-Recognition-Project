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
    private void GR_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder message = new StringBuilder();
        SemanticMeaning[] meanings = args.semanticMeanings;

        
            foreach(SemanticMeaning meaning in meanings)
            {
                string keyString = meaning.key.Trim();
                //Get meanings
                string valueString = meaning.values[0].Trim();
                //output phrase
                message.Append("Key: " + keyString + ", Value: " + valueString );
                //if left is said
                if(valueString == "left"){
                    //move left
                    transform.Translate(new Vector3(-200 * Time.deltaTime,0,0));
                    //transform.Translate(new Vector3(-200 * Time.deltaTime,0,0));
                }
                
                //if right is said
                if(valueString == "right"){
                    //move right
                    transform.Translate(new Vector3(200 * Time.deltaTime,0,0));
                    
                
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

