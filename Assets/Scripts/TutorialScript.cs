using System.Collections;
using System.Collections.Generic;
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
            right();
        }
        if (Input.GetButtonDown("Left") && i > 0)
        {
            left();
        }
    }
    public void right()
    {
        istruzioni[i].SetActive(false);
        i++;
        istruzioni[i].SetActive(true);
    }
    public void left()
    {
        istruzioni[i].SetActive(false);
        i--;
        istruzioni[i].SetActive(true);

    }
}
