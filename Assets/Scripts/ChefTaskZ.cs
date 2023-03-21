using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TimingTools;

public class ChefTaskZ : MonoBehaviour
{
    public bool haveTask = false;
    public string currentTask;
    public float time = 5;
    public Slider progressBar;
    private Timer _timer;

    private void Awake()
    {
        progressBar.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        _timer = new Timer(time: time, countDown: false);
    }

    // Update is called once per frame
    void Update()
    {
        if (haveTask)
        {
            progressBar.gameObject.SetActive(true);
            if (_timer.isTiming())
            {
                // Start cooking
                progressBar.value = _timer.getTimeRemaining() / time;

                // If headchef yell/throw stuff at souschef then reduce time
            }
            else
            {
                // Reset the timer when the souschef finished and assign new task
                progressBar.gameObject.SetActive(false);
                progressBar.value = 0;
                haveTask = false;
                _timer.reset();
            }
        }
    }

    public void assignTask()
    {
        if (!haveTask)
        {
            haveTask = true;
        }
        else
        {
            // Display a message that souschef is currently completing a task
        }
    }
}
