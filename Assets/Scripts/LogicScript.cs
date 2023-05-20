using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    public AudioSource scoreEffect;

    public int speed = 1;
    public int maxSpeed = 5;
    public int minSpeed = 1;

    [ContextMenu("Increase Score")]
    public void AddScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
        scoreEffect.Play();
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
        if (input == 1 && speed<maxSpeed)
        {
            speed++;
            Debug.Log(speed);            
        }

        else if (input == 0 && speed>minSpeed)
        {
            speed--;
            Debug.Log(speed);            
        }
    }
}
