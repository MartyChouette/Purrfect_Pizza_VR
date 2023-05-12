using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
    [SerializeField] private Orders _orders;
    [SerializeField] private GameObject[] debuggers;
    public Dictionary<string, Pizza> allPizzaTypes {get;} = new Dictionary<string, Pizza>();
    public Dictionary<string, GameObject> allIngredients {get;} = new Dictionary<string, GameObject>();

    private void OnValidate()
    {
        if (_orders == null)
        {
            Debug.Log("Order Manager is missing an Orders scriptable object.", this.gameObject);
        }
    }
    
    private void Awake()
    {
        Instance = this;
        collectAllPizzaTypesAndIngredients();
    }

    public List<Pizza> orderList(Character.Characters character)
    {
        return (character == 0)? _orders.chefXOrderList : _orders.chefYOrderList;
    }

    public List<Pizza> pizzaTypes(Character.Characters character)
    {
        return (character == 0)? _orders.chefXPizzaTypes : _orders.chefYPizzaTypes;
    }

    private void collectAllPizzaTypesAndIngredients()
    {
        // Collect all pizza types from Chef X and Chef Y, and debuggers
        foreach (Pizza item in pizzaTypes(Character.Characters.SousChefX))
        {
            if (!allPizzaTypes.ContainsKey(item.name))
            {
                allPizzaTypes.Add(item.name, item);
            }
        }
        foreach (Pizza item in pizzaTypes(Character.Characters.SousChefY))
        {
            if (!allPizzaTypes.ContainsKey(item.name))
            {
                allPizzaTypes.Add(item.name, item);
            }
        }
        collectDebuggerPizzaTypes();

        // Collect all Ingredients
        foreach (Pizza type in allPizzaTypes.Values)
        {
            foreach (var ingredient in type.recipe)
            {
                if (!allIngredients.ContainsKey(ingredient.ingredientPrefab.name))
                {
                    allIngredients.Add(ingredient.ingredientPrefab.name, ingredient.ingredientPrefab);
                }
            }
        }
    }

    private void collectDebuggerPizzaTypes()
    {
        foreach (GameObject deb in debuggers)
        {
            if (deb.activeSelf)
            {
                foreach (Pizza item in deb.GetComponent<Debugger>().pizzas)
                {
                    if (!allPizzaTypes.ContainsKey(item.name))
                    {
                        allPizzaTypes.Add(item.name, item);
                    }
                }
            }
        }
    }
}
