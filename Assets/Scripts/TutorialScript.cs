using UnityEngine;
/// <summary>
/// Gestisce le varie schede del tutorial e i controlli da tastiera.
/// </summary>
public class TutorialScript : MonoBehaviour
{
    /// <summary>
    /// Le schede da attivare
    /// </summary>
    public GameObject[] istruzioni;
    /// <summary>
    /// Riferimento a logic
    /// </summary>
    public LogicScript logic;
    /// <summary>
    /// Contatore della scheda
    /// </summary>
    public int i = 0;
    public void Update()
    {
        if (Input.GetButtonDown("Right") && i < 3)
        {
            Right();
        }
        if (Input.GetButtonDown("Left") && i > 0)
        {
            Left();
        }

        if (Input.GetButtonDown("FireGreen") || Input.GetButtonDown("FireRed") || Input.GetButtonDown("FireYellow"))
        {
            logic.HowtoPlayExitButton();
        }
    }
    /// <summary>
    /// Attiva la scheda successiva.
    /// </summary>
    public void Right()
    {
        istruzioni[i].SetActive(false);
        i++;
        istruzioni[i].SetActive(true);
    }
    /// <summary>
    /// Attiva la scheda precedente.
    /// </summary>
    public void Left()
    {
        istruzioni[i].SetActive(false);
        i--;
        istruzioni[i].SetActive(true);

    }
}
