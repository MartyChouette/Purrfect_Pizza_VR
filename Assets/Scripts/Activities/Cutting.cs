using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimingTools;

public class Cutting : MonoBehaviour
{
    public float time = 5;
    private Timer _timer;
    // Start is called before the first frame update
    void Start()
    {
        _timer = new Timer(time: time, countDown: false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer.isTiming())
        {
            // Cutting
        }
        else
        {
            // Finished cutting
            _timer.reset();
        }
    }
}
