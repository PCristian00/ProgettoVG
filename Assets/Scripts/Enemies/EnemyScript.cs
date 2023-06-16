using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private EnemySpawner spawner;

    public GameObject Bullet;
    private float timer = 0;
    private float fireRate = 2;



    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemySpawner>();
    }

    void Update()
    {
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

    void FireBullet()
    {
        // preleviamo la posizione del player
        GameObject playerShip = GameObject.Find("Ship");
        if (playerShip != null)
        {
            GameObject bullet = Instantiate(Bullet);
            // gli assegnamo la posizione dell'enemy
            bullet.transform.position = transform.position;
            // calcoliamo la direzione verso il player
            Vector2 direction = playerShip.transform.position - bullet.transform.position;
            // impostiamo la direzione del proiettile
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.gameObject.CompareTag("Enemy"))
            // Se entra in collisione con uno ShipBullet dello stesso colore (layer) di questo Enemy
            if (other.gameObject.layer == this.gameObject.layer)
            {
                spawner.EnemyKilled();
                // Distrugge se stesso e ShipBullet
                Destroy(gameObject);
                Destroy(other.gameObject);

                //logic.AddScore(1);
            }
    }
}
