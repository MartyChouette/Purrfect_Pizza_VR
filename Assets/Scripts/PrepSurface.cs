using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PrepSurface : MonoBehaviour
{
    public static PrepSurface Instance;
    [SerializeField] private GameObject _recipeUIContent;
    [SerializeField] private Text _recipeTextPrefab;
    private Dictionary<string, Text> _recipeDisplayList = new Dictionary<string, Text>();

    private void OnValidate()
    {
        if (_recipeUIContent == null)
        {
            Debug.Log("Prep Surface is missing a recipe UI content gameobject.", this.gameObject);
        }
        if (_recipeTextPrefab == null)
        {
            Debug.Log("Prep Surface is missing a recipe text prefab.", this.gameObject);
        }
    }

    private void Awake() => Instance = this;

    public void onPizzaDetected(GameObject pizzaIngredients)
    {
        pizzaIngredients.GetComponent<IngredientsDetector>().onPrepTable = true;
        displayOrderRecipe(pizzaIngredients);
    }

    public void onPizzaUndetected(GameObject pizzaIngredients)
    {
        pizzaIngredients.GetComponent<IngredientsDetector>().onPrepTable = false;
        unlistRecipeDisplayed();
    }

    public void updateRecipeUI(string recipeIngreName, GameObject pizzaIngredients)
    {
        setIngredientText(recipeIngreName, pizzaIngredients);
    }

    private void displayOrderRecipe(GameObject pizzaIngredients)
    {
        GameObject pizza = pizzaIngredients.transform.parent.gameObject;
        if (OrderManager.Instance.allPizzaTypes.ContainsKey(pizza.name))
        {
            foreach (string requiredIngredient in pizzaIngredients.GetComponent<IngredientsDetector>().recipe.Keys)
            {
                _recipeDisplayList.Add(requiredIngredient, Instantiate(_recipeTextPrefab, _recipeUIContent.transform));
                setIngredientText(requiredIngredient, pizzaIngredients);
            }
        }
        else
        {
            Debug.Log("The Pizza is not on the order list.", pizza);
        }
    }

    private void unlistRecipeDisplayed()
    {
        foreach (var ingredientDetail in _recipeDisplayList.Values)
        {
            Destroy(ingredientDetail.gameObject);
        }
        _recipeDisplayList.Clear();
    }

    private void setIngredientText(string recipeIngreName, GameObject pizzaIngredients)
    {
        _recipeDisplayList[recipeIngreName].text = pizzaIngredients.GetComponent<IngredientsDetector>().numberOfIngredientsLeftToAdd(recipeIngreName) + " x " + recipeIngreName;
    }
}
