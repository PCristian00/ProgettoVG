using UnityEngine;
/// <summary>
/// Permette di spawnare in base alla velocita' attuale gli ostacoli
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    /// <summary>
    /// Ostacolo da spawnare
    /// </summary>
    public GameObject obstacle;
    /// <summary>
    /// Riferimento a LogicScript
    /// </summary>
    public LogicScript logic;
    /// <summary>
    /// Timer
    /// </summary>
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
    /// <summary>
    /// Spawna l'ostacolo
    /// </summary>
    void SpawnObstacle()
    {
        // Il range (-6,6) indica l'area delle corsie

        int positionX = (int)Random.Range(-6, 6);

        Instantiate(obstacle, new Vector3(positionX, transform.position.y, 0), transform.rotation);
    }
}
