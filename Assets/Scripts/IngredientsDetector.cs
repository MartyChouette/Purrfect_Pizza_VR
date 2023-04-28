using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsDetector : MonoBehaviour
{
    [HideInInspector] public bool onPrepTable;
    [HideInInspector] public Dictionary<string, int> recipe {get;} = new Dictionary<string, int>();
    [HideInInspector] public Dictionary<string, int> addedIngredients {get;} = new Dictionary<string, int>();
    
    private void Start()
    {
        onPrepTable = false;
    }

    private void OnTriggerEnter(Collider go)
    {
        GameObject pizzaIngredients = this.gameObject;
        if (pizzaIngredients.tag == "Pizza" & go.tag == "Ingredient")
        {
            go.transform.SetParent(pizzaIngredients.transform);
            go.GetComponent<Rigidbody>().isKinematic = true;

            if (addedIngredients.ContainsKey(go.name))
            {
                addedIngredients[go.name]++;
            }
            else
            {
                addedIngredients.Add(go.name, 1);
            }

            if (onPrepTable & recipe.ContainsKey(go.name))
            {
                    PrepSurface.Instance.updateRecipeUI(go.name, pizzaIngredients);
            }
        }
    }

    private void OnTriggerExit(Collider go)
    {
        GameObject pizzaIngredients = this.gameObject;
        if (pizzaIngredients.tag == "Pizza" & go.tag == "Ingredient")
        {
            go.transform.SetParent(pizzaIngredients.transform.root);
            go.GetComponent<Rigidbody>().isKinematic = false;
            addedIngredients[go.name]--;

            if (onPrepTable & recipe.ContainsKey(go.name))
            {
                PrepSurface.Instance.updateRecipeUI(go.name, pizzaIngredients);
            }
        }
    }

    public int numberOfIngredientsLeftToAdd(string recipeIngreName)
    {
        int recipeIngreAmount = recipe[recipeIngreName];
        // Ensure the added ingredients contain an ingredient from the recipe
        if (addedIngredients.ContainsKey(recipeIngreName))
        {
            // The amount of added ingredients may exceed the require amount from the recipe
            if (addedIngredients[recipeIngreName] < recipeIngreAmount)
            {
                return recipeIngreAmount - addedIngredients[recipeIngreName];
            }
            return 0;
        }
        return recipeIngreAmount;
    }
}
