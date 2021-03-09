using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float maxSpeed = 6.0f;
    public float moveDireciton;
    public bool facinRight = true;
    private Rigidbody rigidbody;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDireciton = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            rigidbody.velocity = Vector2.up * 1.0f;
        }
    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(moveDireciton * maxSpeed, rigidbody.velocity.y);
    }
}
