using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SousChefX : MonoBehaviour
{
    [SerializeField] private float _time;
    private Timer _timer;
    private bool haveTask = false;

    private void Awake()
    {
        _timer = GetComponent<Timer>();
        _timer.timeLimit = _time;
    }

    void Update()
    {
        working();
    }

    private void working()
    {
        if (haveTask)
        {
            if (_timer.stopped)
            {
                // Instatiate the pizza base object
                _timer.start();
            }
        }
    }

    public void assignTask()
    {
        if (!haveTask)
        {
            foreach (var task in TaskManager.Instance._chefXTasks)
            {
                if (task.Key.GetComponent<Selectable>().selected)
                {
                    haveTask = true;
                    _timer.start();
                }
            }
        }
        else
        {
            // Display a message that souschef is currently completing a task
        }
    }
}
