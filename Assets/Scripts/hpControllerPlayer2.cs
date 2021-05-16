using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpControllerPlayer2 : MonoBehaviour
{
    public Slider healthBar;
    playerController playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player2").GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
