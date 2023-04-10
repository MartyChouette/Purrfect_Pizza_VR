using UnityEngine;

[CreateAssetMenu(fileName = "New Topping", menuName = "Topping")]
public class Topping : ScriptableObject
{
    public new string name;
    public bool prepared;
}
