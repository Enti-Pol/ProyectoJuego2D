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
    public void PlayGame()
    {
        gameManager = GameObject.Find("gameManager");
        int actualLevel = gameManager.GetComponent<GameManager>().actualLevel;
        sceneLoader.GetComponent<levelLoaderScript>().LoadNextLevel(actualLevel);
        gameManager.GetComponent<GameManager>().actualLevel += 1;
    }
    private string ReturnARandomLevel()
    {
        int randNum = Random.Range(0, 3);
        string nextLevel = "";
        switch (randNum)
        {
            case 0:
                nextLevel = "MiniCircuito";
                break;
            case 1:
                nextLevel = "MiniCircuito2";
                break;
            case 2:
                nextLevel = "MiniCircuito3";
                break;
        }
        return nextLevel;
    }
    public void PlayTutorial()
    {
        sceneLoader.GetComponent<levelLoaderScript>().LoadNextLevel(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
