using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraController : MonoBehaviour
{
    public GameObject player;
    public float speedOfSet;

    public Vector2 posOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float endX = 0;
        float endY = 0;
        float endZ = 0;
        Vector3 startPos = transform.position;

        endX = player.transform.position.x;
        endY = player.transform.position.y;
        endZ = player.transform.position.z;
        
        Vector3 endPos = new Vector3(endX, endY, endZ);
        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        transform.position = Vector3.Lerp(startPos, endPos, speedOfSet * Time.fixedDeltaTime);
    }
}
