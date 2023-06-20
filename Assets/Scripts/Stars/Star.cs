using UnityEngine;

public class Star : MonoBehaviour
{
    // Velocita' di movimento
    //public float moveSpeed = 1f;
    // Zona di despawn
    public float deadZone = -6.5f;
    // Update is called once per frame

    LogicScript logic;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        transform.position += (8-logic.speed) * Time.deltaTime * Vector3.down;
        // Distrugge il power-up quando arriva oltre il limite dello schermo
        if (transform.position.y <= deadZone)
        {
            Destroy(gameObject);
        }
    }
}