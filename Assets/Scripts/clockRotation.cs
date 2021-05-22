using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clockRotation : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Rotate(0, 0, +0.4f);
    }
}
