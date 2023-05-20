using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //Velocità di movimento
    public float moveSpeed=0.5f;
    //Zona di despawn
    public int deadZone = -8;

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
