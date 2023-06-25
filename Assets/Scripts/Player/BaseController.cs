using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected Vector3 speed;
    [SerializeField] private float xSpeed = 4f;
    [SerializeField] private float zSpeed = 15f;
    [SerializeField] private float accelerated = 15f;
    [SerializeField] private float decelerated = 10f;
    [SerializeField] protected float rotationSpeed = 10f;

    [Header("Music")]
    [SerializeField] private float lowSoundPitch;
    [SerializeField] private float normalSoundPitch;
    [SerializeField] private float highSoundPitch;
    [SerializeField] private AudioClip engineSoundOn;
    [SerializeField] private AudioClip engineSoundOff;
    private AudioSource _soundManager;
    private bool _isSlow;

    private void Awake()
    {
        speed = new Vector3(0f, 0f, zSpeed);
        _soundManager = GetComponent<AudioSource>();
    }

    protected void MoveLeft()
    {
        speed = new Vector3(-xSpeed, 0f, speed.z);
    }
    
    protected void MoveRight()
    {
        speed = new Vector3(xSpeed, 0f, speed.z);
    }

    protected void MoveStraight()
    {
        speed = new Vector3(0f, 0f, speed.z);
    }

    protected void MoveNormal()
    {
        if (_isSlow)
        {
            _isSlow = false;
            
            //_soundManager.Stop();
           // _soundManager.clip = engineSoundOn;
           //_soundManager.volume = 0.3f;
           // _soundManager.Play();
        }

        speed = new Vector3(speed.x, 0, zSpeed);
    }

    protected void MoveSlow()
    {
        if (!_isSlow)
        {
            _isSlow = true;
            
           // _soundManager.Stop();
           // _soundManager.clip = engineSoundOff;
           // _soundManager.volume = 0.5f;
           // _soundManager.Play();
        }

        speed = new Vector3(speed.x, 0, decelerated);
    }

    protected void MoveFast()
    {
        speed = new Vector3(speed.x, 0, accelerated);
    }
}
