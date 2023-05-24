using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    // Punteggio del giocatore
    public int playerScore;
    // Casella di testo che mostra il punteggio attuale
    public TextMeshProUGUI scoreText;
    // Casella di testo che mostra il punteggio migliore di sempre
    public TextMeshProUGUI highScoreText;
    // Schermata da caricare quando le vite finiscono
    public GameObject gameOverScreen;
    // Suono da attivare per ogni punto ottenuto
    public AudioSource scoreEffect;
    // Musica di sottofondo
    public AudioSource backGroundMusic;

    // Velocita' di gioco (tasso di spawn ostacoli)
    public float speed = 0f;
    public float maxSpeed = 2f;
    public float minSpeed = 0f;
    // Punteggio migliore di sempre
    public static int highScore;

    private void Start()
    {
        playerScore = 0;
        // Preleva il punteggio migliore dai salvataggi di Unity
        highScore = PlayerPrefs.GetInt("highscore", highScore);
        highScoreText.text = highScore.ToString();
    }

    [ContextMenu("Increase Score")]
    public void AddScore(int scoreToAdd)
    {
        if (!gameOverScreen.activeSelf)
        {
            playerScore += scoreToAdd;
            scoreText.text = playerScore.ToString();
            CheckDifficulty();
            // scoreEffect.Play();
        }
    }

    public void RestartGame()
    {
        // Svuota il DebugLog prima di riavviare la scena
        // Forse inutile in gioco finale
        ClearLog();
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

    public void ChangeSpeed(float input)
    {
        if (input == 1f && speed < maxSpeed)
        {
            speed += 0.25f;
            Debug.Log("VELOCITA': " + speed + " / " + maxSpeed);
        }

        else if (input == -1f && speed > minSpeed)
        {
            speed -= 0.25f;
            Debug.Log("VELOCITA': " + speed + " / " + maxSpeed);
        }
    }

    public void CheckDifficulty()
    {
        // Se il punteggio e' multiplo di 10
        if (playerScore % 10 == 0)
        {
            // Aumento velocita'
            ChangeSpeed(1);
            // Aumento velocita' minima
            if (minSpeed < maxSpeed)
            {
                minSpeed += 0.25f;
                Debug.Log("NUOVA VELOCITA' MINIMA: " + minSpeed);
            }
        }
    }

    // Svuota il DebugLog
    //ATTENZIONE! CAUSA ERRORI DURANTE BUILD
    //INSERIRE IN COMMENTO PRIMA DI BUILD

    public void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}
