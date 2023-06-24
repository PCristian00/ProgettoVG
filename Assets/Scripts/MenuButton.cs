using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gestisce i pulsanti da sparare usati nei menu
/// </summary>
public class MenuButton : MonoBehaviour
{
    /// <summary>
    /// Riferimento allo sprite renderer
    /// </summary>
    public SpriteRenderer spriteRenderer;
    /// <summary>
    /// Scena da caricare una volta sparato
    /// </summary>
    public int sceneToLoad;
    /// <summary>
    /// Gestione della collsione con il proiettive e caricamento della scena.
    /// </summary>
    /// <param name="collision">oggetto in collisione</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Scurisce leggermente il colore del pulsante sparato
        spriteRenderer.color *= 0.6f;

        if (sceneToLoad != -1)
            SceneManager.LoadScene(sceneToLoad);
        else
            Application.Quit();

        Destroy(collision.gameObject);
    }


}
