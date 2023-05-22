using UnityEngine;

public class ShipControllerScript : MonoBehaviour
{
    //Posizione dopo spostamento
    private Vector3 endPos = new(0, -4, 0);

    //Quantita di spostamento (usata solo in ChangeLane)
    //La corsia ha 6 di larghezza, NON cambiare.
    //public int deltaX = 6;

    //Velocita di spostamento laterale
    public float speed = 15f;
    // Indica se l'astronave sia viva o no
    private bool isAlive = true;

    //Riferimento al collider
    private Collider2D colliderComponent;
    //Riferimento al rigidBody
    private Rigidbody2D myBody;

    //SOLO PER TESTING IMMORTALITA'
    private bool NoClip = false;

    public LogicScript logic;

    //Proiettile
    public GameObject Bullet;

    private void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myBody.isKinematic = true;
        colliderComponent = gameObject.GetComponent<Collider2D>();
        colliderComponent.isTrigger = true;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        if (isAlive)
        {
            if (Input.GetKey(KeyCode.D))
            {
                //Spostamento verso destra
                Move(speed);
            }

            //Spostamento verso sinistra
            if (Input.GetKey(KeyCode.A))
            {
                //Spostamento verso sinistra
                Move(-speed);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Shoot();
            }

            if (Input.GetKeyDown(KeyCode.W))
                logic.ChangeSpeed(1);

            if (Input.GetKeyDown(KeyCode.S))
                logic.ChangeSpeed(0);

            // Astronave immortale. TOGLIERE DA PRODOTTO FINALE
            if (Input.GetKeyDown(KeyCode.P))
            {
                NoClip = !NoClip;
                if (NoClip) Debug.Log("NO CLIP ATTIVATO");
                else Debug.Log("NO CLIP DISATTIVATO");
            }
        }
    }

    /*
    Cambio corsia (sinistra e destra) fissato a centro colonna
     void ChangeLane(int deltaX)
     {
         endPos = new Vector3(transform.position.x + deltaX, transform.position.y, 0);
         endPos.x = Mathf.Clamp(endPos.x,-6,6);
         gameObject.transform.position = endPos;        
     }    
     */

    //Movimento libero fluido
    private void Move(float speed)
    {
        endPos.x += speed * Time.deltaTime;
        endPos.x = Mathf.Clamp(endPos.x, -6, 6);
        gameObject.transform.position = endPos;
    }

    private void Shoot()
    {
        //Debug.Log("ANCORA NIENTE ARMI");
        GameObject bullet = Instantiate(Bullet);
        bullet.transform.position = transform.position; // gli assegnamo la posizione dell'enemy
        Vector2 direction = bullet.transform.position + Vector3.up;
        bullet.GetComponent<EnemyBullet>().SetDirection(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //La collisione avviene solo con oggetto del layer Obstacle
        //  && !NoClip e il campo NoClip SERVE SOLO PER IMMORTALITa al momento.
        // RIMUOVERE SE INUTILIZZATO DAL GIOCO FINALE
        if (collision.gameObject.layer == 3 && !NoClip)
        {
            //Distruzione dell'ostacolo all'impatto
            Destroy(collision.gameObject);

            Debug.Log("SEI MORTO");
            logic.GameOver();
            isAlive = false;
            //Disattiva il trigger dell'ostacolo per evitare di morire piu' volte contro lo stesso ostacolo
            collision.isTrigger = false;

            //Sposta l'astronave al contatto con l'ostacolo in una direzione casuale
            //Non e' necessario per il funzionamento del gioco, solo effetto estetico della collisione 
            colliderComponent.isTrigger = false;
            myBody.isKinematic = false;
            gameObject.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * 500f);
        }
    }
}