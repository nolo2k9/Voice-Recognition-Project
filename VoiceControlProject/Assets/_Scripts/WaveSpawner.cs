using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
   //show in unity inspector
    [System.Serializable]
   public class Wave {
       //contains how may enemies can be spawned inside this wave
       public EnemyScript[] enemies;
       //how many enemies can be spawned inside the wave
       public int count;
        //lentgh between enemy spawns
       public float timeBetweenSpawns;
        

   }
   //Array to hold the number of waves
   public Wave[] waves;
   //Array to hold the number of spawn points
   public Transform[] spawnPoints;
   //Time between each wave
   public float timeBetweenWaves;
   //Reference to wave object
   private Wave currentWave;
   //Current wave number
   private int currentWaveIndex;
   //Reference to player object
   private Transform player;
   //Bool to handle if the waves are finshed
    private bool isFinished;
    public Text message;

   private void Start(){

       //finding the player by the assigned tag
       player = GameObject.FindGameObjectWithTag("Player").transform;
       //passing in the wave index into coroutine to start the wave
       StartCoroutine(StartNextWave(currentWaveIndex));
       

   }//Start

    //wait an x ammount of seconds based on timeBetweenWaves and then cll another coroutine to spawn wave
   IEnumerator StartNextWave(int index){

       yield return new WaitForSeconds(timeBetweenWaves);
       StartCoroutine(SpawnWave(index));

   }//StartNextWave

   IEnumerator SpawnWave (int index){
       currentWave = waves[index];

       for (int i = 0; i < currentWave.count; i++)
       {
           //if player is dead break from loop
           if (player == null)
           {
               yield break; 
           }//if

            //generate random enemy 
           EnemyScript randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
           //spawn in random area
           Transform randomArea = spawnPoints[Random.Range(0, spawnPoints.Length)];
           //create new random enemy in random spot on map 
           Instantiate(randomEnemy, randomArea.position, randomArea.rotation);
           //detect if current wave has ended
            // if i is equal to currentWave.count -1 
           if(i==currentWave.count -1)
           {
                isFinished = true;
           } else
           {
               isFinished = false;
           }

           //wait an allotted ammount of time 
           yield return new WaitForSeconds(currentWave.timeBetweenSpawns);

           //wait an allotted ammount of time 
           yield return new WaitForSeconds(currentWave.timeBetweenSpawns);

       }//for

   }//SpawnWave 
  
    // Update is called once per frame
    void Update()
    {
        
        //if the wave has finished and no more enemys are alive inside the game wave done
       if(isFinished == true && GameObject.FindGameObjectsWithTag("Covid").Length ==0)
       {    //new wave
           isFinished = false;
           if(currentWaveIndex + 1 < waves.Length)
           {
                //change message
                message.text = "Wave: " + currentWaveIndex + 2;
               currentWaveIndex ++;
               StartCoroutine(StartNextWave(currentWaveIndex));
           }
       }
    }
}
