using UnityEngine;



public class EnemySpawner : MonoBehaviour
{
    public GameObject [] enemies;
    public GameObject Boss;
    public float maxSpawnRate = 5f; // tempo di spawn
    public float timer = 0;
    public float countSpawn = 0;
    public float countEnemyKill = 0;
    public System.Action<EnemySpawner> killed;

    public bool bossIsAlive;

    public LogicScript logic;



    // Update is called once per frame
    void Update()
    {
        if (countEnemyKill < 10) 
        {
            // Possono comparire max (n*speed) nemici
            // velocita' 5 = max 5 nemici
            if (countSpawn <= (7-logic.speed))
            {
                Debug.Log("Numero massimo di nemici a questa velocita' : " + (8 - logic.speed));
                // Tempo di spawn tra un nemico e l'altro variabile in base a velocita' + 1 secondo
                // logic.speed varia da 7 a 2 (inversamente proporzionale)
                // Vecchio spawnRate era 5
                // a velocita' 5 spawn ogni 3 secondi
                if (timer < (logic.speed+1))
                {
                    
                    Debug.Log("Prossimo nemico tra " + (int)((logic.speed + 1) - timer) + " secondi.");
                    timer += Time.deltaTime;
                }
                else
                {
                    SpawnEnemy();
                    timer = 0;
                }
            }
        }
        // Il boss compare se non e' presente nessun boss o nemico e sono stati uccisi 10 nemici
        else if (!bossIsAlive && countSpawn==0)
        {
            // Attesa di 1 secondo per lo spawn
            bossIsAlive = true;
            Invoke(nameof(SpawnBoss),1);
        }
        
    }
    void SpawnEnemy()
    {
        countSpawn++;
        // Debug.Log(countSpawn);
        //prelevo i bordi della camera
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        max.x = 6f;
        min.x = -6f;
        max.y -= 3f;
        //GameObject[] enemies = { Enemy_1, Enemy_2 };
        GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)]);
        enemy.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(1, max.y));
        ScheduleNextEnemySpawn();
    }
    void SpawnBoss()
    {
        // De-commentare sotto se rimosso invoke di SpawnBoss in Update
        // bossIsAlive = true;
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        max.x = 6f;
        min.x = -6f;
        max.y -= 3f;
        Instantiate(Boss);
        Boss.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(1, max.y));
    }


    void ScheduleNextEnemySpawn()
    {
        float spawnInNSeconds;
        if (maxSpawnRate > 1f)
        {
            spawnInNSeconds = Random.Range(1f, maxSpawnRate);
        }
        else
        {
            spawnInNSeconds = 1f;
        }
    }

    // FORSE INUTILIZZATA
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            killed?.Invoke(this);
        }
    }



}