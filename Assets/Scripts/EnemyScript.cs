using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public EnemySpawner spawner;
    private LogicScript logic;

    public AudioSource deathSound;

    public GameObject Bullet;
    private float timer = 0;
    private float spawnRate = 2;


    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemySpawner>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            FireBullet();
        }
    }

    void FireBullet()
    {
        // preleviamo la posizione del player
        GameObject playerShip = GameObject.Find("Ship");
        if (playerShip != null)
        {
            GameObject bullet = Instantiate(Bullet);
            bullet.transform.position = transform.position; // gli assegnamo la posizione dell'enemy
                                                            // calcoliamo la direzione verso il player
            Vector2 direction = playerShip.transform.position - bullet.transform.position;
            // impostiamo la direzione del proiettile
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        // Se entra in collisione con uno ShipBullet dello stesso colore (layer) di questo Enemy
        if (other.gameObject.layer == this.gameObject.layer)
        {
            spawner.countEnemyKill++;
            // NON SUONA, forse perché l'oggetto viene distrutto subito dopo
            deathSound.Play();

            // Debug.Log("Suono suonato");
            // Distrugge se stesso e ShipBullet
            Destroy(gameObject);
            Destroy(other.gameObject);

            

            logic.AddScore(1);
           if (spawner.countSpawn > 0)
            {
                spawner.countSpawn--;
               // Debug.Log("Nemici in gioco: " + spawner.countSpawn);
            }            
        }
    }
}
