using UnityEngine;
/// <summary>
/// Spawner delle stelle di sfondo.
/// </summary>
public class StarSpawner : MonoBehaviour
{
    /// <summary>
    /// Stelle da spawnare
    /// </summary>
    public GameObject star;
    /// <summary>
    /// Frequenza di spawn, in secondi
    /// </summary>
    public float spawnRate = 4;
    /// <summary>
    /// timer
    /// </summary>
    private float timer = 0;
    /// <summary>
    /// Conteggio delle stelle attualmente su schermo
    /// </summary>
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
    /// <summary>
    /// Instanzia un nuovo set di stelle e incrementa il contatore.
    /// </summary>
    void SpawnStars()
    {
        Instantiate(star, transform.position, transform.rotation);
        starCount++;
    }
}
