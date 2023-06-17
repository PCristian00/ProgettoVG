using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverInput : MonoBehaviour
{
    LogicScript logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FireGreen")) logic.RestartGame();
        if (Input.GetButtonDown("FireYellow")) logic.Menu();
        if (Input.GetButtonDown("FireRed")) logic.QuitGame();
    }
}
