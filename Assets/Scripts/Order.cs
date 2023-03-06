using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimingTools;

public class Order : MonoBehaviour
{
    public float time = 60;
    private Timer _timer;
    // Start is called before the first frame update
    void Start()
    {
        _timer = new Timer(time: time, countDown: true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer.isTiming())
        {
            // If the order is completed, then reset the timer
        }
    }
}
