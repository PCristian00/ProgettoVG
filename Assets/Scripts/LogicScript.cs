using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// LogicScript contiene varie funzioni collegate alla logica di gioco, tra cui la gestione della velocita',
/// dei punteggi, della musica e delle notifiche
/// </summary>

public class LogicScript : MonoBehaviour
{
    /// <summary>
    /// Punteggio del giocatore
    /// </summary>
    public int playerScore;
    /// <summary>
    /// Punteggio migliore di sempre 
    /// </summary>
    public static int highScore;
    /// <summary>
    /// Velocita' di gioco, inversamente proporzionale 
    /// </summary>
    public float speed = 7f;
    /// <summary>
    /// Velocita' massima del gioco
    /// </summary>
    private float maxSpeed;
    /// <summary>
    /// Velocita' minima del gioco
    /// </summary>
    private float minSpeed;
    /// <summary>
    /// Moltiplicatore del punteggio
    /// </summary>
    public int scoreMultiplier = 1;
    /// <summary>
    /// Livello di velocita', usato per comodita' negli array di ChangeSpeed
    /// </summary>
    private int speedLevel = 0;
    /// <summary>
    /// Contiene il conteggio dei punti raggiunti
    /// </summary>
    public TextMeshProUGUI scoreText;
    /// <summary>
    /// Contiene il conteggio del punteggio piu' alto mai raggiunto
    /// </summary>
    public TextMeshProUGUI highScoreText;

    /// <summary>
    /// Contiene le varie tracce audio che si aggiungono alla prima aumentando la velocita'
    /// </summary>
    public AudioSource[] musicLayers;

    /// <summary>
    /// Barra della velocita'
    /// </summary>
    public GameObject speedBar;
    /// <summary>
    /// Contiene gli sprite che la barra della velocita' puo' mostrare
    /// </summary>
    public Sprite[] speedSprites;
    /// <summary>
    /// Riferimento a Image di SpeedBar, serve per modificare sprite
    /// </summary>
    private Image speedImage;

    /// <summary>
    /// Casella di testo usata per notifiche e messaggi
    /// </summary>
    public TextMeshProUGUI message;


    /// <summary>
    /// Indica se la partita e' finita
    /// </summary>
    public bool gameIsOver = false;

    private void Start()
    {
        playerScore = 0;
        // Preleva i punteggi dai salvataggi di Unity
        // Il punteggio precedente viene prelevato solo se il gioco e' finito       
        if (gameIsOver)
        {
            playerScore = PlayerPrefs.GetInt("score", playerScore);
            scoreText.text = playerScore.ToString();
        }

        highScore = PlayerPrefs.GetInt("highscore", highScore);
        highScoreText.text = highScore.ToString();

        speed = 7f;
        maxSpeed = 2f;
        minSpeed = speed;
        // La barra di velocita' viene caricata solo se il gioco non e' finito
        if (!gameIsOver)
            speedImage = speedBar.GetComponent<Image>();
    }

    /// <summary>
    /// Aggiunge n punti, eventualmente moltiplicati 
    /// </summary>
    /// <param name="scoreToAdd"> Punti da aggiungere</param>
    public void AddScore(int scoreToAdd)
    {

        if (!gameIsOver)
        {
            playerScore += (scoreToAdd * scoreMultiplier);
            scoreText.text = playerScore.ToString();
        }
    }

    /// <summary>
    /// Ritorna al menu principale
    /// </summary>
    public void HowtoPlayExitButton()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Carica la schermata del game over e, eventualmente, sovrascrive high score
    /// </summary>
    public void GameOver()
    {
        PlayerPrefs.SetInt("score", playerScore);
        if (playerScore > highScore)
        {
            highScore = playerScore;
            highScoreText.text = playerScore.ToString();
            PlayerPrefs.SetInt("highscore", highScore);
        }
        SceneManager.LoadScene(3);
    }

    /// <summary>
    /// Aumento e diminuzione della velocita', se consentito.
    /// Aggiorna inoltre la visualizzazione della barra della velocita'.
    /// </summary>
    /// <param name="input"> Indica se aumentare (se ==1) o diminuire (se ==-1)</param>
    public void ChangeSpeed(float input)
    {
        // Debug.Log("Changing speed");

        // Aumento di velocita'
        if (input == 1f)
        {
            if (speed > maxSpeed)
            {
                speed--;
                speedLevel++;
                 // Attiva il layer attuale
                musicLayers[speedLevel].mute = false;
                // Diminuisce il volume del layer inferiore
                musicLayers[speedLevel - 1].volume -= 0.2f;
            }
            else ShowMessage("VELOCITA' MASSIMA", 1);
        }
        // Diminuzione di velocita'
        else if (input == -1f)
        {
            if (speed < minSpeed)
            {
                speed++;
                // Spegne il layer attuale
                musicLayers[speedLevel].mute = true;
                // Aumenta il volume del layer inferiore
                musicLayers[speedLevel - 1].volume += 0.2f;
                speedLevel--;
            }
            else ShowMessage("VELOCITA' MINIMA", 1);
        }

        // Debug.Log("VELOCITA': " + speedLevel + " / 5");

        speedImage.sprite = speedSprites[speedLevel];
    }

    /// <summary>
    ///  Aumenta la velocita' minima raggiungibile in gioco.  
    /// </summary>

    public void IncreaseDifficulty()
    {
        // Se il giocatore e' attualmente alla velocita' minima, la sua velocita' viene aumentata
        if (speed == minSpeed)
            ChangeSpeed(1);

        // Aumento velocita' minima
        if (minSpeed > maxSpeed)
        {
            // Mostra per 1 secondo il messaggio a schermo
            ShowMessage("PIU' VELOCE!!!", 1);
            minSpeed--;
            
        }
    }

    /// <summary>
    /// Mostra il testo scelto per il tempo scelto.
    /// Se il tempo viene impostato a 0 la disattivazione automatica non viene impostata.
    /// Il messaggio viene mostrato a schermo in alto al centro.
    /// </summary>
    /// <param name="messageText"> Messaggio da mostrare</param>
    /// <param name="time">Tempo di visualizzazione (0 se infinito)</param> 
    public void ShowMessage(string messageText, int time)
    {
        message.text = messageText;
        message.gameObject.SetActive(true);
        if (time != 0)
            Invoke(nameof(ToggleMessage), time);
    }

    /// <summary>
    /// Disattiva il messaggio precedentemente attivato.
    /// </summary>
    public void ToggleMessage()
    {
        message.gameObject.SetActive(false);
    }
}
