using UnityEngine;

/// <summary>
/// Gestisce il movimento e l'ingrandimento dell'ostacolo
/// </summary>
public class ObstacleScript : MonoBehaviour
{
    /// <summary>
    /// Velocita' di scorrimento verticale
    /// </summary>
    public float moveSpeed = 0.5f;
    /// <summary>
    /// Zona di despawn
    /// </summary>
    public float deadZone = -6.5f;
    /// <summary>
    /// Riferimento a logic
    /// </summary>
    private LogicScript logic;
    /// <summary>
    /// Tasso di ridimensionamento, inversamente proporzionale
    /// </summary>
    public float scaleRate;

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }
    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.down;

        // Calcola e applica la dimensione che l'oggetto ha durante il tragitto
        // In caso di elevato ridimensionamento, attivare V-Sync in modalita' Game nell'Editor
        float newScale = -transform.position.y / scaleRate;
        if (transform.position.y < 0)
            transform.localScale += new Vector3(newScale, newScale, 0);

        // Distrugge l'ostacolo quando arriva oltre il limite dello schermo
        if (transform.position.y <= deadZone)
        {
            Destroy(gameObject);
            logic.AddScore(1);
        }
    }
}