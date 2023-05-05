using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer
{
    public static Timer Create(Action action, float time, Slider progressBar)
    {
        GameObject gameObject = new GameObject("Timer", typeof(MonoBehaviourHook));
        Timer timer = new Timer(action, time, progressBar, gameObject);
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = timer.Update;

        return timer;
    }
    public class MonoBehaviourHook : MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            onUpdate?.Invoke();
        }
    }

    private Action _action;
    private float _timeLimit;
    private float _time;
    private bool _isDestroyed;
    private Slider _progressBar;
    private GameObject _gameObject;

    private Timer(Action action, float time, Slider progressBar, GameObject gameObject)
    {
        _action = action;
        _timeLimit = time;
        _time = 0;
        _progressBar = progressBar;
        _gameObject = gameObject;
        _isDestroyed = false;
        enableProgressBar();
    }

    public void Update()
    {
        if (!_isDestroyed)
        {
            _time += Time.deltaTime;
            if (_progressBar != null)
            {
                _progressBar.value = _time / _timeLimit;
            }

            if (_time > _timeLimit)
            {
                _action();
                DestroySelf();
            }
        }
    }

    public float timeLimit
    {
        set
        {
            _timeLimit = value;
        }
    }

    private void enableProgressBar()
    {
        if (_progressBar != null)
        {
            _progressBar.gameObject.SetActive(true);
        }
    }

    private void disableProgressBar()
    {
        if (_progressBar != null)
        {
            _progressBar.gameObject.SetActive(false);
            _progressBar.value = 0;
        }
    }

    private void DestroySelf()
    {
        _isDestroyed = true;
        UnityEngine.Object.Destroy(_gameObject);
        disableProgressBar();
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