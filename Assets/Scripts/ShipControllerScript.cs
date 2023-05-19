using UnityEngine;

public class ShipControllerScript : MonoBehaviour
{

   //Posizione dopo spostamento
    private Vector3 endPos = new(0, -4, 0);

    //Quantita di spostamento.
    //La corsia ha 6 di larghezza, NON cambiare.
    public int deltaX = 6;
    public float speed=10f;

    // Update is called once per frame
    void Update()
    {
        //Spostamento verso destra
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Si sposta solo se non si trova già al bordo
            if (transform.position.x < 6)
            {
                //ChangeLane(deltaX);
                Move(speed);
            }       
        }

        //Spostamento verso sinistra
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Si sposta solo se non si trova già al bordo
            if (transform.position.x > -6)
            {
                //ChangeLane(-deltaX);

                Move(-speed);
            }
        }
    }

    /*
     * Cambio corsia (sinistra e destra) fissato a centro colonna
     * 
     void ChangeLane(int deltaX)
     {
         endPos = new Vector3(transform.position.x + deltaX, transform.position.y, 0);
         gameObject.transform.position = endPos;        
     }
    
     */

    //Movimento libero fluido
    private void Move(float speed)
    {
        endPos.x += speed * Time.deltaTime;
        gameObject.transform.position = endPos;
    }
}
