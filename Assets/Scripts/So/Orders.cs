using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Orders in Level", menuName = "Level Orders")]
public class Orders : ScriptableObject
{
    public int numberOfOrders;
    public List<Pizza> pizzaTypes = new List<Pizza>();
    [HideInInspector] public List<Pizza> orderList;

    private void OnEnable()
    {
        createList();
    }

    private void OnDisable()
    {
        orderList.Clear();
    }

    private void createList()
    {
        orderList = new List<Pizza>();
        for (int i = 0; i < numberOfOrders; i++)
        {
            orderList.Add(getPizzaType(Random.Range(0, pizzaTypes.Count)));
        }
    }

    private Pizza getPizzaType(int type)
    {
        return pizzaTypes[type];
    }
}
