using UnityEngine;
/// <summary>
/// Gestisce il movimento e il tipo del proiettile dell'astronave
/// </summary>
public class ShipBullet : MonoBehaviour
{
    /// <summary>
    /// Velocita' del proiettile
    /// </summary>    
    public float bulletSpeed = 8f;
    /// <summary>
    /// Zona di despawn
    /// </summary>    
    public float deadZone = 5.5f;
    /// <summary>
    /// riferimento allo spriteRenderer, necessario per cambiare lo sprite
    /// </summary>
    public SpriteRenderer spriteRenderer;
    /// <summary>
    /// Proiettile rosso, uccide nemici rossi
    /// </summary>
    public Sprite redBullet;
    /// <summary>
    /// Proiettile verde, uccide nemici verdi
    /// </summary>
    public Sprite greenBullet;
    /// <summary>
    /// Proiettile giallo, uccide nemici gialli
    /// </summary>
    public Sprite yellowBullet;

    void Update()
    {

        LoadSprite();
        transform.position += bulletSpeed * Time.deltaTime * Vector3.up;

        // Despawn del proiettile
        if (transform.position.y > deadZone)
            Destroy(gameObject);
    }

  /// <summary>
  /// Carica lo sprite corretto in base al tipo (layer) del proiettile istanziato
  /// </summary>
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
