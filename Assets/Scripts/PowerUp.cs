using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool isShield;
    public bool isBoost;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1" || other.tag == "Player2" || other.tag == "Player3" || other.tag == "Player4")
        {
            if (isShield)
            {
                other.gameObject.GetComponent<playerController>().ActivateShield();
            }

            if (isBoost)
            {
                other.gameObject.GetComponent<playerController>().ActivateSpeedBoost();
            }
        }
    }
}
