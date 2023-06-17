using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScript : MonoBehaviour
{
    bool avvia = false;
    float speed = 10f;
    public Vector3 movimento;
     void Update()
    {
        if (avvia)
        Move();

        if (Input.GetButtonDown("FireGreen") || Input.GetButtonDown("FireRed") || Input.GetButtonDown("FireYellow")) ButtonClick();
    }
    public void ButtonClick()
    {
        avvia = true;
    }
    
    public void Move()
   {
        transform.position += speed * Time.deltaTime * Vector3.left;
    }
    public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 14)
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	
}
