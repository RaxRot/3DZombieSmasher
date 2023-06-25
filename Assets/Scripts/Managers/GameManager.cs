using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool _isGameStarted;

    private int _score;
    
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject[] models;
    [SerializeField] private GameObject defaultModel;
    

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _score = PlayerPrefs.HasKey(TagManager.COINS_PREFS) ? PlayerPrefs.GetInt(TagManager.COINS_PREFS) : 0;
        UIManager.Instance.SetScore(_score);
        
        ChosePlayer();
    }
    
    private void ChosePlayer()
    {
        for (int i = 0; i < models.Length; i++)
        {
            if (models[i].name==PlayerPrefs.GetString(TagManager.SELECTED_CAR_PREFS))
            {
                GameObject clone = Instantiate(models[i], player.modelHolder.position, player.modelHolder.rotation);
                clone.transform.parent = player.modelHolder;
                clone.transform.localScale = new Vector3(1f, 1f, 1f);
                defaultModel.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        UIManager.OnTimeIsOver += TimeOver;
        UIManager.OnGameStarted += StartGame;
        PlayerController.OnPlayerDied += GameOver;
        Obstacle.OnEnemyKilled += IncreaseScore;
    }

    private void OnDisable()
    {
        UIManager.OnTimeIsOver -= TimeOver;
        UIManager.OnGameStarted -= StartGame;
        PlayerController.OnPlayerDied -= GameOver;
        Obstacle.OnEnemyKilled -= IncreaseScore;
    }

    private void TimeOver()
    {
        StopGame();
        UIManager.Instance.GameOver(true);
    }

    private void StartGame()
    {
        _isGameStarted = true;
    }

    private void StopGame()
    {
        _isGameStarted = false;
        
        PlayerPrefs.SetInt(TagManager.COINS_PREFS,_score);
        
        UIManager.Instance.SetFinalScore(_score);
        
        AudioManager.Instance.StopBgm();
    }

    public bool IsGamePlaying()
    {
        return _isGameStarted;
    }

    private void GameOver()
    {
        StopGame();
        UIManager.Instance.GameOver(false);
    }

    private void IncreaseScore()
    {
        _score++;
        UIManager.Instance.SetScore(_score);
    }
}
