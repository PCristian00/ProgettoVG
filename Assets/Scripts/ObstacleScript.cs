using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //Velocit� di movimento
    public float moveSpeed = 0.5f;
    //Zona di despawn
    public float deadZone = -6.5f;
    public LogicScript logic;
    //Punteggio assegnato da ogni ostacolo superato
    public int scoreValue = 1;
    public ShipControllerScript player;

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.down;

        if (transform.position.y <= deadZone)
        {
            Destroy(gameObject);
            logic.AddScore(scoreValue);
        }
    }
}
