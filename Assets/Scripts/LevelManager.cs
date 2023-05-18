using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelManager : MonoBehaviour
{
    //public static event Action LevelBegin;
    public static LevelManager Instance;
    [SerializeField] private Score _levelScore;
    [SerializeField] private GameObject _scoreUI;
    [SerializeField] private GameObject _endScreen;
    private GameOver _gameOver;
    private TextMeshProUGUI _scoreText;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _sousChefX;
    [SerializeField] private GameObject _sousChefY;
    [SerializeField] private GameObject _clock;


    private void OnEnable()
    {
        if (_levelScore == null)
        {
            Debug.Log("The Level Manager is missing level Score scriptable object.", this.gameObject);
        }
        if (_scoreUI == null)
        {
            Debug.Log("The Level Manager is missing Score UI prefab.", this.gameObject);
        }
        if (_endScreen == null)
        {
            Debug.Log("The Level Manager is missing End Screen gameobject.", this.gameObject);
        }
        if (_mainMenu == null)
        {
            Debug.Log("The Level Manager is missing Main Menu gameobject.", this.gameObject);
        }
    }

    private void Awake()
    {
        Instance = this;
        _gameOver = _endScreen.GetComponentInChildren<GameOver>();
        _scoreText = _scoreUI.GetComponentInChildren<TextMeshProUGUI>();
        _scoreText.text = scoreTextFormat;

        // Show the main menu at the start of the game
       _mainMenu.SetActive(true);
       _scoreUI.SetActive(false);
       _sousChefX.SetActive(false);
       _sousChefY.SetActive(false);
       _clock.SetActive(false);
    }

    public void StartGame()
    {
        // Start the game
        _mainMenu.SetActive(false);
        _scoreUI.SetActive(true);
        _sousChefX.SetActive(true);
        _sousChefY.SetActive(true);
        _clock.SetActive(true);
    }
    
    /*
    private void Update()
    {
        LevelBegin?.Invoke(); 
    }
    */

    public void addScore()
    {
        _levelScore.currentScore++;
        _scoreText.text = scoreTextFormat;
        if (_levelScore.currentScore >= _levelScore.goal)
        {
            onGameWon();
        }
    }

    public void onTimeout()
    {
        _scoreUI.gameObject.SetActive(false);
        _gameOver.Setup(_levelScore.currentScore, false);
    }

    private void onGameWon()
    {
        _scoreUI.gameObject.SetActive(false);
        _gameOver.Setup(_levelScore.currentScore, true);
    }

    private string scoreTextFormat
    {
        get
        {
            return "Score: " + _levelScore.currentScore + "/" + _levelScore.goal;
        }
    }
}
