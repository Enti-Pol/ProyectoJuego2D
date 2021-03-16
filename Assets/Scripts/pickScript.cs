using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickScript : MonoBehaviour
{
    private playerController lookRight;
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
}
