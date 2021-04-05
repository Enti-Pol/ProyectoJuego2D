﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public float volume = 1;
    public Slider soundSlider;
    public AudioMixer audioMixer;
    public int playerOrIA = 0;
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
    public void setMusic(float vol)
    {
        audioMixer.SetFloat("music", vol);
    }
    public void setSFX(float vol)
    {
        audioMixer.SetFloat("sfx", vol);
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
