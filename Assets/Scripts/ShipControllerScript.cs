using UnityEngine;

public class ShipControllerScript : MonoBehaviour
{

   //Posizione dopo spostamento
    private Vector3 endPos = new(0, -4, 0);

    //Quantita di spostamento.
    //La corsia ha 6 di larghezza, NON cambiare.
    //public int deltaX = 6;

    //Velocita di spostamento laterale
    public float speed=10f;

    // Update is called once per frame
    void Update()
    {
        //Spostamento verso destra
        if (Input.GetKey(KeyCode.RightArrow))
        {
           Move(speed);       
        }

        //Spostamento verso sinistra
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(-speed);
        }

        if (Input.GetKey(KeyCode.Space))
        {
         Shoot();
        }
    }

    /*
     * Cambio corsia (sinistra e destra) fissato a centro colonna
     * 
     void ChangeLane(int deltaX)
     {
         endPos = new Vector3(transform.position.x + deltaX, transform.position.y, 0);
         endPos.x = Mathf.Clamp(endPos.x,-6,6);
         gameObject.transform.position = endPos;        
     }
    
     */

    //Movimento libero fluido
    private void Move(float speed)
    {
        endPos.x += speed * Time.deltaTime;
        endPos.x = Mathf.Clamp(endPos.x,-6,6);
        gameObject.transform.position = endPos;
    }

    private void Shoot()
    {
        Debug.Log("ANCORA NIENTE ARMI");
    }
}
