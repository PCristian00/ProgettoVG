using UnityEngine;
/// <summary>
/// Gestisce il movimento delle stelle
/// </summary>
public class Star : MonoBehaviour
{
    /// <summary>
    /// Zona di despawn
    /// </summary>    
    public float deadZone = -6.5f;
    /// <summary>
    /// Riferimento allo spawner
    /// </summary>
    StarSpawner spawner;
    /// <summary>
    /// Riferimento a logic
    /// </summary>
    LogicScript logic;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spawner = GameObject.FindGameObjectWithTag("StarSpawner").GetComponent<StarSpawner>();
    }

    void Update()
    {
        transform.position += (8-logic.speed) * Time.deltaTime * Vector3.down;
        // Distrugge la stella quando arriva oltre il limite dello schermo
        if (transform.position.y <= deadZone)
        {
            Destroy(gameObject);
            spawner.starCount--;
        }
    }
}