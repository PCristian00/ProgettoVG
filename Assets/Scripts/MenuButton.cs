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
        if (collision.gameObject.layer == 7)
        {
            spriteRenderer.color = new Color(100, 0, 0);
            // text.text = "Rosso";
        }

        if (collision.gameObject.layer == 12)
        {
            spriteRenderer.color = new Color(0, 100, 0);
            // text.text = "Verde";
        }

        if (collision.gameObject.layer == 13)
        {
            spriteRenderer.color = new Color(0, 0, 100);
            // text.text = "Blu";
        }
        // Debug.Log("Selezione");

        if (sceneToLoad != -1)
            SceneManager.LoadScene(sceneToLoad);
        else
            Application.Quit();
        
        Destroy(collision.gameObject);
    }


}
