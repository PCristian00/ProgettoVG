using UnityEngine;
/// <summary>
/// Gestisce l'animazione d'arrivo, lo sparo, la morte del nemico
/// </summary>
public class EnemyScript : MonoBehaviour
{
    /// <summary>
    /// Riferimento allo spawner
    /// </summary>
    private EnemySpawner spawner;
    /// <summary>
    /// Il proiettile da sparare
    /// </summary>
    public GameObject Bullet;
    /// <summary>
    /// Timer
    /// </summary>
    private float timer = 0;

    /// <summary>
    /// Frequenza di sparo
    /// </summary>    
    private float fireRate = 2;
    /// <summary>
    /// Suono del proiettile
    /// </summary>    
    public AudioSource bulletSound;
    /// <summary>
    /// Indica se il nemico puo' sparare o no
    /// </summary>
    private bool canFire = false;

    /// <summary>
    /// Array contenente tutti i power-up che il boss puo' rilasciare alla morte
    /// </summary>    
    public GameObject[] powerups;

    /// <summary>
    /// Riferimento al collider
    /// </summary>    
    private Collider2D colliderComponent;
    /// <summary>
    /// Riferimento al rigidBody
    /// </summary>    
    private Rigidbody2D myBody;
    /// <summary>
    /// Posizione del nemico scelta casualmente
    /// </summary>
    private Vector2 target;

    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemySpawner>();

        myBody = gameObject.GetComponent<Rigidbody2D>();
        myBody.isKinematic = true;
        colliderComponent = gameObject.GetComponent<Collider2D>();
        colliderComponent.isTrigger = true;
        target = new Vector2((int)Random.Range(-6, 6), (int)Random.Range(1, 4));

        //transform.position = Vector3.MoveTowards(new Vector3(0,0,0),transform.position,3);
    }

    void Update()
    {
        //Vector2 target = new Vector2(Random.Range(-6, 6), Random.Range(1, 3));

        transform.position = Vector3.MoveTowards(transform.position, target, 2.5f * Time.deltaTime);
        if (transform.position.Equals(target)) canFire = true;


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
    /// Spara il proiettile
    /// </summary>
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
    /// <summary>
    /// Gestisce la collisione
    /// </summary>
    /// <param name="other">oggetto in collisione</param>
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
    /// <summary>
    /// Distrugge il nemico
    /// </summary>
    private void Kill()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Lancia Enemy verso l'alto con una direzione diagonale casuale,
    /// segnala allo spawner la morte e infine distrugge se stesso.
    /// </summary>    
    private void DeathAnimation()
    {
        canFire = false;
        spawner.EnemyKilled();

        // Sorteggia un numero da 1 a 100.
        // Se il numero e' inferiore a 20 (1 possiblita' su 5) viene rilasciato un power-up 
        int number = (int)Random.Range(1, 100);

        if (number <= 20)
            Instantiate(powerups[Random.Range(0, powerups.Length)], transform.position, transform.rotation);

        colliderComponent.isTrigger = false;
        myBody.isKinematic = false;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 250), 500));
        Invoke(nameof(Kill), 0.5f);
    }
}