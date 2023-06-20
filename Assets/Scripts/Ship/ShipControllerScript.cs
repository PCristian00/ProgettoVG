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
    // public Sprite[] sprites;
    // Riferimento allo SpriteRenderer
    public SpriteRenderer spriteRenderer;

    // Contatore che calcola il tempo al prossimo sparo possibile
    private float nextFireTime;
    // Secondi che devono passare tra uno sparo e l'altro, misurato in secondi
    private float timeBetweenShots = 1;

    // Riferimento alla barra della vita
    public GameObject lifeBar;
    // Riferimento a Image della barra della vita
    private Image lifeImage;
    // Array contenente i vari sprite della barra della vita
    public Sprite[] lifeSprites;
    // La vita dell'astronave
    public int life;

    public AudioSource shootSound;
    public AudioSource powerUpSound;
    public AudioSource deathSound;

    //Icone dei power-up attivi
    public GameObject[] icons;


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
            if (Input.GetButtonDown("FireRed"))
            {
                // Spara un proiettile che colpisce il layer 6, ovvero i nemici
                // Bozza
                Shoot(7);

            }

            // Sparo (Arma 2)
            if (Input.GetButtonDown("FireGreen"))
            {
                // Colpisce su layer 2, NON UCCIDE NEMICI PER ORA

                Shoot(12);
            }

            // Sparo (Arma 3)
            if (Input.GetButtonDown("FireYellow"))
            {
                // Colpisce su layer 3, NON UCCIDE NEMICI PER ORA

                Shoot(13);
            }

            // Astronave immortale. TOGLIERE DA PRODOTTO FINALE
            if (Input.GetButtonDown("No Clip"))
            {
                Shield();
                if (NoClip) logic.ShowMessage("NO CLIP ATTIVATO", 0);
                else logic.ToggleMessage();
            }
        }
        // Se il giocatore e' morto, il pulsante Restart riavvia la partita
        // CAMBIATO FUNZIONAMENTO DI GAMEOVER: RIMUOVERE
        //else if (Input.GetButtonDown("Restart"))
        //{
        //    logic.RestartGame();
        //}
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
                // spriteRenderer.sprite = sprites[1];
                transform.rotation = Quaternion.Euler(0, 0, 10);
            }
            else if (Mathf.Sign(moveRate) == 1)
            {
                // Debug.Log("VERSO DESTRA");
                // spriteRenderer.sprite = sprites[2];
                transform.rotation = Quaternion.Euler(0, 0, -10);
            }
        }
        else
        {
            // Debug.Log("Sto fermo");
            // spriteRenderer.sprite = sprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Controlla se l'astronave puo' sparare
    private bool CanFire
    {
        get { return Time.time > nextFireTime; }
    }

    // Permette di sparare proiettili.
    // Il valore type indica il tipo di nemico che il proiettile puo' distruggere.
    // Associare ogni tipo di nemico ad un layer diverso e collegare al proiettile giusto.
    private void Shoot(int type)
    {
        if (CanFire)
        {
            nextFireTime = Time.time + timeBetweenShots;

            // Debug.Log("Proiettile di tipo " + type);

            // Crea un oggetto di tipo Bullet
            // Il movimento del Bullet viene gestito in un altro script
            GameObject bullet = Instantiate(Bullet);
            // Il proiettile ha lo stesso layer specificato dall'input

            shootSound.Play();

            bullet.layer = type;

            // Debug.Log("Assegnato tipo "+bullet.layer);

            // Il Bullet parte dalla posizione di Ship modificata di +1 in verticale
            Vector2 shootPos = new(transform.position.x, transform.position.y + 1);
            bullet.transform.position = shootPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se la collisione avviene con Obstacle o Bullet e il NoClip disattivato (Immortalita')
        if ((collision.gameObject.layer == 3 || collision.gameObject.layer == 6) && !NoClip)
        {
            life--;
            lifeImage.sprite = lifeSprites[life];
            deathSound.Play();


            if (life == 0)
            {
                DeathAnimation();
                // Debug.Log("SEI MORTO");
                logic.gameIsOver = true;
                Invoke(nameof(LoadGameOver), 1.5f);
                // Debug.Log("INVOKE ...");
                // Disattiva il trigger dell'ostacolo / proiettile per evitare di morire piu' volte
                // contro lo stesso ostacolo
                collision.isTrigger = false;

            }
            else
            {
                // Riduce la velocita' del gioco con la collisione, se non è gia' al minimo
                logic.ChangeSpeed(-1);
                // Invulnerabile per 1 secondo
                Shield();
                Invoke(nameof(Shield), 1);
            }
        }

        // Collisioni con PowerUp
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            powerUpSound.Play();
            // Debug.Log("PowerUp preso: "+ collision.name);

            // Se la collisione avviene con HealthPowerUp
            if (collision.gameObject.layer == 8)
            {
                logic.ShowMessage("HEALTH", 1);
                if (life < 5)
                {
                    life = 5;
                    lifeImage.sprite = lifeSprites[life];
                }

            }

            // Se la collisione avviene con MultiplierPowerUp
            if (collision.gameObject.layer == 9)
            {
                // powerUpSound.Play();
                // Debug.Log("Fuoco rapido");
                logic.ShowMessage("SHOOT FASTER", 10);
                icons[1].SetActive(true);
                timeBetweenShots = 0.5f;
                // Il power-up dura 10 secondi
                Invoke(nameof(MultiplierResetEffect), 10);

            }

            // Se la collisione avviene con SpeedPowerUp
            if (collision.gameObject.layer == 10)
            {
                // powerUpSound.Play();
                // Debug.Log("Spostamento veloce");
                logic.ShowMessage("INCREASE SPEED", 10);
                icons[0].SetActive(true);
                speed += 10;
                // Il power-up dura 10 secondi
                Invoke(nameof(SpeedResetEffect), 10);

            }

            // Se la collisione avviene con DoubleScorePowerUp

            if (collision.gameObject.layer == 11)
            {
                // powerUpSound.Play();
                // Debug.Log("Punti doppi");
                logic.ShowMessage("DOUBLE POINTS", 10);
                icons[2].SetActive(true);
                logic.scoreMultiplier *= 2;
                // Il power-up dura 10 secondi
                Invoke(nameof(ScoreResetEffect), 10);

            }
        }

        Destroy(collision.gameObject);
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


        // Rende invisibile la barra della vita dopo la morte
        // lifeBar.SetActive(false);
    }

    // Annulla il power-up Multiplier
    private void MultiplierResetEffect()
    {
        // Debug.Log("Tempo power-up Multiplier scaduto");
        //logic.ShowMessage("FINE FUOCO RAPIDO", 1);
        icons[1].SetActive(false);
        timeBetweenShots = 1;
    }

    private void SpeedResetEffect()
    {
        // Debug.Log("Tempo power-up Speed scaduto");
        //logic.ShowMessage("FINE SPOSTAMENTO RAPIDO", 1);
        icons[0].SetActive(false);
        speed -= 10;
    }

    private void ScoreResetEffect()
    {
        // Debug.Log("Tempo power-up DoubleScore scaduto");
        //logic.ShowMessage("FINE PUNTI DOPPI", 1);
        icons[2].SetActive(false);
        logic.scoreMultiplier /= 2;
    }

    // Attiva / disattiva NoClip (Invulnerabilita') e attiva un effetto di trasparenza
    private void Shield()
    {
        NoClip = !NoClip;
        if (NoClip)
            spriteRenderer.color = new Color(1, 1, 1, .5f);

        else
            spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    // Richiama il GameOver di LogicScript per risolvere il problema di Invoke
    private void LoadGameOver()
    {
        logic.GameOver();
    }
}