using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimingTools;

public class ChefTaskY : MonoBehaviour
{

    public bool haveTask = false;
    public string currentTask;
    public float time = 5;
    private Timer _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timer = new Timer(time: time, countDown: true);
    }

    // Update is called once per frame
    void Update()
    {
        if (haveTask)
        {
            if (_timer.isTiming())
            {
                // Start cooking

                // If headchef yell/throw stuff at souschef then reduce time
            }
            else
            {
                // Reset the timer when the souschef finished and assign new task
                _timer.reset();
            }
        }
    }
}
