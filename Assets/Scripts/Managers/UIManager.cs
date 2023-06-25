using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Settings")]
    [SerializeField] private Image healthImage;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private float time = 90;
    public static event Action OnTimeIsOver;
    public static event Action OnGameStarted;
    [SerializeField] private TMP_Text startPressText;

    [Header("Game Over")]
    [SerializeField] private GameObject overPanel;
    [SerializeField] private TMP_Text timeIsOverText;
    [SerializeField] private TMP_Text gameIsOverText;
    [SerializeField] private TMP_Text goScoreText;

    [Header("Resume Settings")]
    [SerializeField] private GameObject resumePanel;
    private bool _isPaused;
    
    private bool _isGameStarted;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown && !_isGameStarted)
        {
            _isGameStarted = true;
            startPressText.gameObject.SetActive(false);
            OnGameStarted?.Invoke();
        }

        if (GameManager.Instance.IsGamePlaying())
        {
            ControlTime();
            
           ControlResumePanel();
        }
    }

    private void ControlTime()
    {
        time -= Time.deltaTime;
        int seconds = Mathf.RoundToInt(time);
        timeText.text = seconds.ToString();

        if (time <= 0)
        {
            time = 0;
           OnTimeIsOver?.Invoke();
        }
    }

    private void ControlResumePanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                Time.timeScale = 0f;
                resumePanel.SetActive(_isPaused);
            }
            else
            {
                Time.timeScale = 1f;
                resumePanel.SetActive(_isPaused);
            }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(TagManager.MAIN_MENU_SCENE_NAME);
    }

    public void GoToSelect()
    {
        SceneManager.LoadScene(TagManager.SELECT_CHARACTER_SCENE_NAME);
    }
    
    public void UpdateHealth(float newHealth)
    {
        float health = newHealth /PlayerController.Instance.GetStartHealth();
        healthImage.fillAmount = health;
    }

    public void SetScore(int score)
    {
        scoreText.text = TagManager.KILLED_TEXT + score;
    }

    public void GameOver(bool isTimeOver)
    {
        overPanel.SetActive(true);

        if (isTimeOver)
        {
            timeIsOverText.gameObject.SetActive(true);
        }
        else
        {
            gameIsOverText.gameObject.SetActive(true);
        }
    }

    public void SetFinalScore(int score)
    {
        goScoreText.text = score.ToString();
    }
    
}
