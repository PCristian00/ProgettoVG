using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // public GameObject spawner;
    public EnemySpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        //spawner = gameObject.GetComponent<EnemySpawner>();
        spawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemySpawner>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Se entra in collisione con uno ShipBullet
        if (other.gameObject.layer == 6)
        {
            // Distrugge se stesso e ShipBullet
            Destroy(gameObject);
            Destroy(other.gameObject);
            if (spawner.countSpawn > 0)
            {
                spawner.countSpawn--;
                Debug.Log("Nemici in gioco: " + spawner.countSpawn);
            }            
        }
    }
}
