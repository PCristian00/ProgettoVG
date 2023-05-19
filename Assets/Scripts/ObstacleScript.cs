using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //Velocità di movimento
    public int moveSpeed=1;
    //Zona di despawn
    public int deadZone = -8;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.down;

        if (transform.position.y <= deadZone)
        {
            //Debug.Log("Ostacolo distrutto");
            Destroy(gameObject);
        }
    }
}
