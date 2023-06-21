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
        if (timer < 1.5f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (starCount < 4)
                SpawnStars();

            timer = 0;
        }
    }

    void SpawnStars()
    {
        Instantiate(star, transform.position, transform.rotation);
        starCount++;
    }
}
