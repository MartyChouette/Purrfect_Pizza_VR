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

    void Update()
    {
        if (_isTiming)
        {
            if (_timeRemaining < _timeLimit)
            {
                _timeRemaining += Time.deltaTime;
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
        }
        else
        {
            Debug.Log("The timer is already reset.");
        }
    }

    public bool isEnded
    {
        get
        {
            return _timeRemaining >= _timeLimit;
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