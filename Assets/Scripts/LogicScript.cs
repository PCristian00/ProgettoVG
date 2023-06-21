// Reflection serve solo per ClearLog, rimuovere da versione finale
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
    
    

    // Controlla se il gioco e' finito
    public bool gameIsOver = false;

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
        
        if (!gameIsOver)
        {
            playerScore += (scoreToAdd*scoreMultiplier);
            scoreText.text = playerScore.ToString();
        }
    }

    // RIMUOVERE E COLLEGARE A MENU PER FUNZIONI CHE LA USANO
    public void HowtoPlayExitButton()
    {
        SceneManager.LoadScene(0);
    }


    // APPUNTI SU ERRORE DI SELEZIONE GAMEOVER CON MOUSE
    // Il gioco si chiude qualsiasi sia l'opzione scelta in GameOver (SOLO CON ON CLICK).
    // Il problema è presente anche nell'editor (NOTARE DEBUG)
    // Questa funzione viene richiamata anche se il pulsante quit è disattivato.
    // VIENE RICHIAMATA CON QUALSIASI CLICK, ANCHE SU SFONDO
    // Se non si riesce a risolvere prima del 26, ricopiare la scena Menu, con selezione tramite astronave
    // Verificare in versione molto precedente se il problema sia presente.
    public void QuitGame()
    {
        Debug.Log("CHIUSURA GIOCO");
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    { 
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        // Debug.Log("Partita finita con un punteggio di " + playerScore);
        if (playerScore > highScore)
        {
            // Debug.Log("NUOVO RECORD");

            highScore = playerScore;
            highScoreText.text = playerScore.ToString();
            PlayerPrefs.SetInt("highscore", highScore);
            // Debug.Log(highScore);
        }

        SceneManager.LoadScene(3);
    }

    public void ChangeSpeed(float input)
    {
        // Debug.Log("Changing speed");

        // Aumento di velocita'
        if (input == 1f)
        {
            if (speed > maxSpeed)
            {
                Debug.Log("Velocita' in aumento");
                speed--;
                speedLevel++;
                // Debug.Log("Nuova traccia musicale");

                // Attiva il layer attuale
                musicLayers[speedLevel].mute = false;
                // Diminuisce il volume del layer inferiore
                musicLayers[speedLevel - 1].volume -= 0.2f;
            }
            else ShowMessage("MAX SPEED", 1);
         }
        // Diminuzione di velocita'
        else if (input == -1f)
        {
            if (speed < minSpeed)
            {
                Debug.Log("Velocita' in diminuzione");
                speed++;
                // Debug.Log("Traccia musicale rimossa");

                // Spegne il layer attuale
                musicLayers[speedLevel].mute = true;
                // Aumenta il volume del layer inferiore
                musicLayers[speedLevel - 1].volume += 0.2f;
                speedLevel--;
            }
            else ShowMessage("MIN SPEED", 1);
        }

        // Debug.Log("VELOCITA': " + speedLevel + " / 5");

        speedImage.sprite = speedSprites[speedLevel];
    }

    // Aumenta la velocita' minima raggiungibile in gioco.    
    public void IncreaseDifficulty()
    {
        // Se il giocatore e' attualmente alla velocita' minima, la sua velocita' viene aumentata
        if (speed==minSpeed)
            ChangeSpeed(1);

            // Aumento velocita' minima
            if (minSpeed > maxSpeed)
            {
                // Mostra per 1 secondo il messaggio a schermo (sotto le barre)
                // Forse da sostituire con una soluzione migliore

                ShowMessage("SPEED UP!!!", 1);

                minSpeed--;
                Debug.Log("NUOVA VELOCITA' MINIMA: " + (7 - minSpeed));
            }
    }

    // Mostra il testo scelto per il tempo scelto.
    // Se il tempo viene impostato a 0 la disattivazione automatica non viene impostata.
    // Il messaggio viene mostrato a schermo sotto la barra della velocita'.
    public void ShowMessage(string messageText, int time)
    {
        message.text = messageText;
        message.gameObject.SetActive(true);
        if(time!=0)
        Invoke(nameof(ToggleMessage), time);
    }

    // Rende nuovamente invisibile il messaggio
    public void ToggleMessage()
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
