using UnityEngine;
/// <summary>
/// Gestisce il movimento verticale dei power-up.
/// </summary>
public class PowerUpScript : MonoBehaviour
{
    /// <summary>
    /// Velocita' di movimento
    /// </summary>    
    public float moveSpeed = 1f;
    /// <summary>
    /// Zona di despawn
    /// </summary>    
    public float deadZone = -6.5f;

    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.down;
        // Distrugge il power-up quando arriva oltre il limite dello schermo
        if (transform.position.y <= deadZone)
        {
            Destroy(gameObject);
        }
    }
}