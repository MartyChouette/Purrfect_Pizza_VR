using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayTimer : MonoBehaviour
{
    [SerializeField] private float _time = 300;
    //[SerializeField] private GameObject TimerCanvas;
    private TextMeshProUGUI _timerText;
    private Timer _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timerText = GetComponentInChildren<TextMeshProUGUI>();
        _timer = GetComponent<Timer>();
        _timer.timeLimit = _time;
        LevelManager.LevelBegin += updateTime;
        _timer.start();

    }

    // Update is called once per frame
    private void updateTime()
    {
        _timer.updateTimerText(_timerText);
    }

    private void OnDisable()
    {
        LevelManager.LevelBegin -= updateTime;
    }
}
