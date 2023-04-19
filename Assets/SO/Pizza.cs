using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pizza", menuName = "Pizza")]
public class Pizza : ScriptableObject
{
  public string name; 
  public GameObject model; 
  public IngredientsSO[] toppings; 
  public RecipesSO recipe; 
}
