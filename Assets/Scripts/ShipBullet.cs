using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBullet : MonoBehaviour
{
    public float bulletSpeed = 8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos.y += bulletSpeed * Time.deltaTime;

        //bullet.GetComponent<EnemyBullet>().SetDirection(direction);

        transform.position = pos;
    }
}
