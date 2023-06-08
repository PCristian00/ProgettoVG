using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // public GameObject spawner;
    public EnemySpawner spawner;
    private LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        //spawner = gameObject.GetComponent<EnemySpawner>();
        spawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemySpawner>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Se entra in collisione con uno ShipBullet
        if (other.gameObject.layer == this.gameObject.layer)
        {
            // Distrugge se stesso e ShipBullet
            Destroy(gameObject);
            Destroy(other.gameObject);

            logic.AddScore(1);
           if (spawner.countSpawn > 0)
            {
                spawner.countSpawn--;
                Debug.Log("Nemici in gioco: " + spawner.countSpawn);
            }            
        }
    }
}
