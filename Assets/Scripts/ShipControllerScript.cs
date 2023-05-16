using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControllerScript : MonoBehaviour
{
    //Posizione dopo spostamento
    private int endXPos = 0;

    //Rotazione dello sprite.
    //Vedere come rimuovere senza fare danni.
    private Quaternion rotation;
    
    //Quantita di spostamento.
    //La corsia ha 6 di larghezza, NON cambiare.
    public int deltaX = 6;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Spostamento verso destra
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Si sposta solo se non si trova già al bordo
            if (transform.position.x < 6)
            {
                ChangeLane(deltaX);
            }       
        }

        //Spostamento verso sinistra
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Si sposta solo se non si trova già al bordo
            if (transform.position.x > -6)
            {
                ChangeLane(-deltaX);
            }
        }
    }

    //Cambio corsia (sinistra e destra)
    void ChangeLane(int deltaX)
    {
        endXPos = (int)(transform.position.x + deltaX);
        gameObject.transform.SetPositionAndRotation(new Vector3(endXPos, transform.position.y, 0), rotation);
    }
}
