using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject star;
    //public float spawnRate = 5;

    public LogicScript logic;

    private float timer = 0;

    public int starCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 2)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if(starCount<5)
            SpawnStars();

            timer = 0;
        }
    }

    void SpawnStars()
    {
        // Il range (-6,6) indica l'area delle corsie


        Instantiate(star, new Vector3(Random.Range(-6, 6), transform.position.y, 0), transform.rotation);
        starCount++;

    }
}
