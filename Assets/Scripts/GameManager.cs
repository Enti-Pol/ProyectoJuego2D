using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public float volume = 1;
    public Slider soundSlider;
    public AudioMixer audioMixer;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Warning: multiple " + this + " in scene!");
        }
    }
    public void getVolume(float vol)
    {
        audioMixer.SetFloat("volume", vol);
    }
    // Start is called before the first frame update
    void Start()
    {
        soundSlider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
