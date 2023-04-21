using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SousChefY : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private GameObject _pizzaSpawnPoint;
    private Slider _progressBar;
    private int orderIndex;
    private bool isMakingPizza;

    void Awake()
    {
        _progressBar = GetComponentInChildren<Slider>();
        _progressBar.gameObject.SetActive(false);
    }

    void Start()
    {
        orderIndex = 0;
        isMakingPizza = false;
    }

    void Update()
    {
        onPizzaCreate();
    }

    private void onPizzaCreate()
    {
        if (!isMakingPizza)
        {
            isMakingPizza = true;
            Timer.Create(onPizzaComplete, _time, _progressBar);
        }
    }

    private void onPizzaComplete()
    {
        //Instantiate(OrderManager.Instance.orderList[orderIndex].doughPrefab, _pizzaSpawnPoint.transform);
        OrderManager.Instance.orderList[orderIndex].instantiatePizza(_pizzaSpawnPoint.transform);
        isMakingPizza = false;
        orderIndex++;
        if (orderIndex >= OrderManager.Instance.orderList.Count)
        {
            orderIndex = 0;
        }
    }
}
