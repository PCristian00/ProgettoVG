using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy_1;
    public GameObject Enemy_2;



    public float maxSpawnRate = 5f; // tempo di spawn
    public float timer = 0;
    public float countSpawn = 0;
    public System.Action<EnemySpawner> killed;

    public bool bossIsAlive;
    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        if (countSpawn < 3f && !bossIsAlive)
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
        GameObject[] enemies = { Enemy_1, Enemy_2 };
        GameObject enemy = Instantiate(enemies[Random.Range(0, 2)]);
        enemy.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(1, max.y));
        ScheduleNextEnemySpawn();
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
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            killed?.Invoke(this);
        }
    }



}