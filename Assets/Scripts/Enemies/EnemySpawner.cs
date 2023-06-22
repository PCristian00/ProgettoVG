using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject Boss;
    float timer = 0;
    float countSpawn = 0;
    float countEnemyKill = 0;

    bool bossIsAlive;

    public LogicScript logic;

    public AudioSource deathSound;


    // Update is called once per frame
    void Update()
    {
        if (countEnemyKill < 10)
        {
            // Possono comparire max (n*speed) nemici
            // velocita' 5 = max 5 nemici
            if (countSpawn <= (7 - logic.speed))
            {
                // Tempo di spawn tra un nemico e l'altro variabile in base a velocita' + 1 secondo
                // logic.speed varia da 7 a 2 (inversamente proporzionale)
                // Vecchio spawnRate era 5
                // a velocita' 5 spawn ogni 3 secondi
                if (timer < (logic.speed + 1))
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
        // Il boss compare se non e' presente nessun boss o nemico e sono stati uccisi 10 nemici
        else if (!bossIsAlive && countSpawn == 0)
        {
            // Attesa di 1 secondo per lo spawn
            bossIsAlive = true;
            logic.ShowMessage("ATTENZIONE!!!", 1);
            Invoke(nameof(SpawnBoss), 1);
        }

    }
    void SpawnEnemy()
    {
        countSpawn++;
        //prelevo i bordi della camera
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        max.x = 6f;
        min.x = -6f;
        max.y -= 3f;
        GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)]);

        enemy.transform.position = transform.position;
    }
    void SpawnBoss()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        max.x = 6f;
        min.x = -6f;
        max.y -= 3f;
        Instantiate(Boss);
        Boss.transform.position = transform.position;
    }

    public void EnemyKilled()
    {
        countEnemyKill++;
        deathSound.Play();
        logic.AddScore(1);

        if (countSpawn > 0)
            countSpawn--;
    }

    public void BossKilled()
    {
        deathSound.Play();
        countEnemyKill = 0;
        bossIsAlive = false;
        logic.AddScore(10);
        logic.IncreaseDifficulty();
    }
}