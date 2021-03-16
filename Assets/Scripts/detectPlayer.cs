using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            gameObject.transform.parent.GetComponent<chocolateController>().playerDetect = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.transform.parent.GetComponent<chocolateController>().playerDetect = false;
    }
}
