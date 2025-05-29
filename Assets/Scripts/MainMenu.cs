using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script realizat de Craciun Claudiu-Viorel
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName: "ApocalypticTerra");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
