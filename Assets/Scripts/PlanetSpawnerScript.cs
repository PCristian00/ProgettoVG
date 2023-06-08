using UnityEngine;

public class PlanetSpawnerScript : MonoBehaviour
{
    public GameObject[] planets;
    // public float spawnRate = 5;
    // public int planetNumber;

    public LogicScript logic;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        logic =
            GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < (logic.speed*2f))
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnPlanet();
            timer = 0;
        }
    }

    void SpawnPlanet()
    {
        int PositionY = Random.Range(-1, 3);

        //if (PositionY <= 33)
        //{
        //    PositionY = 0;
        //}
        //else if (PositionY <= 66)
        //{
        //    PositionY = 2;
        //}
        //else
        //{
        //    PositionY = 4;
        //}

        Instantiate(planets[Random.Range(0, planets.Length)], new Vector3(transform.position.x, PositionY, 0),
                    transform.rotation);
    }
}
