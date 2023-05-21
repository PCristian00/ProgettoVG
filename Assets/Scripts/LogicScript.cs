using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    public AudioSource scoreEffect;

    public float speed = 0;
    public float maxSpeed = 2f;
    public float minSpeed = 0f;

    //ANCORA NON IMPLEMENTATA
    [ContextMenu("Increase Score")]
    public void AddScore(int scoreToAdd)
    {
        if (!gameOverScreen.activeSelf)
        {
            playerScore += scoreToAdd;
            scoreText.text = playerScore.ToString();
            //scoreEffect.Play();
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void ChangeSpeed(int input)
    {
        if (input == 1 && speed < maxSpeed)
        {
            speed += 0.25f;
            Debug.Log("VELOCITA': " + speed + " / " + maxSpeed);
        }

        else if (input == 0 && speed > minSpeed)
        {
            speed -= 0.25f;
            Debug.Log("VELOCITA': " + speed + " / " + maxSpeed);
        }
    }
}
