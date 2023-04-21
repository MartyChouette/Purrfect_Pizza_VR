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

    public void instantiatePizza(Transform parentTransform)
    {
        GameObject go = Instantiate(doughPrefab, parentTransform);
        go.name = this.name;
        go.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }
}
