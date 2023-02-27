using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TimingTools
{
    public class Timer
    {
        private float _time;
        private float _timeRemaining;
        
        public Timer()
        {
            _time = 0;
            _timeRemaining = 0;
        }
        public Timer(float t)
        {
            _time = t;
            _timeRemaining = _time;
        }

        /*
        Check and update the remaining time, by counting down the initial set time
        - return true if time is still ticking down
        - return false if time runs out
        */
        public bool isCountingDown()
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
                return true;
            }
            return false;
        }

        public void isTiming()
        {
            _timeRemaining += Time.deltaTime;
        }

        // Reset the time remaining to initial set time
        public void reset()
        {
            if (_timeRemaining <= 0)
            {
                _timeRemaining = _time;
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
}