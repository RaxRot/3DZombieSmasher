using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource bgm;

    [SerializeField] private AudioSource[] sfx;

    
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        
    }

    public void StopBgm()
    {
        bgm.Stop();
    }

    public void PlaySfx(int index)
    {
        sfx[index].Play();
    }
}

