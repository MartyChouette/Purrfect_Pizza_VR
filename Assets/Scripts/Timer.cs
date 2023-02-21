using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimingTools
{
    public class Timer : MonoBehaviour
    {
        private float time;
        private float timeRemaining;
        
        public Timer(float t)
        {
            time = t;
            timeRemaining = time;
        }

        public bool isRunning()
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                return true;
            }
            return false;
        }

        public void reset()
        {
            if (timeRemaining <= 0)
            {
                timeRemaining = time;
            }
        }

        //TODO: Create a function that returns a string of time in a format of minutes and seconds (00:00)
    }
}