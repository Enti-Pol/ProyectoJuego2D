using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public enum Direction { NONE, UP, DOWN, LEFT, RIGHT };
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
        if (!isAlive) { Destroy(gameObject); }

        KeyCode wButton = KeyCode.W;
        KeyCode sButton = KeyCode.S;
        KeyCode aButton = KeyCode.A;
        KeyCode dButton = KeyCode.D;

        direction = Direction.NONE;
        if (wButton != KeyCode.None && sButton != KeyCode.None)
        {
            if (Input.GetKey(wButton))
            {
                direction = Direction.UP;
            }
            else if (Input.GetKey(sButton))
            {
                direction = Direction.DOWN;
            }
            else if (Input.GetKey(aButton))
            {
                direction = Direction.LEFT;
            }
            else if (Input.GetKey(dButton))
            {
                direction = Direction.RIGHT;
            }
        }
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;

        if (direction == Direction.RIGHT)
        {
            rigidBody.AddForce(transform.right * baseSpeed * delta);
        }
        else if (direction == Direction.LEFT)
        {
            rigidBody.AddForce(-(transform.right) * baseSpeed * delta);
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
