using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle;
    //public float spawnRate = 5;

    public LogicScript logic;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < logic.speed)
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
        // Il range (-6,6) indica l'area delle corsie

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
