using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float baseSpeed = 0.1f;
    public float currentSpeedV = 0.0f;
    enum direction { NONE, UP, DOWN, RIGHT, LEFT, UPLEFT, UPRIGHT, DOWNLEFT, DOWNRIGHT };
    direction actualDirection = direction.RIGHT;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime * 1000;
        switch (actualDirection)
        {
            case direction.UP:
                baseSpeed = 0.05f;
                rigidBody.velocity = new Vector2(0, baseSpeed) * delta;
                break;
            case direction.DOWN:
                baseSpeed = 0.05f;
                rigidBody.velocity = new Vector2(0, -baseSpeed) * delta;
                break;
            case direction.RIGHT:
                baseSpeed = 0.2f;
                rigidBody.velocity = new Vector2(baseSpeed, 0) * delta;
                break;
            case direction.LEFT:
                baseSpeed = 0.2f;
                rigidBody.velocity = new Vector2(-baseSpeed, 0) * delta;
                break;
            case direction.DOWNLEFT:
                baseSpeed = 0.1f;
                rigidBody.velocity = new Vector2(-baseSpeed, -baseSpeed) * delta;
                break;
            case direction.DOWNRIGHT:
                baseSpeed = 0.1f;
                rigidBody.velocity = new Vector2(baseSpeed, -baseSpeed) * delta;
                break;
            case direction.UPRIGHT:
                baseSpeed = 0.1f;
                rigidBody.velocity = new Vector2(baseSpeed, baseSpeed) * delta;
                break;
            case direction.UPLEFT:
                baseSpeed = 0.1f;
                rigidBody.velocity = new Vector2(-baseSpeed, baseSpeed) * delta;
                break;

            default:
                baseSpeed = 0.2f;
                rigidBody.velocity = new Vector2(baseSpeed, 0) * delta;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "direction")
        {
            float zRotationCollision = collision.gameObject.transform.rotation.eulerAngles.z;
            if (zRotationCollision == 0)
            {
                actualDirection = direction.UP;
            }
            else if (zRotationCollision == 90 || zRotationCollision == -270)
            {
                actualDirection = direction.LEFT;
            }
            else if (zRotationCollision == 180 || zRotationCollision == -180)
            {
                actualDirection = direction.DOWN;
            }
            else if (zRotationCollision == 270 || zRotationCollision == 90)
            {
                actualDirection = direction.RIGHT;
            }
            else if (zRotationCollision == 45 || zRotationCollision == -315)
            {
                actualDirection = direction.UPLEFT;
            }
            else if (zRotationCollision == 315 || zRotationCollision == -45)
            {
                actualDirection = direction.UPRIGHT;
            }
            else if (zRotationCollision == 135 || zRotationCollision == -225)
            {
                actualDirection = direction.DOWNLEFT;
            }
            else if (zRotationCollision == -135 || zRotationCollision == 225)
            {
                actualDirection = direction.DOWNRIGHT;
            }
        }
    }
}
