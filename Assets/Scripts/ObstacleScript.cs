using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    
    // Dimensione a distanza massima
    public float scale=0.5f;

    //NON UTILIZZATE
    //private Vector3 endPos;
    //private float deltaY;

    //Velocità movimento
    public int moveSpeed=1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Slide();
    }

    void Slide()
    {
        // Ingrandimento dell'ostacolo (Effetto di avvicinamento)
        // DA OTTIMIZZARE / MODIFICARE. STRANO COMPORTAMENTO LAMPEGGIANTE
        scale+=0.5f;
        gameObject.transform.localScale =new Vector3 (scale,scale,scale)*moveSpeed*Time.deltaTime;
        
        //SPOSTAMENTO NON USATO, RIMUOVERE?
        //endPos = new Vector3(transform.position.x, transform.position.y+deltaY, 0);
        //gameObject.transform.position = endPos;

        // Spostamento dell'ostacolo verso il giocatore
        transform.position = transform.position + (Vector3.down * moveSpeed * Time.deltaTime);
    }
}
