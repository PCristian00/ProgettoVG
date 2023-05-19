using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle;
    public float spawnRate = 2;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        SpawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnObstacle();
            timer = 0;
        }


    }

    void SpawnObstacle()
    {
        //Il range (-6,6) indica l'area delle corsie
        //        Instantiate(obstacle, new Vector3(Random.Range(-6, 6),transform.position.y, 0), transform.rotation);

        int PositionX = Random.Range(1, 100);

        if (PositionX <= 33)
        {
            PositionX = -6;
                    }
        else if (PositionX <= 66)
        {
            PositionX = 0;
        }
        else
        {
            PositionX = 6;
        }

        Instantiate(obstacle, new Vector3(PositionX, transform.position.y, 0), transform.rotation);
    }
}
