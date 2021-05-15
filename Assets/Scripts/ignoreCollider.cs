using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreCollider : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    // Start is called before the first frame update
    void Start()
    {
        Collider2D player = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(player1.GetComponent<Collider2D>(), player);
        Physics2D.IgnoreCollision(player2.GetComponent<Collider2D>(), player);
        Physics2D.IgnoreCollision(player3.GetComponent<Collider2D>(), player);
    }
}
