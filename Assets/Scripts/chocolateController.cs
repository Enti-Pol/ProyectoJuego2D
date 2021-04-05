using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chocolateController : MonoBehaviour
{
    public bool playerDetect = false;

    private Rigidbody2D rigidBody;
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
        float delta = Time.deltaTime * 1000;
        if (playerDetect)
        {
            rigidBody.velocity = new Vector2(0, 0.01f) * delta;
        }
        else
        {
            rigidBody.velocity = new Vector2(0, 0) * delta;
        }
    }
}
