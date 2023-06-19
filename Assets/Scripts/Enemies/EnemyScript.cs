using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private EnemySpawner spawner;

    public GameObject Bullet;

    private float timer = 0;

    // Frequenza di sparo
    private float fireRate = 2;
    // Suono del proiettile
    public AudioSource bulletSound;
    private bool canFire = true;

    // Array contenente tutti i power-up che il boss puo' rilasciare alla morte
    public GameObject[] powerups;

    // Riferimento al collider
    private Collider2D colliderComponent;
    // Riferimento al rigidBody
    private Rigidbody2D myBody;

    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemySpawner>();

        myBody = gameObject.GetComponent<Rigidbody2D>();
        myBody.isKinematic = true;
        colliderComponent = gameObject.GetComponent<Collider2D>();
        colliderComponent.isTrigger = true;
    }

    void Update()
    {
        if (timer < fireRate)
            timer += Time.deltaTime;
        else
        {
            timer = 0;
            if (canFire)
                FireBullet();
        }
    }

    void FireBullet()
    {
        // preleviamo la posizione del player
        GameObject playerShip = GameObject.Find("Ship");
        bulletSound.Play();
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
                DeathAnimation();
                Destroy(other.gameObject);
            }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    // Lancia Enemy verso l'alto con una direzione diagonale casuale, segnala allo spawner la morte e infine distrugge se stesso.
    private void DeathAnimation()
    {
        canFire = false;
        spawner.EnemyKilled();

        // Sorteggia un numero da 1 a 100.
        // Se il numero e' inferiore a 20 (1 possiblita' su 5) viene rilasciato un power-up 
        int number = (int)Random.Range(1, 100);
        Debug.Log("Numero sorteggiato: " + number);
        if (number <= 20)
            Instantiate(powerups[Random.Range(0, powerups.Length)], transform.position, transform.rotation);

        colliderComponent.isTrigger = false;
        myBody.isKinematic = false;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 250), 500));
        Invoke(nameof(Kill), 0.5f);
    }
}
