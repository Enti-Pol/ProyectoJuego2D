using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject player2;
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
        if (!player.active)
        {
            endX = player2.transform.position.x;
            endY = player2.transform.position.y;
            endZ = player2.transform.position.z;
        }
        else if (!player2.active){
            endX = player.transform.position.x;
            endY = player.transform.position.y;
            endZ = player.transform.position.z;
        }
        else
        {
            endX = (player.transform.position.x + player2.transform.position.x) / 2;
            endY = (player.transform.position.y + player2.transform.position.y) / 2;
            endZ = (player.transform.position.z + player2.transform.position.z) / 2;
        }
        
        Vector3 endPos = new Vector3(endX, endY, endZ);
        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        transform.position = Vector3.Lerp(startPos, endPos, speedOfSet * Time.fixedDeltaTime);
    }
}
