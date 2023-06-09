using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public EnemySpawner spawner;
    private LogicScript logic;

    public AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemySpawner>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        // Se entra in collisione con uno ShipBullet dello stesso colore (layer) di questo Enemy
        if (other.gameObject.layer == this.gameObject.layer)
        {
            // NON SUONA, forse perché l'oggetto viene distrutto subito dopo
            deathSound.Play();

            Debug.Log("Suono suonato");
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
