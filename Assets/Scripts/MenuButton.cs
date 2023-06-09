using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    public TextMeshProUGUI text;
    
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
            text.text = "Rosso";
        }

        if (collision.gameObject.layer == 12)
        {
            spriteRenderer.color = new Color(0, 100, 0);
            text.text = "Verde";
        }

        if (collision.gameObject.layer == 13)
        {
            spriteRenderer.color = new Color(0, 0, 100);
            text.text = "Blu";
        }
        Debug.Log("Selezione");
        
        Destroy(collision.gameObject);
    }


}
