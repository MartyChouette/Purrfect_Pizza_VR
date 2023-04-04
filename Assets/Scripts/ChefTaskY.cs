using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChefTaskY : MonoBehaviour
{
    public bool haveTask = false;
    public string currentTask;
    [SerializeField] private float _time = 5;
    private Slider _progressBar;
    private Timer _timer;

    private void Awake()
    {
        _progressBar = GetComponentInChildren<Slider>();
        _progressBar.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        _timer = GetComponent<Timer>();
        _timer.timeLimit = _time;
        //_timer = GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        working();
    }

    private void working()
    {
        if (haveTask)
        {
            _progressBar.gameObject.SetActive(true);
            _timer.start();
            if (!_timer.isEnded)
            {
                // Start cooking
                _progressBar.value = _timer.timeRemaining / _time;

                // If headchef yell/throw stuff at souschef then reduce time
            }
            else
            {
                // Reset the timer when the souschef finished and assign new task
                _progressBar.gameObject.SetActive(false);
                _progressBar.value = 0;
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
