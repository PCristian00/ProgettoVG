using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    Vector2 _direction; //per indirizzare il proiettile verso il player
    bool isReady; // controlla se il proiettile ha trovato la direzione dove andare
    // Start is called before the first frame update

    public ShipControllerScript ship;

    // Riferimento al LogicManager
    public LogicScript logic;

    public void Awake() // nel momento in cui viene istanziato il gameobject del proiettile, gli assegnamo una velocità 
    {
        speed = 4f;
        isReady = false; // ancora non trova la direzione
    }
    void Start()
    {
        ship = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipControllerScript>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        // Debug.Log("Caricamento ship e logic riusciti");
    }
    public void SetDirection(Vector2 direction)  // calcoliamo la direzione, la normalizziamo, quindi isReady
    {
        //normalizziamo la direzione
        _direction = direction.normalized; // normalized ci restituisce un vettore unitario
        isReady = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isReady) // quando è pronto, inizia ad aggiornare il movimento del proiettile
        {
            //aggiorno la posizione del proiettile
            Vector2 position = transform.position; //prelevo la posizione
            position += _direction * speed * Time.deltaTime; // la incremento per direction per speed e time.delta<time
            transform.position = position;  // applichiamo di nuovo la posizione
            //prelevo i bordi della camera
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            if (transform.position.x < min.x || transform.position.x > max.x || transform.position.y < min.y || transform.position.y > max.y)
                Destroy(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("COLLISIONE!!!");
        if (collision.gameObject.CompareTag("Player"))
        {
            
            //Destroy(collision.gameObject);

            //Debug.Log("SEI MORTO");
            logic.GameOver();
            // ship.isAlive = false;
            // Disattiva il trigger dell'ostacolo per evitare di morire piu' volte
            // contro lo stesso ostacolo
            collision.isTrigger = false;
            ship.DeathAnimation();
        }
    }
}
