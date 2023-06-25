using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    public static PlayerController Instance;
    
    private Rigidbody _rb;

    [Header("Shooting")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletStartPoint;
    [SerializeField] private GameObject shootFx;
    [SerializeField] private float timeBetweenShoots = 1f;
    private float _shootTimer;
    private bool _canShoot;

    [Header("Health")]
    [SerializeField] private int health=5;
    private int _startHealth;
    public static event Action OnPlayerDied;
    
    public Transform modelHolder;
    
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        Obstacle.OnPlayerDamaged += DamagePlayer;
    }

    private void OnDisable()
    {
        Obstacle.OnPlayerDamaged -= DamagePlayer;
    }
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _startHealth = health;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            ControlMovement();
        
            ControlShooting();
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            MoveTank();
        }
    }

    private void MoveTank()
    {
        _rb.MovePosition(_rb.position + speed*Time.deltaTime);
    }


    private void ControlMovement()
    {
        HandleContinuousMovement();
        HandleSingleMovement();
    }

    private void ControlShooting()
    {
        _shootTimer -= Time.deltaTime;
        if (_shootTimer<=0)
        {
            _canShoot = true;
        }
        
        if (Input.GetMouseButtonDown(0) && _canShoot)
        {
            Instantiate(bullet, bulletStartPoint.position, Quaternion.identity);
            Instantiate(shootFx, bulletStartPoint.position, Quaternion.identity);

            _shootTimer = timeBetweenShoots;
            _canShoot = false;
            
            AudioManager.Instance.PlaySfx(0);
        }
        
    }

    private void HandleContinuousMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
    
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
    
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveFast();
        }
    
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveSlow();
        }
    }

    private void HandleSingleMovement()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            MoveStraight();
        }
    
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            MoveStraight();
        }
    
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            MoveNormal();
        }
    
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            MoveNormal();
        }
    }

    public int GetStartHealth()
    {
        return _startHealth;
    }

    private void DamagePlayer(int damage)
    {
        health -= damage;
        if (health<=0)
        {
            health = 0;
            OnPlayerDied?.Invoke();
        }
        UIManager.Instance.UpdateHealth(health);
    }
}
