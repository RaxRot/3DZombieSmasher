using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float timeToDestroyBullet = 4f;
    [SerializeField] private float bulletSpeed = 2000f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    private void Start()
    {
        _rb.AddForce(transform.forward.normalized*bulletSpeed);
        
        Invoke(nameof(DeactivateBullet),timeToDestroyBullet);
    }

    private void DeactivateBullet()
    {
        Destroy(gameObject);
    }
}
