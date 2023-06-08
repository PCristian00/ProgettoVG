using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawnerScript : MonoBehaviour
{
    public GameObject planet;
    //public float spawnRate = 5;

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
        if (timer < (logic.speed*2))
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
        // Il range (-6,6) indica l'area delle corsie

        int PositionY = Random.Range(1, 100);

        if (PositionY <= 33)
        {
            PositionY = -6;
        }
        else if (PositionY <= 66)
        {
            PositionY = 0;
        }
        else
        {
            PositionY = 6;
        }

        Instantiate(planet, new Vector3(transform.position.x, transform.position.y, 0),
                    transform.rotation);
    }
}
