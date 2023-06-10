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

    // Ancora non implementata
    public bool bossIsAlive;



    // Update is called once per frame
    void Update()
    {
        // Abbassato da 3 a 10 per test
        if (countEnemyKill < 3f) 
        {
            if (countSpawn < 3f)
            {
                if (timer < maxSpawnRate)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    SpawnEnemy();
                    timer = 0;
                }
            }
        }
        else if (!bossIsAlive)
        {
            SpawnBoss();
        }
        
    }
    void SpawnEnemy()
    {
        countSpawn++;
        Debug.Log(countSpawn);
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
        bossIsAlive = true;
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