using UnityEngine;

public class BossScript : MonoBehaviour
{
    public EnemySpawner spawner;
    public int boss_life = 3;
    public LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        boss_life = 3;
        spawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemySpawner>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        logic.ShowMessage("ATTENZIONE!!!", 1);
    }

    // Update is called once per frame
    void Update()
    {

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

                    logic.AddScore(10);
                    logic.CheckDifficulty(true);

                    Destroy(gameObject);
                }
            }    
        }
    }
}