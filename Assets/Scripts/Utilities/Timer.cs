using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer
{
    public static Timer Create(Action action, float time, ProgressBar progressBar=null)
    {
        GameObject gameObject = new GameObject("Timer", typeof(MonoBehaviourHook));
        Timer timer = new Timer(action, time, progressBar, gameObject);
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = timer.Update;

        return timer;
    }
    public class MonoBehaviourHook : MonoBehaviour
    {
        public Action onUpdate;
        private void FixedUpdate()
        {
            onUpdate?.Invoke();
        }
    }

    private Action _action;
    private float _timeLimit;
    private float _currentTime;
    private bool _isDestroyed;
    private ProgressBar _progressBar;
    private GameObject _gameObject;

    private Timer(Action action, float time, ProgressBar progressBar, GameObject gameObject)
    {
        _action = action;
        _timeLimit = time;
        _currentTime = 0;
        _progressBar = progressBar;
        _gameObject = gameObject;
        _isDestroyed = false;
    }

    public void Update()
    {
        if (!_isDestroyed)
        {
            if (_progressBar != null) // Calculate time with progress bar
            {
                if (_progressBar.isActive)
                {
                    _currentTime += Time.deltaTime; // Update current time
                    _progressBar.sliderValue = _currentTime / _timeLimit; // Update progress bar value

                    if (_progressBar.lerpColorTDelta > 0) 
                    {
                        _progressBar.setFillColor(); // Update the progress bar color
                    }
                }
            }
            else // Calculate time without progress bar
            {
                _currentTime += Time.deltaTime; // Update current time
            }

            if (_currentTime >= _timeLimit)
            {
                _action();
                DestroySelf();
            }
        }
    }

    private void DestroySelf()
    {
        _isDestroyed = true;
        UnityEngine.Object.Destroy(_gameObject);
        
        if (_progressBar != null)
        {
            _progressBar.sliderValue = 0;
        }
    }

    /*
    // Get the float number of minutes left in the time remaining
    private float minute() 
    {
        return Mathf.FloorToInt(_time / 60);
    }

    // Get the float number of seconds left in the time remaining
    private float second() 
    {
        return Mathf.FloorToInt(_time % 60);
    }
    
    public void updateTimerText(TextMeshProUGUI timerText) // Text Mesh Pro, part of UI components
    {
        timerText.text = string.Format("{0:00}:{1:00}", minute(), second());
    }
    public void updateTimerText(TMP_Text timerText) // Text Mesh Pro, part of 3D Object components
    {
        timerText.text = string.Format("{0:00}:{1:00}", minute(), second());
    }
    public void updateTimerText(Text timerText) // simple Text, part of UI components (common with older Unity version)
    {
        timerText.text = string.Format("{0:00}:{1:00}", minute(), second());
    }
    */
}