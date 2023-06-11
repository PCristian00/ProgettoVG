using UnityEngine;

public class BossScript : MonoBehaviour
{
    public EnemySpawner spawner;
    public int boss_life = 3;
    public LogicScript logic;
    public GameObject[] powerups;
    
    private Vector2 endPos;
    public int direction;

    

    // Start is called before the first frame update
    void Start()
    {
        boss_life = 3;
        spawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemySpawner>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        logic.ShowMessage("ATTENZIONE!!!", 1);

        endPos = new Vector2(transform.position.x, transform.position.y);

        direction = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x == 6 || transform.position.x == -6)
        {
            direction *= -1;
        }
        Move(direction);
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
            // Debug.Log("Sto fermo");
            // spriteRenderer.sprite = sprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 0);
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

                Destroy(other.gameObject);
                if (boss_life == 0)
                {
                    Debug.Log("BOSS MORTO");
                    Destroy(other.gameObject);

                    spawner.countEnemyKill = 0;
                    spawner.bossIsAlive = false;

                    // Il boss rilascia un power-up casuale alla morte
                    // Euler nella rotazione serve per riportare il power-up alla rotazione nulla invece di quella del boss
                    Instantiate(powerups[Random.Range(0, powerups.Length)], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.Euler(0, 0, 0));

                    logic.AddScore(10);
                    logic.CheckDifficulty(true);

                    Destroy(gameObject);
                }
            }    
        }
    }
}