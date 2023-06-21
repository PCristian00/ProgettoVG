using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject[] istruzioni;
    public LogicScript logic;
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
    public void Right()
    {
        istruzioni[i].SetActive(false);
        i++;
        istruzioni[i].SetActive(true);
    }
    public void Left()
    {
        istruzioni[i].SetActive(false);
        i--;
        istruzioni[i].SetActive(true);

    }
}
