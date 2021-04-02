using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SopleteController : MonoBehaviour
{
    public int damage = 1;
    private float timeToActivate;
    private bool activate = false;
    private Vector3 size;
    // Start is called before the first frame update
    void Start()
    {
        timeToActivate = 0;
        size = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * 1000;
        timeToActivate += delta;
        if (timeToActivate >= 3000 && activate)
        {
            transform.localScale = size;
            activate = false;
            timeToActivate = 0;
        }
        else if (timeToActivate >= 2000 && !activate)
        {
            transform.localScale = new Vector3(0, 0, 0);
            activate = true;
            timeToActivate = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<playerController>().hp = collision.GetComponent<playerController>().hp - damage;
    }
}
