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
        sceneLoader.GetComponent<levelLoaderScript>().LoadNextLevel(ReturnARandomLevel());
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
        sceneLoader.GetComponent<levelLoaderScript>().LoadNextLevel("TestMap");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
