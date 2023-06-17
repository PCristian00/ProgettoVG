using UnityEngine;

public
class ObstacleScript : MonoBehaviour
{
    // Velocita' di movimento
    public float moveSpeed = 0.5f;
    // Zona di despawn
    public float deadZone = -6.5f;
    public LogicScript logic;
    // Punteggio assegnato da ogni ostacolo superato
    public int scoreValue = 1;
    //public ShipControllerScript player;

    // Tasso di ridimensionamento, inversamente proporzionale
    public float scaleRate = 350f;

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.down;
        // Calcola e applica la dimensione che l'oggetto ha durante il tragitto
        float newScale = -transform.position.y / scaleRate;
        transform.localScale += new Vector3(newScale, newScale, 0);

        // Distrugge l'ostacolo quando arriva oltre il limite dello schermo
        if (transform.position.y <= deadZone)
        {
            Destroy(gameObject);
            logic.AddScore(scoreValue);
        }
    }
}