using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Pizza", menuName = "Pizza")]
public class Pizza : ScriptableObject
{
    [Serializable]
    public class Ingredient
    {
        public int amount;
        public GameObject ingredientPrefab;
    }
    public GameObject doughPrefab;
    public Ingredient[] recipe;

    private void OnValidate()
    {
        if (doughPrefab == null)
        {
            Debug.Log(this.name + " scriptable object is missing a dough prefab.", this);
        }

        foreach (var item in recipe)
        {
            if (item.ingredientPrefab == null)
            {
                Debug.Log(this.name + " scriptable object is missing an ingredient prefab.", this);
            }
        }
    }

    public void instantiatePizza(Transform parentTransform)
    {
        GameObject go = Instantiate(doughPrefab, parentTransform);
        go.name = this.name;
        go.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

        foreach (Ingredient ingredient in recipe)
        {
            go.GetComponentInChildren<IngredientsDetector>().recipe.Add(ingredient.ingredientPrefab.name, ingredient.amount);
        } 
    }
}
