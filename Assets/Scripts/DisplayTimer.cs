using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TimingTools;
public class DisplayTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private Timer _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timer = new Timer();
    }

    // Update is called once per frame
    void Update()
    {
        _timer.isTiming();
        _timer.updateTimerText(timerText);
    }
}
