using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static event Action LevelBegin;

    private void Update()
    {
        LevelBegin?.Invoke(); // Similar to... if(LevelBegin != null) {LevelBegin();}
    }
}
