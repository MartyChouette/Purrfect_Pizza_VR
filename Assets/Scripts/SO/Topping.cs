using UnityEngine;

[CreateAssetMenu(fileName = "New Topping", menuName = "Topping")]
public class Topping : ScriptableObject
{
    public bool prepared;
    public GameObject prefab;
}
