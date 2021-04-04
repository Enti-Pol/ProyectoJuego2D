using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnToMenu : MonoBehaviour
{
    public GameObject sceneLoader;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("callIt", 4);
    }
    private void callIt()
    {
        sceneLoader.GetComponent<levelLoaderScript>().LoadNextLevel("StartMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
