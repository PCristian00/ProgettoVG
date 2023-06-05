using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    // Punteggio del giocatore
    public int playerScore;
    // Punteggio migliore di sempre
    public static int highScore;
    // Velocita' di gioco (tasso di spawn ostacoli), inversamente proporzionale
    public float speed = 7f;
    private float maxSpeed;
    private float minSpeed;
    // Contatore di velocita' (TROVARE MODO DI RIMUOVERE)
    private int speedLevel = 0;
    // Casella di testo che mostra il punteggio attuale
    public TextMeshProUGUI scoreText;
    // Casella di testo che mostra il punteggio migliore di sempre
    public TextMeshProUGUI highScoreText;
    // Casella di testo che mostra la velocita' attuale
    public TextMeshProUGUI speedText;
    // Schermata da caricare quando le vite finiscono
    public GameObject gameOverScreen;
    // Suono da attivare per ogni punto ottenuto
    public AudioSource scoreEffect;
    // Strati musicali, regolati da velocita'
    public AudioSource[] musicLayers;

    // Riferimento alla barra della velocita'
    public GameObject speedBar;
    // Riferimento allo spriteRenderer della barra della velocita'
    private SpriteRenderer speedSpriteRenderer;
    // Array contenente i vari sprite della barra della velocita'
    public Sprite[] speedSprites;

    private void Start()
    {
        playerScore = 0;
        // Preleva il punteggio migliore dai salvataggi di Unity
        highScore = PlayerPrefs.GetInt("highscore", highScore);
        highScoreText.text = highScore.ToString();

        speed = 7f;
        maxSpeed = 2f;
        minSpeed = speed;

        speedSpriteRenderer = speedBar.GetComponent<SpriteRenderer>();
    }

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
        // Debug.Log("Changing speed");

        // Aumento di velocita'
        if (input == 1f && speed > maxSpeed)
        {
            Debug.Log("Velocita' in aumento");
            speed--;
            speedLevel++;
            Debug.Log("Nuova traccia musicale");
            // Attiva il layer attuale
            musicLayers[speedLevel].mute = false;
            // Diminuisce il volume del layer inferiore
            musicLayers[speedLevel - 1].volume -= 0.2f;

        }
        // Diminuzione di velocita'
        else if (input == -1f && speed < minSpeed)
        {
            Debug.Log("Velocita' in diminuzione");
            speed++;
            Debug.Log("Traccia musicale rimossa");
            // Spegne il layer attuale
            musicLayers[speedLevel].mute = true;
            // Aumenta il volume del layer inferiore
            musicLayers[speedLevel - 1].volume += 0.2f;
            speedLevel--;
        }

        Debug.Log("VELOCITA': " + speedLevel + " / 5");
        speedText.text = speedLevel.ToString();
        speedSpriteRenderer.sprite = speedSprites[speedLevel];
    }

    // Controlla il punteggio e regola la difficolta' (velocita') di conseguenza
    public void CheckDifficulty()
    {
        // Se il punteggio e' multiplo di 10
        if (playerScore % 10 == 0)
        {
            // Aumento velocita'
            ChangeSpeed(1);
            // Aumento velocita' minima
            if (minSpeed > maxSpeed)
            {
                minSpeed--;
                Debug.Log("NUOVA VELOCITA' MINIMA: " + (7 - minSpeed));
            }
        }
    }

    // Svuota il DebugLog
    // ATTENZIONE! CAUSA ERRORI DURANTE BUILD
    // INSERIRE IN COMMENTO PRIMA DI BUILD

    public void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}
