using UnityEngine;

public class ShipControllerScript : MonoBehaviour
{
    // Posizione dopo spostamento
    private Vector2 endPos = new(0, -4);

    // Velocita di spostamento laterale
    public float speed = 15f;

    // Indica se l'astronave sia viva o no
    private bool isAlive = true;
    // Direzione del movimento orizzontale
    float horizontalMove = 0f;

    // Riferimento al collider
    private Collider2D colliderComponent;

    //Riferimento al rigidBody
    private Rigidbody2D myBody;

    // SOLO PER TESTING IMMORTALITA'
    private bool NoClip = false;

    // Riferimento al LogicManager
    public LogicScript logic;

    // Riferimento al Bullet
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
            // Spostamento laterale
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
            Move(horizontalMove);

           /* //Spostamento verso destra
            if (Input.GetAxisRaw("Horizontal")== 1)
                Move(speed);

            //Spostamento verso sinistra
            if (Input.GetAxisRaw("Horizontal") == -1)
                Move(-speed);*/

            /*if (Input.GetKey(KeyCode.D))
             {
                 //Spostamento verso destra
                 Move(speed);
             }

             //Spostamento verso sinistra
             if (Input.GetKey(KeyCode.A))
             {
                 //Spostamento verso sinistra
                 Move(-speed);
             }*/

            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }

            if (Input.GetButtonDown("Speed Up"))
                logic.ChangeSpeed(1);

            if (Input.GetButtonDown("Speed Down"))
                logic.ChangeSpeed(-1);

            // Astronave immortale. TOGLIERE DA PRODOTTO FINALE
            if (Input.GetButtonDown("No Clip"))
            {
                NoClip = !NoClip;
                if (NoClip) Debug.Log("NO CLIP ATTIVATO");
                else Debug.Log("NO CLIP DISATTIVATO");
            }
        }
    }

    //Movimento libero fluido
    private void Move(float moveRate)
    {
        endPos.x += moveRate * Time.deltaTime;
        endPos.x = Mathf.Clamp(endPos.x, -6, 6);
        gameObject.transform.position = endPos;
    }

    
    private void Shoot()
    {
        //Crea un oggetto di tipo Bullet
        //Il movimento del Bullet viene gestito in un altro script
        GameObject bullet = Instantiate(Bullet);
        //Il Bullet parte dalla posizione di Ship modificata di +1 in verticale 
        Vector2 shootPos = new(transform.position.x, transform.position.y + 1);
        bullet.transform.position = shootPos;
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