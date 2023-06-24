using UnityEngine;
/// <summary>
/// Gestisce l'animazione d'arrivo, il movimento, gli spari e la morte del Boss
/// </summary>
public class BossScript : MonoBehaviour
{
    /// <summary>
    /// Spawner dei nemici, utilizzato per resettare il contatore di Spawn
    /// </summary>    
    EnemySpawner spawner;
    /// <summary>
    /// Vita del Boss
    /// </summary>    
    int life = 3;
    /// <summary>
    /// Array contenente tutti i power-up che il boss puo' rilasciare alla morte
    /// </summary>    
    public GameObject[] powerups;
    /// <summary>
    /// Array contenente tutti i tipi di proiettili che il boss spara contemporaneamente
    /// </summary>
    public GameObject[] bullets;

    /// <summary>
    /// Suono emesso se colpito
    /// </summary>    
    public AudioSource hitSound;

    /// <summary>
    /// Suono allo sparo
    /// </summary>
    public AudioSource bulletSound;
    /// <summary>
    /// Indica se il Boss puo' sparare o meno
    /// </summary>
    private bool canFire = true;

    /// <summary>
    /// Posizione finale del movimento
    /// </summary>    
    private Vector2 endPos;
    /// <summary>
    /// Direzione del movimento
    /// </summary>
    private int direction;
    /// <summary>
    /// Indica se il boss e' in movimento
    /// </summary>
    private bool isMoving = false;

    /// <summary>
    /// Timer usato per le pause tra uno sparo e l'altro
    /// </summary>
    private float timer = 0;
    /// <summary>
    /// Frequenza di fuoco in secondi
    /// </summary>    
    private float fireRate = 2;

    /// <summary>
    /// Riferimento al collider
    /// </summary>
    private Collider2D colliderComponent;

    /// <summary>
    /// Riferimento al rigidBody
    /// </summary>
    private Rigidbody2D myBody;
    /// <summary>
    /// Posizione del boss scelta casualmente
    /// </summary>
    private Vector2 target;

    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemySpawner>();

        // Direzione in cui il Boss si avvia alla partenza
        direction = -1;

        myBody = gameObject.GetComponent<Rigidbody2D>();
        myBody.isKinematic = true;
        colliderComponent = gameObject.GetComponent<Collider2D>();
        colliderComponent.isTrigger = true;

        target = new Vector2(0, 1.5f);
        endPos = target;
    }

    void Update()
    {
        if (!isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 2.5f * Time.deltaTime);
            if (transform.position.Equals(target))
            {
                isMoving = true;
            }

        }

        // Cambio direzione se arrivato al limite orizzontale
        if (transform.position.x == 6 || transform.position.x == -6)
            direction *= -1;


        if (isMoving)
            Move(direction);

        if (timer < fireRate)
            timer += Time.deltaTime;
        else
        {
            timer = 0;
            if (canFire)
                FireBullet();
        }
    }

    /// <summary>
    /// Permette lo spostamento laterale del nemico, con effetto di inclinazione sprite associato
    /// </summary>
    /// <param name="moveRate">tasso e direzione di movimento</param>
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
            // IL BOSS PER ADESSO NON SI FERMA MAI
            // QUESTO ELSE POTREBBE NON ESSERE MAI RAGGIUNTO
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    /// <summary>
    /// Spara i tre proiettili
    /// </summary>
    void FireBullet()
    {
        // Array necessario per instanziare i proiettili correttamente
        GameObject[] objects = new GameObject[bullets.Length];

        bulletSound.Play();

        // Per ogni proiettile caricato in BossScript
        for (int i = 0; i < bullets.Length; i++)
        {
            // Instanzia il proiettile nella posizione attuale del Boss
            objects[i] = Instantiate(bullets[i]);
            objects[i].transform.position = transform.position;
        }
    }
    /// <summary>
    /// Gestisce le collisioni con i proiettili e dunque danni e morte del nemico
    /// </summary>
    /// <param name="other">Oggetto in collisione</param>
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.layer == 7 || other.gameObject.layer == 12 || other.gameObject.layer == 13)
            {
                life--;
                hitSound.Play();

                Destroy(other.gameObject);
                if (life == 0)
                    DeathAnimation();

            }
        }
    }

    /// <summary>
    /// Lancia il Boss verso l'alto con una direzione diagonale casuale,
    /// segnala allo spawner la morte e infine distrugge se stesso.
    /// </summary>    
    private void DeathAnimation()
    {
        isMoving = false;
        canFire = false;
        spawner.BossKilled();
        colliderComponent.isTrigger = false;
        myBody.isKinematic = false;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 250), 500));

        // Il boss rilascia un power-up casuale alla morte
        // Euler nella rotazione serve per riportare il power-up alla rotazione nulla invece di quella del boss
        Instantiate(powerups[Random.Range(0, powerups.Length)], transform.position, Quaternion.Euler(0, 0, 0));

        Invoke(nameof(Kill), 0.5f);
    }
    /// <summary>
    /// Uccide il boss
    /// </summary>
    private void Kill()
    {
        Destroy(gameObject);
    }
}