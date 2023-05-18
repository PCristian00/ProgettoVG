using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //NON UTILIZZATE
    //private Vector3 endPos;
    //private float deltaY;
    //public float scale = 0.5f;

    //Velocità di movimento
    public int moveSpeed=1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Slide();
        transform.position += moveSpeed * Time.deltaTime * Vector3.down;
    }

    
    //OBSOLETA. LE FUNZIONALITA DI INGRANDIMENTO SONO GESTITE DA DISTANCESCRIPT

    //void Slide()
    //{
        // Ingrandimento dell'ostacolo (Effetto di avvicinamento)
        // DA OTTIMIZZARE / MODIFICARE. STRANO COMPORTAMENTO LAMPEGGIANTE
        //if (scale <= 2)
        //{
            //scale += 0.5f;
            // Vale 0.02 sul mio portatile
            //Debug.Log(Time.deltaTime);

         //   float scaleRate =scale*(moveSpeed * Time.deltaTime);

          //  gameObject.transform.localScale = new Vector3(scaleRate, scaleRate, scaleRate);
        //}
        

        // Spostamento dell'ostacolo verso il giocatore
        //transform.position += (Vector3.down * moveSpeed * Time.deltaTime);
 //   }
}
