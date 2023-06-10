using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public int scoreMultiplier = 1;

    // Contatore di velocita' (TROVARE MODO DI RIMUOVERE)
    private int speedLevel = 0;
    // Casella di testo che mostra il punteggio attuale
    public TextMeshProUGUI scoreText;
    // Casella di testo che mostra il punteggio migliore di sempre
    public TextMeshProUGUI highScoreText;

    // Schermata da caricare quando le vite finiscono (INUTILE CON SCENA GAMEOVER)
    public GameObject gameOverScreen;
    // Suono da attivare per ogni punto ottenuto
    public AudioSource scoreEffect;
    // Strati musicali, regolati da velocita'
    public AudioSource[] musicLayers;

    // Riferimento alla barra della velocita'
    public GameObject speedBar;
    // Array contenente i vari sprite della barra della velocita'
    public Sprite[] speedSprites;
    //Riferimento a Image della barra della velocita'
    private Image speedImage;

    // Messaggio di testo
    public TextMeshProUGUI message;

    private void Start()
    {
        playerScore = 0;
        // Preleva il punteggio migliore dai salvataggi di Unity
        highScore = PlayerPrefs.GetInt("highscore", highScore);
        highScoreText.text = highScore.ToString();

        speed = 7f;
        maxSpeed = 2f;
        minSpeed = speed;
        speedImage = speedBar.GetComponent<Image>();
    }

    
    public void AddScore(int scoreToAdd)
    {
        if (!gameOverScreen.activeSelf)
        {
            playerScore += (scoreToAdd*scoreMultiplier);
            scoreText.text = playerScore.ToString();
            CheckDifficulty(false);
            // scoreEffect.Play();
        }
    }

    public void RestartGame()
    {
        // Svuota il DebugLog prima di riavviare la scena
        // Forse inutile in gioco finale
        // ClearLog();
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        // Le parti ora commentate servivano PRIMA dell'introduzione della scena.
        // Rimuovere quando la scena del Game Over e' completa al 100%.

        // speedBar.SetActive(false);        
        // gameOverScreen.SetActive(true);

        // Debug.Log("Partita finita con un punteggio di " + playerScore);
        if (playerScore > highScore)
        {
            // Debug.Log("NUOVO RECORD");

            // highScore = playerScore;
           //  highScoreText.text = playerScore.ToString();
            PlayerPrefs.SetInt("highscore", highScore);
            // Debug.Log(highScore);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

        speedImage.sprite = speedSprites[speedLevel];
    }

    // Controlla il punteggio e regola la difficolta' (velocita') di conseguenza

    // PROMEMORIA
    // Il boss aggiunge 10 punti di colpo, quindi supera una fascia di difficolta' ma non la aumenta (Es. 12 non divisible per 10)
    // All'uccisione del boss viene passato true e viene ignorato il controllo sui multipli di 10.
    // Forse va trovata una soluzione migliore (funzione specifica per l'uccisione del boss (addScore personalizzato?) 
    public void CheckDifficulty(bool BossKilled)
    {
        // Se il punteggio e' multiplo di 10
        if (playerScore % 10 == 0 || BossKilled)
        {
            // Aumento velocita'
            ChangeSpeed(1);
            // Aumento velocita' minima
            if (minSpeed > maxSpeed)
            {
                // Mostra per 1 secondo il messaggio a schermo (sotto le barre)
                // Forse da sostituire con una soluzione migliore

                message.gameObject.SetActive(true);
                Invoke(nameof(ToggleMessage), 1);
                minSpeed--;
                Debug.Log("NUOVA VELOCITA' MINIMA: " + (7 - minSpeed));
            }
        }
    }

    // Rende nuovamente invisibile il messaggio
    private void ToggleMessage()
    {
        message.gameObject.SetActive(false);
    }

    // Svuota il DebugLog
    // ATTENZIONE! CAUSA ERRORI DURANTE BUILD
    // INSERIRE IN COMMENTO PRIMA DI BUILD

    
    //public void ClearLog()
    //{
    //    var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
    //    var type = assembly.GetType("UnityEditor.LogEntries");
    //    var method = type.GetMethod("Clear");
    //    method.Invoke(new object(), null);
    //}

 
}
