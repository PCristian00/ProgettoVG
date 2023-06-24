using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlla il movimento, lo sparo, la vita e i power-up dell'astronave.
/// </summary>
public class ShipControllerScript : MonoBehaviour
{
    /// <summary>
    /// Posizione dopo spostamento
    /// </summary>
    private Vector2 endPos = new(0, -4);

    /// <summary>
    /// Velocita' di spostamento laterale
    /// </summary>
    public float speed = 15f;
    /// <summary>
    /// Direzione del movimento orizzontale
    /// </summary>
    float horizontalMove = 0f;
    /// <summary>
    /// Indica se l'astronave sia distrutta o meno
    /// </summary>
    public bool isAlive = true;
    /// <summary>
    ///Riferimento al collider
        /// </summary>
    private Collider2D colliderComponent;

    /// <summary>
    /// Riferimento al rigidBody
    /// </summary>
    private Rigidbody2D myBody;

    /// <summary>
    /// Rende l'astronave invulnerabile
    /// </summary>
    private bool NoClip = false;

    /// <summary>
    /// Riferimento al LogicManager
    /// </summary>    
    public LogicScript logic;

    /// <summary>
    /// Riferimento al Bullet
    /// </summary>    
    public GameObject Bullet;
    /// <summary>
    /// Riferimento allo SpriteRenderer
    /// </summary>    
    public SpriteRenderer spriteRenderer;

    /// <summary>
    /// Contatore che calcola il tempo al prossimo sparo possibile
    /// </summary>    
    private float nextFireTime;
    /// <summary>
    /// Secondi che devono passare tra uno sparo e l'altro, misurato in secondi
    /// </summary>    
    private float timeBetweenShots = 1;

    /// <summary>
    /// Riferimento alla barra della vita
    /// </summary>    
    public GameObject lifeBar;
    /// <summary>
    /// Riferimento a Image della barra della vita
    /// </summary>
    private Image lifeImage;
    /// <summary>
    /// Array contenente i vari sprite della barra della vita
    /// </summary>    
    public Sprite[] lifeSprites;
    /// <summary>
    /// La vita dell'astronave
    /// </summary>    
    public int life;
    /// <summary>
    /// Suono dello sparo
    /// </summary>
    public AudioSource shootSound;
    /// <summary>
    /// Suono raccolta power-up
    /// </summary>
    public AudioSource powerUpSound;
    /// <summary>
    /// Suono eseguito se colpita
    /// </summary>
    public AudioSource deathSound;

    /// <summary>
    /// Icone dei power-up attivi
    /// </summary>
    public GameObject[] icons;

    /// <summary>
    /// Indica se l'astronave sia in modalita' menu (rimuove pulsanti velocita')
    /// </summary>    
    public bool MenuMode = false;


    private void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myBody.isKinematic = true;
        colliderComponent = gameObject.GetComponent<Collider2D>();
        colliderComponent.isTrigger = true;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        life = 5;
        if (!MenuMode)
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
                Shoot(7);
            }

            // Sparo (Arma 2)
            if (Input.GetButtonDown("FireGreen"))
            {
                Shoot(12);
            }

            // Sparo (Arma 3)
            if (Input.GetButtonDown("FireYellow"))
            {
                Shoot(13);
            }

            // Astronave immortale.
            // USATO SOLO IN FASE DI TESTING
            //if (Input.GetButtonDown("No Clip"))
            //{
            //    Shield();
            //    if (NoClip) logic.ShowMessage("NO CLIP ATTIVATO", 0);
            //    else logic.ToggleMessage();
            //}
        }
    }

    /// <summary>
    /// Permette all'astronave di muoversi lateralmente in due direzioni e con una leggera animazione di inclinazione
    /// </summary>
    /// <param name="moveRate">Quantita' di spostamento</param>
    private void Move(float moveRate)
    {
        endPos.x += moveRate * Time.deltaTime;
        endPos.x = Mathf.Clamp(endPos.x, -6, 6);
        transform.position = endPos;

        if (moveRate != 0)
        {
            if (Mathf.Sign(moveRate) == -1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 10);
            }
            else if (Mathf.Sign(moveRate) == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, -10);
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    /// <summary>
    /// Controlla se l'astronave sia pronta a sparare
    /// </summary>
    private bool CanFire
    {
        get { return Time.time > nextFireTime; }
    }

    // Permette di sparare proiettili.
    // Il valore type indica il tipo di nemico che il proiettile puo' distruggere.
    // Associare ogni tipo di nemico ad un layer diverso e collegare al proiettile giusto.
    /// <summary>
    /// Permette di sparare proiettili diversi.
    /// </summary>
    /// <param name="type">Indica il tipo (layer / colore) del proiettile</param>
    private void Shoot(int type)
    {
        if (CanFire)
        {
            nextFireTime = Time.time + timeBetweenShots;
            // Crea un oggetto di tipo Bullet
            // Il movimento del Bullet viene gestito in un altro script
            GameObject bullet = Instantiate(Bullet);
            // Il proiettile ha lo stesso layer specificato dall'input
            shootSound.Play();
            bullet.layer = type;
            // Il Bullet parte dalla posizione di Ship modificata di +1 in verticale
            Vector2 shootPos = new(transform.position.x, transform.position.y + 1);
            bullet.transform.position = shootPos;
        }
    }

    /// <summary>
    /// Gestisce tutte le collisioni tra astronave e proiettili, ostacoli e power-up
    /// </summary>
    /// <param name="collision">oggetto con cui e' avvenuta la collisione</param>
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
                logic.gameIsOver = true;
                Invoke(nameof(LoadGameOver), 1.5f);
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
            // Se la collisione avviene con HealthPowerUp
            if (collision.gameObject.layer == 8)
            {
                logic.ShowMessage("RECUPERO SALUTE", 1);
                if (life < 5)
                {
                    life = 5;
                    lifeImage.sprite = lifeSprites[life];
                }

            }
            // Se la collisione avviene con FastFirePowerUp
            if (collision.gameObject.layer == 9)
            {
                logic.ShowMessage("FUOCO RAPIDO", 10);
                icons[1].SetActive(true);
                timeBetweenShots = 0.5f;
                // Il power-up dura 10 secondi
                Invoke(nameof(FireResetEffect), 10);

            }
            // Se la collisione avviene con SpeedPowerUp
            if (collision.gameObject.layer == 10)
            {
                logic.ShowMessage("SCHIVATA RAPIDA", 10);
                icons[0].SetActive(true);
                speed += 10;
                // Il power-up dura 10 secondi
                Invoke(nameof(SpeedResetEffect), 10);

            }

            // Se la collisione avviene con DoubleScorePowerUp

            if (collision.gameObject.layer == 11)
            {
                logic.ShowMessage("DOPPI PUNTI", 10);
                icons[2].SetActive(true);
                logic.scoreMultiplier *= 2;
                // Il power-up dura 10 secondi
                Invoke(nameof(ScoreResetEffect), 10);

            }
        }
        Destroy(collision.gameObject);
    }

    /// <summary>
    /// Lancia l'astronave in una direzione casuale (effetto estetico usato per la sconfitta).
    /// </summary>
    public void DeathAnimation()
    {
        isAlive = false;
        colliderComponent.isTrigger = false;
        myBody.isKinematic = false;
        gameObject.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * 500f);
    }

    /// <summary>
    /// Annulla il power-up FastFire
    /// </summary>
    private void FireResetEffect()
    {
        icons[1].SetActive(false);
        timeBetweenShots = 1;
    }
    /// <summary>
    /// Annulla il power-up Speed
    /// </summary>
    private void SpeedResetEffect()
    {
        icons[0].SetActive(false);
        speed -= 10;
    }

    /// <summary>
    /// Annulla il power-up DoubleScore
    /// </summary>
    private void ScoreResetEffect()
    {
        icons[2].SetActive(false);
        logic.scoreMultiplier /= 2;
    }

    /// <summary>
    /// Rende l'astronave invulnerabile e trasparente, o disattiva l'effetto se gia' presente
    /// </summary>
    private void Shield()
    {
        NoClip = !NoClip;
        if (NoClip)
            spriteRenderer.color = new Color(1, 1, 1, .5f);

        else
            spriteRenderer.color = new Color(1, 1, 1, 1);
    }

  /// <summary>
  /// Richiama logic.GameOver (Risolve problemi di Invoke alla sconfitta)
  /// </summary>
    private void LoadGameOver()
    {
        logic.GameOver();
    }
}