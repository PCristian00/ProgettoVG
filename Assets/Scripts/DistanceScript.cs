using UnityEngine;

public class DistanceScript : MonoBehaviour
{
    // Tasso di ingrandimento
    public float scaleRate = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            // Appena un oggetto Obstacle raggiunge la linea, la sua dimensione
            // aumenta
            collision.gameObject.transform.localScale +=
                new Vector3(scaleRate, scaleRate, 0);
        }
    }
}
