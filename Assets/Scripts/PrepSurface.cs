using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepSurface : MonoBehaviour
{
    [SerializeField] private GameObject _recipeUIContent;
    [SerializeField] private Text _recipeTextPrefab;
    private List<Text> _recipeDisplayList = new List<Text>();

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

    public void onPizzaDetected(GameObject pizzaChildObject)
    {
        displayOrderRecipe(pizzaChildObject);
    }

    public void onPizzaUndetected()
    {
        unlistRecipeDisplayed();
    }

    private void displayOrderRecipe(GameObject pizzaChildObject)
    {
        GameObject pizza = pizzaChildObject.transform.parent.gameObject;
        Pizza pizzaType = OrderManager.Instance.allPizzaTypes[pizza.name];
        foreach (var requiredIngredient in pizzaType.recipe)
        {
            _recipeTextPrefab.text = requiredIngredient.amount + " x " + requiredIngredient.ingredientPrefab.name;
            _recipeDisplayList.Add(Instantiate(_recipeTextPrefab, _recipeUIContent.transform));
        }
    }

    private void unlistRecipeDisplayed()
    {
        foreach (var ingredientDetail in _recipeDisplayList)
        {
            Destroy(ingredientDetail.gameObject);
        }
        _recipeDisplayList.Clear();
    }
}
