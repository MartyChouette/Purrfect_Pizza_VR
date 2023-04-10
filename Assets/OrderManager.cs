using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OrderManager : MonoBehaviour
{
    //public static event Action Order;
    public static OrderManager Instance;
    [SerializeField] private Orders _orders;
    
    void Awake() => Instance = this;

    public List<Pizza> orderList
    {
        get
        {
            return _orders.orderList;
        }
    }

    public List<Pizza> pizzaTypes
    {
        get
        {
            return _orders.pizzaTypes;
        }
    }
}
