using UnityEngine;

public class ShipBullet : MonoBehaviour
{
    // Velocita' del proiettile
    public float bulletSpeed = 8f;
    // Zona di despawn
    public float deadZone = 5.5f;

    public SpriteRenderer spriteRenderer;

    public Sprite redBullet;
    public Sprite greenBullet;
    public Sprite yellowBullet;

    // Update is called once per frame
    void Update()
    {

        LoadSprite();
        //Vector2 pos = transform.position;
        //pos.y += bulletSpeed * Time.deltaTime;
        //transform.position = pos;

        transform.position += bulletSpeed * Time.deltaTime * Vector3.up;

        // Despawn del proiettile
        if (transform.position.y > deadZone)
            Destroy(gameObject);
    }

    // SOLO PER PROVA
    // Sostituire sprite invece di colorare e basta per un effetto migliore
    void LoadSprite()
    {
        // colora di rosso
        if (gameObject.layer == 7) spriteRenderer.sprite = redBullet;
        // colora di verde
        else if (gameObject.layer == 12) spriteRenderer.sprite = greenBullet;
        // colora di giallo
        else spriteRenderer.sprite = yellowBullet;
    }
}
