using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipes", menuName = "Recipe")]
public class RecipesSO : ScriptableObject
{
    public string name;
    public List<IngredientsSO> ingredients;
}
