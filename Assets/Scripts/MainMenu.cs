using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject sceneLoader;
    public GameObject gameManager;
    private void Start()
    {
    }
    public void PlayGame(int value)
    {
        gameManager.gameObject.GetComponent<GameManager>().playerOrIA = value;
        Debug.Log(gameManager.gameObject.GetComponent<GameManager>().playerOrIA);
        sceneLoader.GetComponent<levelLoaderScript>().LoadNextLevel("MiniCircuito");
    }
    public void PlayTutorial()
    {
        sceneLoader.GetComponent<levelLoaderScript>().LoadNextLevel("TestMap");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
