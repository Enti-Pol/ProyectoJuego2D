using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickScript : MonoBehaviour
{
    private playerController lookRight;
    public enum Player { NONE, PLAYER1, PLAYER2, PLAYER3, PLAYER4, IA }
    private bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        lookRight = transform.parent.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (lookRight.isRight)
        {
            case playerController.Direction.RIGHT:
                transform.position = new Vector3(1.1f + lookRight.transform.position.x, transform.position.y, transform.position.z);
                break;
            case playerController.Direction.LEFT:
                transform.position = new Vector3(-1.1f + lookRight.transform.position.x, transform.position.y, transform.position.z);
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player2")
        {
            facingRight = collision.gameObject.GetComponent<playerController>().facingRight;
            if (Input.GetKey(KeyCode.E))
            {
                if (lookRight.isRight == playerController.Direction.RIGHT)
                {
                    collision.gameObject.GetComponent<playerController>().moveDirection = 2;
                }
                else
                {
                    collision.gameObject.GetComponent<playerController>().moveDirection = -2;
                }
            }
        }
        else if (collision.tag == "Player1")
        {
            facingRight = collision.gameObject.GetComponent<playerController>().facingRight;
            if (Input.GetKey(KeyCode.O))
            {
                if (lookRight.isRight == playerController.Direction.RIGHT)
                {
                    collision.gameObject.GetComponent<playerController>().moveDirection = 2;
                }
                else
                {
                    collision.gameObject.GetComponent<playerController>().moveDirection = -2;
                }
            }
        }
    }
}
