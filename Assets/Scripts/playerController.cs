using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public enum Direction { NONE, UP, DOWN, LEFT, RIGHT };
    public enum Player { NONE, PLAYER1, PLAYER2, PLAYER3, PLAYER4 }
    public Player playerNum;

    private Direction direction = Direction.NONE;

    public bool isAlive = true;
    private bool isJumping = false;

    public float baseSpeed = 1f;
    private float JumpForce = 35f;

    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape")) { Application.Quit(); }
        KeyCode upButton = KeyCode.W;
        KeyCode downButton = KeyCode.S;
        KeyCode leftButton = KeyCode.A;
        KeyCode rightButton = KeyCode.D;
        
        switch (playerNum)
        {
            default:
                upButton = KeyCode.W;
                downButton = KeyCode.S;
                leftButton = KeyCode.A;
                rightButton = KeyCode.D;
                break;
            case Player.PLAYER1:
                upButton = KeyCode.W;
                downButton = KeyCode.S;
                leftButton = KeyCode.A;
                rightButton = KeyCode.D;
                if (!isAlive) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
                break;
            case Player.PLAYER2:
                upButton = KeyCode.I;
                downButton = KeyCode.K;
                leftButton = KeyCode.J;
                rightButton = KeyCode.L;
                break;
        }
        if (!isAlive) { Destroy(gameObject); }
        direction = Direction.NONE;
        if (upButton != KeyCode.None && downButton != KeyCode.None)
        {
            if (Input.GetKey(upButton))
            {
                direction = Direction.UP;
            }
            else if (Input.GetKey(downButton))
            {
                direction = Direction.DOWN;
            }
            else if (Input.GetKey(leftButton))
            {
                direction = Direction.LEFT;
            }
            else if (Input.GetKey(rightButton))
            {
                direction = Direction.RIGHT;
            }
        }
    }

    private void FixedUpdate()
    {
        transform.localScale = new Vector3(3, 5, 1);
        float delta = Time.fixedDeltaTime * 1000;

        if (direction == Direction.RIGHT)
        {
            rigidBody.AddForce(transform.right * baseSpeed * delta);
        }
        else if (direction == Direction.LEFT)
        {
            rigidBody.AddForce(-(transform.right) * baseSpeed * delta);
        }
        else if (direction == Direction.DOWN)
        {
            transform.localScale = new Vector3(3, 3, 1);
        }
        if (!isJumping)
        {
            if (direction == Direction.UP)
            {
                rigidBody.AddForce(transform.up * JumpForce * delta);
                isJumping = true;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "camBorder")
        {
            isAlive = false;
        }
    }
}
