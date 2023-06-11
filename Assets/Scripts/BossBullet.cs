using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 4f;
    // Indica l'inclinazione diagonale (Positiva se verso sinistra, negativa verso destra)
    public float direction = 0;

    // public float deadZone = -6.5f;

    public void Update()
    {
        // Debug.Log("Mi sto muovendo");

        transform.position += (speed * Time.deltaTime * Vector3.down) + (direction * speed * Time.deltaTime * Vector3.left);

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.x < min.x || transform.position.x > max.x || transform.position.y < min.y || transform.position.y > max.y)
        {
            // Debug.Log("Bullet boss distrutto");
            Destroy(gameObject);
        }



    }
}
