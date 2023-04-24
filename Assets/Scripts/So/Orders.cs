using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Orders", menuName = "Level Orders")]
public class Orders : ScriptableObject
{
    public int numberOfOrders;
    public List<Pizza> chefXPizzaTypes = new List<Pizza>();
    public List<Pizza> chefYPizzaTypes = new List<Pizza>();
    [HideInInspector] public List<Pizza> chefXOrderList {get;} = new List<Pizza>();
    [HideInInspector] public List<Pizza> chefYOrderList {get;} = new List<Pizza>();

    private void OnEnable()
    {
        createList();
    }

    private void OnDisable()
    {
        chefXOrderList.Clear();
        chefYOrderList.Clear();
    }

    private void createList()
    {
        if (chefXPizzaTypes.Count > 0 && chefYPizzaTypes.Count > 0)
        {
            for (int i = 0; i < numberOfOrders; i++)
            {
                chefXOrderList.Add(chefXPizzaTypes[Random.Range(0, chefXPizzaTypes.Count)]);
                chefYOrderList.Add(chefYPizzaTypes[Random.Range(0, chefYPizzaTypes.Count)]);
            }
        }
        else
        {
            Debug.Log("Chef X and/or Chef Y pizza types list is empty.", this);
        }
    }
}
