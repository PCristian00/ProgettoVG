using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject star;
    public float spawnRate = 4;

    private float timer = 0;

    public int starCount = 0;

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
