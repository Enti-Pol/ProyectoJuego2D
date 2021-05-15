using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class returnToMenu : MonoBehaviour
{
    public GameObject sceneLoader;
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("callIt", 4);
    }
    private void callIt()
    {
        gameManager = GameObject.Find("gameManager");
        int actualLevel = gameManager.GetComponent<GameManager>().actualLevel;
        gameManager.GetComponent<GameManager>().actualLevel += 1;
        if (gameManager.GetComponent<GameManager>().actualLevel == SceneManager.sceneCountInBuildSettings)
        {
            gameManager.GetComponent<GameManager>().actualLevel = 0;
        }
        sceneLoader.GetComponent<levelLoaderScript>().LoadNextLevel(actualLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
