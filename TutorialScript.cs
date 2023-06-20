using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject[] istruzioni;
    public LogicScript logic;
    int i = 0;
    // Update is called once per frame
    public void Update()
    {
        if (Input.GetAxisRaw("Horizontal")== 1 && i < 3)
        {
                istruzioni[i].SetActive(false);
                i++;
                istruzioni[i].SetActive(true);
        }
        if (Input.GetAxisRaw("Horizontal") == -1 && i > 0)
        {
            istruzioni[i].SetActive(false);
            i--;
            istruzioni[i].SetActive(true);
        }
        if (Input.GetButtonDown("FireRed"))
        {
            logic.HowtoPlayExitButton();
        }
    }
}
