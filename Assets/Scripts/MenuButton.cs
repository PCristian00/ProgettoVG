using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    public TextMeshProUGUI text;

    public int sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
