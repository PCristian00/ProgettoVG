using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public float maxSpawnRate = 5f; // tempo di spawn
    void Start()
    {
        Invoke("ScheduleEnemySpawn", maxSpawnRate);
    }
    void SpawnEnemy()  //preleva i bordi e istanzia una navicella nemica e gli assegna una posizione random sull'asse x
    {
        
        //prelevo i bordi della camera
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
      max.x -= 0.225f;
      min.x += 0.225f;
      max.y -= 0.225f;

        GameObject enemy = Instantiate(Enemy);
        enemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        ScheduleNextEnemySpawn();
    }
    void ScheduleNextEnemySpawn()  
    {
        float spawnInNSeconds;
        if(maxSpawnRate > 1f)
        {
            spawnInNSeconds = Random.Range(1f, maxSpawnRate); //questa variabile assume un valore casuale tra 1 e 5 
        } 
        else
        {
            spawnInNSeconds = 1f;
        }
    }
    void increseSpawnRate() //aumenta la difficoltà
    {
        if (maxSpawnRate > 1f)
        {
            maxSpawnRate--;
        }
        else if(maxSpawnRate == 1)
        {
            CancelInvoke("increseSpawnRate");
        }
    }
    public void ScheduleEnemySpawn()
    {
        maxSpawnRate = 5f; // va ridefinito perchè nei metodi precedenti ne abbiamo modificato il valore
        Invoke("SpawnEnemy", maxSpawnRate);
        //incrementiamo lo spawnrate ogni 10 secondi
        InvokeRepeating("increseSpawnRate", 0f, 10f); // il metodo incresespawnrate viene chiamato ogni 10 secondi
    }
    public void UnscheduleEnemySpawn() //stoppa l'enemy spawn
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("increseSpawnRate");
    }
}
