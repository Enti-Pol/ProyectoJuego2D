using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPath : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
