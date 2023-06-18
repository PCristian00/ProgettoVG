using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private EnemySpawner spawner;

    public GameObject Bullet;

    private float timer = 0;

    // Frequenza di sparo
    private float fireRate = 1;
    // Suono del proiettile
    public AudioSource bulletSound;
    private bool canFire = true;

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
            if(canFire)
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
        Invoke(nameof(Kill), 0.5f);
        colliderComponent.isTrigger = false;
        myBody.isKinematic = false;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 250), 500));
    }
}
