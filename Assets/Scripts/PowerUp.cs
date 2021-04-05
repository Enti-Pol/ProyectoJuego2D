using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool isShield;
    public bool isBoost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            Destroy(gameObject);

            if (isShield)
            {
                other.gameObject.GetComponent<playerController>().ActivateShield();
            }

            if (isBoost)
            {
                other.gameObject.GetComponent<playerController>().boostLength = 2;
                other.gameObject.GetComponent<playerController>().ActivateSpeedBoost();
            }
        }
    }
}
