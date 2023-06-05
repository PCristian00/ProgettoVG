using UnityEngine;
using UnityEngine.UI;

public class ShipControllerScript : MonoBehaviour
{
    // Posizione dopo spostamento
    private Vector2 endPos = new(0, -4);

    // Velocita di spostamento laterale
    public float speed = 15f;
    // Direzione del movimento orizzontale
    float horizontalMove = 0f;

    // Indica se l'astronave sia viva o no
    public bool isAlive = true;

    // Riferimento al collider
    private Collider2D colliderComponent;

    // Riferimento al rigidBody
    private Rigidbody2D myBody;

    // SOLO PER TESTING IMMORTALITA'
    private bool NoClip = false;

    // Riferimento al LogicManager
    public LogicScript logic;

    // Riferimento al Bullet
    public GameObject Bullet;

    // Array degli sprite
    public Sprite[] sprites;
    // Riferimento allo SpriteRenderer
    public SpriteRenderer spriteRenderer;

    // Contatore che calcola il tempo al prossimo sparo possibile
    private float nextFireTime;
    // Secondi che devono passare tra uno sparo e l'altro, misurato in secondi
    private readonly float timeBetweenShots = 1;

    // Riferimento alla barra della vita
    public GameObject lifeBar;
    //Riferimento a Image della barra della vita
    private Image lifeImage;
    // Array contenente i vari sprite della barra della vita
    public Sprite[] lifeSprites;
    // La vita dell'astronave
    private int life;
    private void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myBody.isKinematic = true;
        colliderComponent = gameObject.GetComponent<Collider2D>();
        colliderComponent.isTrigger = true;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        life = 5;
        lifeImage = lifeBar.GetComponent<Image>();


    }

    void Update()
    {

        // Se il giocatore e' ancora in vita
        if (isAlive)
        {
            // Spostamento laterale
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
            Move(horizontalMove);

            // Aumento velocita'
            if (Input.GetButtonDown("Speed Up"))
                logic.ChangeSpeed(1);

            // Diminuzione velocita'
            if (Input.GetButtonDown("Speed Down"))
                logic.ChangeSpeed(-1);

            // Sparo (Arma 1)
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }

            // Sparo (Arma 2)
            if (Input.GetButtonDown("Fire2"))
            {
                Shoot();
            }

            // Sparo (Arma 3)
            if (Input.GetButtonDown("Fire3"))
            {
                Shoot();
            }

            // Astronave immortale. TOGLIERE DA PRODOTTO FINALE
            if (Input.GetButtonDown("No Clip"))
            {
                NoClip = !NoClip;
                if (NoClip)
                    Debug.Log("NO CLIP ATTIVATO");
                else
                    Debug.Log("NO CLIP DISATTIVATO");
            }
        }
        // Se il giocatore e' morto, il pulsante Restart riavvia la partita
        else if (Input.GetButtonDown("Restart"))
        {
            logic.RestartGame();
        }
    }

    // Movimento libero fluido
    private void Move(float moveRate)
    {
        endPos.x += moveRate * Time.deltaTime;
        endPos.x = Mathf.Clamp(endPos.x, -6, 6);
        transform.position = endPos;

        if (moveRate != 0)
        {
            if (Mathf.Sign(moveRate) == -1)
            {
                // Debug.Log("VERSO SINISTRA");
                spriteRenderer.sprite = sprites[1];
                transform.rotation = Quaternion.Euler(0, 0, 10);
            }
            else if (Mathf.Sign(moveRate) == 1)
            {
                // Debug.Log("VERSO DESTRA");
                spriteRenderer.sprite = sprites[2];
                transform.rotation = Quaternion.Euler(0, 0, -10);
            }
        }
        else
        {
            // Debug.Log("Sto fermo");
            spriteRenderer.sprite = sprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Controlla se l'astronave puo' sparare
    private bool CanFire
    {
        get { return Time.time > nextFireTime; }
    }

    private void Shoot()
    {
        if (CanFire)
        {
            nextFireTime = Time.time + timeBetweenShots;

            // Crea un oggetto di tipo Bullet
            // Il movimento del Bullet viene gestito in un altro script
            GameObject bullet = Instantiate(Bullet);
            // Il Bullet parte dalla posizione di Ship modificata di +1 in verticale
            Vector2 shootPos = new(transform.position.x, transform.position.y + 1);
            bullet.transform.position = shootPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // La collisione avviene solo con oggetto del layer Obstacle o Enemy
        //   && !NoClip e il campo NoClip SERVE SOLO PER IMMORTALITa al momento.
        //  RIMUOVERE SE INUTILIZZATO DAL GIOCO FINALE
        if ((collision.gameObject.layer == 3 || collision.gameObject.layer == 7) && !NoClip)
        {
            life--;
            lifeImage.sprite = lifeSprites[life];

            // Distruzione dell'ostacolo / proiettile all'impatto
            Destroy(collision.gameObject);
            Debug.Log("Ti rimane " + life + "/5 salute");
            if (life == 0)
            {
                Debug.Log("SEI MORTO");
                logic.GameOver();
                // Disattiva il trigger dell'ostacolo / proiettile per evitare di morire piu' volte
                // contro lo stesso ostacolo
                collision.isTrigger = false;
                DeathAnimation();
            }
        }
    }

    // Sposta l'astronave al contatto con l'ostacolo in una direzione casuale
    // Non e' necessario per il funzionamento del gioco, solo effetto estetico
    // della collisione
    public void DeathAnimation()
    {
        isAlive = false;
        colliderComponent.isTrigger = false;
        myBody.isKinematic = false;
        gameObject.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * 500f);
        // Rende invisibile la barra della vita dopo la morta
        lifeBar.SetActive(false);
    }
}