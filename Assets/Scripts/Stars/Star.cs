using UnityEngine;

public class Star : MonoBehaviour
{
    // Zona di despawn
    public float deadZone = -6.5f;
    // Update is called once per frame
    StarSpawner spawner;

    LogicScript logic;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spawner = GameObject.FindGameObjectWithTag("StarSpawner").GetComponent<StarSpawner>();
    }

    void Update()
    {
        transform.position += (8-logic.speed) * Time.deltaTime * Vector3.down;
        // Distrugge il power-up quando arriva oltre il limite dello schermo
        if (transform.position.y <= deadZone)
        {
            Destroy(gameObject);
            spawner.starCount--;
        }
    }
}