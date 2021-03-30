using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject sceneLoader;
    public void PlayGame()
    {
        sceneLoader.GetComponent<levelLoaderScript>().LoadNextLevel("MiniCircuito");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
