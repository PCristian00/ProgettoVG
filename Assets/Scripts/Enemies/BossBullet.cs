using UnityEngine;
/// <summary>
/// Gestisce il movimento del proiettile del boss, con eventuale traiettoria diagonale
/// </summary>
public class BossBullet : MonoBehaviour
{
    /// <summary>
    /// Velocita' del proiettile
    /// </summary>
    public float speed = 4f;
    /// <summary>
    /// Indica l'inclinazione diagonale (Positiva se verso sinistra, negativa verso destra)
    /// </summary>    
    public float direction = 0;

    public void Update()
    {
        transform.position += (speed * Time.deltaTime * Vector3.down) + (direction * speed * Time.deltaTime * Vector3.left);

        // Distrugge il proiettile se fuori dai bordi dello schermo
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.x < min.x || transform.position.x > max.x || transform.position.y < min.y || transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }
}
