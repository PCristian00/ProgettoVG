using UnityEngine;

public class BossScript : MonoBehaviour
{
    // Spawner dei nemici, utilizzato per resettare il contatore di Spawn
    EnemySpawner spawner;
    // Vita del Boss
    int life = 3;
    // Array contenente tutti i power-up che il boss puo' rilasciare alla morte
    public GameObject[] powerups;
    // Array contenente tutti i tipi di proiettili che il boss spara contemporaneamente
    public GameObject[] bullets;

    // Suono emesso se colpito
    public AudioSource hitSound;

    //Suono allo sparo
    public AudioSource bulletSound;
    private bool canFire = true;

    // Posizione finale del movimento
    private Vector2 endPos;
    //Direzione del movimento
    private int direction;
    private bool isMoving = false;

    // Timer usato per le pause tra uno sparo e l'altro
    private float timer = 0;
    // Frequenza di fuoco in secondi
    private float fireRate = 2;

    // Riferimento al collider
    private Collider2D colliderComponent;

    // Riferimento al rigidBody
    private Rigidbody2D myBody;

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
            transform.position = Vector3.MoveTowards(transform.position, target, 2 * Time.deltaTime);
            if (transform.position.Equals(target))
            {
                Debug.Log("ARRIVATO A DESTINAZIONE");
                Debug.Log("Posizione: " +transform.position+" Target: "+target);
                isMoving = true;
            }
                        
        }

        Debug.Log("EEEE Posizione: " + transform.position + " Target: " + target);

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
            if(canFire)
            FireBullet();
        }
    }

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

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.layer == 7 || other.gameObject.layer == 12 || other.gameObject.layer == 13)
            {
                life--;
                // Debug.Log("Vita BOSS: " + life + " / 3");

                hitSound.Play();

                Destroy(other.gameObject);
                if (life == 0)
                    DeathAnimation();

            }
        }
    }

    // Lancia il Boss verso l'alto con una direzione diagonale casuale, segnala allo spawner la morte e infine distrugge se stesso.
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

    private void Kill()
    {
        Destroy(gameObject);
    }
}