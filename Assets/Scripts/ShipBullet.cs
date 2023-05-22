using UnityEngine;

public class ShipBullet : MonoBehaviour
{
    //Velocita' del proiettile
    public float bulletSpeed = 8f;
    //Zona di despawn
    public float deadZone = 5.5f;

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos.y += bulletSpeed * Time.deltaTime;
        transform.position = pos;

        //Despawn del proiettile
        if (transform.position.y >deadZone)
            Destroy(gameObject);
    }
}
