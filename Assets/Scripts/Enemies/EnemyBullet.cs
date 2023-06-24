using UnityEngine;
/// <summary>
/// Gestisce e calcola, attraverso la posizione dell'astronave, la traiettoria del proiettile nemico
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    /// <summary>
    /// Velocita' del proiettile
    /// </summary>
    public float speed;
    /// <summary>
    /// direzione necessaria a indirizzare il proiettile verso il player
    /// </summary>
    Vector2 _direction;
    /// <summary>
    /// Indica se il proiettile ha trovato la direzione dove andare
    /// </summary>
    bool isReady;
    /// <summary>
    /// Nel momento in cui viene istanziato il gameobject del proiettile, assegna una velocità
    /// </summary>
    public void Awake()
    {
        speed = 4f;
        isReady = false;
    }
    /// <summary>
    ///  Calcola la direzione, la normalizza e da' la conferma
    /// </summary>
    /// <param name="direction">Direzione del proiettile</param>
    public void SetDirection(Vector2 direction)
    {
        // Normalizziamo la direzione
        // normalized ci restituisce un vettore unitario
        _direction = direction.normalized;
        isReady = true;
    }

    void Update()
    {
        // quando è pronto, inizia ad aggiornare il movimento del proiettile
        if (isReady)
        {
            // la posizione del proiettile viene aggiornata
            Vector2 position = transform.position;
            position += _direction * speed * Time.deltaTime;
            transform.position = position;
            // Distruggo il proiettile se esce dai bordi dello schermo
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            if (transform.position.x < min.x || transform.position.x > max.x ||
                transform.position.y < min.y || transform.position.y > max.y)
                Destroy(gameObject);
        }
    }
}
