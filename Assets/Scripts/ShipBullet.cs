using UnityEngine;

public class ShipBullet : MonoBehaviour
{
    // Velocita' del proiettile
    public float bulletSpeed = 8f;
    // Zona di despawn
    public float deadZone = 5.5f;

    public SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void Update()
    {

        colorSprite();
        Vector2 pos = transform.position;
        pos.y += bulletSpeed * Time.deltaTime;
        transform.position = pos;

        // Despawn del proiettile
        if (transform.position.y > deadZone)
            Destroy(gameObject);
    }

    void colorSprite()
    {
        if (gameObject.layer == 7) spriteRenderer.color = new Color(100, 0, 0);
        else if (gameObject.layer == 12) spriteRenderer.color = new Color(0, 100, 0);
        else spriteRenderer.color = new Color(0, 0, 100);
    }
}
