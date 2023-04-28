using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public Pizza testPizza;

    void Start()
    {
        testPizza.instantiatePizza(this.transform);
    }
}
