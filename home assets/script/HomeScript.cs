using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScript : MonoBehaviour
{
    public void StartGame(/*string SampleScene*/)
    {
        SceneManager.LoadScene("Game");
        //SceneManager.LoadSceneAsync(SampleScene);
    }
    

}
