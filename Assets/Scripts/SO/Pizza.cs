using UnityEngine;

[CreateAssetMenu(fileName = "New Pizza", menuName = "Pizza")]
public class Pizza : ScriptableObject
{
    public new string name;
    public Base pizzaBase;
    public Topping[] toppings;
}
