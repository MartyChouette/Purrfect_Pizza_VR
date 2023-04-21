using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
    [SerializeField] private Orders _orders;
    [SerializeField] private GameObject _recipeUIContent;
    [SerializeField] private Text _recipeTextPrefab;
    private List<Text> _recipeDisplayList = new List<Text>();
    
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

    public List<GameObject> allIngredients
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            foreach (Pizza type in pizzaTypes)
            {
                foreach (var ingredient in type.recipe)
                {
                    list.Add(ingredient.ingredientPrefab);
                }
            }
            return list;
        }
    }

    public void checkOrder(GameObject pizzaIngredientsObject)
    {
        bool isMissingIngredient = false;
        Dictionary<string, int> addedIngredients = pizzaIngredientsObject.GetComponent<IngredientsDetector>().ingredients;
        GameObject pizza = pizzaIngredientsObject.transform.parent.gameObject;
        // Go through the ingredients added on the pizza by checking Ingredient object child of the pizza game object
        foreach (var requiredIngredient in pizzaTypes.Find(x => x.name.Contains(pizza.name)).recipe)
        {
            if (addedIngredients.ContainsKey(requiredIngredient.ingredientPrefab.name))
            {
                if (addedIngredients[requiredIngredient.ingredientPrefab.name] < requiredIngredient.amount)
                {
                    Debug.Log("Short of " + requiredIngredient.ingredientPrefab.name);
                    isMissingIngredient = true;
                }
            }
            else
            {
                Debug.Log("Missing " + requiredIngredient.ingredientPrefab.name + "; " + addedIngredients.Count);
                isMissingIngredient = true;
            }
        }

        if (!isMissingIngredient)
        {
            Timer.Create(() => Destroy(pizza), 3, null);
        }
    }

    public void displayOrderRecipe(GameObject pizzaIngredientsObject)
    {
        GameObject pizza = pizzaIngredientsObject.transform.parent.gameObject;
        foreach (var requiredIngredient in pizzaTypes.Find(x => x.name.Contains(pizza.name)).recipe)
        {
            _recipeTextPrefab.text = requiredIngredient.amount + " x " + requiredIngredient.ingredientPrefab.name;
            _recipeDisplayList.Add(Instantiate(_recipeTextPrefab, _recipeUIContent.transform));
        }
    }

    public void unlistRecipe()
    {
        foreach (var ingredientDetail in _recipeDisplayList)
        {
            Destroy(ingredientDetail.gameObject);
        }
        _recipeDisplayList.Clear();
    }
}
