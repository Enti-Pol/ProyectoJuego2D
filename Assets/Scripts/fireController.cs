using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireController : MonoBehaviour
{
    public ParticleSystem fire;
    private float timeToActivate;
    private bool activate = false;
    // Start is called before the first frame update
    void Start()
    {
        timeToActivate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * 1000;
        timeToActivate += delta;
        if (timeToActivate >= 3000 && activate)
        {
            fire.gameObject.SetActive(true);
            activate = false;
            timeToActivate = 0;
        }
        else if (timeToActivate >= 2000 && !activate)
        {
            fire.gameObject.SetActive(false);
            activate = true;
            timeToActivate = 0;
        }
    }
}
