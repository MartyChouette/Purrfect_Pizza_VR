using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceSurface : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy;

    public void onPizzaDetected(GameObject pizzaChildObject)
    {
        checkOrder(pizzaChildObject);
    }

    private void checkOrder(GameObject pizzaChildObject)
    {
        bool isMissingIngredient = false;
        Dictionary<string, int> addedIngredients = pizzaChildObject.GetComponent<IngredientsDetector>().ingredients;
        GameObject pizza = pizzaChildObject.transform.parent.gameObject;
        Pizza pizzaType = OrderManager.Instance.allPizzaTypes[pizza.name];

        // Go through the ingredients added on the pizza by checking Ingredient object child of the pizza game object
        foreach (var requiredIngredient in pizzaType.recipe)
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
            Timer.Create(() => Destroy(pizza), _timeToDestroy, null);
        }
    }
}
