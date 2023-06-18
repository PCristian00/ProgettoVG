using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInput : MonoBehaviour
{
    public LogicScript logic;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FireGreen") || Input.GetButtonDown("FireRed") || Input.GetButtonDown("FireYellow")) logic.HowtoPlayExitButton();
    }
}
