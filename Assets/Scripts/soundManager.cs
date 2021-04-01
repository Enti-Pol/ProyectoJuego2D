using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public GameObject obtainVolume;
    public float volume;
    private AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        volume = obtainVolume.GetComponent<soundManager>().volume;
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        volume = obtainVolume.GetComponent<soundManager>().volume;
        audioSrc.volume = volume;
    }
}
