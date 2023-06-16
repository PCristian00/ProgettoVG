using UnityEngine;

public class BossScript : MonoBehaviour
{
    // Spawner dei nemici, utilizzato per resettare il contatore di Spawn
    public EnemySpawner spawner;
    // Vita del Boss
    public int boss_life = 3;
    // Riferimento al LogicManager
    private LogicScript logic;
    // Array contenente tutti i power-up che il boss puo' rilasciare alla morte
    public GameObject[] powerups;
    // Array contenente tutti i tipi di proiettili che il boss spara contemporaneamente
    public GameObject[] bullets;

    // Suono emesso se colpito
    public AudioSource hitSound;

    //Suono allo sparo
    public AudioSource bulletSound;

    // Posizione finale del movimento
    private Vector2 endPos;
    //Direzione del movimento
    private int direction;

    // Timer usato per le pause tra uno sparo e l'altro
    private float timer = 0;
    // Frequenza di fuoco in secondi
    private float fireRate = 2;


    // Start is called before the first frame update
    void Start()
    {
        boss_life = 3;
        spawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemySpawner>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        logic.ShowMessage("ATTENZIONE!!!", 1);

        endPos = new Vector2(transform.position.x, transform.position.y);

        // Direzione in cui il Boss si avvia alla partenza
        direction = -1;
    }

    // Update is called once per frame
    void Update()
    {
        // Cambio direzione se arrivato al limite orizzontale
        if (transform.position.x == 6 || transform.position.x == -6)
        {
            direction *= -1;
        }

        Move(direction);

        if (timer < fireRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
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
            // IL BOSS PER ADESSO NON SI FERMA MAI
            // QUESTO ELSE POTREBBE NON ESSERE MAI RAGGIUNTO

            // Debug.Log("Sto fermo");
            // spriteRenderer.sprite = sprites[0];
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
        // ASSICURARSI CHE OGNI ENEMY E ENEMYBULLET SIANO TAGGATI CON ENEMY
        // ALTRIMENTI IL BOSS POTREBBE ESSERE DANNEGGIATO DAL CORPO DEI NEMICI

        if (!other.gameObject.CompareTag("Enemy"))
        {
            // Debug.Log("Era Enemy? " + other.gameObject.CompareTag("Enemy"));
            // Debug.Log("Il tag era "+other.tag);
            // Debug.Log("Il nome era " + other.name);
            // Debug.Log("NON ERA UN NEMICO IN COLLISIONE");

            if (other.gameObject.layer == 7 || other.gameObject.layer == 12 || other.gameObject.layer == 13)
            {
                // Debug.Log("Colpito!!!");
                boss_life--;
                Debug.Log("Vita BOSS: " + boss_life + " / 3");

                hitSound.Play();

                Destroy(other.gameObject);
                if (boss_life == 0)
                {
                    Debug.Log("BOSS MORTO");
                    Destroy(other.gameObject);

                    spawner.BossKilled();
                    //spawner.countEnemyKill = 0;
                    //spawner.bossIsAlive = false;
                    //logic.AddScore(10);
                    //logic.CheckDifficulty(true);

                    // Il boss rilascia un power-up casuale alla morte
                    // Euler nella rotazione serve per riportare il power-up alla rotazione nulla invece di quella del boss
                    Instantiate(powerups[Random.Range(0, powerups.Length)], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.Euler(0, 0, 0));

                    

                    Destroy(gameObject);
                }
            }
        }
    }
}