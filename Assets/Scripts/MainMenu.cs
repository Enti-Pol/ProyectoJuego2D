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
        gameManager.GetComponent<GameManager>().actualLevel += 1;
        if (actualLevel == 1 || actualLevel == 0)
        {
            gameManager.GetComponent<GameManager>().actualLevel = 6;
            actualLevel = 6;
        }
        sceneLoader.GetComponent<levelLoaderScript>().LoadNextLevel(actualLevel);
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
