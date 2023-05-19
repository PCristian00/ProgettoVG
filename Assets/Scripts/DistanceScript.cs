using UnityEngine;

public class DistanceScript : MonoBehaviour
{
    //Tasso di ingrandimento
    public float scaleRate=0.5f;
    
    //public GameObject obstacle;

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            //Debug.Log("TRIGGER");

            // Mostra l'ostacolo quando passa la linea. Presenta errori.
            // Per qualche motivo annulla lo scaling.
            //Forse non serve se gli oggetti spawnano direttamente all'orizzonte
            //obstacle.SetActive(true);

            // Appena un oggetto Obstacle raggiunge la linea, la sua dimensione aumenta
            collision.gameObject.transform.localScale += new Vector3(scaleRate, scaleRate, 0);
        }
    }
}
