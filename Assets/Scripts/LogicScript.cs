using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverScreen;
    public AudioSource scoreEffect;
    public AudioSource backGroundMusic;

    public float speed = 0f;
    public float maxSpeed = 2f;
    public float minSpeed = 0f;
    public static int highScore;

    private void Start()
    {
        //highScoreText = GetComponent<TextMeshProUGUI>();

        playerScore = 0;

        highScore = PlayerPrefs.GetInt("highscore", highScore);
        highScoreText.text = highScore.ToString();
    }

    //ANCORA NON IMPLEMENTATA
    [ContextMenu("Increase Score")]
    public void AddScore(int scoreToAdd)
    {
        if (!gameOverScreen.activeSelf)
        {
            playerScore += scoreToAdd;
            scoreText.text = playerScore.ToString();
            CheckDifficulty();
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

        Debug.Log("Partita finita con un punteggio di " + playerScore);
        if (playerScore > highScore)
        {
            Debug.Log("NUOVO RECORD");
            highScore = playerScore;
            highScoreText.text = playerScore.ToString();
            PlayerPrefs.SetInt("highscore", highScore);
            Debug.Log(highScore);
        }
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

    public void CheckDifficulty()
    {
        //Se il punteggio e' multiplo di 10
        if (playerScore % 10 == 0)
        {
            //Aumento velocita'
            ChangeSpeed(1);
            //Aumento velocita' minima
            if (minSpeed < maxSpeed)
            {
                minSpeed += 0.25f;
                Debug.Log("NUOVA VELOCITA' MINIMA: " + minSpeed);
            }
        }
    }
}
