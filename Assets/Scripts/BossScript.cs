using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public EnemySpawner spawner;
    public int boss_life = 3;
    // public bool isAlive = true;
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
        if (other.gameObject.layer == 7 || other.gameObject.layer == 12 || other.gameObject.layer == 13)
        {
            Debug.Log("Colpito!!!");
            Debug.Log("Vita rimanente: " + boss_life + " / 3");
            boss_life--;
            Destroy(other.gameObject);
            // Distrugge se stesso e ShipBullet
            if (boss_life == 0)
            {
                
                Debug.Log(" BOSS MORTO");
                
                Destroy(other.gameObject);
                //isAlive = false;
                spawner.countEnemyKill = 0;
                spawner.bossIsAlive = false;
                logic.AddScore(10);
                logic.CheckDifficulty(true);
                Destroy(gameObject);
            }


        }
    }
}

    
    //public void OnTriggerEnter2D(Collider2D other)
    //{
    //    // Se entra in collisione con uno ShipBullet dello stesso colore (layer) di questo Enemy
    //    if (other.gameObject.layer == 7 || other.gameObject.layer==12 || other.gameObject.layer==13)
    //    {
    //        Debug.Log("Colpito");
    //        Destroy(gameObject);
    //        Destroy(other.gameObject);
    //        isAlive = false;
    //        count.countEnemyKill = 0;
    //        // Distrugge se stesso e ShipBullet
    //        /*boss_life--;
    //        Destroy(other.gameObject);

    //        logic.AddScore(1);
    //        if (boss_life== 0)
    //        {
    //            Destroy(gameObject);
    //            isAlive = false;
    //            count.countEnemyKill = 0;
    //        }*/
    //    }
    //}


