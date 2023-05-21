using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //Velocità di movimento
    public float moveSpeed = 0.5f;
    //Zona di despawn
    public int deadZone = -8;
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
            //Debug.Log("Ostacolo distrutto");
            Destroy(gameObject);
            logic.AddScore(scoreValue);
        }
    }
}
