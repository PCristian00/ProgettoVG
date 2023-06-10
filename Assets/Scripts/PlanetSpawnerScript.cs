using UnityEngine;

public class PlanetSpawnerScript : MonoBehaviour
{
    // Contiene i pianeti
    public GameObject[] planets;

    public LogicScript logic;

    // Indice dell'array planets, usato per spawnarli in sequenza
    private int planetIndex = 0;

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
        if (timer < (logic.speed * 2f))
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
        int PositionY = Random.Range(-4, 4);

        // Spawn casuale dei pianeti
        // Instantiate(planets[Random.Range(0, planets.Length)], new Vector3(transform.position.x, PositionY, 0), transform.rotation);

        // Spawn sequenziale dei pianeti
        Instantiate(planets[planetIndex], new Vector3(transform.position.x, PositionY, 0), transform.rotation);

        // Debug.Log("Pianeta spawnato: " + planetIndex+ " / "+ (planets.Length-1));

        if (planetIndex == (planets.Length - 1))
        {
            // Debug.Log("Sequenza pianeti finita. Ricomincio.");
            planetIndex = 0;
        }
        else planetIndex++;
    }
}
