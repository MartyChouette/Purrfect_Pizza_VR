using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private float _timeLimit;
    private float _timeRemaining;
    private bool _isTiming = false;
    private Slider _progressBar;

    void Awake()
    {
        _progressBar = GetComponentInChildren<Slider>();
        if (_progressBar != null)
        {
            _progressBar.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (_isTiming)
        {
            if (_timeRemaining < _timeLimit)
            {
                _timeRemaining += Time.deltaTime;
                if (_progressBar != null)
                {
                    _progressBar.value = _timeRemaining / _timeLimit;
                }
            }
            else
            {
                reset();
            }
        }
    }

    public void start()
    {
        if (!_isTiming && _timeRemaining <= 0)
        {
            _isTiming = true;
            if (_progressBar != null)
            {
                _progressBar.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("The timer is currently running. Please reset the timer.");
        }
    }

    public void pause()
    {
        if (_isTiming)
        {
            _isTiming = false;
        }
        else
        {
            Debug.Log("The timer has not yet started or resumed.");
        }
    }

    public void resume()
    {
        if (!_isTiming && (_timeRemaining > 0 & _timeRemaining < _timeLimit))
        {
            _isTiming = true;
        }
        else
        {
            Debug.Log("The timer is currently running.");
        }
    }

    // Reset the time remaining to initial set time
    public void reset()
    {
        if (_timeRemaining >= _timeLimit)
        {
            _timeRemaining = 0;
            _isTiming = false;
            if (_progressBar != null)
            {
                _progressBar.gameObject.SetActive(false);
                _progressBar.value = 0;
            }
        }
        else
        {
            Debug.Log("The timer is already reset.");
        }
    }

    public bool stopped
    {
        get
        {
            return _timeRemaining <= 0 && !_isTiming;
        }
    }

    public float timeRemaining
    {
        set
        {
            _timeRemaining = value;
        }
        get
        {
            return _timeRemaining;
        }
    }
    public float timeLimit
    {
        set
        {
            _timeLimit = value;
        }
    }

    // Get the float number of minutes left in the time remaining
    private float minute() 
    {
        return Mathf.FloorToInt(_timeRemaining / 60);
    }

    // Get the float number of seconds left in the time remaining
    private float second() 
    {
        return Mathf.FloorToInt(_timeRemaining % 60);
    }

    /*
    Update timer text to display in the scene
    - Overloaded to 3 different text types: TextMeshProUGUI, TMP_Text, and Text
    */
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
}