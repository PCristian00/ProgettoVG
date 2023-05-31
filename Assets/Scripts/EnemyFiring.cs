using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    public GameObject Bullet;
    private float timer = 0;
    private float spawnRate = 2;

    // Start is called before the first frame update
    void Start()
    {
        //spara dopo due secondi
        Invoke("FireBullet", 2f);
    }

    // Update is called once per frame
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
            //calcoliamo la direzione verso il player
            Vector2 direction = playerShip.transform.position - bullet.transform.position;
            //impostiamo la direzione del proiettile
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);

        }
    }
}
